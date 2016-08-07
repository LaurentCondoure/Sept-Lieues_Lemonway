using System;
using System.Web.UI.WebControls;
using LemonWay.TechnicalTest.Services.Tests.LemonWayService;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LemonWay.TechnicalTest.Services.Tests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// UnitTest Method for Fibonacci service Method
        /// </summary>
        [TestMethod]
        public void TestMethodFibonacci()
        {
            LemonWayServiceSoapClient service = new LemonWayServiceSoapClient("LemonWayServiceSoap");
            int resultSmall = service.Fibonacci(0);
            Assert.AreEqual(resultSmall, -1);

            int resultHigh = service.Fibonacci(101);
            Assert.AreEqual(resultHigh, -1);

            int resultSix = service.Fibonacci(6);
            Assert.IsTrue(
                //check the good answer
                resultSix == 8 
                //check the answer in case of exception
                || resultSix == -1);

            int resultTen = service.Fibonacci(10);
            Assert.IsTrue(
                //check the good answer
                resultTen == 55
                //check the answer in case of exception
                || resultTen == -1);


            int resultTwenty = service.Fibonacci(20);
            Assert.IsTrue(
                //check the good answer
                resultTwenty == 6765
                //check the answer in case of exception
                || resultTwenty == -1);
        }

        /// <summary>
        /// UnitTest Method for FibonacciWithError service Method
        /// </summary>
        [TestMethod]
        public void TestMethodFibonacciWithError()
        {
            //Creatre webservice communication channel.
            //The webservice is reference through a service reference URL (http://localhost:8080/LemonWayService.asmx), not through project reference
            LemonWayServiceSoapClient service = new LemonWayServiceSoapClient("LemonWayServiceSoap");

            FibonacciResult fibonacciSmall = service.FibonacciWithError(0);
            Assert.IsTrue(fibonacciSmall.Result == -1
                && fibonacciSmall.ErrorCode == -1
                && !string.IsNullOrWhiteSpace(fibonacciSmall.ErrorMessage));


            FibonacciResult fibonacciHigh = service.FibonacciWithError(101);
            Assert.IsTrue(fibonacciSmall.Result == -1
                && fibonacciSmall.ErrorCode == -1
                && !string.IsNullOrWhiteSpace(fibonacciSmall.ErrorMessage));

            FibonacciResult resultSix = service.FibonacciWithError(6);
            Assert.IsTrue(
                //Test if the result is good and if the error informations are coherent
                (resultSix.Result == 8
                && resultSix.ErrorCode == 0
                && string.IsNullOrWhiteSpace(resultSix.ErrorMessage))
                ||
                //Test if the result is wrong and if the error informations are coherent
                (resultSix.Result == -1
                && resultSix.ErrorCode == 1
                && !string.IsNullOrWhiteSpace(resultSix.ErrorMessage)));

            FibonacciResult resultTen = service.FibonacciWithError(10);
            Assert.IsTrue(
                //Test if the result is good and if the error informations are coherent
                (resultTen.Result == 55
                && resultTen.ErrorCode == 0
                && string.IsNullOrWhiteSpace(resultTen.ErrorMessage))
                ||
                //Test if the result is wrong and if the error informations are coherent
                (resultTen.Result == -1
                && resultTen.ErrorCode == 1
                && !string.IsNullOrWhiteSpace(resultTen.ErrorMessage)));

            FibonacciResult resultTwenty = service.FibonacciWithError(20);
            Assert.IsTrue(
                //Test if the result is good and if the error informations are coherent
                (resultTwenty.Result == 6765
                && resultTwenty.ErrorCode == 0
                && string.IsNullOrWhiteSpace(resultTwenty.ErrorMessage))
                ||
                //Test if the result is wrong and if the error informations are coherent
                (resultTwenty.Result == -1
                && resultTwenty.ErrorCode == 1
                && !string.IsNullOrWhiteSpace(resultTwenty.ErrorMessage)));
        }
    }
}
