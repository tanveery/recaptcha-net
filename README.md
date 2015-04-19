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

'''
PM> Install-Package RecaptchaNet
'''

If the Package Manager Console is not visible in your Microsoft Visual Studio IDE, click on the <strong>Tools > Library Package Manager > Package Manager Console</strong> menu.

<strong>Note</strong>: Nuget is a Visual Studio extension and it needs to be installed before you can use Nuget packages in your Visual Studio projects. You can download and install <a href="https://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c">Nuget</a> from here.
