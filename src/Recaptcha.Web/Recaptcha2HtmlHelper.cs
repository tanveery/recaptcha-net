/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the functionality to generate HTML for Recaptcha API v2.0.
    /// </summary>
    public class Recaptcha2HtmlHelper : RecaptchaHtmlHelperBase
    {
        #region Fields

        private const string PARAM_ONLOAD = "onload";
        private const string PARAM_RENDER = "render";
        private const string PARAM_HL = "hl";

        private const string PARAM_SITEKEY = "sitekey";
        private const string PARAM_THEME = "theme";
        private const string PARAM_SIZE = "size";
        private const string PARAM_TABINDEX = "tabindex";
        private const string PARAM_CALLBACK = "callback";
        private const string PARAM_EXPIRED_CALLBACK = "expired-callback";
        private const string PARAM_ERROR_CALLBACK = "error-callback";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Creates an instance of the <see cref="Recaptcha2HtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key to be part of the recaptcha HTML.</param>
        public Recaptcha2HtmlHelper(string publicKey)
        : base(publicKey)
        {
            DataSize = null;
        }

        /// <summary>
        /// Creates an instance of the <see cref="Recaptcha2HtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key of the recaptcha HTML.</param>
        /// <param name="theme">Sets the theme of the recaptcha HTML.</param>
        /// <param name="language">Sets the language of the recaptcha HTML.</param>
        /// <param name="tabIndex">Sets the tab index of the recaptcha HTML.</param>   
        public Recaptcha2HtmlHelper(string publicKey, RecaptchaTheme theme, string language, int tabIndex)
            : base(publicKey, theme, language, tabIndex)
        {
            DataSize = null;
        }

        /// <summary>
        /// Creates an instance of the <see cref="Recaptcha2HtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key of the recaptcha HTML.</param>
        /// <param name="theme">Sets the theme of the recaptcha HTML.</param>
        /// <param name="language">Sets the language of the recaptcha HTML.</param>
        /// <param name="tabIndex">Sets the tab index of the recaptcha HTML.</param>    
        /// <param name="useSsl">Determines whether to use SSL in reCAPTCHA API URLs.</param>
        public Recaptcha2HtmlHelper(string publicKey, RecaptchaTheme theme, string language, int tabIndex, SslBehavior useSsl)
            : base(publicKey, theme, language, tabIndex, useSsl)
        {
            DataSize = null;
        }

        /// <summary>
        /// Creates an instance of the <see cref="Recaptcha2HtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key of the recaptcha HTML.</param>
        /// <param name="theme">Sets the theme of the recaptcha HTML.</param>
        /// <param name="language">Sets the language of the recaptcha HTML.</param>
        /// <param name="tabIndex">Sets the tab index of the recaptcha HTML.</param>    
        /// <param name="dataSize">Sets the size for the recpatcha HTML.</param>
        /// <param name="useSsl">Determines whether to use SSL in reCAPTCHA API URLs.</param>
        public Recaptcha2HtmlHelper(string publicKey, RecaptchaTheme theme, string language, int tabIndex, RecaptchaDataSize? dataSize, SslBehavior useSsl)
            : base(publicKey, theme, language, tabIndex, useSsl)
        {
            DataSize = dataSize;
        }

        /// <summary>
        /// Creates an instance of the <see cref="Recaptcha2HtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key of the recaptcha HTML.</param>
        /// <param name="theme">Sets the theme of the recaptcha HTML.</param>
        /// <param name="language">Sets the language of the recaptcha HTML.</param>
        /// <param name="tabIndex">Sets the tab index of the recaptcha HTML.</param>    
        /// <param name="dataSize">Sets the size for the recpatcha HTML.</param>
        /// <param name="useSsl">Determines whether to use SSL in reCAPTCHA API URLs.</param>
        /// <param name="dataCallback">Sets the data-callback property of the recaptcha HTML.</param>    
        /// <param name="dataExpiredCallback">Sets the data-expired-callback property of the recaptcha HTML.</param>
        public Recaptcha2HtmlHelper(string publicKey, RecaptchaTheme theme, string language, int tabIndex, RecaptchaDataSize? dataSize, SslBehavior useSsl, string dataCallback, string dataExpiredCallback)
           : base(publicKey, theme, language, tabIndex, useSsl, dataCallback, dataExpiredCallback)
        {
            DataSize = dataSize;
            DataCallback = dataCallback;
            DataExpiredCallback = dataExpiredCallback;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the size of the reCAPTCHA control.
        /// </summary>
        public RecaptchaDataSize? DataSize
        {
            get;
            set;
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Gets the recaptcha's HTML that needs to be rendered in an HTML page.
        /// </summary>
        /// <returns>Returns the HTML as an instance of the <see cref="String"/> type.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            string lang = "";

            if (!String.IsNullOrEmpty(Language))
            {
                lang = string.Format("?hl={0}", Language);
            }

            bool doUseSsl = true;

            if (UseSsl == SslBehavior.DoNotUseSsl)
            {
                doUseSsl = false;
            }
            else if (UseSsl == SslBehavior.AlwaysUseSsl)
            {
                doUseSsl = true;
            }
            else if (UseSsl == SslBehavior.SameAsRequestUrl)
            {
                doUseSsl = HttpContext.Current.Request.IsSecureConnection;
            }

            var protocol = "https://";

            if (!doUseSsl)
            {
                protocol = "http://";
            }

            sb.Append(string.Format("<script src=\"{0}www.google.com/recaptcha/api.js{1}\" async defer></script>", protocol, lang));
            sb.Append(string.Format("<div class=\"g-recaptcha\" data-sitekey=\"{0}\"", PublicKey));

            if (Theme != RecaptchaTheme.Default)
            {
                var theme = "light";

                if (Theme == RecaptchaTheme.Dark)
                {
                    theme = "dark";
                }

                sb.Append(string.Format(" data-theme=\"{0}\"", theme));
            }

            if (TabIndex != 0)
            {
                sb.Append(string.Format(" data-tabindex=\"{0}\"", TabIndex));
            }

            if (!String.IsNullOrEmpty(DataCallback))
            {
                sb.Append(String.Format(" data-callback=\"{0}\"", DataCallback));
            }

            if (!String.IsNullOrEmpty(DataExpiredCallback))
            {
                sb.Append(String.Format(" data-expired-callback=\"{0}\"", DataExpiredCallback));
            }

            if (DataSize != null)
            {
                string dataSize = null;

                switch (DataSize)
                {
                    case RecaptchaDataSize.Compact:
                        dataSize = "compact";
                        break;
                    default:
                        dataSize = "normal";
                        break;
                }

                sb.Append(string.Format(" data-size=\"{0}\"", dataSize));
            }

            sb.Append("></div>");

            return sb.ToString();
        }

        #endregion Public Methods
    }
}