/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

namespace Recaptcha.Web
{
    /// <summary>
    /// Determines whether to use SSL in reCATPCHA API URLs.
    /// </summary>
    public enum RecaptchaSslBehavior
    {
        /// <summary>
        /// Always use SSL.
        /// </summary>
        AlwaysUseSsl = 0,
        /// <summary>
        /// Use SSL if HTTP request itself uses SSL.
        /// </summary>
        SameAsRequestUrl = 1,
        /// <summary>
        /// Do not use SSL.
        /// </summary>
        DoNotUseSsl = 2
    }
}
