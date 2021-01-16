/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using Microsoft.Extensions.Configuration;
using System;

namespace Recaptcha.Web.Configuration
{
    /// <summary>
    /// Represents a class that manages recaptcha configuration.
    /// </summary>
    public static class RecaptchaConfigurationManager
    {
        private static IConfiguration _configuration = null;

        public static void SetConfiguration(IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            _configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration from the default source.
        /// </summary>
        /// <returns>Returns configuration as an instance of the <see cref="RecaptchaConfiguration"/> class.</returns>
        public static RecaptchaConfiguration GetConfiguration()
        {
            string siteKey = _configuration["RecaptchaSiteKey"];
            string secretKey = _configuration["RecaptchaSecretKey"];
            string language = _configuration["RecaptchaLanguage"];
            string apiVersion = _configuration["RecaptchaApiVersion"] == null ? "2" : _configuration["RecaptchaApiVersion"];
            RecaptchaSize size = _configuration["RecaptchaSize"] == null ? RecaptchaSize.Default : Enum.Parse<RecaptchaSize>(_configuration["RecaptchaSize"]);
            RecaptchaTheme theme = _configuration["RecaptchaTheme"] == null ? RecaptchaTheme.Default : Enum.Parse<RecaptchaTheme>(_configuration["RecaptchaTheme"]);
            SslBehavior useSsl = _configuration["RecaptchaUseSsl"] == null ? SslBehavior.AlwaysUseSsl : Enum.Parse<SslBehavior>(_configuration["RecaptchaUseSsl"]);

            return new RecaptchaConfiguration(siteKey, secretKey, apiVersion, language, theme, size, useSsl);
        }
    }
}
