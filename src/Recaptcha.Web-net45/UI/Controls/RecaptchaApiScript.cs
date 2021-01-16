/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using Recaptcha.Web.Configuration;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recaptcha.Web.UI.Controls
{
    /// <summary>
    /// Renders API script for Google's reCAPTCHA control.
    /// </summary>
    [DefaultProperty("SiteKey")]
    [ToolboxData("<{0}:RecaptchaApiScript runat=server></{0}:RecaptchaApiScript>")]
    public class RecaptchaApiScript : RecaptchaControlBase
    {
        #region Control Events

        /// <summary>
        /// Calls the OnLoad method of the parent class <see cref="System.Web.UI.WebControls.WebControl"/>
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> object passed to the Load event of the control.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// Redners the HTML output. This method is automatically called by ASP.NET during the rendering process.
        /// </summary>
        /// <param name="output">The output object to which the method will write HTML to.</param>
        /// <exception cref="InvalidOperationException">The exception is thrown if the public key is not set.</exception>
        protected override void RenderContents(HtmlTextWriter output)
        {
            if (this.DesignMode)
            {
                output.Write("<p>Recaptcha API Script Control</p>");
            }
            else
            {
                if (ApiVersion == null || ApiVersion == "2")
                {
                    var htmlHelper = new Recaptcha2HtmlHelper(this.SiteKey);
                    output.Write(htmlHelper.CreateApiScripttHtml(Language, UseSsl));
                }
                else
                {
                    throw new InvalidOperationException("The API version is either invalid or not supported.");
                }
            }
        }

        #endregion Control Events
    }
}
