/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using Recaptcha.Web.Configuration;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Recaptcha.Web.Mvc
{
    /// <summary>
    /// Represents the Recaptcha method extensions container for the <see cref="System.Web.Mvc.HtmlHelper"/> and <see cref="System.Web.Mvc.Controller"/> classes.
    /// </summary>
    public static class RecaptchaMvcExtensions
    {
        #region Public Methods

        /// <summary>
        /// Renders the recaptcha HTML in an MVC view. It is an extension method to the <see cref="System.Web.Mvc.HtmlHelper"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="System.Web.Mvc.HtmlHelper"/> object to which the extension is added.</param>
        /// <param name="siteKey">Sets the site key of recaptcha.</param>
        /// <param name="theme">Sets the theme of recaptcha.</param>
        /// <param name="language">Sets the language of recaptcha. If no language is specified, the language of the current UI culture will be used.</param>
        /// <param name="tabIndex">Sets the tab index of recaptcha.</param>
        /// <param name="size">Sets the size of recaptcha.</param>
        /// <param name="useSsl">Sets the value to the UseSsl property.</param>
        /// <param name="apiVersion">Determines the version of the reCAPTCHA API.</param>
        /// <returns>Returns an instance of the IHtmlString type.</returns>
        public static IHtmlString Recaptcha(
            this HtmlHelper htmlHelper,
            string siteKey = null,
            RecaptchaTheme? theme = null,
            string language = null,
            int? tabIndex = null,
            RecaptchaDataSize? size = null,
            SslBehavior? useSsl = null,
            string apiVersion = null)
        {
            var config = RecaptchaConfigurationManager.GetConfiguration();
            string ver;
            if (!string.IsNullOrEmpty(apiVersion))
            {
                ver = apiVersion;
            }
            else
            {
                ver = config.ApiVersion;
            }

            if (ver == null || ver == "2")
            {
                var rHtmlHelper = new Recaptcha2HtmlHelper(siteKey != null ? siteKey : config.SiteKey, theme != null ? (RecaptchaTheme)theme : config.DefaultTheme, language != null ? language : config.DefaultLanguage, tabIndex != null ? (int)tabIndex : 0, size != null ? (RecaptchaDataSize)size : config.DefaultSize, useSsl != null ? (SslBehavior)useSsl : config.UseSsl);
                var writer = new HtmlTextWriter(new StringWriter());
                writer.Write(rHtmlHelper.ToString());

                return htmlHelper.Raw(writer.InnerWriter.ToString());
            }
            else
            {
                throw new InvalidOperationException("The API version is either invalid or not supported.");
            }
        }

        /// <summary>
        /// Gets an instance of the <see cref="RecaptchaVerificationHelper"/> class that can be used to verify user's response to the recaptcha's challenge. 
        /// </summary>
        /// <param name="controller">The <see cref="System.Web.Mvc.Controller"/> object to which the extension method is added to.</param>
        /// <param name="secretKey">The private key required for making the recaptcha verification request.</param>
        /// <returns>Returns an instance of the <see cref="RecaptchaVerificationHelper"/> class.</returns>
        public static RecaptchaVerificationHelper GetRecaptchaVerificationHelper(this System.Web.Mvc.Controller controller, string secretKey)
        {
            return new RecaptchaVerificationHelper(secretKey);
        }

        /// <summary>
        /// Gets an instance of the <see cref="RecaptchaVerificationHelper"/> class that can be used to verify user's response to the recaptcha's challenge. 
        /// </summary>
        /// <param name="controller">The <see cref="System.Web.Mvc.Controller"/> object to which the extension method is added to.</param>
        /// <returns>Returns an instance of the <see cref="RecaptchaVerificationHelper"/> class.</returns>
        public static RecaptchaVerificationHelper GetRecaptchaVerificationHelper(this System.Web.Mvc.Controller controller)
        {
            var config = RecaptchaConfigurationManager.GetConfiguration();
            return new RecaptchaVerificationHelper(config.SecretKey);
        }

        #endregion Public Methods
    }
}