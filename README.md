# PaymentKata
Two webApi made on ASP.NET Core 6 that communicates each other to perform a payment proccess. Both web api have their own docker file to make container.
The first webApi (payment) receive the request and do the bussiness logic to process this and store (or not) on the DateBase. In the middle, this webApi call another to check the payment (if the amount have decimals, the payment is denied).
The second webApi (paymentChecker) receive a float and decide if the payment have to be approved or denied.

#### NOTE:  
If you run both project on local you can connect easly to a local SqlServer (Scripts below). But if you know how to containerize a sqlServer instance you have to change de connectionString on the webapi_payment sln.

Steps to containerize:
1. Open a poweshell comand and go the the directory --> cd youDirectory\Challenge Payments\PaymentChallenge
2. docker image build -t webapi_payment:1.0.0 -f .\Dockerfile .
3. cd ..
4. cd youDirectory\Challenge Payments\PaymentChecker
5. docker image build -t webapi_paymentchecker:1.0.0 -f .\Dockerfile .

6. docker container create --name app.payment.webapi -p 8080:8080 webapi_payment:1.0.0
7. docker container create --name app.paymentchecker.webapi -p 8090:8090 webapi_paymentchecker:1.0.0

8. docker container start app.payment.webapi
    - 8.1 - Doc swagger: http://localhost:8080/swagger/index.html
9. docker container start app.paymentchecker.webapi
	  - 9.1 - Doc swagger: http://localhost:8090/swagger/index.html

 #### ANOTHER NOTE:
 The app.payment.webapi connect to a localhost sqlServer. (sorry but i couldn´t make a sqlServer container) So you have to create a DataBase on you machine (localhost). This is the script to do it:
 
 ```
 CREATE DATABASE PaymentDB;
GO
USE PaymentDB;
GO
CREATE TABLE ApprovedPayments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Amount FLOAT,
    DateEntry SMALLDATETIME,
    ClientId INT,
    PaymentCode NVARCHAR(50)
);
CREATE TABLE PaymentState (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    amount FLOAT,
    dateEntry SMALLDATETIME,
    clientId INT,
	stateId INT,
	isActive bit DEFAULT 1,
    paymentCode NVARCHAR(50)
);
```

