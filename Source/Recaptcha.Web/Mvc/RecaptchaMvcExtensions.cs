/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.UI;

namespace Recaptcha.Web.Mvc
{
    /// <summary>
    /// Represents the Recaptcha method extensions container for the <see cref="System.Web.Mvc.HtmlHelper"/> and <see cref="System.Web.Mvc.Controller"/> classes.
    /// </summary>
    public static class RecaptchaMvcExtensions
    {
        /// <summary>
        /// Renders the recaptcha HTML in an MVC view. It is an extension method to the <see cref="System.Web.Mvc.HtmlHelper"/> class.
        /// </summary>
        /// <param name="htmlHelper">The <see cref="System.Web.Mvc.HtmlHelper"/> object to which the extension is added.</param>
        /// <param name="publicKey">Sets the public key of recaptcha.</param>
        /// <param name="theme">Sets the theme of recaptcha.</param>
        /// <param name="language">Sets the language of recaptcha. If no language is specified, the language of the current UI culture will be used.</param>
        /// <param name="tabIndex">Sets the tab index of recaptcha.</param>
        /// <param name="useSsl">Sets the value to the UseSsl property.</param>
        /// <returns>Returns an instance of the IHtmlString type.</returns>
        public static IHtmlString Recaptcha(
            this HtmlHelper htmlHelper,
            string publicKey = "{recaptchaPublicKey}",
            RecaptchaTheme theme = RecaptchaTheme.Red,
            string language = null,
            int tabIndex = 0,
            bool useSsl = false)
        {            
            RecaptchaHtmlHelper rHtmlHelper = new RecaptchaHtmlHelper(publicKey, theme, language, tabIndex);

            HtmlTextWriter writer = new HtmlTextWriter(new StringWriter());
            writer.Write(rHtmlHelper.ToString());

            return htmlHelper.Raw(writer.InnerWriter.ToString());
        }

        /// <summary>
        /// Gets an instance of the <see cref="RecaptchaVerificationHelper"/> class that can be used to verify user's response to the recaptcha's challenge. 
        /// </summary>
        /// <param name="controller">The <see cref="System.Web.Mvc.Controller"/> object to which the extension method is added to.</param>
        /// <param name="privateKey">The private key required for making the recaptcha verification request.</param>
        /// <returns>Returns an instance of the <see cref="RecaptchaVerificationHelper"/> class.</returns>
        public static RecaptchaVerificationHelper GetRecaptchaVerificationHelper(this System.Web.Mvc.Controller controller, string privateKey)
        {
            return new RecaptchaVerificationHelper(privateKey);
        }

        /// <summary>
        /// Gets an instance of the <see cref="RecaptchaVerificationHelper"/> class that can be used to verify user's response to the recaptcha's challenge. 
        /// </summary>
        /// <param name="controller">The <see cref="System.Web.Mvc.Controller"/> object to which the extension method is added to.</param>
        /// <returns>Returns an instance of the <see cref="RecaptchaVerificationHelper"/> class.</returns>
        public static RecaptchaVerificationHelper GetRecaptchaVerificationHelper(this System.Web.Mvc.Controller controller)
        {
            return new RecaptchaVerificationHelper("{recaptchaPrivateKey}");
        }
    }
}