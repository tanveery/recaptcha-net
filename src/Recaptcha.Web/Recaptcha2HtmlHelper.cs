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
        public Recaptcha2HtmlHelper(string siteKey)
        {
            if (String.IsNullOrEmpty(siteKey))
            {
                throw new InvalidOperationException("Site key cannot be null or empty.");
            }

            this.SiteKey = siteKey;
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

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Gets the recaptcha's HTML that needs to be rendered in an HTML page.
        /// </summary>
        /// <returns>Returns the HTML as an instance of the <see cref="String"/> type.</returns>
        public string CreateWidgetHtml(bool renderApiScript, RecaptchaTheme theme, string language, int tabIndex, RecaptchaSize size, SslBehavior useSsl)
        {
            var dictAttributes = new Dictionary<string, string>();            
            dictAttributes.Add("data-" + PARAM_SITEKEY, SiteKey);

            if (theme != RecaptchaTheme.Default)
            {
                dictAttributes.Add("data-" + PARAM_THEME, theme.ToString().ToLower());
            }

            if (tabIndex != 0)
            {
                dictAttributes.Add("data-" + PARAM_TABINDEX, tabIndex.ToString());
            }

            if (size != RecaptchaSize.Default)
            {
                dictAttributes.Add("data-" + PARAM_SIZE, size.ToString().ToLower());
            }

            var sbAttributes = new StringBuilder();
            foreach(var key in dictAttributes.Keys)
            {
                sbAttributes.Append($"{key}=\"{dictAttributes[key]}\" ");
            }

            StringBuilder sbHtml = new StringBuilder();

            if (renderApiScript)
            {
                sbHtml.Append(CreateApiScripttHtml(language, useSsl));
            }

            sbHtml.Append($"<div class=\"g-recaptcha\" {sbAttributes.ToString()}></div>");

            return sbHtml.ToString();
        }

        /// <summary>
        /// Gets the recaptcha's HTML that needs to be rendered in an HTML page.
        /// </summary>
        /// <returns>Returns the HTML as an instance of the <see cref="String"/> type.</returns>
        public string CreateApiScripttHtml(string language, SslBehavior useSsl)
        {
            bool doUseSsl = true;

            if (useSsl == SslBehavior.DoNotUseSsl)
            {
                doUseSsl = false;
            }
            else if (useSsl == SslBehavior.AlwaysUseSsl)
            {
                doUseSsl = true;
            }
            else if (useSsl == SslBehavior.SameAsRequestUrl)
            {
                doUseSsl = HttpContext.Current.Request.IsSecureConnection;
            }

            var protocol = "https://";

            if (!doUseSsl)
            {
                protocol = "http://";
            }

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

        #endregion Public Methods
    }
}