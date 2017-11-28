<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Autoescuelas.Web.Login" %>
<%@ Import Namespace="Autoescuelas.Web.App_Class" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="Ivan Cruz">

    <title>Autoescuelas</title>

    <!-- Bootstrap Core CSS -->
    <link href="~/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">

    <!-- MetisMenu CSS -->
    <link href="~/vendor/metisMenu/metisMenu.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="~/sb-admin/css/sb-admin-2.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="~/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <link rel="shortcut icon" type="image/x-icon" href="<%= ResolveClientUrl("~/images/favicon.ico") %>" />
    
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4">
                <div class="login-panel panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <asp:Label ID="lblTitulo" runat="server" Text="Inicio de Sesion"></asp:Label>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <form id="frmLogin" runat="server" enctype="multipart/form-data" method="post" role="form">
                            <fieldset>
                                <div class="form-group" align="center">
                                    <img src="images/<%= (CParametrosWeb.StrNombreInstitucion == "" ? "" : CParametrosWeb.StrNombreInstitucion + "-") %>splash-single.png" 
                                        class="img-responsive col-xs-12" alt="Responsive image">
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtUsuario" runat="server"
                                        onkeypress="return textUtil.allowChars(this,event,true)"
                                        validtipo="letras" oncopy="return false" onpaste="return false"
                                        CssClass="form-control" PlaceHolder="Usuario"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox ID="txtPass" runat="server"
                                        CssClass="form-control" TextMode="Password"
                                        onkeypress="return textUtil.allowChars(this,event,true)"
                                        validtipo="password" oncopy="return false" onpaste="return false"
                                        PlaceHolder="Contrase&ntilde;a"></asp:TextBox>
                                </div>
                                <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger alert-dismissable" Visible="False">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                                </asp:Panel>
                                <!-- Change this to a button or input when using this as a form -->

                                <asp:Button ID="btnLogin" runat="server" Text="Ingresar" CssClass="btn btn-lg btn-success btn-block" Class="btn btn-lg btn-success btn-block" OnClick="btnLogin_Click" />
                                <hr/>
                                <asp:HyperLink ID="btnConsulta" runat="server" CssClass="btn btn-lg btn-success btn-block" NavigateUrl="Consulta.aspx">Consulta ciudadana</asp:HyperLink>
                                <asp:Label ID="lblVersion" runat="server" ForeColor="Blue" Text="Label" Font-Size="XX-Small"></asp:Label>
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <!-- jQuery -->
    <script src="~/vendor/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="~/vendor/bootstrap/js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="~/vendor/metisMenu/metisMenu.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="~/sb-admin/js/sb-admin-2.js"></script>
    
</body>
</html>