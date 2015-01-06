/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

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
        /// <summary>
        /// Renders the reCAPTCHA HTML in an MVC view.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="System.Web.Mvc.HtmlHelper"/> object to which the extension is added.</param>
        /// <param name="publicKey">Sets the public key (Site key) used to render the reCAPTCHA HTML.</param>
        /// <param name="theme">Sets the theme used to render the reCAPTCHA HTML.</param>
        /// <param name="language">Sets the language used to render the reCAPTCHA HTML.
        /// <para/> If null, no language will be specified and the language should be detected by the reCAPTCHA API.</param>
        /// <param name="tabIndex">Sets the tab index used to render the reCAPTCHA HTML.</param>
        /// <param name="useSsl">Sets the value to the UseSsl property.</param>
        /// <returns>Returns rendered reCAPTCHA HTML as <see cref="IHtmlString"/>.</returns>
        [Obsolete("Use RecaptchaWidget method instead.")]
        public static IHtmlString Recaptcha(
            this HtmlHelper htmlHelper,
            string publicKey = "{recaptchaPublicKey}",
#pragma warning disable 618
            RecaptchaTheme theme = RecaptchaTheme.Red,
#pragma warning restore 618
            string language = null,
            int tabIndex = 0,
            bool useSsl = false)
        {
            var recaptchaHtmlHelper = new RecaptchaHtmlHelper(publicKey, theme, language, tabIndex);
            using (var stringWriter = new StringWriter())
            {
                using (var htmlWriter = new HtmlTextWriter(stringWriter))
                {
                    recaptchaHtmlHelper.Render(htmlWriter);
                }

                return new MvcHtmlString(stringWriter.ToString());
            }
        }

        /// <summary>
        /// Renders the reCAPTCHA HTML in an MVC view.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="System.Web.Mvc.HtmlHelper"/> object to which the extension is added.</param>
        /// <param name="publicKey">Sets the public key (Site key) used to render the reCAPTCHA HTML.</param>
        /// <param name="colorTheme">Sets the color theme used to render the reCAPTCHA HTML.
        /// <para/> If null, no color theme will be specified and light theme should be used by the reCAPTCHA API.</param>
        /// <param name="language">Sets the language used to render the reCAPTCHA HTML.
        /// <para/> If null, no language will be specified and the language should be detected by the reCAPTCHA API.</param>
        /// <returns>Returns rendered reCAPTCHA HTML as <see cref="IHtmlString"/>.</returns>
        public static IHtmlString RecaptchaWidget(
            this HtmlHelper htmlHelper,
            string publicKey = "{recaptchaPublicKey}",
            ColorTheme? colorTheme = null,
            string language = null)
        {
            var recaptchaHtmlHelper = new RecaptchaHtmlHelper(publicKey);
            if (colorTheme.HasValue)
            {
                recaptchaHtmlHelper.ColorTheme = colorTheme.Value;
            }

            if (String.IsNullOrEmpty(language) == false)
            {
                recaptchaHtmlHelper.Language = language;
            }

            using (var stringWriter = new StringWriter())
            {
                using (var htmlWriter = new HtmlTextWriter(stringWriter))
                {
                    recaptchaHtmlHelper.Render(htmlWriter);
                }

                return new MvcHtmlString(stringWriter.ToString());
            }
        }

        /// <summary>
        /// Gets an instance of the <see cref="Verifier"/> class
        /// that can be used to verify reCAPTCHA client-side API response with request to the server-side reCAPTCHA API.
        /// </summary>
        /// <param name="controller">The <see cref="System.Web.Mvc.Controller"/> object to which the extension method is added to.</param>
        /// <param name="privateKey">The private key (Secret key) required to be part of the reCAPTCHA verification request.</param>
        /// <returns>Returns an instance of the <see cref="Verifier"/> class.</returns>
        public static Verifier GetRecaptchaVerifier(this Controller controller, string privateKey = "{recaptchaPrivateKey}")
        {
            return new Verifier(privateKey);
        }

        /// <summary>
        /// Gets an instance of the <see cref="RecaptchaVerificationHelper"/> class
        /// that can be used to verify reCAPTCHA client-side API response with request to the server-side reCAPTCHA API.
        /// </summary>
        /// <param name="controller">The <see cref="System.Web.Mvc.Controller"/> object to which the extension method is added to.</param>
        /// <param name="privateKey">The private key (Secret key) required to be part of the reCAPTCHA verification request.</param>
        /// <returns>Returns an instance of the <see cref="RecaptchaVerificationHelper"/> class.</returns>
        [Obsolete("Use GetRecaptchaVerifier method instead.")]
        public static RecaptchaVerificationHelper GetRecaptchaVerificationHelper(this Controller controller, string privateKey = "{recaptchaPrivateKey}")
        {
            return new RecaptchaVerificationHelper(privateKey);
        }
    }
}