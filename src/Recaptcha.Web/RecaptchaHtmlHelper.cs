﻿/* ============================================================================================================================
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
    /// Represents the functionality to generate recaptcha HTML.
    /// </summary>
    public class RecaptchaHtmlHelper : IRecaptchaHtmlHelper
    {
        /// <summary>
        /// Creates an instance of the <see cref="RecaptchaHtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key to be part of the recaptcha HTML.</param>
        public RecaptchaHtmlHelper(string publicKey)
        {
            if (String.IsNullOrEmpty(publicKey))
            {
                throw new InvalidOperationException("Public key cannot be null or empty.");
            }

            this.PublicKey = RecaptchaKeyHelper.ParseKey(publicKey);
        }

        /// <summary>
        /// Creates an instance of the <see cref="RecaptchaHtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key of the recaptcha HTML.</param>
        /// <param name="theme">Sets the theme of the recaptcha HTML.</param>
        /// <param name="language">Sets the language of the recaptcha HTML.</param>
        /// <param name="tabIndex">Sets the tab index of the recaptcha HTML.</param>    
        public RecaptchaHtmlHelper(string publicKey, RecaptchaTheme theme, string language, int tabIndex)
        {
            this.PublicKey = RecaptchaKeyHelper.ParseKey(publicKey);

            if (String.IsNullOrEmpty(this.PublicKey))
            {
                throw new InvalidOperationException("Public key cannot be null or empty.");
            }

            this.Theme = theme;
            this.Language = language;
            this.TabIndex = tabIndex;
        }

        /// <summary>
        /// Creates an instance of the <see cref="RecaptchaHtmlHelper"/> class.
        /// </summary>
        /// <param name="publicKey">Sets the public key of the recaptcha HTML.</param>
        /// <param name="theme">Sets the theme of the recaptcha HTML.</param>
        /// <param name="language">Sets the language of the recaptcha HTML.</param>
        /// <param name="tabIndex">Sets the tab index of the recaptcha HTML.</param>    
        /// <param name="useSsl">Determines whether to use SSL in reCAPTCHA API URLs.</param>
        public RecaptchaHtmlHelper(string publicKey, RecaptchaTheme theme, string language, int tabIndex, SslBehavior useSsl)
        {
            this.PublicKey = RecaptchaKeyHelper.ParseKey(publicKey);

            if (String.IsNullOrEmpty(this.PublicKey))
            {
                throw new InvalidOperationException("Public key cannot be null or empty.");
            }

            this.Theme = theme;
            this.Language = language;
            this.TabIndex = tabIndex;

            UseSsl = useSsl;
        }

        /// <summary>
        /// Gets the public key of the recaptcha HTML.
        /// </summary>
        public string PublicKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Determines if HTTPS intead of HTTP is to be used in reCAPTCHA API calls.
        /// </summary>
        public SslBehavior UseSsl
        {
            get;
            private set;
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
        /// Gets the recaptcha's HTML that needs to be rendered in an HTML page.
        /// </summary>
        /// <returns>Returns the HTML as an instance of the <see cref="String"/> type.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<script type=\"text/javascript\">\nvar RecaptchaOptions = {");

            string language = this.Language;

            if (String.IsNullOrEmpty(language))
            {
                language = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            }

            sb.Append(String.Format("\ntheme : '{0}',\nlang : '{1}',\ntabindex : {2}\n", Theme.ToString().ToLower(), language, TabIndex));
            sb.Append("};\n</script>");

            bool doUseSsl = false;

            if(UseSsl == SslBehavior.DoNotUseSsl)
            {
                doUseSsl = false;
            }
            else if(UseSsl == SslBehavior.AlwaysUseSsl)
            {
                doUseSsl = true;
            }
            else if(UseSsl == SslBehavior.SameAsRequestUrl)
            {
                doUseSsl = HttpContext.Current.Request.IsSecureConnection;
            }

            if (!doUseSsl)
            {
                sb.Append(String.Format("<script type=\"text/javascript\" src=\"http://www.google.com/recaptcha/api/challenge?k={0}&lang={1}\">", PublicKey, Language));
            }
            else
            {
                sb.Append(String.Format("<script type=\"text/javascript\" src=\"https://www.google.com/recaptcha/api/challenge?k={0}&lang={1}\">", PublicKey, Language));
            }

            sb.Append("</script>");

            return sb.ToString();
        }
    }
}
