/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the theme of an ASP.NET Recaptcha control.
    /// </summary>
    public enum RecaptchaTheme
    {
        /// <summary>
        /// Default theme. No theme will be specified in the rendered HTML / JavaScript of the reCAPTCHA control.
        /// </summary>
        Default = 0,
        /// <summary>
        /// Light theme. Applicable for reCAPTCHA v2.
        /// </summary>
        Light = 50,
        /// <summary>
        /// Dark theme. Applicable for reCAPTCHA v2.
        /// </summary>
        Dark = 51
    }
}
