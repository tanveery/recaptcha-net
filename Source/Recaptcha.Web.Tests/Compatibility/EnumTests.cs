using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Recaptcha.Web.Tests.Compatibility
{
#pragma warning disable 618
    [TestClass]
    public class EnumTests
    {
        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void VerificationResultIsInt32Enum()
        {
            Assert.AreEqual(typeof(int), Enum.GetUnderlyingType(typeof(RecaptchaVerificationResult)));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void VerificationResultUnknownErrorHasCorrectValue()
        {
            Assert.AreEqual((RecaptchaVerificationResult)0, RecaptchaVerificationResult.UnknownError);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void VerificationResultSuccessHasCorrectValue()
        {
            Assert.AreEqual((RecaptchaVerificationResult)1, RecaptchaVerificationResult.Success);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void VerificationResultIncorrectCaptchaSolutionHasCorrectValue()
        {
            Assert.AreEqual((RecaptchaVerificationResult)2, RecaptchaVerificationResult.IncorrectCaptchaSolution);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void VerificationResultInvalidCookieParametersHasCorrectValue()
        {
            Assert.AreEqual((RecaptchaVerificationResult)3, RecaptchaVerificationResult.InvalidCookieParameters);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void VerificationResultInvalidPrivateKeyHasCorrectValue()
        {
            Assert.AreEqual((RecaptchaVerificationResult)4, RecaptchaVerificationResult.InvalidPrivateKey);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void VerificationResultNullOrEmptyCaptchaSolutionHasCorrectValue()
        {
            Assert.AreEqual((RecaptchaVerificationResult)5, RecaptchaVerificationResult.NullOrEmptyCaptchaSolution);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void VerificationResultChallengeNotProvidedHasCorrectValue()
        {
            Assert.AreEqual((RecaptchaVerificationResult)6, RecaptchaVerificationResult.ChallengeNotProvided);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void ThemeIsInt32Enum()
        {
            Assert.AreEqual(typeof(int), Enum.GetUnderlyingType(typeof(RecaptchaTheme)));
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void ThemeRedHasCorrectValue()
        {
            Assert.AreEqual((RecaptchaTheme)0, RecaptchaTheme.Red);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void ThemeBlackglassHasCorrectValue()
        {
            Assert.AreEqual((RecaptchaTheme)1, RecaptchaTheme.Blackglass);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void ThemeWhiteHasCorrectValue()
        {
            Assert.AreEqual((RecaptchaTheme)2, RecaptchaTheme.White);
        }

        [TestMethod]
        [TestCategory(CompatibilityTests.Category)]
        public void ThemeCleanHasCorrectValue()
        {
            Assert.AreEqual((RecaptchaTheme)3, RecaptchaTheme.Clean);
        }
    }
}
