/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;

namespace Recaptcha.Web
{
    /// <summary>
    /// Represents values of the error code as defined at https://developers.google.com/recaptcha/docs/verify.
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// No error is found.
        /// </summary>
        NoError = 0,

        /// <summary>
        /// Verification is failed but the exact reason is unknown.
        /// </summary>
        Unknown = 1,

        /// <summary>
        /// The secret parameter is missing.
        /// </summary>
        MissingInputSecret = 1 << 1,

        /// <summary>
        /// The secret parameter is invalid or malformed.
        /// </summary>
        InvalidInputSecret = 1 << 2,

        /// <summary>
        /// The response parameter is missing.
        /// </summary>
        MissingInputResponse = 1 << 3,

        /// <summary>
        /// The response parameter is invalid or malformed.
        /// </summary>
        InvalidInputResponse = 1 << 4
    }
}
