using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recaptcha.Web.Mvc;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Fakes;

namespace Recaptcha.Web.Tests.Compatibility
{
    [TestClass]
    public class RecaptchaVerificationHelperTests
    {
        private static HttpContext TestHttpContext;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            TestHttpContext = new HttpContext(new StubHttpWorkerRequest
            {
                GetFilePathTranslated01 = () => @"c:\dir\page.aspx",
                GetKnownRequestHeaderInt32 = i => "localhost",
                GetProtocol01 = () => Uri.UriSchemeHttp,
                GetQueryString01 = () => "recaptcha_challenge_field=Test&recaptcha_response_field=Test",
                GetRawUrl01 = () => "/",
                GetRemoteAddress01 = () => "127.0.0.1",
                GetServerName01 = () => "localhost",
                GetUriPath01 = () => "/"
            });
        }

        [TestInitialize]
        public void TestInit()
        {
            HttpContext.Current = TestHttpContext;
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void PrivateKeyIsPassedCorrectly()
        {
            var key = "TestKey";
            var target = RecaptchaMvcExtensions.GetRecaptchaVerificationHelper(null, key);
            Assert.AreEqual(key, target.PrivateKey);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void UseSslIsBoolean()
        {
            var target = RecaptchaMvcExtensions.GetRecaptchaVerificationHelper(null, "TestKey");
            Assert.IsInstanceOfType(target.UseSsl, typeof(bool));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void UserHostAddressIsString()
        {
            var target = RecaptchaMvcExtensions.GetRecaptchaVerificationHelper(null, "TestKey");
            Assert.IsInstanceOfType(target.UserHostAddress, typeof(string));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void ResponseIsString()
        {
            var target = RecaptchaMvcExtensions.GetRecaptchaVerificationHelper(null, "TestKey");
            Assert.IsInstanceOfType(target.Response, typeof(string));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void VerifyRecaptchaResponseReturnsRecaptchaVerificationResult()
        {
            var target = RecaptchaMvcExtensions.GetRecaptchaVerificationHelper(null, "TestKey");
            Assert.IsInstanceOfType(target.VerifyRecaptchaResponse(), typeof(RecaptchaVerificationResult));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void VerifyRecaptchaResponseTaskAsyncReturnsTaskOfRecaptchaVerificationResult()
        {
            var target = RecaptchaMvcExtensions.GetRecaptchaVerificationHelper(null, "TestKey");
            Assert.IsInstanceOfType(target.VerifyRecaptchaResponseTaskAsync(), typeof(Task<RecaptchaVerificationResult>));
        }
    }
}
