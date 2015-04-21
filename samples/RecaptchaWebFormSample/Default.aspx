﻿<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RecaptchaWebFormSample.Default" %>

<%@ Register Assembly="Recaptcha.Web" Namespace="Recaptcha.Web.UI.Controls" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recaptcha for .NET - ASP.NET Web Forms Sample</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <table>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Username:</td>
            <td><asp:TextBox ID="txtUsername" runat="server" /></td>
        </tr>
        <tr>
            <td>Password:</td>
            <td><asp:TextBox ID="txtPassword" TextMode="Password" runat="server" /></td>
        </tr>
        <tr>
            <td>First Name:</td>
            <td><asp:TextBox ID="txtFirstName" runat="server" /></td>
        </tr>
        <tr>
            <td>Last Name:</td>
            <td><asp:TextBox ID="txtLastName" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="2">
                <!-- Render reCAPTCHA widget with the default look and feel. -->
                <cc1:Recaptcha ID="Recaptcha1" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table> 
    </div>
    </form>
</body>
</html>
