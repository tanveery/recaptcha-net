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
        /// <param name="theme">The color theme of the widget.</param>
        /// <param name="language">Forces the reCAPTCHA widget to render in a specific language. By default, the user's language is used.</param>
        /// <param name="size">The size of the reCAPTCHA widget.</param>
        /// <param name="useSsl">Determines if SSL is to be used in Google reCAPTCHA API calls.</param>
        /// 
        public RecaptchaConfiguration(string siteKey, string secretKey, string apiVersion, string language = null, RecaptchaTheme theme = RecaptchaTheme.Default, RecaptchaSize size= RecaptchaSize.Default, SslBehavior useSsl = SslBehavior.AlwaysUseSsl)
        {
            SiteKey = siteKey;
            SecretKey = secretKey;
            ApiVersion = apiVersion;
            Language = language;
            Theme = theme;
            Size = size;
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
        /// Determines if SSL is to be used in Google reCAPTCHA API calls.
        /// </summary>
        public SslBehavior UseSsl
        {
            get;
            private set;
        }

        /// <summary>
        /// Forces the reCAPTCHA widget to render in a specific language. By default, the user's language is used.
        /// </summary>
        public string Language
        {
            get;
            private set;
        }

        /// <summary>
        /// The color theme of the widget.
        /// </summary>
        public RecaptchaTheme Theme
        {
            get;
            private set;
        }

        /// <summary>
        /// The size of the widget.
        /// </summary>
        public RecaptchaSize Size
        {
            get;
            private set;
        }
    }
}
