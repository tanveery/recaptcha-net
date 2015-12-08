/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents the size of reCAPTCHA.
    /// </summary>
    public enum RecaptchaDataSize
    {
        /// <summary>
        /// Specifies the normal size to be used for reCAPTCHA.
        /// </summary>
        Normal = 0,
        /// <summary>
        /// Specifies the compact size to be used for reCAPTCHA.
        /// </summary>
        Compact = 1
    }
}
