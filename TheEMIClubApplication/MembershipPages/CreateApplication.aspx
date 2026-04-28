<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
CodeBehind="CreateApplication.aspx.cs" Inherits="TheEMIClubApplication.MembershipPages.CreateApplication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <div class="hold-transition sidebar-mini">
        <div class="wrapper">
            <!-- Content Wrapper Contains page content -->
            <div class="main-box-container"
                style="width: 100%; position: relative; left: 0px; top: 0px; margin: 0px; padding: 25px 0px;">
                <!-- Main content -->
                <section class="content">
                    <div class="container-fluid p-0">
                        <!-- Form -->
                        <div class="card card-default">
                            <div class="card-header text-center p-2 bg-primary">
                                <h3 class="card-title col-md-12 text-center m-0"><span id="spnUpdateApplication"
                                    runat="server" /></h3>
                            </div>
                            <!-- Card Header -->
                            <form action="#">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-12 card-body-box">
                                            <div class="row">
                                                <div class="col-md-12 text-center">
                                                    <div class="form-group">
                                                        <div class="row SectionHeader" id="tdCreateApplication"
                                                            runat="server" style="display: none">
                                                        </div>
                                                        <span id="spnMessage" runat="server" />
                                                        <asp:ValidationSummary ID="valSummary" runat="server"
                                                            ShowSummary="true" DisplayMode="BulletList"
                                                            EnableClientScript="true" ValidationGroup="OnSubmit" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-center">
                                                    <span id="Span1" runat="server" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                                        ShowSummary="true" DisplayMode="BulletList"
                                                        EnableClientScript="true" ValidationGroup="OnSubmit" />
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Application Code
                                                                <span class="text-danger">*</span></span>
                                                        <asp:TextBox ID="txtApplicationCode" runat="server"
                                                            CssClass="form-control" MaxLength="3"
                                                            onblur="makeUppercase();" />
                                                        <asp:RequiredFieldValidator ID="valReqApplicationCode"
                                                            runat="server" ControlToValidate="txtApplicationCode"
                                                            SetFocusOnError="true" Display="None"
                                                            ValidationGroup="OnSubmit" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Application Name
                                                                <span class="text-danger">*</span></span>
                                                        <asp:TextBox ID="txtApplicationName" runat="server"
                                                            CssClass="form-control" MaxLength="50" />
                                                        <asp:RequiredFieldValidator ID="valReqApplicationName"
                                                            runat="server" ControlToValidate="txtApplicationName"
                                                            SetFocusOnError="true" Display="None"
                                                            ValidationGroup="OnSubmit" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Deploy Location</span>
                                                        <asp:TextBox ID="txtDeployLocation" runat="server"
                                                            CssClass="form-control" MaxLength="50" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Application Type
                                                                <span class="text-danger">*</span></span>
                                                        <asp:DropDownList ID="ddlApplicationType" runat="server"
                                                            CssClass="form-control" />
                                                        <asp:RequiredFieldValidator ID="valReqApplicationType"
                                                            runat="server" ControlToValidate="ddlApplicationType"
                                                            SetFocusOnError="true" InitialValue="0" Display="None"
                                                            ValidationGroup="OnSubmit"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Application URL</span>
                                                        <asp:TextBox ID="txtApplicationURL" runat="server"
                                                            CssClass="form-control" MaxLength="50" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4" style="display: none;">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Primary DB Name</span>
                                                        <asp:TextBox ID="txtPrimaryDBName" runat="server"
                                                            CssClass="form-control" MaxLength="20" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4" style="display: none;">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">DB Code</span>
                                                        <asp:TextBox ID="txtDBCode" runat="server"
                                                            CssClass="form-control" MaxLength="3" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Application
                                                                Description</span>
                                                        <asp:TextBox ID="txtApplicationDescription" runat="server"
                                                            CssClass="form-control" Height="40" TextMode="MultiLine"
                                                            MaxLength="250" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4" id="trActiveStatus"
                                                    runat="server" visible="false">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Active Status</span>
                                                        <asp:DropDownList ID="ddlActiveStatus" runat="server"
                                                            CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-center mb-3">
                                                    <asp:Button ID="btnSaveApplication" runat="server" Text="Save"
                                                        CssClass="btn btn-primary"
                                                        OnClick="btnSaveApplication_Click"
                                                        ValidationGroup="OnSubmit" />
                                                    <asp:Button ID="btnClearApplication" runat="server" Text="Clear"
                                                        CssClass="btn btn-danger"
                                                        OnClick="btnClearApplication_Click" />
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
    <script language="javascript" type="text/javascript">
        function makeUppercase() {
            var txtApplicationCode = document.getElementById("<%=txtApplicationCode.ClientID%>");
            {
                txtApplicationCode.value = txtApplicationCode.value.toUpperCase();
            }
        }
    </script>
</asp:Content>