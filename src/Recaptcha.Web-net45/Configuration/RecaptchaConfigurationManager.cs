/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

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
            string siteKey = null, secretKey = null, language = null, apiVersion = "2", apiSource = null;
            RecaptchaSize size = RecaptchaSize.Default;
            RecaptchaTheme theme = RecaptchaTheme.Default;
            RecaptchaSslBehavior useSsl = RecaptchaSslBehavior.AlwaysUseSsl;

            if (ConfigurationManager.AppSettings.AllKeys.Contains("RecaptchaSiteKey"))
            {
                siteKey = ConfigurationManager.AppSettings["RecaptchaSiteKey"];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("RecaptchaSecretKey"))
            {
                secretKey = ConfigurationManager.AppSettings["RecaptchaSecretKey"];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("RecaptchaApiVersion"))
            {
                apiVersion = ConfigurationManager.AppSettings["RecaptchaApiVersion"];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("RecaptchaLanguage"))
            {
                language = ConfigurationManager.AppSettings["RecaptchaLanguage"];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("RecaptchaTheme"))
            {
                Enum.TryParse<RecaptchaTheme>(ConfigurationManager.AppSettings["RecaptchaTheme"], out theme);
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("RecaptchaSize"))
            {
                Enum.TryParse<RecaptchaSize>(ConfigurationManager.AppSettings["RecaptchaSize"], out size);
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("RecaptchaUseSsl"))
            {
                Enum.TryParse<RecaptchaSslBehavior>(ConfigurationManager.AppSettings["RecaptchaUseSsl"], out useSsl);
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("RecaptchaApiSource"))
            {
                apiSource = ConfigurationManager.AppSettings["RecaptchaApiSource"];
            }

            return new RecaptchaConfiguration(siteKey, secretKey, apiVersion, language, theme, size, useSsl, apiSource);
        }
    }
}
