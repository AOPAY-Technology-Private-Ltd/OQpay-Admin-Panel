<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="TheEMIClubApplication.MembershipPages.CreateUser" %>

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
                                <h3 class="card-title col-md-12 text-center m-0">Create User</h3>
                            </div>
                            <!-- /.card-header -->
                            <form action="#">
                                <div class="card-body p-4">
                                    <div class="row">
                                        <div class="col-md-12 card-body-box">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:RequiredFieldValidator ID="valReqEmployee" runat="server" ControlToValidate="ddlEmployee" SetFocusOnError="true"
                                                            InitialValue="0" Display="None" ValidationGroup="OnSubmit" ErrorMessage="Select Employee"></asp:RequiredFieldValidator>
                                                        <span id="spnCreateUser" runat="server" />
                                                        <span id="spnUsernametoGetValue" runat="server" visible="false" />
                                                        <span id="spnMessage" runat="server" />
                                                        <asp:ValidationSummary ID="valSummary" runat="server" ShowSummary="true" DisplayMode="BulletList" EnableClientScript="true"
                                                            ValidationGroup="OnSubmit" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold ">Select Employee <code>*</code></span>
                                                        <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control" OnClientFocus="expandControl" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Nick Name <code>*</code></span>
                                                        <asp:TextBox ID="txtNickName" runat="server" CssClass="form-control" MaxLength="30" Style="width: 100%;" placeholder="Enter Nick Name" />
                                                        <asp:ScriptManager ID="smUserDetail" runat="server"></asp:ScriptManager>
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-center mb-3">
                                                    <asp:RegularExpressionValidator ID="valrexNickName" runat="server" ControlToValidate="txtNickName"
                                                        SetFocusOnError="true" Display="None" ValidationGroup="OnSubmit" ValidationExpression="[^\s]+"></asp:RegularExpressionValidator>
                                                    <asp:Button ID="btnCreate" runat="server" Text="Create User" CssClass="btn btn-primary" ValidationGroup="OnSubmit"
                                                        OnClick="btnCreate_Click" />
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:RequiredFieldValidator ID="valReqNickName" runat="server" ControlToValidate="txtNickName"
                                                        SetFocusOnError="true" Display="None" ValidationGroup="OnSubmit" />
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
    <div id="CreateUserSection" runat="server" style="display: none">
        <table cellpadding="0" cellspacing="0" width="100%" class="DefaultTableStyle">
            <tr>
                <td class="SectionHeader PaddingLeft_5" colspan="5">Create User</td>
            </tr>
            <tr>
                <td class="BoldLabel PaddingLeft_10">Select Employee<span class="Star">*</span>
                </td>
                <td class="BoldLabel PaddingLeft_10">Nick Name<span class="Star">*</span>
                </td>
            </tr>
            <tr>
                <td class="PaddingLeft_10"></td>
                <td class="PaddingLeft_10"></td>
                <td></td>
            </tr>
        </table>
    </div>
    <%--Sixth Section Application Assignment for--%>
    <div id="ApplicationLinkSection" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%" class="DefaultTableStyle">
            <tr class="PaddingLeft_5">
                <td class="SectionHeader" colspan="5">&nbsp;Application Assignment for- <span id="spnUser" runat="server" />
                </td>
            </tr>
            <tr>
                <td height="8px" align="center" colspan="4">
                    <span id="spnMessageAppSection" runat="server" class="BigText_Green" />
                    <span id="spnMessageError" runat="server" class="BigText_Red" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="PaddingLeft_10" style="height: 8px">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" ValidationGroup="OnSubmitApp" />&nbsp;
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                </td>
            </tr>
            <tr>
                <td class="PaddingLeft_10" style="height: 20px">
                    <asp:CheckBoxList ID="cblApplication" runat="server" RepeatLayout="Table" RepeatDirection="Vertical" RepeatColumns="4" />
                </td>
            </tr>
        </table>
    </div>
    <%--Seventh Section Role Assignment for--%>
    <div id="RoleAssignmentSection" runat="server" visible="false" class="row box-body-inner-five">
        <div class="col-xl-10 offset-xl-1">
            <div class="row default-padding-body">
                <table cellpadding="0" cellspacing="0" width="100%" class="DefaultTableStyle" style="border: 2px solid #dadada; border-radius: 4px;">
                    <tr class="PaddingLeft_5" style="text-align: center;">
                        <td class="SectionHeader" colspan="5" style="height: 20px">Role Assignment for- <span id="spnUsername" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td height="6px" colspan="3"></td>
                    </tr>
                    <tr style="text-align: center;">
                        <td height="8px" colspan="3" align="center">
                            <span id="spnMessageRoleAssign" runat="server" class="BigText_Green" /><span id="spnMessageRoleAssignError" runat="server" class="BigText_Red" />
                        </td>
                    </tr>
                    <tr>
                        <td height="6px" colspan="3"></td>
                    </tr>
                    <tr>
                        <td class="PaddingLeft_10">
                            <asp:Button ID="btnSaveRole" runat="server" Text="Save" CssClass="btn btn-primary button-box-bar-inner" OnClick="btnSaveRole_Click" />
                            <asp:Button ID="btnClearRole" runat="server" Text="Clear" CssClass="btn btn-primary button-box-bar-inner" OnClick="btnClearRole_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td height="6px" colspan="3"></td>
                    </tr>
                    <tr>
                        <td class="PaddingLeft_10">
                            <asp:DropDownList ID="ddlApplication" runat="server" CssClass="form-control" Width="220px"
                                OnSelectedIndexChanged="ddlApplication_SelectedIndexChanged" AutoPostBack="true" />
                        </td>

                    </tr>
                    <tr>
                        <td height="6px" colspan="2" class="PaddingLeft_10">
                            <span id="spnNoRolesDefined" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="PaddingLeft_10">
                            <asp:CheckBoxList ID="cblRole" runat="server" RepeatLayout="Table" RepeatDirection="Vertical" RepeatColumns="4" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td height="6px" colspan="2"></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scriptContent" runat="Server">
    <script language="javascript" type="text/javascript">
        function expandControl(sender, eventArgs) {
            //This will expand the dropdown on focus (with tab or otherwise)
            sender.showDropDown();
        }
          //    var msg = 'Do You want to send mail write now? If No, then please uncheck Active Check Box.';
    </script>
</asp:Content>