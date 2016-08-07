# Sept-Lieues_Lemonway

The repository Contains the whole solution corresponbding to the answer for the Technical Test.
This Solutions had been made under Windows 7.1 and Visual Studio Community 2015.

She is composed by the following projects Developped under the .NET 4.5.2 Framework :

LemonWay.technicalTest.Utilities :
This project Had been planned to contains independant classes, usable by all projects in the solutions

LemonWay.technicalTest.Services :
This is an ASP.Net web projet which contains the services for the First question.
It was configured to be Hosted on My IIS Local server on port 8080.

LemonWay.technicalTest.Services.Tests :
Unit Test project to perform test on the web service methods.
The Web Service was referred through a reference service at the URL http://localhost:8080/LemonWayService.asmx

LemonWay.technicalTest.Services.Consumer : 
Application console made to call the Web Service and display the result.
The Web Service was referred through a reference service at the URL http://localhost:8080/LemonWayService.asmx
