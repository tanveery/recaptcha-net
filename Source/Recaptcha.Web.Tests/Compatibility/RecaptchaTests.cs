using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Fakes;
using System.Web.UI.WebControls;
using RecaptchaControl = Recaptcha.Web.UI.Controls.Recaptcha;

namespace Recaptcha.Web.Tests.Compatibility
{
#pragma warning disable 618
    [TestClass]
    public class RecaptchaTests
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
                GetQueryString01 = () => "recaptcha_challenge_field=Test&recaptcha_response_field=Test",
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
        public void RecaptchaIsWebControl()
        {
            var target = new RecaptchaControl();
            Assert.IsInstanceOfType(target, typeof(WebControl));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void PublicKeyIsStringByDefault()
        {
            var target = new RecaptchaControl();
            Assert.IsInstanceOfType(target.PublicKey, typeof(string));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void PublicKeyIsSetCorrectly()
        {
            var target = new RecaptchaControl();
            target.PublicKey = Key;
            Assert.AreEqual(Key, target.PublicKey);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void PrivateKeyIsStringByDefault()
        {
            var target = new RecaptchaControl();
            Assert.IsInstanceOfType(target.PrivateKey, typeof(string));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void PrivateKeyIsSetCorrectly()
        {
            var target = new RecaptchaControl();
            target.PrivateKey = Key;
            Assert.AreEqual(Key, target.PrivateKey);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void RedThemeIsSetByDefault()
        {
            var target = new RecaptchaControl();
            Assert.AreEqual(RecaptchaTheme.Red, target.Theme);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void RedThemeIsSetCorrectly()
        {
            var value = RecaptchaTheme.Red;
            var target = new RecaptchaControl();
            target.Theme = value;
            Assert.AreEqual(value, target.Theme);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void BlackglassThemeIsSetCorrectly()
        {
            var value = RecaptchaTheme.Blackglass;
            var target = new RecaptchaControl();
            target.Theme = value;
            Assert.AreEqual(value, target.Theme);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void WhiteThemeIsSetCorrectly()
        {
            var value = RecaptchaTheme.White;
            var target = new RecaptchaControl();
            target.Theme = value;
            Assert.AreEqual(value, target.Theme);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void CleanThemeIsSetCorrectly()
        {
            var value = RecaptchaTheme.Clean;
            var target = new RecaptchaControl();
            target.Theme = value;
            Assert.AreEqual(value, target.Theme);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void LanguageIsNullByDefault()
        {
            var target = new RecaptchaControl();
            Assert.IsNull(target.Language);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void LanguageIsSetCorrectly()
        {
            var value = "en";
            var target = new RecaptchaControl();
            target.Language = value;
            Assert.AreEqual(value, target.Language);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void ResponseIsString()
        {
            var target = new RecaptchaControl();
            Assert.IsInstanceOfType(target.Response, typeof(string));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void ResponseIsStringAfterVerification()
        {
            var target = new RecaptchaControl();
            target.Verify();
            Assert.IsInstanceOfType(target.Response, typeof(string));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void VerifyReturnsRecaptchaVerificationResult()
        {
            var target = new RecaptchaControl();
            Assert.IsInstanceOfType(target.Verify(), typeof(RecaptchaVerificationResult));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void VerifyTaskAsyncReturnsTaskOfRecaptchaVerificationResult()
        {
            var target = new RecaptchaControl();
            Assert.IsInstanceOfType(target.VerifyTaskAsync(), typeof(Task<RecaptchaVerificationResult>));
        }
    }
}
