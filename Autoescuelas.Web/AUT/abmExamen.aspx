<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="abmExamen.aspx.cs" Inherits="Autoescuelas.Web.AUT.abmExamen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="StyleSection" runat="server">
    <link href="<%= ResolveClientUrl("~/vendor/font-awesome/css/font-awesome.min.css") %>" rel="stylesheet">
    <link href="<%= ResolveClientUrl("~/vendor/morrisjs/morris.css") %>" rel="stylesheet">
    <link href="<%= ResolveClientUrl("~/vendor/bootstrap-table/bootstrap-table.min.css") %>" rel="stylesheet">
    <link href="<%= ResolveClientUrl("~/vendor/bootstrap-select.min.css") %>" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentSection" runat="server">
<div class="row">
    <div class="col-lg-12">
        <h1 class="page-header">Examenes</h1>
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
                            <label id="Label1" runat="server">Sucursal:</label>
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
                            <asp:BoundField ReadOnly="True" DataField="tipo" HeaderText="Tipo" ShowHeader="false">
                                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField ReadOnly="True" DataField="categoria" HeaderText="Cat." ShowHeader="false">
                                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField ReadOnly="True" DataField="instructor" HeaderText="Instructor" ShowHeader="false">
                                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField ReadOnly="True" DataField="placa" HeaderText="Vehiculo" ShowHeader="false">
                                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField ReadOnly="True" DataField="ci" HeaderText="Postulante" ShowHeader="false">
                                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField ReadOnly="True" DataField="postulante" HeaderText="Nombre postulante" ShowHeader="false">
                                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField ReadOnly="True" DataField="nota" HeaderText="Nota" ShowHeader="false">
                                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField ReadOnly="True" DataField="feccre" HeaderText="Fecha" ShowHeader="false">
                                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Acciones">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                <HeaderStyle></HeaderStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="ImbEliminar" CommandName="eliminar" CommandArgument='<%# Bind("id_examen") %>' runat="server"
                                                    ToolTip="Eliminar la asignacion" OnClientClick="return confirm('¿Esta seguro que desea eliminar el registro?');"
                                                     CssClass="btn btn-danger btn-circle" Text="<i class='fa fa-eraser'></i>"/>
                                    
                                    <button type="button" class="btn btn-warning btn-circle" data-container="body"
                                            data-toggle="popover" data-trigger="click" 
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
    <div class="modal-dialog">
        <asp:UpdatePanel ID="updModaleNew" runat="server">
            <ContentTemplate>
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close"
                                data-dismiss="modal" aria-hidden="true">
                            ×
                        </button>
                        <h4 class="modal-title" id="H2">Nuevo registro</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label id="Label6" runat="server">Postulante:</label>
                            <asp:DropDownList ID="ddlNew_id_postulante" runat="server"
                                              class="form-control selectpicker" data-live-search="true"
                                              data-none-selected-text="No existen datos" 
                                              data-select-on-tab="true" data-size="7" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label id="Label3" runat="server">Evaluador:</label>
                            <asp:DropDownList ID="ddlNew_id_usuario" runat="server"
                                              class="form-control selectpicker" data-live-search="true"
                                              data-none-selected-text="No existen datos" 
                                              data-select-on-tab="true" data-size="7" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label id="Label2" runat="server">Categoria Licencia:</label>
                            <asp:DropDownList ID="ddlNew_id_tipolicencia" runat="server"
                                              class="form-control selectpicker" data-live-search="true"
                                              data-none-selected-text="No existen datos" 
                                              data-select-on-tab="true" data-size="7" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label id="Label4" runat="server">Tipo Evaluación:</label>
                            <asp:DropDownList ID="ddlNew_id_tipoexamen" runat="server"
                                              class="form-control selectpicker" data-live-search="true"
                                              data-none-selected-text="No existen datos" 
                                              data-select-on-tab="true" data-size="7" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label id="lblNew_id_usuario" runat="server">Vehículo:</label>
                            <asp:DropDownList ID="ddlNew_id_vehiculo" runat="server"
                                              class="form-control selectpicker" data-live-search="true"
                                              data-none-selected-text="No existen datos" 
                                              data-select-on-tab="true" data-size="7" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label id="Label5" runat="server">Nota:</label>
                            <asp:TextBox id="txtNew_nota_exa_prac" required=""
                                         data-parsley-maxlength="3"
                                         data-parsley-minlength="1"
                                         onkeypress="return textUtil.allowChars(this,event,true)" validtipo="numeros"
                                         data-parsley-group="validation-new"
                                         Class="form-control" CssClass="form-control"
                                         runat="server" MaxLength="15">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Label ID="Label" Visible="false" runat="server"></asp:Label>
                        <asp:Button ID="btnNewGuardar" runat="server"
                                    Text="Guardar" CssClass="btn btn-info"
                                    OnClientClick="return $('#aspnetForm').parsley().validate('validation-new');"
                                    OnClick="btnNewGuardar_Click"/>
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptSection" runat="server">
    <script src="<%= ResolveClientUrl("~/vendor/bootstrap-table/bootstrap-table.min.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/vendor/bootstrap-table/extensions/export/bootstrap-table-export.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/vendor/bootstrap-table/locale/bootstrap-table-es-MX.min.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/vendor/bootstrap-select.min.js") %>"></script>

    <script>
        $(document).ready(function() {
            $('[data-toggle="popover"]').popover();
        });

        function pageLoad() {
            $(function() {
                $('select').selectpicker();
                $('[data-toggle="popover"]').popover();
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