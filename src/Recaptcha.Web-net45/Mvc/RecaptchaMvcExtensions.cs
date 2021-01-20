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
        /// <param name="renderApiScript">Determines if the API script is to be rendered.</param>
        /// <param name="theme">The color theme of the widget.</param>
        /// <param name="language">Forces the reCAPTCHA widget to render in a specific language. By default, the user's language is used.</param>
        /// <param name="tabIndex">The tabindex of the reCAPTCHA widget.</param>
        /// <param name="size">The size of the reCAPTCHA widget.</param>
        /// <param name="useSsl">Determines if SSL is to be used in Google reCAPTCHA API calls.</param>
        /// <param name="apiVersion">Determines the version of the reCAPTCHA API.</param>
        /// <returns>Returns an instance of the IHtmlString type.</returns>
        [Obsolete("This method is obsolete and will be removed in future. Please use RecaptchaWidget method instead.")]
        public static IHtmlString Recaptcha(
            this HtmlHelper htmlHelper,
            string siteKey = null,
            bool renderApiScript = true,
            RecaptchaTheme? theme = null,
            string language = null,
            int? tabIndex = null,
            RecaptchaSize? size = null,
            RecaptchaSslBehavior? useSsl = null,
            string apiVersion = null)
        {
            return RecaptchaWidget(htmlHelper, siteKey, renderApiScript, theme, language, tabIndex, size, useSsl, apiVersion);
        }

        /// <summary>
        /// Renders the recaptcha HTML in an MVC view. It is an extension method to the <see cref="System.Web.Mvc.HtmlHelper"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="System.Web.Mvc.HtmlHelper"/> object to which the extension is added.</param>
        /// <param name="siteKey">Sets the site key of recaptcha.</param>
        /// <param name="renderApiScript">Determines if the API script is to be rendered.</param>
        /// <param name="theme">The color theme of the widget.</param>
        /// <param name="language">Forces the reCAPTCHA widget to render in a specific language. By default, the user's language is used.</param>
        /// <param name="tabIndex">The tabindex of the reCAPTCHA widget.</param>
        /// <param name="size">The size of the reCAPTCHA widget.</param>
        /// <param name="useSsl">Determines if SSL is to be used in Google reCAPTCHA API calls.</param>
        /// <param name="apiVersion">Determines the version of the reCAPTCHA API.</param>
        /// <returns>Returns an instance of the IHtmlString type.</returns>
        public static IHtmlString RecaptchaWidget(
            this HtmlHelper htmlHelper,
            string siteKey = null,
            bool renderApiScript = true,
            RecaptchaTheme? theme = null,
            string language = null,
            int? tabIndex = null,
            RecaptchaSize? size = null,
            RecaptchaSslBehavior? useSsl = null,
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
                var rHtmlHelper = new Recaptcha2HtmlHelper(siteKey ?? config.SiteKey);
                var writer = new HtmlTextWriter(new StringWriter());
                writer.Write(rHtmlHelper.CreateWidgetHtml(renderApiScript, theme != null ? (RecaptchaTheme)theme : config.Theme, language ?? config.Language, tabIndex != null ? (int)tabIndex : 0, size != null ? (RecaptchaSize)size : config.Size, useSsl != null ? (RecaptchaSslBehavior)useSsl : config.UseSsl));

                return htmlHelper.Raw(writer.InnerWriter.ToString());
            }
            else
            {
                throw new InvalidOperationException("The API version is either invalid or not supported.");
            }
        }

        /// <summary>
        /// Renders the recaptcha HTML in an MVC view. It is an extension method to the <see cref="System.Web.Mvc.HtmlHelper"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="System.Web.Mvc.HtmlHelper"/> object to which the extension is added.</param>
        /// <param name="siteKey">Sets the site key of recaptcha.</param>
        /// <param name="language">Forces the reCAPTCHA widget to render in a specific language. By default, the user's language is used.</param>
        /// <param name="useSsl">Determines if SSL is to be used in Google reCAPTCHA API calls.</param>
        /// <param name="apiVersion">Determines the version of the reCAPTCHA API.</param>
        /// <returns>Returns an instance of the IHtmlString type.</returns>
        public static IHtmlString RecaptchaApiScript(
            this HtmlHelper htmlHelper,
            string siteKey = null,
            string language = null,
            RecaptchaSslBehavior? useSsl = null,
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
                var rHtmlHelper = new Recaptcha2HtmlHelper(siteKey ?? config.SiteKey);
                var writer = new HtmlTextWriter(new StringWriter());
                writer.Write(rHtmlHelper.CreateApiScripttHtml(language ?? config.Language, useSsl != null ? (RecaptchaSslBehavior)useSsl : config.UseSsl));

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