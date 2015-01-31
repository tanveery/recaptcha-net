using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web;
using System.Web.Fakes;

namespace Recaptcha.Web.Tests.Compatibility
{
    [TestClass]
    public class RecaptchaHtmlHelperTests
    {
        private static HttpContext TestHttpContext;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            TestHttpContext = new HttpContext(new StubHttpWorkerRequest
            {
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
        public void PublicKeyIsPassedCorrectly()
        {
            var key = "TestKey";
            var target = new RecaptchaHtmlHelper(key);
            Assert.AreEqual(key, target.PublicKey);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void PublicKeyIsParsedCorrectly()
        {
            var target = new RecaptchaHtmlHelper("{TestKey}");
            Assert.AreEqual("TestValue", target.PublicKey);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void RedThemeIsSetByDefault()
        {
            var target = new RecaptchaHtmlHelper("TestKey");
            Assert.AreEqual(RecaptchaTheme.Red, target.Theme);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void LanguageIsNullByDefault()
        {
            var target = new RecaptchaHtmlHelper("TestKey");
            Assert.IsNull(target.Language);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void TabIndexIsZeroByDefault()
        {
            var target = new RecaptchaHtmlHelper("TestKey");
            Assert.AreEqual(0, target.TabIndex);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void UseSslIsBoolean()
        {
            var target = new RecaptchaHtmlHelper("TestKey");
            Assert.IsInstanceOfType(target.UseSsl, typeof(bool));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void RedThemeIsPassedCorrectly()
        {
            var target = new RecaptchaHtmlHelper("TestKey", RecaptchaTheme.Red, null, 0);
            Assert.AreEqual(RecaptchaTheme.Red, target.Theme);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void BlackglassThemeIsPassedCorrectly()
        {
            var target = new RecaptchaHtmlHelper("TestKey", RecaptchaTheme.Blackglass, null, 0);
            Assert.AreEqual(RecaptchaTheme.Blackglass, target.Theme);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void WhiteThemeIsPassedCorrectly()
        {
            var target = new RecaptchaHtmlHelper("TestKey", RecaptchaTheme.White, null, 0);
            Assert.AreEqual(RecaptchaTheme.White, target.Theme);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void CleanThemeIsPassedCorrectly()
        {
            var target = new RecaptchaHtmlHelper("TestKey", RecaptchaTheme.Clean, null, 0);
            Assert.AreEqual(RecaptchaTheme.Clean, target.Theme);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void LanguageIsPassedCorrectly()
        {
            var language = "en";
            var target = new RecaptchaHtmlHelper("TestKey", RecaptchaTheme.Red, language, 0);
            Assert.AreEqual(language, target.Language);
        }
    }
}
