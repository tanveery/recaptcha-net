/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

namespace Recaptcha.Web.Configuration
{
    /// <summary>
    /// Represents a singleton class that manages recaptcha configuration.
    /// </summary>
    public static class RecaptchaConfigurationManager
    {
        /// <summary>
        /// Gets the configuration from the default source.
        /// </summary>
        /// <returns>Returns configuration as an instance of the <see cref="RecaptchaConfiguration"/> class.</returns>
        public static RecaptchaConfiguration GetConfiguration()
        {
            string siteKey = null, secretKey = null, language=null, apiVersion="2";
            RecaptchaSize size = RecaptchaSize.Default;
            RecaptchaTheme theme = RecaptchaTheme.Default;
            SslBehavior useSsl = SslBehavior.AlwaysUseSsl;

            return new RecaptchaConfiguration(siteKey, secretKey, apiVersion, language, theme, size, useSsl);
        }
    }
}
