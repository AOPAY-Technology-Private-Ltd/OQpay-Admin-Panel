<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
CodeBehind="ChangePassword.aspx.cs" Inherits="TheEMIClubApplication.CommonPages.ChangePassword" %>

<asp:Content ID="pgContent" ContentPlaceHolderID="mainContent" runat="Server">
    <table cellpadding="0" cellspacing="0" width="45%" class=" DefaultTableStyle" align="center" style="display: none;">
        <tr>
            <td class="SectionHeader PaddingLeft_5 ">Change Password
            </td>
        </tr>
        <tr>
            <td align="center">
                <span id="spnMessage1" runat="server"></span>
            </td>
        </tr>
        <tr id="trReLogin" runat="server" visible="false">
            <td align="center">Please <a href="../Login.aspx">Click Here</a> to Re-Login.
            </td>
        </tr>
        <tr>
            <td align="center">
                <table cellpadding="0" cellspacing="0" width="100%" id="divContainer" runat="server"
                    visible="true">
                    <tr>
                        <td></td>
                        <td class="PaddingLeft_5" style="width: 70%;"></td>
                    </tr>
                    <tr>
                        <td class="PaddingLeft_5" style="width: 30%;"><%--BoldLabel add this class the text will become bold--%>
                            Old Password<span class="Star">*</span>
                        </td>
                        <td class="PaddingLeft_5" style="width: 70%;"></td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="PaddingLeft_5" style="width: 30%;">New Password<span class="Star">*</span>
                        </td>
                        <td class="PaddingLeft_5" style="width: 70%;"></td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="PaddingLeft_5" style="width: 30%;">Confirm Password<span class="Star">*</span>
                        </td>
                        <td class="PaddingLeft_5" style="width: 70%;"></td>
                    </tr>
                    <tr>
                        <td style="height: 10px;"></td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td class="PaddingLeft_5" style="width: 70%;">

                            <%--CssClass for Button - BigButton--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 10px;"></td>
        </tr>
    </table>
    <div class="hold-transition sidebar-mini">
        <div class="wrapper">
            <!-- Content Wrapper. Contains page content -->
            <div class="main-box-container w-100 m-0" style="position: relative; left: 0px; top: 0px; padding: 25px 0px;">
                <!-- Main content -->
                <section class="content">
                    <div class="container-fluid">
                        <!-- Form -->
                        <div class="card card-default">
                            <div class="card-header form-header-bar">
                                <h3 class="card-title mb-0"><span id="spnMessage11" runat="server">Change Password</span></h3>
                            </div>
                            <!-- Card Header -->
                            <form action="#">
                                <div class="card-body ">
                                    <div class="row">
                                        <div class="col-md-12 card-body-box">
                                            <div class="row">
                                                <div class="col-md-12 text-center">
                                                    <div class="form-group">
                                                        <span id="spnMessage" runat="server"></span>
                                                        <asp:ValidationSummary ID="valSummary" runat="server" ShowSummary="true" 
                                                            DisplayMode="BulletList" EnableClientScript="true" ValidationGroup="OnSubmit" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Current Password <code>*</code></span>
                                                        <asp:TextBox ID="txtOldPassword" runat="server" MaxLength="20" placeholder="Enter Current Password"
                                                            TextMode="Password" onpaste="return BlockHTMLOnPaste(this,event);" 
                                                            CssClass="form-control w-100 fw-bold"
                                                            Style="padding: 0px 8px; word-spacing: 2px;"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="valReqOldPassword" runat="server" ControlToValidate="txtOldPassword"
                                                            SetFocusOnError="true" Display="None" ValidationGroup="OnSubmit" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">New Password <code>*</code></span>
                                                        <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="20" placeholder="Enter New Password"
                                                            TextMode="Password" onpaste="return BlockHTMLOnPaste(this,event);" CssClass="form-control w-100 fw-bold"
                                                            Style="padding: 0px 8px; word-spacing: 2px;"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="valReqNewPassword" runat="server" SetFocusOnError="true"
                                                            Display="None" ControlToValidate="txtNewPassword" ValidationGroup="OnSubmit" Width="100%" />
                                                        <asp:RegularExpressionValidator ID="valRegExpNewPassword" runat="server" SetFocusOnError="true"
                                                            Display="None" ValidationGroup="OnSubmit" ControlToValidate="txtNewPassword"
                                                            ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,20})$" Width="100%" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Confirm Password <code>*</code></span>
                                                        <asp:TextBox ID="txtConfPassword" runat="server" MaxLength="20" placeholder="Enter Confirm Password"
                                                            TextMode="Password" onpaste="return BlockHTMLOnPaste(this,event);" CssClass="form-control w-100 fw-bold"
                                                            Style="padding: 0px 8px; word-spacing: 2px;"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="valReqConfPassword" runat="server" SetFocusOnError="true"
                                                            Display="None" ControlToValidate="txtConfPassword" ValidationGroup="OnSubmit" Width="100%" />
                                                        <asp:CompareValidator ID="valComparePassword" runat="server" Display="None" ControlToValidate="txtConfPassword"
                                                            ControlToCompare="txtNewPassword" SetFocusOnError="true" ValidationGroup="OnSubmit" Width="100%" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-center mb-3">
                                                    <asp:Button ID="btnChangePswd" runat="server" Text="Change Password" CssClass="btn btn-primary"
                                                        ValidationGroup="OnSubmit" OnClick="btnChangePswd_Click" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="btnClear_Click" />
                                                </div>
                                                <div class="col-md-12 text-center" id="divReLogin" runat="server" style="display: none">
                                                    <div class="form-group">
                                                        Please <a href="../Login.aspx">Click Here</a> to Re-Login.
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scriptContent" runat="Server">
    <script type="text/javascript" language="javascript">
        function BlockHTMLOnPaste(Sender, e) {
            try {
                var txt = e.clipboardData.getData('text/plain');
                if (txt.match("<") || txt.match(">")) {
                    return false;
                }
            } catch (err) {
            }
        }
    </script>
</asp:Content>
