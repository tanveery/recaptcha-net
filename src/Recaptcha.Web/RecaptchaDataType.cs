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
    /// Represents the type of reCAPTCHA control. 
    /// </summary>
    public enum RecaptchaDataType
    {
        /// <summary>
        /// Specifies reCPATCHA to be used as an image.
        /// </summary>
        Image = 0,
        /// <summary>
        /// Specifies reCAPTCHA to be used an audio.
        /// </summary>
        Audio = 1
    }
}
