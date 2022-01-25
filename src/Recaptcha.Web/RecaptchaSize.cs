/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the size of reCAPTCHA widget.
    /// </summary>
    public enum RecaptchaSize
    {
        /// <summary>
        /// The default size. No size is specified in the generated HTML.
        /// </summary>
        Default = 0,
        /// <summary>
        /// Specifies the normal size to be used for reCAPTCHA.
        /// </summary>
        Normal = 1,
        /// <summary>
        /// Specifies the compact size to be used for reCAPTCHA.
        /// </summary>
        Compact = 2
    }
}
