/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Web.UI;

namespace RecaptchaWebFormSample
{
    public partial class Default : Page
    {
        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            if (await Recaptcha1.VerifyIfSolvedAsync())
            {
                Response.Redirect("Welcome.aspx");
            }
            else
            {
                lblMessage.Text = "Incorrect captcha response.";
            }
        }
    }
}