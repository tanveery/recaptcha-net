/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using Recaptcha.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RecaptchaWebFormSample
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Recaptcha1.Response))
            {
                lblMessage.Text = "Captcha cannot be empty.";
            }
            else
            {
                var result = Recaptcha1.Verify();

                if (result.Success)
                {
                    Response.Redirect("Welcome.aspx");
                }
                else
                {
                    lblMessage.Text = "Error(s): ";

                    foreach(var err in result.ErrorCodes)
                    {
                        lblMessage.Text = lblMessage.Text + err;
                    }
                }
            }
        }
    }
}