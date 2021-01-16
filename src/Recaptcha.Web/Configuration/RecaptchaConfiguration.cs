/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

namespace Recaptcha.Web.Configuration
{
    /// <summary>
    /// Represents reCAPTCHA configuration.
    /// </summary>
    public class RecaptchaConfiguration
    {
        /// <summary>
        /// Creates an instance of the <see cref="RecaptchaConfiguration"/> class.
        /// </summary>
        /// <param name="siteKey">The site key.</param>
        /// <param name="secretKey">The secret key.</param>
        /// <param name="apiVersion">The API version of reCAPTCHA to be used.</param>
        /// <param name="defaultLanguage">Forces the widget to render in a specific language. Auto-detects the user's language if unspecified.</param>
        /// <param name="defaultTheme">The color theme of the widget.</param>
        /// <param name="defaultSize">The size of the widget.</param>
        /// <param name="useSsl">Determines if HTTPS is to be used in reCAPTCHA URLs.</param>
        /// 
        public RecaptchaConfiguration(string siteKey, string secretKey, string apiVersion, string defaultLanguage = null, RecaptchaTheme defaultTheme = RecaptchaTheme.Default, RecaptchaSize defaultSize= RecaptchaSize.Default, SslBehavior useSsl = SslBehavior.AlwaysUseSsl)
        {
            SiteKey = siteKey;
            SecretKey = secretKey;
            ApiVersion = apiVersion;
            DefaultLanguage = defaultLanguage;
            DefaultTheme = defaultTheme;
            DefaultSize = defaultSize;
            UseSsl = useSsl;
        }

        /// <summary>
        /// The target reCAPTCHA API version.
        /// </summary>
        public string ApiVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// The site key.
        /// </summary>
        public string SiteKey
        {
            get;
            private set;
        }

        /// <summary>
        /// The secret key
        /// </summary>
        public string SecretKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Use SSL
        /// </summary>
        public SslBehavior UseSsl
        {
            get;
            private set;
        }

        /// <summary>
        /// Forces the widget to render in a specific language. Auto-detects the user's language if unspecified.
        /// </summary>
        public string DefaultLanguage
        {
            get;
            private set;
        }

        /// <summary>
        /// The color theme of the widget.
        /// </summary>
        public RecaptchaTheme DefaultTheme
        {
            get;
            private set;
        }

        /// <summary>
        /// The size of the widget.
        /// </summary>
        public RecaptchaSize DefaultSize
        {
            get;
            private set;
        }
    }
}
