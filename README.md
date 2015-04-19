<h1>reCAPTCHA library for .NET</h1>

reCAPTCHA for .NET is a library that allows a developer to easily integrate Google's reCAPTCHA service (the most popular captcha used by millions of sites) in an ASP.NET Web Forms or ASP.NET MVC web application. 

The library is one of the most solid captcha libraries with the best documentation. Unlike most other reCAPTCHA libraries, it supports both ASP.NET Web Forms and ASP.NET MVC.

<h2>Features</h2>

The primary features of the library are:
<ul>
<li>Render recaptcha control (HTML) with appropriate options for pre-defined themes and culture (language).</li>
<li>Verify user's answer to recaptcha's challenge.</li>
<li>Supports ASP.NET Web Forms and ASP.NET MVC.</li>
</ul>

<h2>API Support</h2>
The library supports Google's reCATPCAH API version 1. Support for API version 2 is coming soon while the support for version 1 will remain in tact as long as Google does the same.

<h2>Creating a reCAPTCHA Keys</h2>
Before you can use reCAPTCHA in your web application, you must first create a reCAPTCHA key (a pair of public and private keys). Creating reCAPTCHA key is very straight-forward. The following are the steps:
<ol>
<li>Go to the Google's <a href="https://www.google.com/recaptcha">Recaptcha</a> site.</li>
<li>Click on the <strong>Get reCAPTCHA</strong> button. You will be required to login with your Google account.</li>
<li>Enter a label for this reCAPTCHA and the domain of your web application. You can enter more than one domain if you want to.</li>
<li>Expand <strong>Keys</strong> under the <strong>Adding reCAPTCHA to your site</strong> section. Note down your <strong>Site Key</strong> and <strong>Secret Key</strong>.</li>
</ol>

<h2>Installing reCAPTCHA for .NET</h2>
<h3>Recaptcha Nuget Package</h3>
The best and the recommended way to install the latest version of reCAPTCHA for .NET is through Nuget. From the <a href="http://docs.nuget.org/consume/package-manager-console">Nuget's Package Manager Console</a> in your Visual Studio .NET IDE, simply execute the following command:

```
PM> Install-Package RecaptchaNet
```

If the Package Manager Console is not visible in your Microsoft Visual Studio IDE, click on the <strong>Tools > Library Package Manager > Package Manager Console</strong> menu.

<strong>Note</strong>: Nuget is a Visual Studio extension and it needs to be installed before you can use Nuget packages in your Visual Studio projects. You can download and install <a href="https://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c">Nuget</a> from here.

<h2>How to Use reCAPTCHA in an ASP.NET Web Forms Application</h2>
Add the following line just under the Page directive in your .aspx or .ascx file:

```
<%@ Register Assembly="Recaptcha.Web" Namespace="Recaptcha.Web.UI.Controls"
TagPrefix="cc1" %>
```

Then at the desired line in the same file add the Recaptcha control as follows:

```
<cc1:Recaptcha ID="Recaptcha1" PublicKey="Your site key"
PrivateKey="Your secret key" runat="server" />
```

Rather than setting the recaptcha key of the control through its PublicKey and PrivateKey properties, you can set them in your web.config file instead:

<a href="#keyInWebConfig">How to Set Recpatcha Key in Web.config File</a>

<h3>How to Set Recpatcha Key in Web.config File</h3>
After you set the private and public keys in your web.config file, all you need in your web form is this following piece of code:

```
<cc1:Recaptcha ID="Recaptcha1" runat="server" />
```

By default, the theme of the Recaptcha control is Red. However, you can change this default theme to one of the other three themes if you like. Those themes are: Blackglass, White, and Clean. Theme can be set by using the <strong>RecaptchaTheme</strong> enum. The following is an example:

```
<cc1:Recaptcha ID="Recaptcha1" Theme="RecaptchaTheme.Clean" runat="server" />
```

<h4>Add the Recaptcha Control to the Visual Studio Toolbox</h4>

Instead of writing the above code manually, you can easily drag and drop the same Recaptcha control from the Visual Studio Toolbox onto your page designer just like the way you would do for other standard ASP.NET controls. However, you would need to add the Recaptcha control to the Toolbox first. Simply, right click on the Toolbox and select Choose Items... from the context menu and then under the .NET Framework Components tab click on the Browse button and locate the <strong>Recaptcha.Web.dll</strong> assembly.

<h3>Verify User's Response to Recaptcha Challenge</h3>

When your end-user submits the form that contains the Recaptcha control, you obviously would want to verify whether the user's answer was valid based on what was displayed in the recaptcha image. It is very easy to do with one or two lines.

First of all as expected, import the namespace <strong>Recaptcha.Web</strong> in your code-behind file:

```
using Recaptcha.Web;
```

To verify whether the user's answer is correct, call the control's <strong>Verify()</strong> method which returns RecaptchaVerificationResult. You can also use the control's <strong>Response</strong> property to check what the actual answer is. Generally, you would want to use the Response property to check if the user provided a blank response which of course is always wrong:

```
if (String.IsNullOrEmpty(Recaptcha1.Response))
{
    lblMessage.Text = "Captcha cannot be empty.";
}
else
{
    RecaptchaVerificationResult result = await Recaptcha1.Verify();

    if (result == RecaptchaVerificationResult.Success)
    {
        Response.Redirect("Welcome.aspx");
    }
    if (result == RecaptchaVerificationResult.IncorrectCaptchaSolution)
    {
        lblMessage.Text = "Incorrect captcha response.";
    }
    else
    {
        lblMessage.Text = "Some other problem with captcha.";
    }
}
```

Instead of calling the <strong>Verify()</strong> method, you can call the <strong>VerifyTaskAsync()</strong> method to verify the user's response asynchronously which at the same time can be used along with the new await keyword:

```
if (String.IsNullOrEmpty(Recaptcha1.Response))
{
    lblMessage.Text = "Captcha cannot be empty.";
}
else
{
    RecaptchaVerificationResult result = await Recaptcha1.VerifyTaskAsync();

    if (result == RecaptchaVerificationResult.Success)
    {
        Response.Redirect("Welcome.aspx");
    }
    if (result == RecaptchaVerificationResult.IncorrectCaptchaSolution)
    {
        lblMessage.Text = "Incorrect captcha response.";
    }
    else
    {
        lblMessage.Text = "Some other problem with captcha.";
    }
}
```
<h2>How to Use Recaptcha in an ASP.NET MVC Web Application</h2>

<h4>Add the Recaptcha Control to Your MVC View</h4>

Add the following line at the top of your view (a .cshtml file):

```
@using Recaptcha.Web.Mvc;
```

Then at the desired line in the same file call the Recaptcha extension method of the HtmlHelper class as follows:

```
@Html.Recaptcha(publicKey:"6LdkfdwSAAAAABL1099CPTr6473FQFXNLR_04Bb5",
privateKey:"6LdkfdwSAAAAAFC8jtUY44wuhC9lmDlFrL6qMAAh")
```

Rather than setting the recaptcha key through the PublicKey and PrivateKey properties of the HtmlHelper's recaptcha extension, you can set them in your web.config file instead:

<a href="#keyInWebConfig">How to Set Recpatcha Key in Web.config File</a>

After you set the private and public keys in your web.config file, all you need in your view is this following piece of code:

```
@Html.Recaptcha()
```

By default, the theme of recaptcha is Red. However, you can change this default theme to one of the other three themes if you like. Those themes are: Blackglass, White, and Clean. Theme can be set by using the RecaptchaTheme enum. The following is an example:

```
@Html.Recaptcha(theme:Recaptcha.Web.RecaptchaTheme.Clean);
```

<h3>Verify User's Response to Recaptcha Challenge</h3>

When your end-user submits the form that contains the Recaptcha control, you obviously would want to verify whether the user's answer was valid based on what was displayed in the recaptcha image. It is very easy to do with few lines.

First of all as expected, import the namespaces <strong>Recaptcha.Web</strong> and <strong>Recaptcha.Web.Mvc</strong> in your controller file:

```
using Recaptcha.Web;
using Recaptcha.Web.Mvc;
```

To verify whether the user's answer is correct, you need to create an instance of the <strong>RecaptchaVerificationHelper</strong> class by calling the extension method <strong>GetRecaptchaVerificationHelper()</strong> of the controller. You can then call the <strong>RecaptchaVerificationHelper</strong> object's <strong>VerifyRecaptchaResponse()</strong> method which returns a <strong>RecaptchaVerificationResult</strong> enum. You can also use the helper object's <strong>Response</strong> property to check what the actual answer of the user is. Generally, you would want to use the Response property to check if the user provided a blank response which of course is always wrong:

```
RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();

if (String.IsNullOrEmpty(recaptchaHelper.Response))
{
    ModelState.AddModelError("", "Captcha answer cannot be empty.");
    return View(model);
}

RecaptchaVerificationResult recaptchaResult = await recaptchaHelper.VerifyRecaptchaResponse();

if (recaptchaResult != RecaptchaVerificationResult.Success)
{
    ModelState.AddModelError("", "Incorrect captcha answer.");
}
```

Instead of calling the <strong>VerifyRecaptchaResponse()</strong> method, you can call the <strong>VerifyRecaptchaResponseTaskAsync()</strong> method to verify the user's response asynchronously which at the same time can be used along with the new await keyword:

```
RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();

if (String.IsNullOrEmpty(recaptchaHelper.Response))
{
    ModelState.AddModelError("", "Captcha answer cannot be empty.");
    return View(model);
}

RecaptchaVerificationResult recaptchaResult = await recaptchaHelper.VerifyRecaptchaResponseTaskAsync();

if (recaptchaResult != RecaptchaVerificationResult.Success)
{
    ModelState.AddModelError("", "Incorrect captcha answer.");
}
```

<h2 id="keyInWebConfig">How to Set reCAPTCHA Key in Web.config File</h2>

As you may have already seen, you can directly assign public and private keys to the respective properties of Recpatcha ASP.NET control or Recaptcha MVC HTML extension. However, a better way is to store these keys in your web.config file. The obvious benefit is that you can change these keys anytime you want without requiring you to modify your code and perhaps most important benefit is that you the keys you define in your web.config are global in your web project.

In the appSettings section of your web.config file, add the keys as follows:

```
<appSettings>
<add name="recaptchaPublicKey" value="Your site key" />
<add name="recaptchaPrivateKey" value="Your secret key" />
</appSettings>
```

Note: The appSettings keys are automatically added to your web.config file if you install Recaptcha for .NET through Nuget. However, you would still need to provide your own public and private keys in the web.config file of your project.

<strong>Note</strong>: The <strong>GetRecaptchaVerificationHelper()</strong> is an extension method to the MVC's built-in <strong>Controller</strong> class. This means you must import the <strong>Recaptcha.Web.Mvc</strong> namespace explicitly at the top of the controller file otherwise the code will not compile.
