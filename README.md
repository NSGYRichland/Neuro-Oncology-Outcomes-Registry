# TumorTaskforce

This solution houses all the code for Tumor Taskforce's "Tumor App."

The code for the Web App is .NET MVC based and connects to a SQL database hosted on Microsoft Azure. 
The Web App is also deployed via Microsoft Azure.
Simply go to tumor.azurewebsites.net to access the latest version of the web app.


"TUMOR APP"
V 0.1

Features:
Create, view, and edit patient information, including:
	General Info
	Tumor Info
	Review of health systems
	Symptoms
	Health Factors
	Other Medications
	Treatments

Search for a patient
	Via ID
	Via Tumor Classification

Get a suggested treatment option
	Compare a selected patient with other patients
	Receive a suggested treatment option based on comparison with similar patients.


TESTING:

To run behavioral test:

	1) Download the necessary Selenium Nuget packages from Visual Studios and include their corresponding headers
	**This is the most friendly method**
		
		-Selenium.WebDriver
		
		-Selenium.Firfox.WebDriver
		
	2) Build the solution inside Visual Studios and this will activate the built in Test Explorer
	
	3) The available tests in the project will appear
	
	4) You can choose to run them individually or click the "Run All" button to automatically see whether the tests pass
	**In the future we plan on having the tests run automatically when the project is built and show errors if any portion of the 		build is unsuccessful**
	
	
