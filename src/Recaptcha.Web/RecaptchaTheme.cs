/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the theme of an ASP.NET Recaptcha control.
    /// </summary>
    public enum RecaptchaTheme
    {
        #region Common value for reCAPTCHA v1 and v2.
        /// <summary>
        /// Default theme. No theme will be specified in the rendered HTML / JavaScript of the reCAPTCHA control.
        /// </summary>
        Default = 0,
        #endregion Common values for reCAPTCHA v1 and v2.

        #region Values for reCAPTCHA v1
        /// <summary>
        /// Red theme. Applicable for reCAPTCHA v1.
        /// </summary>
        Red = 1,
        /// <summary>
        /// Blackglass theme. Applicable for reCAPTCHA v1.
        /// </summary>
        Blackglass = 2,
        /// <summary>
        /// White theme. Applicable for reCAPTCHA v1.
        /// </summary>
        White = 3,
        /// <summary>
        /// Clean theme. Applicable for reCAPTCHA v1.
        /// </summary>
        Clean = 4,
        #endregion Values for reCAPTCHA v1

        #region Values for reCAPTCHA v2
        /// <summary>
        /// Light theme. Applicable for reCAPTCHA v2.
        /// </summary>
        Light = 50,
        /// <summary>
        /// Dark theme. Applicable for reCAPTCHA v2.
        /// </summary>
        Dark = 51
        #endregion Values for reCAPTCHA v2
    }
}
