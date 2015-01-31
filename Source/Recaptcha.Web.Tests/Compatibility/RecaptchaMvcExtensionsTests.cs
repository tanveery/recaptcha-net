using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recaptcha.Web.Mvc;
using System;
using System.Web;
using System.Web.Fakes;
using System.Web.Mvc;

namespace Recaptcha.Web.Tests.Compatibility
{
#pragma warning disable 618
    [TestClass]
    public class RecaptchaMvcExtensionsTests
    {
        private const string Key = "TestKey";
        private static HttpContext TestHttpContext;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            TestHttpContext = new HttpContext(new StubHttpWorkerRequest
            {
                GetFilePathTranslated01 = () => @"c:\dir\page.aspx",
                GetProtocol01 = () => Uri.UriSchemeHttp,
                GetRawUrl01 = () => "/",
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
        public void RecaptchaMethodWritesNonEmptyString()
        {
            IHtmlString result;
            using (var viewPage = new ViewPage())
            {
                result = RecaptchaMvcExtensions.Recaptcha(new HtmlHelper(new ViewContext(), viewPage), Key);
            }

            Assert.IsInstanceOfType(result, typeof(IHtmlString));
            Assert.IsFalse(string.IsNullOrEmpty(result.ToHtmlString()));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void GetRecaptchaVerificationHelperReturnsVerificationHelper()
        {
            var result = RecaptchaMvcExtensions.GetRecaptchaVerificationHelper(null);
            Assert.IsInstanceOfType(result, typeof(RecaptchaVerificationHelper));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void GetRecaptchaVerificationHelperWithKeyReturnsVerificationHelper()
        {
            var result = RecaptchaMvcExtensions.GetRecaptchaVerificationHelper(null, Key);
            Assert.IsInstanceOfType(result, typeof(RecaptchaVerificationHelper));
        }
    }
}
