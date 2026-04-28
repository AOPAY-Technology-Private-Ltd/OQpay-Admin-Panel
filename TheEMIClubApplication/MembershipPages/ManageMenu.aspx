<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="ManageMenu.aspx.cs" Inherits="TheEMIClubApplication.MembershipPages.ManageMenu" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <div class="hold-transition sidebar-mini">
        <div class="wrapper">
            <!-- Content Wrapper. Contains page content -->
            <div class="main-box-container" style="width: 100%; position: relative; left: 0px; top: 0px; margin: 0px; padding: 25px 0px;">
                <!-- Main content -->
                <section class="content">
                    <div class="container-fluid p-0">
                        <!-- Form -->
                        <div class="card card-default">
                            <div class="card-header text-center p-2 bg-primary">
                                <h3 class="card-title col-md-12 text-center m-0">Manage Menu</h3>
                            </div>
                            <!-- Card Header -->
                            <form action="#">
                                <div class="card-body p-4">
                                    <div class="row">
                                        <div class="col-md-12 card-body-box">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:ScriptManager ID="smRoleDetail" runat="server"></asp:ScriptManager>
                                                        <span id="spnMessage" runat="server" />
                                                        <asp:ValidationSummary ID="valSummary" runat="server" ShowSummary="true" DisplayMode="BulletList" EnableClientScript="true"
                                                            ValidationGroup="OnSubmit" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold ">Application <code>*</code></span>
                                                        <asp:DropDownList ID="ddlApplicationName" runat="server" class="form-control" OnClientFocus="expandControl"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Menu Name</span>
                                                        <asp:TextBox ID="txtMenuName" runat="server" placeholder="Enter Menu Name" CssClass="form-control" MaxLength="30" Style="width: 100%;" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Navigate URL</span>
                                                        <asp:TextBox ID="txtNavigateURL" runat="server" placeholder="Enter Navigate URL" CssClass="form-control" MaxLength="50" Style="width: 100%;" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Menu Halt Code</span>
                                                        <asp:DropDownList ID="ddlMenuHaltCode" runat="server" class="form-control" OnClientFocus="expandControl"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Status</span>
                                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control" OnClientFocus="expandControl"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-center mb-3">
                                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Search"
                                                        OnClick="btnSearch_Click" ValidationGroup="OnSubmit" />
                                                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-danger" Text="Clear" OnClick="btnClear_Click" />
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:RequiredFieldValidator ID="valReqApplicationName" runat="server" ControlToValidate="ddlApplicationName"
                                                            SetFocusOnError="true" Display="None" InitialValue="0" ValidationGroup="OnSubmit"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:GridView ID="gvManageMenu" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                            class="GridStyle table table-bordered table-condensed table-hover"
                                                            Width="100%" OnPageIndexChanging="gvManageMenu_PageIndexChanging"
                                                            OnRowCommand="gvManageMenu_RowCommand">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="SNo." DataField="SrNo" HeaderStyle-CssClass="PaddingLeft_5" />
                                                                <asp:BoundField HeaderText="Menu Code" DataField="MenuCode" HeaderStyle-CssClass="PaddingLeft_5" />
                                                                <asp:BoundField HeaderText="Menu Name" DataField="MenuName" HeaderStyle-CssClass="PaddingLeft_5" />
                                                                <asp:BoundField HeaderText="Navigate URL" DataField="NavigateURL" HeaderStyle-CssClass="PaddingLeft_5" />
                                                                <asp:BoundField HeaderText="Menu Halt Code" DataField="MenuHaltCode" HeaderStyle-CssClass="PaddingLeft_5" />
                                                                <asp:BoundField HeaderText="Status" DataField="Active_YN" HeaderStyle-CssClass="PaddingLeft_5" />
                                                                <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="PaddingLeft_5">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkAction" runat="server" Text='<%#Eval("IsActive")%>' OnClientClick="return confirm('Are you sure?');"
                                                                            CommandName="ACT" CommandArgument='<%#Eval("MenuCode") +  "|" + Eval("IsActive")%>'>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="PaddingLeft_5">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit"
                                                                            CommandName="EDT" CommandArgument='<%#Eval("MenuCode") +  "|" + Eval("IsActive")%>'>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle CssClass="NorRow" />
                                                            <AlternatingRowStyle CssClass="AltRowTable" />
                                                            <HeaderStyle CssClass="SectionHeader" />
                                                            <PagerStyle HorizontalAlign="Center" Font-Bold="true" />
                                                        </asp:GridView>
                                                    </div>
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
            <td class="SectionHeader PaddingLeft_5" colspan="5">Manage Menu</td>
        </tr>
        <tr>
            <td height="6px" colspan="2"></td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td class="BoldLabel PaddingLeft_10">Application <span class="Star">*</span>
                            <td class="PaddingLeft_10" height="25px" colspan="3"></td>
                    </tr>
                    <tr>
                        <td class="BoldLabel PaddingLeft_10">Menu Name
                    <td class="PaddingLeft_10" height="25px"></td>
                            <td class="BoldLabel PaddingLeft_10">Navigate URL
                    <td class="PaddingLeft_10" height="25px" colspan="3"></td>
                    </tr>
                    <tr>
                        <td class="BoldLabel PaddingLeft_10">Menu Halt Code
                            <td class="PaddingLeft_10" height="25px"></td>
                            <td class="BoldLabel PaddingLeft_10">Status
                            <td class="PaddingLeft_10" height="25px"></td>
                    </tr>
                    <tr>
                        <td height="6px" colspan="4"></td>
                    </tr>
                    <tr>
                        <td colspan="4" class="PaddingLeft_5">
                            <asp:Button ID="btnSearch" runat="server" CssClass="BigButton" Text="Search"
                                OnClick="btnSearch_Click" ValidationGroup="OnSubmit" />&nbsp;
                                <asp:Button ID="btnClear" runat="server" CssClass="BigButton" Text="Clear" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="10px" colspan="2"></td>
        </tr>
        <tr>
            <td height="8px" colspan="3" align="center"></td>
        </tr>
        <tr>
            <td class="PaddingLeft_10" colspan="2"></td>
        </tr>
        <tr>
            <td height="6px" colspan="3"></td>
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