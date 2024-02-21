using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentChallenge.Contract.DB;
using PaymentChallenge.Contract.ExternalServices;
using PaymentChallenge.Data;
using PaymentChallenge.Exceptions;
using PaymentChallenge.Model;
using PaymentChallenge.Model.Response;
using PaymentChallenge.Repository.Command;
using PaymentChallenge.Repository.DB;

namespace PaymentChallenge.Business
{
    public class PaymentLogic : IPaymentLogic
    {
        private readonly IPaymentCheckerService _paymentCheckerService;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IInvoker _invoker;
        private readonly IMapper _mapper;
        public PaymentLogic(IPaymentCheckerService paymentCheckerService, IPaymentRepository paymentRepository, IInvoker invoker, IMapper mapper)
        {
            _paymentCheckerService = paymentCheckerService;
            _paymentRepository = paymentRepository;
            _invoker = invoker;
            _mapper = mapper;
        }

        public ResultResponse NewPayment(Payment payment)
        {
            ResultResponse response = new ResultResponse() { Success = false, Error = ""};
            Payment newPayment = payment;
            newPayment.PaymentCode = $"{newPayment.ClientId}{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}";
            _invoker.AddCommand(new NewPaymentRecord(newPayment, Data.Enum.OperationType.WaitingConfirmation));

            if (!_paymentCheckerService.IsPaymentValid(newPayment.Amount).Result) {
                response.Error = "Pago Denegado";
                newPayment.OperationType = Data.Enum.OperationType.Denied;
                _invoker.AddCommand(new DisabledPaymentRecord(newPayment));
                _invoker.AddCommand(new NewPaymentRecord(newPayment, Data.Enum.OperationType.Denied));
                _invoker.ExecuteCommands();
                return response;
            }
            newPayment.OperationType = Data.Enum.OperationType.Approved;
            _invoker.AddCommand(new DisabledPaymentRecord(newPayment));
            _invoker.AddCommand(new NewPaymentRecord(newPayment, Data.Enum.OperationType.Approved));
            _invoker.AddCommand(new NewApprovedPaymentRecord(newPayment));
            
            _invoker.ExecuteCommands();

            response.Success = true;
            response.Payment = _mapper.Map<PaymentResponse>(newPayment);
            return response;
        }

        public ResultResponse ConfirmPayment(Payment paymentToConfirm)
        {
            ResultResponse response = new ResultResponse() { Success = false, Error = "" };

            if (!_paymentRepository.Exists(paymentToConfirm.PaymentCode, paymentToConfirm.Amount)) {
                throw new NotFoundException(nameof(ConfirmPayment), paymentToConfirm);
            }
            if (paymentToConfirm.OperationType == Data.Enum.OperationType.Undo || paymentToConfirm.OperationType == Data.Enum.OperationType.Cancelled)
            {
                var payment = _paymentRepository.GetApproved(paymentToConfirm.PaymentCode);
                if (payment != null && (DateTime.Now - payment.DateEntry) > new TimeSpan(0,5,0)) { 
                    response.Error = "Supero 5 minutos de espera para cancelar/revertir Pago";
                    return response;
                }
            }

            _invoker.AddCommand(new DisabledPaymentRecord(paymentToConfirm));
            _invoker.AddCommand(new NewPaymentRecord(paymentToConfirm, paymentToConfirm.OperationType));
            if (paymentToConfirm.OperationType == Data.Enum.OperationType.Approved)
            {
                if (!_paymentRepository.ApprovedPaymentExists(paymentToConfirm.PaymentCode, paymentToConfirm.Amount))
                    _invoker.AddCommand(new NewApprovedPaymentRecord(paymentToConfirm));
            }
            else 
                    _invoker.AddCommand(new DeleteApprovedPayment(paymentToConfirm));
            _invoker.ExecuteCommands();
            response.Payment = _mapper.Map<PaymentResponse>(paymentToConfirm);
            response.Success = true;
            return response;
        }
    }
}
