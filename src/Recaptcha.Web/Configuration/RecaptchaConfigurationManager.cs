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
            string siteKey = null, secretKey = null, language=null, apiVersion="2";
            RecaptchaDataSize size = RecaptchaDataSize.Default;
            RecaptchaTheme theme = RecaptchaTheme.Default;
            SslBehavior useSsl = SslBehavior.AlwaysUseSsl;

            if(ConfigurationManager.AppSettings.AllKeys.Contains("recaptcha:sitekey"))
            {
                siteKey = ConfigurationManager.AppSettings["recaptcha:sitekey"];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("recaptcha:secretkey"))
            {
                secretKey = ConfigurationManager.AppSettings["recaptcha:secretkey"];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("recaptcha:apiversion"))
            {
                apiVersion = ConfigurationManager.AppSettings["recaptcha:apiversion"];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("recaptcha:language"))
            {
                language = ConfigurationManager.AppSettings["recaptcha:language"];
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("recaptcha:theme"))
            {
                Enum.TryParse<RecaptchaTheme>(ConfigurationManager.AppSettings["recaptcha:theme"], out theme);
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("recaptcha:size"))
            {
                Enum.TryParse<RecaptchaDataSize>(ConfigurationManager.AppSettings["recaptcha:size"], out size);
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains("recaptcha:usessl"))
            {
                Enum.TryParse<SslBehavior>(ConfigurationManager.AppSettings["recaptcha:usessl"], out useSsl);
            }

            return new RecaptchaConfiguration(siteKey, secretKey, apiVersion, language, theme, size, useSsl);
        }
    }
}
