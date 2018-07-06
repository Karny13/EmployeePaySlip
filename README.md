# PaySlipgeneration
Test project

Release Notes:

Requirements to run the code
1. Visual studio 2015+
2. .Net 4.5 Installed
3. Postman for testing

Running the code is Postman
Once the code project is run from visual studio or your hosting, do the following to test it in Postman 

1. Copy the base Url 
eg. http://localhost:51994

2. Pase it into Postman address bar adding the api url as below
   http://localhost:51994/Payslip

3. Copy the Json Request Provided below into the body section with type as raw
{
	
"FirstName":"John",
	
"LastName":"Doe",
	
"AnnualSalary":120000,
	
"SuperRate" : 5,
	
"StartDate":"03/01/2017"

}

4. Hit send.

5. You should see the response as its provided below
{
    
"Name": "John Doe",
    
"PayPeriod": "01 Mar-31 Mar",
    
"GrossIncome": 10000,
    
"IncomeTax": 2669,
    
"NetIncome": 7331,
    
"SuperAmount": 500

}

# Run Tests
1. Install Nunit From Nuget Package Manager
2. Install Nunit3TestAdapter from Nuget Package Manager
3. Build the Solution
4. From Test Explorer Run All the test

Assumptions :
1. The values entered by the user are correct.
2. Dates range is 30 days from the Start date entered.
