/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the themes used to render the reCAPTCHA HTML.
    /// </summary>
    [Obsolete("Use ColorTheme enumeration to set a color theme, which is supported by the current version of API.")]
    public enum RecaptchaTheme
    {
        /// <summary>
        /// The theme is red.
        /// </summary>
        Red = 0,

        /// <summary>
        /// The theme is black glass.
        /// </summary>
        Blackglass = 1,

        /// <summary>
        /// The theme is white.
        /// </summary>
        White = 2,

        /// <summary>
        /// The theme is clean.
        /// </summary>
        Clean = 3
    }
}
