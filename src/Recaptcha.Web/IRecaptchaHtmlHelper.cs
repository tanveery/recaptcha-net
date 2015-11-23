/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the interface for generating recaptcha HTML.
    /// </summary>
    public interface IRecaptchaHtmlHelper
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ID of the HTML tag that represents recaptcha.
        /// </summary>
        string RecaptchaHtmlId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the public key of the recaptcha HTML.
        /// </summary>
        string PublicKey
        {
            get;
            set;
        }

        /// <summary>
        /// Determines if HTTPS intead of HTTP is to be used in reCAPTCHA API calls.
        /// </summary>
        SslBehavior UseSsl
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the theme of the reCAPTCHA HTML.
        /// </summary>
        RecaptchaTheme Theme
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the language of the recaptcha HTML.
        /// </summary>
        string Language
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tab index of the recaptcha HTML.
        /// </summary>
        int TabIndex
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the recaptcha's HTML that needs to be rendered in an HTML page.
        /// </summary>
        /// <returns>Returns the HTML as an instance of the <see cref="String"/> type.</returns>
        string ToString();

        #endregion Methods
    }
}
