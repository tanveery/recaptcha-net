/* ============================================================================================================================
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
 * =========================================================================================================================== */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Recaptcha.Web.UI.Controls
{
  /// <summary>
  /// An ASP.NET control that wraps Google's recaptcha control.
  /// </summary>
  [DefaultProperty("PublicKey")]
  [ToolboxData("<{0}:Recaptcha runat=server></{0}:Recaptcha>")]
  public class Recaptcha : WebControl
  {
    #region Fields

    private RecaptchaVerificationHelper _verificationHelper = null;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Gets or sets the API version of the recaptcha control.
    /// </summary>
    /// <remarks>The value of the <see cref="ApiVersion"/> property is optional. If the value is not set, version 1 is automatically assumed.</remarks>
    [Bindable(true)]
    [Category("Behavior")]
    [DefaultValue("{recaptchaApiVersion}")]
    [Localizable(false)]
    public string ApiVersion
    {
      get
      {
        String s = (String)ViewState["ApiVersion"];
        return ((s == null) ? "{recaptchaApiVersion}" : s);
      }

      set
      {
        ViewState["ApiVersion"] = value;
      }
    }

    /// <summary>
    /// Gets or sets the public key of the recaptcha control.
    /// </summary>
    /// <remarks>The value of the <see cref="PublicKey"/> property is required. If the key is not set, a runtime exception will be thrown. The key can be set either directly as a literal value or as an appSettings key from the application configuration file. An appSettings key needs to be specified within {} curly braces.</remarks>
    [Bindable(true)]
    [Category("Behavior")]
    [DefaultValue("{recaptchaPublicKey}")]
    [Localizable(false)]
    public string PublicKey
    {
      get
      {
        String s = (String)ViewState["PublicKey"];
        return ((s == null) ? "{recaptchaPublicKey}" : s);
      }

      set
      {
        ViewState["PublicKey"] = value;
      }
    }

    /// <summary>
    /// Gets or sets the private key of the recaptcha control.
    /// </summary>
    /// <remarks>The value of the <see cref="PrivateKey"/> property is required. If the key is not set, a runtime exception will be thrown. The key can be set either directly as a literal value or as an appSettings key from the application configuration file. An appSettings key needs to be specified within {} curly braces.</remarks>
    [Bindable(true)]
    [Category("Behavior")]
    [DefaultValue("{recaptchaPrivateKey}")]
    [Localizable(false)]
    public string PrivateKey
    {
      get
      {
        String s = (String)ViewState["PrivateKey"];
        return ((s == null) ? "{recaptchaPrivateKey}" : s);
      }
      set
      {
        ViewState["PrivateKey"] = value;
      }
    }

    /// <summary>
    /// Gets or sets the theme of the recaptcha control.
    /// </summary>
    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue(RecaptchaTheme.Red)]
    [Localizable(false)]
    public RecaptchaTheme Theme
    {
      get
      {
        object t = ViewState["RecaptchaTheme"];
        return ((t == null) ? RecaptchaTheme.Red : (RecaptchaTheme)t);
      }

      set
      {
        ViewState["RecaptchaTheme"] = value;
      }
    }

    /// <summary>
    /// Gets or sets the language of the recaptcha control.
    /// </summary>
    /// <remarks>If the property is not set then the language of the current UI culture will be used.</remarks>
    [Bindable(true)]
    [Category("Appearance")]
    [Localizable(false)]
    public string Language
    {
      get
      {
        return ViewState["RecaptchaLanguage"] as string;
      }

      set
      {
        ViewState["RecaptchaLanguage"] = value;
      }
    }

    /// <summary>
    /// Determines whether to use SSL in reCAPTCHA URLs.
    /// </summary>
    /// <remarks>The default value is <see cref="SslBehavior.SameAsRequestUrl"/>.</remarks>
    [Bindable(true)]
    [Category("Behavior")]
    [DefaultValue(false)]
    [Localizable(false)]
    public SslBehavior UseSsl
    {
      get
      {
        if (ViewState["UseSsl"] == null)
        {
          ViewState["UseSsl"] = SslBehavior.SameAsRequestUrl.ToString();
        }

        return (SslBehavior)Enum.Parse(typeof(SslBehavior), ViewState["UseSsl"].ToString());
      }

      set
      {
        ViewState["UseSsl"] = value;
      }
    }

    /// <summary>
    /// Gets the user's response to the recaptcha challenge.
    /// </summary>
    [Bindable(true)]
    [Category("Appearance")]
    [Localizable(false)]
    public string Response
    {
      get
      {
        if (_verificationHelper != null)
        {
          return _verificationHelper.Response;
        }

        return String.Empty;
      }
    }

    /// <summary>
    /// Gets or sets the data size of the recaptcha control.
    /// </summary>
    /// <remarks>This property is only relevant for v2 API. It has no effect if you are using v1 API.</remarks>
    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue(RecaptchaDataSize.Normal)]
    [Localizable(false)]
    public RecaptchaDataSize DataSize
    {
      get
      {
        object t = ViewState["RecaptchaDataSize"];
        return ((t == null) ? RecaptchaDataSize.Normal : (RecaptchaDataSize)t);
      }

      set
      {
        ViewState["RecaptchaDataSize"] = value;
      }
    }

    /// <summary>
    /// Gets or sets the data size of the recaptcha control.
    /// </summary>
    /// <remarks>This property is only relevant for v2 API. It has no effect if you are using v1 API.</remarks>
    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue(RecaptchaDataType.Image)]
    [Localizable(false)]
    public RecaptchaDataType DataType
    {
      get
      {
        object t = ViewState["RecaptchaDataType"];
        return ((t == null) ? RecaptchaDataType.Image : (RecaptchaDataType)t);
      }

      set
      {
        ViewState["RecaptchaDataType"] = value;
      }
    }

    /// <summary>
    /// Gets or sets the data-callback of recaptcha control.
    /// </summary>
    /// <remarks>The value of the <see cref="DataCallback"/> property is optional.</remarks>
    [Bindable(true)]
    [Category("Behavior")]
    [DefaultValue("")]
    [Localizable(false)]
    public string DataCallback
    {
      get
      {
        String s = (String)ViewState["DataCallback"];
        return (s ?? "");
      }

      set
      {
        ViewState["DataCallback"] = value;
      }
    }

    /// <summary>
    /// Gets or sets the data-expired-callback of recaptcha control.
    /// </summary>
    /// <remarks>The value of the <see cref="DataExpiredCallback"/> property is optional.</remarks>
    [Bindable(true)]
    [Category("Behavior")]
    [DefaultValue("")]
    [Localizable(false)]
    public string DataExpiredCallback
    {
      get
      {
        String s = (String)ViewState["DataExpiredCallback"];
        return (s ?? "");
      }

      set
      {
        ViewState["DataExpiredCallback"] = value;
      }
    }

    /// <summary>
    /// Determines if the Api Script Tag should be rendered.
    /// </summary>
    [Bindable(true)]
    [Category("Behavior")]
    [Localizable(false)]
    public bool RenderApiScriptTag
    {
      get
      {
          bool? s = (bool?)ViewState["RenderApiScriptTag"];
          return (s ?? true);
      }

      set
      {
          ViewState["RenderApiScriptTag"] = value;
      }
    }




      

    #endregion Properties

    #region Control Events

    /// <summary>
    /// Calls the OnLoad method of the parent class <see cref="System.Web.UI.WebControls.WebControl"/> and initializes the internal state of the <see cref="Recaptcha"/> control for verification of the user's response to the recaptcha challenge.
    /// </summary>
    /// <param name="e">The <see cref="EventArgs"/> object passed to the Load event of the control.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      if (this.Page.IsPostBack)
      {
        _verificationHelper = new RecaptchaVerificationHelper(this.PrivateKey);
      }
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
        output.Write("<p>Recaptcha Control</p>");
      }
      else
      {
        IRecaptchaHtmlHelper htmlHelper = null;

        string apiVersion = RecaptchaKeyHelper.ParseKey(ApiVersion);

        if (apiVersion != "2")
        {
          htmlHelper = new RecaptchaHtmlHelper(this.PublicKey, this.Theme, this.Language, this.TabIndex, this.UseSsl);
        }
        else
        {
            htmlHelper = new Recaptcha2HtmlHelper(this.PublicKey, this.Theme, this.Language, this.TabIndex, this.DataType, this.DataSize, this.UseSsl, this.DataCallback, this.DataExpiredCallback, this.RenderApiScriptTag);
        }

        output.Write(htmlHelper.ToString());
      }
    }

    #endregion Control Events

    #region Public Methods

    /// <summary>
    /// Verifies the user's answer to the recaptcha challenge.
    /// </summary>
    /// <returns>Returns the verification result as <see cref="RecaptchaVerificationResult"/> enum value.</returns>
    ///<exception cref="InvalidOperationException">The private key is null or empty.</exception>
    ///<exception cref="System.Net.WebException">The time-out period for the recaptcha verification request expired.</exception>
    public RecaptchaVerificationResult Verify()
    {
      if (_verificationHelper == null)
      {
        _verificationHelper = new RecaptchaVerificationHelper(this.PrivateKey);
      }

      return _verificationHelper.VerifyRecaptchaResponse();
    }

    /// <summary>
    /// Verifies the user's answer to the recaptcha challenge.
    /// </summary>
    /// <returns>Returns the verification result as <see cref="RecaptchaVerificationResult"/> enum value.</returns>
    ///<exception cref="InvalidOperationException">The private key is null or empty.</exception>
    ///<exception cref="System.Net.WebException">The time-out period for the recaptcha verification request expired.</exception>
    public Task<RecaptchaVerificationResult> VerifyTaskAsync()
    {
      if (_verificationHelper == null)
      {
        _verificationHelper = new RecaptchaVerificationHelper(this.PrivateKey);
      }

      return _verificationHelper.VerifyRecaptchaResponseTaskAsync();
    }

    #endregion Public Methods
  }
}
