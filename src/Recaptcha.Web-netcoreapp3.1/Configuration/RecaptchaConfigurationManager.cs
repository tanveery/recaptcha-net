/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using Microsoft.Extensions.Configuration;
using System;

namespace Recaptcha.Web.Configuration
{
    /// <summary>
    /// Represents a class that manages reCAPTCHA configuration.
    /// </summary>
    public static class RecaptchaConfigurationManager
    {
        private static IConfiguration _configuration = null;

        /// <summary>
        /// Initializes the configuration context.
        /// </summary>
        /// <param name="configuration">The configuration context of the application.</param>
        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
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
            string apiVersion = _configuration["RecaptchaApiVersion"] ?? "2";
            RecaptchaSize size = _configuration["RecaptchaSize"] == null ? RecaptchaSize.Default : Enum.Parse<RecaptchaSize>(_configuration["RecaptchaSize"]);
            RecaptchaTheme theme = _configuration["RecaptchaTheme"] == null ? RecaptchaTheme.Default : Enum.Parse<RecaptchaTheme>(_configuration["RecaptchaTheme"]);
            RecaptchaSslBehavior useSsl = _configuration["RecaptchaUseSsl"] == null ? RecaptchaSslBehavior.AlwaysUseSsl : Enum.Parse<RecaptchaSslBehavior>(_configuration["RecaptchaUseSsl"]);

            return new RecaptchaConfiguration(siteKey, secretKey, apiVersion, language, theme, size, useSsl);
        }
    }
}
