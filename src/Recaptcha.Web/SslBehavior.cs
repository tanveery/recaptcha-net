using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recaptcha.Web
{
    /// <summary>
    /// Determines whether to use SSL in reCATPCHA API URLs.
    /// </summary>
    public enum SslBehavior
    {
        /// <summary>
        /// Use SSL if the HttpContext.Current.Request.IsSecureConnection is True.
        /// </summary>
        SameAsRequestUrl = 0,
        /// <summary>
        /// Do not use SSL.
        /// </summary>
        DoNotUseSsl = 1,
        /// <summary>
        /// Always use SSL.
        /// </summary>
        AlwaysUseSsl = 2
    }
}
