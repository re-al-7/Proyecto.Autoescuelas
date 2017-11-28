<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="abmUsuarios.aspx.cs" Inherits="Autoescuelas.Web.AUT.AbmUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">
    <link href="<%= ResolveClientUrl("~/vendor/font-awesome/css/font-awesome.min.css") %>" rel="stylesheet">
    <link href="<%= ResolveClientUrl("~/vendor/morrisjs/morris.css") %>" rel="stylesheet">
    <link href="<%= ResolveClientUrl("~/vendor/bootstrap-table/bootstrap-table.min.css") %>" rel="stylesheet">
    <link href="<%= ResolveClientUrl("~/vendor/bootstrap-select.min.css") %>" rel="stylesheet">
    <link href="<%= ResolveClientUrl("~/vendor/bootstrap-datetimepicker.css") %>" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header">Usuarios</h1>
    </div>
    <!-- /.col-lg-12 -->
</div>
<div class="row">
    <div class="col-lg-12">
        <%--  --%>
        <div class="panel panel-primary">
            <div class="panel-heading">
                &nbsp;
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-group">
                            <!-- Botones de Accion -->
                            <asp:LinkButton ID="btnAtras" runat="server" Text="<i class='fa fa-arrow-left'></i>" ToolTip="Atras"
                                            CssClass="btn btn-primary btn-circle btn-lg" class="btn btn-default btn-circle" OnClick="btnAtras_Click"/>
                            <asp:LinkButton ID="btnNuevo" runat="server" Text="<i class='fa fa-file-o'></i>"
                                            ToolTip="Nuevo registro" 
                                            CssClass="btn btn-primary btn-circle btn-lg" class="btn btn-default btn-circle" OnClick="btnNuevo_Click"/>
                            <asp:LinkButton ID="btnImprimir" runat="server" Text="<i class='fa fa-print'></i>"
                                            ToolTip="Imprimir registros" 
                                            CssClass="btn btn-primary btn-circle btn-lg" class="btn btn-default btn-circle" OnClick="btnImprimir_Click"/>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-6 col-md-6">
                        <div class="form-group">
                            <label id="lblid_autoescuela" runat="server">Autoescuela:</label>
                            <asp:DropDownList ID="ddlid_autoescuela" runat="server"
                                              class="form-control selectpicker" data-live-search="true"
                                              data-select-on-tab="true" data-size="7" AutoPostBack="True"
                                              OnSelectedIndexChanged="ddlid_autoescuela_OnSelectedIndexChanged"
                                              CssClass="form-control selectpicker" >
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6">
                        <div class="form-group">
                            <label id="Label10" runat="server">Sucursal:</label>
                            <asp:DropDownList ID="ddlid_sucursal" runat="server"
                                              class="form-control selectpicker" data-live-search="true"
                                              data-select-on-tab="true" data-size="7"
                                              CssClass="form-control selectpicker" AutoPostBack="True"
                                              OnSelectedIndexChanged="ddlid_sucursal_OnSelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="dataTable_wrapper">
                    <asp:GridView ID="dtgListado" runat="server" AutoGenerateColumns="False"
                                  data-toggle="table" data-show-columns="true" data-pagination="true"
                                  data-search="true" data-show-toggle="true" data-sortable="true"
                                  data-page-size="25" data-pagination-v-align="both" data-show-export="true"
                                  DataKeyNames="" OnRowCommand="dtgListado_RowCommand"
                                  OnRowDataBound="dtgListado_OnRowDataBound"
                                  CssClass="table table-striped table-bordered table-hover">
                        <Columns>
                            <asp:BoundField ReadOnly="True" DataField="login" HeaderText="Usuario" ShowHeader="false">
                                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField ReadOnly="True" DataField="nombres" HeaderText="Nombres" ShowHeader="false">
                                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField ReadOnly="True" DataField="apellidos" HeaderText="Apellidos" ShowHeader="false">
                                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Acciones">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                <HeaderStyle></HeaderStyle>
                                <ItemTemplate>                                   

                                    <asp:LinkButton ID="ImbModificar" CommandName="modificar" CommandArgument='<%# Bind("id_usuario") %>' runat="server"
                                                    ToolTip="Modificar el registro" 
                                                    CssClass="btn btn-primary btn-circle" Text="<i class='fa fa-edit'></i>"/>

                                    <asp:LinkButton ID="ImbEliminar" CommandName="eliminar" CommandArgument='<%# Bind("id_usuario") %>' runat="server"
                                                    ToolTip="Eliminar el registro" OnClientClick="return confirm('¿Esta seguro que desea eliminar el registro?');"
                                                     CssClass="btn btn-danger btn-circle" Text="<i class='fa fa-eraser'></i>"/>

                                    <button type="button" class="btn btn-warning btn-circle" data-container="body"
                                            data-toggle="popover" data-trigger="click"  ToolTip="Auditoria del registro"
                                            data-placement="left" data-html="true" title="Auditoria"
                                            data-content='<b>Usuario Creacion:</b> <%# Eval("usucre") %> <br />
															<b>Fecha Creacion:</b> <%# Eval("feccre") %> <br />
															<b>Usuario Modificacion:</b> <%# Eval("usumod") %> <br />
															<b>Fecha Modificacion:</b> <%# Eval("fecmod") %> <br />
															<b>Estado:</b> <%# Eval("apiestado") %> '>
                                        <i class='fa fa-search-plus'></i>
                                    </button>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="hdnIdDatos" runat="server"/>

<!-- Detail Modal -->
<div class="modal fade" id="currentdetail" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="modal-dialog">

        <asp:UpdatePanel ID="updModaleDetail" runat="server">
            <ContentTemplate>
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="myModalLabel">Detalle del registro</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="updVerDetalle" runat="server">
                            <ContentTemplate>
                                <asp:DetailsView ID="dtgDetalles" runat="server"
                                                 CssClass="table table-striped table-bordered table-hover"
                                                 FieldHeaderStyle-Font-Bold="true" AutoGenerateRows="True">
                                    <Fields>
                                    </Fields>
                                </asp:DetailsView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dtgListado" EventName="RowCommand"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!-- /.modal-dialog -->
</div>
<!-- /.modal -->

<!-- New Modal Starts here -->
<div id="newModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="editModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
        <asp:UpdatePanel ID="updModaleNew" runat="server">
            <ContentTemplate>
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="H2">Nuevo usuario</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label id="Label8" runat="server">Login:</label>
                            <asp:TextBox id="txtNew_Login" runat="server"
                                         data-parsley-minlength="5"
                                         data-parsley-maxlength="15"
                                         required="" MaxLength="15"
                                         data-parsley-group="validation-new"
                                         validtipo="letras" onkeypress="return textUtil.allowChars(this,event,true)"
                                         oncopy="return false" onpaste="return false"
                                         Class="form-control" CssClass="form-control">
                            </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label id="Label1" runat="server">Password:</label>
                            <asp:TextBox id="txtNew_password" runat="server"
                                         data-parsley-minlength="5"
                                         data-parsley-maxlength="15"
                                         required="" MaxLength="15"
                                         data-parsley-group="validation-new"
                                         validtipo="letras" onkeypress="return textUtil.allowChars(this,event,true)"
                                         oncopy="return false" onpaste="return false"
                                         Class="form-control" CssClass="form-control">
                            </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label id="Label5" runat="server">Nombres:</label>
                            <asp:TextBox id="txtNew_nombres" runat="server"
                                         data-parsley-minlength="3"
                                         data-parsley-maxlength="15"
                                         required="" MaxLength="15"
                                         data-parsley-group="validation-new"
                                         validtipo="letras" onkeypress="return textUtil.allowChars(this,event,true)"
                                         oncopy="return false" onpaste="return false"
                                         Class="form-control" CssClass="form-control">
                            </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label id="Label6" runat="server">Apellidos:</label>
                            <asp:TextBox id="txtNew_apellidos" runat="server"
                                         data-parsley-minlength="3"
                                         data-parsley-maxlength="15"
                                         required="" MaxLength="15"
                                         data-parsley-group="validation-new"
                                         validtipo="letras" onkeypress="return textUtil.allowChars(this,event,true)"
                                         oncopy="return false" onpaste="return false"
                                         Class="form-control" CssClass="form-control">
                            </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label id="Label9" runat="server">Es administrador?:</label>
                            <div class="checkbox checkbox-slider--b">
                                <label>
                                    <input ID="chkNew_Administrador" runat="server" type="checkbox"/><span>Usuario Administrador</span>
                                </label>
                            </div>
                        </div>                       
                    </div>
                    <div class="modal-footer">
                        <asp:Label ID="Label" Visible="false" runat="server"></asp:Label>
                        <asp:Button ID="btnNewGuardar" runat="server" Text="Guardar"
                                    OnClientClick="return $('#aspnetForm').parsley().validate('validation-new');"
                                    CssClass="btn btn-info" OnClick="btnNewGuardar_Click"/>
                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="Click"/>
                <asp:AsyncPostBackTrigger ControlID="btnNewGuardar" EventName="Click"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>
<!-- New Modal Ends here -->

<!-- Edit Modal Starts here -->
<div id="editModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="editModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <asp:UpdatePanel ID="updModaleEdit" runat="server">
            <ContentTemplate>
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close"
                                data-dismiss="modal" aria-hidden="true">
                            ×
                        </button>
                        <h4 class="modal-title" id="H1">Modo edici&oacute;n</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label id="Label3" runat="server">Usuario:</label>
                            <asp:TextBox id="txtEdit_Id" Class="form-control"
                                         runat="server" ReadOnly="true">
                            </asp:TextBox>
                        </div>
                        
                        <div class="form-group">
                            <label id="Label2" runat="server">Nombres:</label>
                            <asp:TextBox id="txtEdit_nombres" runat="server"
                                         data-parsley-minlength="3"
                                         data-parsley-maxlength="15"
                                         required="" MaxLength="15"
                                         data-parsley-group="validation-edit"
                                         validtipo="letras" onkeypress="return textUtil.allowChars(this,event,true)"
                                         oncopy="return false" onpaste="return false"
                                         Class="form-control" CssClass="form-control">
                            </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label id="Label4" runat="server">Apellidos:</label>
                            <asp:TextBox id="txtEdit_apellidos" runat="server"
                                         data-parsley-minlength="3"
                                         data-parsley-maxlength="15"
                                         required="" MaxLength="15"
                                         data-parsley-group="validation-edit"
                                         validtipo="letras" onkeypress="return textUtil.allowChars(this,event,true)"
                                         oncopy="return false" onpaste="return false"
                                         Class="form-control" CssClass="form-control">
                            </asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label id="Label7" runat="server">Es administrador?:</label>
                            <div class="checkbox checkbox-slider--b">
                                <label>
                                    <input ID="chkEdit_Administrador" runat="server" type="checkbox"/><span>Usuario Administrador</span>
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                        <asp:Button ID="btnEditaGuardar" runat="server" Text="Guardar Cambios"
                                    OnClientClick="return $('#aspnetForm').parsley().validate('validation-edit');"
                                    CssClass="btn btn-info" OnClick="btnEditaGuardar_Click"/>
                        <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dtgListado" EventName="RowCommand"/>
                <asp:AsyncPostBackTrigger ControlID="btnEditaGuardar" EventName="Click"/>
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>
<!-- Edit Modal Ends here -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
    <script src="<%= ResolveClientUrl("~/vendor/bootstrap-table/bootstrap-table.min.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/vendor/bootstrap-table/extensions/export/bootstrap-table-export.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/vendor/bootstrap-table/locale/bootstrap-table-es-MX.min.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/vendor/bootstrap-select.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveClientUrl("~/vendor/moment.min.js") %>"></script>
    <script type="text/javascript" src="<%= ResolveClientUrl("~/vendor/bootstrap-datetimepicker.js") %>"></script>

    <script>
        $(document).ready(function() {
            $('[data-toggle="popover"]').popover();
            $('#datetimepicker1').datetimepicker();
            $('#datetimepicker2').datetimepicker();
        });

        function pageLoad() {
            $(function() {
                $('select').selectpicker();
                $('[data-toggle="popover"]').popover();
                $('#datetimepicker1').datetimepicker();
                $('#datetimepicker2').datetimepicker();
            });
        }

        $(document).on('show.bs.popover', function (e) {
            if ($(e.target).data('trigger') == 'click') {
                var timeoutDataName = 'shownBsTooltipTimeout';
                if ($(e.target).data(timeoutDataName) != null) {
                    clearTimeout($(e.target).data(timeoutDataName));
                }
                var timeout = setTimeout(function () {
                    $(e.target).click();
                }, 3000);
                $(e.target).data(timeoutDataName, timeout);
            }
        });

        $(document).on('hide.bs.popover', function (e) {
            if ($(e.target).data('trigger') == 'click') {
                var timeoutDataName = 'shownBsTooltipTimeout';
                if ($(e.target).data(timeoutDataName) != null) {
                    clearTimeout($(e.target).data(timeoutDataName));
                }
            }
        });
    </script>
</asp:Content>