<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
CodeBehind="CreateMenu.aspx.cs" Inherits="TheEMIClubApplication.MembershipPages.CreateMenu" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <div class="hold-transition sidebar-mini" id="dvCreateRoleSection" runat="server">
        <div class="wrapper">
            <!-- Content Wrapper. Contains page content -->
            <div class="main-box-container"
                style="width: 100%; position: relative; left: 0px; top: 0px; margin: 0px; padding: 25px 0px;">
                <!-- Main content -->
                <section class="content">
                    <div class="container-fluid p-0">
                        <!-- Form -->
                        <div class="card card-default">
                            <div class="card-header text-center p-2 bg-primary" id="tdCreateMenu"
                                runat="server">
                                <h3 class="card-title col-md-12 text-center m-0"><span id="spnUpdateMenu"
                                    runat="server" /></h3>
                            </div>
                            <!-- Card Header -->
                            <form action="#">
                                <div class="card-body p-4">
                                    <div class="row">
                                        <div class="col-md-12 card-body-box">
                                            <div class="row">
                                                <div class="col-md-12 tect-center">
                                                    <div class="form-group">
                                                        <asp:ValidationSummary ID="valSummary" runat="server"
                                                            ShowSummary="true" DisplayMode="BulletList"
                                                            EnableClientScript="true"
                                                            ValidationGroup="OnSubmit" />
                                                        <span id="spnMessage" runat="server" />
                                                        <asp:ScriptManager ID="smRoleDetail" runat="server">
                                                        </asp:ScriptManager>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Application Name
                                                                    <code>*</code></span>
                                                        <asp:DropDownList ID="ddlApplicationName" runat="server"
                                                            class="form-control"
                                                            OnSelectedIndexChanged="ddlApplicationName_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="valReqApplicationName"
                                                            runat="server"
                                                            ControlToValidate="ddlApplicationName"
                                                            SetFocusOnError="true" Display="None"
                                                            InitialValue="0" ValidationGroup="OnSubmit">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Menu Name
                                                                    <code>*</code></span>
                                                        <asp:TextBox ID="txtMenuName" runat="server"
                                                            CssClass="form-control" MaxLength="50"
                                                            Style="width: 100%;"
                                                            placeholder="Enter Menu Name" />
                                                        <asp:RequiredFieldValidator ID="valReqMenuName"
                                                            runat="server" ControlToValidate="txtMenuName"
                                                            SetFocusOnError="true" Display="None"
                                                            ValidationGroup="OnSubmit" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Menu Description</span>
                                                        <asp:TextBox ID="txtMenuDescription" runat="server"
                                                            CssClass="form-control" MaxLength="50"
                                                            placeholder="Enter Menu Description"
                                                            Style="width: 100%;" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Tool Tip</span>
                                                        <asp:TextBox ID="txtToolTip" runat="server"
                                                            CssClass="form-control" MaxLength="50"
                                                            placeholder="Enter Tool Tip" Style="width: 100%;" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">URL</span>
                                                        <asp:TextBox ID="txtNavigateURL" runat="server"
                                                            CssClass="form-control" MaxLength="100"
                                                            placeholder="Enter URL" Style="width: 100%;" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Parent Menu</span>
                                                        <asp:DropDownList ID="ddlParentMenu" runat="server"
                                                            class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Display Order</span>
                                                        <asp:TextBox ID="txtDisplayOrder" runat="server"
                                                            CssClass="form-control" MaxLength="5"
                                                            Style="width: 100%;"
                                                            placeholder="Enter Display Order" />
                                                        <b><span id="spnDisplayOrder" runat="server" /></b>
                                                        <asp:RegularExpressionValidator ID="valRegDisplayOrder"
                                                            runat="server" ControlToValidate="txtDisplayOrder"
                                                            ValidationExpression="^\d+$" SetFocusOnError="true"
                                                            Display="None" ValidationGroup="OnSubmit">
                                                        </asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                             <%--   <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Subsciption
                                                                    Feature</span>
                                                        <asp:DropDownList ID="ddlFeature" runat="server"
                                                            class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Menu Halt Code</span>
                                                        <asp:DropDownList ID="ddlMenuHaltCode" runat="server"
                                                            class="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>--%>
                                                <div class="col-md-4" id="trActiveStatus" runat="server"
                                                    visible="false">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Active Status</span>
                                                        <asp:DropDownList ID="ddlActiveStatus" runat="server"
                                                            CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-center mb-3">
                                                    <asp:Button ID="Button1" runat="server" Text="Save"
                                                        CssClass="btn btn-primary" OnClick="btnSaveMenu_Click"
                                                        ValidationGroup="OnSubmit" />
                                                    <asp:Button ID="Button2" runat="server" Text="Clear"
                                                        CssClass="btn btn-danger"
                                                        OnClick="btnClearMenu_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </div>
    <table cellpadding="0" cellspacing="0" width="100%" class="DefaultTableStyle" style="display: none">
        <tr>
            <td class="SectionHeader PaddingLeft_5" colspan="5"></td>
        </tr>
        <tr>
            <td height="6px" colspan="3"></td>
        </tr>
        <tr>
            <td></td>
            <td class="PaddingLeft_10" style="width: 70%;" colspan="2"></td>
        </tr>
        <tr>
            <td height="8px" colspan="3" align="center"></td>
        </tr>
        <tr>
            <td class="BoldLabel PaddingLeft_10">Application Name<span class="Star">*</span>
            </td>
            <td class="PaddingLeft_10" height="25px"></td>
        </tr>
        <tr>
            <td class="BoldLabel PaddingLeft_10">Menu Name<span class="Star">*</span>
            </td>
            <td class="PaddingLeft_10" height="25px"></td>
        </tr>
        <tr>
            <td class="BoldLabel PaddingLeft_10">Menu Description
            </td>
            <td class="PaddingLeft_10" height="25px"></td>
        </tr>
        <tr>
            <td class="BoldLabel PaddingLeft_10">ToolTip
            </td>
            <td class="PaddingLeft_10" height="25px"></td>
        </tr>
        <tr>
            <td class="BoldLabel PaddingLeft_10">URL
            </td>
            <td class="PaddingLeft_10" height="25px"></td>
        </tr>
        <tr>
            <td class="BoldLabel PaddingLeft_10" style="height: 25px">Parent Menu
            </td>
            <td class="PaddingLeft_10" style="height: 25px"></td>
        </tr>
        <tr>
            <td class="BoldLabel PaddingLeft_10">Display Order
            </td>
            <td class="PaddingLeft_10" height="25px"></td>
        </tr>
        <tr>
            <td class="BoldLabel PaddingLeft_10" style="height: 25px">Menu Halt Code
            </td>
            <td class="PaddingLeft_10" style="height: 25px"></td>
        </tr>
        <tr>
            <td class="BoldLabel PaddingLeft_10" style="height: 25px">Active Status
            </td>
            <td class="PaddingLeft_10" style="height: 25px"></td>
        </tr>
        <tr>
            <td height="6px" colspan="2"></td>
        </tr>
        <tr>
            <td></td>
            <td class="PaddingLeft_10">
                <asp:Button ID="btnSaveMenu" runat="server" Text="Save" CssClass="btn btn-primary"
                    OnClick="btnSaveMenu_Click" ValidationGroup="OnSubmit" />&nbsp;
                        <asp:Button ID="btnClearMenu" runat="server" Text="Clear" CssClass="btn btn-primary"
                            OnClick="btnClearMenu_Click" />
            </td>
        </tr>
        <tr>
            <td height="10px" colspan="2"></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scriptContent" runat="Server">
    <script language="javascript" type="text/javascript">
        function expandControl(sender, eventArgs) {
            //This will expand the dropdown on focus (with tab or otherwise)
            sender.showDropDown();
        }
    </script>
</asp:Content>