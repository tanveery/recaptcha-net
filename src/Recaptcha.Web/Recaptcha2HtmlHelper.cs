/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the functionality to generate HTML for Recaptcha API v2.0.
    /// </summary>
    public class Recaptcha2HtmlHelper
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
        /// <param name="siteKey">Sets the public key of the recaptcha HTML.</param>
        /// <param name="theme">Sets the theme of the recaptcha HTML.</param>
        /// <param name="language">Sets the language of the recaptcha HTML.</param>
        /// <param name="tabIndex">Sets the tab index of the recaptcha HTML.</param>    
        /// <param name="dataSize">Sets the size for the recpatcha HTML.</param>
        /// <param name="useSsl">Determines whether to use SSL in reCAPTCHA API URLs.</param>
        public Recaptcha2HtmlHelper(string siteKey, RecaptchaTheme theme, string language, int tabIndex, RecaptchaDataSize dataSize, SslBehavior useSsl)
        {
            if (String.IsNullOrEmpty(siteKey))
            {
                throw new InvalidOperationException("Site key cannot be null or empty.");
            }

            this.SiteKey = siteKey;
            this.Theme = theme;
            this.Language = language;
            this.TabIndex = tabIndex;

            UseSsl = useSsl;
            Size = dataSize;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the site key of the recaptcha HTML.
        /// </summary>
        public string SiteKey
        {
            get;
            set;
        }

        /// <summary>
        /// Determines if HTTPS intead of HTTP is to be used in reCAPTCHA API calls.
        /// </summary>
        public SslBehavior UseSsl
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the theme of the reCAPTCHA HTML.
        /// </summary>
        public RecaptchaTheme Theme
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the language of the recaptcha HTML.
        /// </summary>
        public string Language
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tab index of the recaptcha HTML.
        /// </summary>
        public int TabIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the size of the reCAPTCHA control.
        /// </summary>
        public RecaptchaDataSize Size
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
            var dictAttributes = new Dictionary<string, string>();
            
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

            dictAttributes.Add("data-" + PARAM_SITEKEY, SiteKey);

            if (Theme != RecaptchaTheme.Default)
            {
                dictAttributes.Add("data-" + PARAM_THEME, Theme.ToString().ToLower());
            }

            if (TabIndex != 0)
            {
                dictAttributes.Add("data-" + PARAM_TABINDEX, TabIndex.ToString());
            }

            if (Size != RecaptchaDataSize.Default)
            {
                dictAttributes.Add("data-" + PARAM_SIZE, Size.ToString().ToLower());
            }

            var sbAttributes = new StringBuilder();
            foreach(var key in dictAttributes.Keys)
            {
                sbAttributes.Append($"{key}=\"{dictAttributes[key]}\" ");
            }

            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append(CreateRecaptchaScript(protocol, Language));            
            sbHtml.Append($"<div class=\"g-recaptcha\" {sbAttributes.ToString()}></div>");

            return sbHtml.ToString();
        }

        #endregion Public Methods

        #region Private Methods

        private string CreateRecaptchaScript(string protocol, string language)
        {
            var dictQS = new Dictionary<string, string>();
            var url = $"{protocol}www.google.com/recaptcha/api.js";

            if (!string.IsNullOrEmpty(language))
            {
                dictQS.Add(PARAM_HL, language);
            }

            var qs = new StringBuilder();

            foreach (var key in dictQS.Keys)
            {
                if (qs.Length <= 0)
                {
                    qs.Append($"?{key}={dictQS[key]}");
                }
                else
                {
                    qs.Append($"&{key}={dictQS[key]}");
                }
            }

            return $"<script src=\"{url}{qs.ToString()}\" async defer></script>";
        }

        #endregion Private Methods
    }
}