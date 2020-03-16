<%@ Page Title="About" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.vb" Inherits="VbClient.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <p>Your app description page.</p>
    <p>Use this area to provide additional information.</p>

    <script type="text/javascript">
        new Oidc.UserManager({response_mode:"query"}).signinRedirectCallback().then(function() {
            window.location = "index.html";
        }).catch(function(e) {
            console.error(e);
        });
    </script>

</asp:Content>
