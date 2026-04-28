<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true"
    CodeBehind="ManageUser.aspx.cs" Inherits="TheEMIClubApplication.MembershipPages.ManageUser" %>

<%@ Register Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" Namespace="RJS.Web.WebControl" TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <div class="hold-transition sidebar-mini">
        <div class="wrapper">
            <!-- Content Wrapper. Contains page content -->
            <div class="main-box-container"
                style="width: 100%; position: relative; left: 0px; top: 0px; margin: 0px; padding: 25px 0px;">
                <!-- Main content -->
                <section class="content">
                    <div class="container-fluid p-0">
                        <!-- Form -->
                        <div class="card card-default">
                            <div class="card-header text-center p-2 bg-primary">
                                <h3 class="card-title col-md-12 text-center m-0">Manage User</h3>
                            </div>
                            <!-- Card-header -->
                            <form action="#">
                                <div class="card-body p-4" id="dvUserDetail" runat="server">
                                    <div class="row">
                                        <div class="col-md-12 card-body-box">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <span id="spnMessage" runat="server"
                                                            class="row box-body-inner-one" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold ">From Date</span>
                                                        <asp:TextBox ID="txtFromDate" runat="server"
                                                            TextMode="Date" CssClass="form-control"
                                                            AutoCompleteType="Disabled" Style="width: 100%;" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">To Date</span>
                                                        <asp:TextBox ID="txtToDate" runat="server"
                                                            TextMode="Date" CssClass="form-control"
                                                            AutoCompleteType="Disabled" Style="width: 100%;">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Search User</span>
                                                        <asp:TextBox ID="txtUserName" runat="server"
                                                            CssClass="form-control" AutoCompleteType="Disabled"
                                                            Style="width: 100%;"
                                                            placeholder="Enter User Code or Name" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <span id="spnchange" runat="server"></span>
                                                        <span id="spnMsg" runat="server"></span>
                                                        <span id="spnerror" runat="server"></span>
                                                        <span id="spnResetPassword" runat="server"
                                                            visible="false"></span>
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-center mb-3">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search"
                                                        CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear"
                                                        CssClass="btn btn-danger" OnClick="btnClear_Click" />
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group table-responsive">
                                                        <asp:GridView ID="gvManageUser" runat="server" CssClass="table table-bordered table-hover table-striped"
                                                            AutoGenerateColumns="False" AllowPaging="True"
                                                            OnPageIndexChanging="gvManageUser_PageIndexChanging"
                                                            DataKeyNames="UserName"
                                                            OnRowCommand="gvManageUser_RowCommand" Width="100%"
                                                           PagerSettings-Mode="Numeric"
           PagerSettings-Position="Bottom"
           PagerStyle-HorizontalAlign="Center"
           PagerStyle-CssClass="pagination justify-content-center"
           UseAccessibleHeader="true"
           GridLines="None">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="S.No."
                                                                    DataField="SrNo"
                                                                    HeaderStyle-CssClass="PaddingLeft_5" />
                                                                <asp:BoundField HeaderText="User Code"
                                                                    DataField="UserName"
                                                                    HeaderStyle-CssClass="PaddingLeft_5" />
                                                                <asp:BoundField HeaderText="User Name"
                                                                    DataField="NickName"
                                                                    HeaderStyle-CssClass="PaddingLeft_5" />
                                                                <asp:BoundField HeaderText="User Full Name"
                                                                    DataField="Name"
                                                                    HeaderStyle-CssClass="PaddingLeft_5"
                                                                    Visible="false" />
                                                                <asp:BoundField HeaderText="Creation Date"
                                                                    DataField="CreateDate"
                                                                    HeaderStyle-CssClass="PaddingLeft_5" />
                                                                <asp:TemplateField HeaderText="Action"
                                                                    HeaderStyle-CssClass="PaddingLeft_5">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkAction"
                                                                            runat="server"
                                                                            Text='<%#Eval("Active_YN")%>'
                                                                            OnClientClick="return confirm('Are you sure?');"
                                                                            CommandName="ACT"
                                                                            CommandArgument='<%#Eval("UserName") + "|" + Eval("Name") + "|" + Eval("Active_YN")%>'>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Applications"
                                                                    HeaderStyle-CssClass="PaddingLeft_5">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton
                                                                            ID="lnkManageApplication"
                                                                            runat="server"
                                                                            Text="Manage Application"
                                                                            CommandName="APP"
                                                                            CommandArgument='<%#Eval("UserName") + "|" + Eval("Name") + "|" + Eval("Active_YN")%>'>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Roles"
                                                                    HeaderStyle-CssClass="PaddingLeft_5">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkManageRole"
                                                                            runat="server" Text="Manage Role"
                                                                            CommandName="ROL"
                                                                            CommandArgument='<%#Eval("UserName") + "|" + Eval("Name") + "|" + Eval("Active_YN")%>'>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                    <%--            <asp:TemplateField HeaderText="Reports"
                                                                    HeaderStyle-CssClass="PaddingLeft_5">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkManageReport"
                                                                            runat="server" Text="Manage Report"
                                                                            CommandName="REPO"
                                                                            CommandArgument='<%#Eval("UserName") + "|" + Eval("Name") + "|" + Eval("Active_YN")%>'>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Reset Password"
                                                                    HeaderStyle-CssClass="PaddingLeft_5">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkResetPassword"
                                                                            runat="server" Text="Reset Password"
                                                                            OnClientClick="return confirm('Are you sure to Reset password for this user?');"
                                                                            CommandName="RSP"
                                                                            CommandArgument='<%#Eval("UserName") + "|" + Eval("Name") + "|" + Eval("Active_YN")%>'>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--                      <asp:TemplateField HeaderText="Edit"
                                                                    HeaderStyle-CssClass="PaddingLeft_5">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEdit"
                                                                            runat="server" Text="Edit"
                                                                            CommandName="EDT"
                                                                            CommandArgument='<%#Eval("UserName") + "|" + Eval("Name") + "|" + Eval("Active_YN")%>' Enabled="false">
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                            </Columns>
                                         <%--                   <RowStyle CssClass="NorRow" />
                                                            <AlternatingRowStyle CssClass="AltRowTable" />
                                                            <HeaderStyle CssClass="SectionHeader" />
                                                            <PagerStyle HorizontalAlign="Center"
                                                                Font-Bold="true" />--%>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container-fluid p-0" id="dvManageApplication" runat="server"
                                        visible="false">
                                        <!-- Form -->
                                        <div class="card card-default">
                                            <div class="card-header text-center p-2 bg-primary">
                                                <h3 class="card-title col-md-12 text-center m-0">Manage
                                                            Application for User: <span id="spnFullNameApplication"
                                                                runat="server" />
                                                    <span id="spnUsername" runat="server" />
                                                </h3>
                                            </div>
                                            <!-- Card Header -->
                                            <form action="#">
                                                <div class="card-body p-4">
                                                    <div class="row">
                                                        <div class="col-md-12 card-body-box">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <asp:GridView ID="gvShowSelectedApp"
                                                                            runat="server"
                                                                            AutoGenerateColumns="False"
                                                                            OnRowCreated="gvShowSelectedApp_RowCreated"
                                                                            Width="100%"
                                                                            DataKeyNames="AppAccessible_YN"
                                                                            class="GridStyle table table-bordered table-condensed table-hover">
                                                                            <Columns>
                                                                                <asp:TemplateField
                                                                                    HeaderText="Applications"
                                                                                    HeaderStyle-CssClass="PaddingLeft_5">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox
                                                                                            ID="chkSelectedApp"
                                                                                            runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField
                                                                                    HeaderText="Application Code"
                                                                                    DataField="ApplicationCode"
                                                                                    HeaderStyle-CssClass="PaddingLeft_5" />
                                                                                <asp:BoundField
                                                                                    HeaderText="Application Name"
                                                                                    DataField="AppName"
                                                                                    HeaderStyle-CssClass="PaddingLeft_5" />
                                                                            </Columns>
                                                                            <RowStyle CssClass="NorRow" />
                                                                            <AlternatingRowStyle
                                                                                CssClass="AltRowTable" />
                                                                            <HeaderStyle
                                                                                CssClass="SectionHeader" />
                                                                            <PagerStyle HorizontalAlign="Center"
                                                                                Font-Bold="true" />
                                                                        </asp:GridView>
                                                                        <span id="spnMessageApplication"
                                                                            runat="server"
                                                                            class="BigText_Green" />
                                                                        <span id="spnMessageApplicationError"
                                                                            runat="server"
                                                                            class="BigText_Red" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 text-center mb-3">
                                                                    <asp:Button ID="btnUpdateApplication"
                                                                        runat="server" Text="Update"
                                                                        CssClass="btn btn-primary"
                                                                        OnClick="btnUpdateApplication_Click"
                                                                        CausesValidation="false" />
                                                                    <asp:Button ID="btnCancelApplication"
                                                                        runat="server" Text="Cancel"
                                                                        CssClass="btn btn-danger"
                                                                        OnClick="btnCancelApplication_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </form>
                                        </div>
                                    </div>
                                    <div class="container-fluid p-0 DefaultTableStyle p-all-area d-inline-block"
                                        id="dvManageRole" runat="server" visible="false">
                                        <!-- Form -->
                                        <div class="card card-default">
                                            <div class="card-header text-center p-2 bg-primary SectionHeader">
                                                <h3 class="card-title col-md-12 text-center m-0">Manage Role for
                                                            User: <span id="spnFullNameforRole" runat="server" />
                                                    <span id="spnUsernameForRole" runat="server" />
                                                </h3>
                                            </div>
                                            <!-- /.card-header -->
                                            <form action="#">
                                                <div class="card-body p-4">
                                                    <div class="row">
                                                        <div class="col-md-12 card-body-box">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <span id="spnMessageRole"
                                                                            runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4 text-center mb-3"
                                                                    style="visibility: hidden;">
                                                                    <asp:DropDownList ID="DropDownList1"
                                                                        runat="server" CssClass="form-control"
                                                                        AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlApplication_SelectedIndexChanged" />
                                                                    <asp:RequiredFieldValidator
                                                                        ID="RequiredFieldValidator1"
                                                                        runat="server"
                                                                        ControlToValidate="ddlApplication"
                                                                        SetFocusOnError="true" InitialValue="0"
                                                                        Display="Static"
                                                                        ValidationGroup="OnSubmit">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="col-md-4 text-center mb-3">
                                                                    <asp:DropDownList ID="ddlApplication"
                                                                        runat="server" CssClass="form-control"
                                                                        AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlApplication_SelectedIndexChanged" />
                                                                    <asp:RequiredFieldValidator
                                                                        ID="valReqApplication" runat="server"
                                                                        ControlToValidate="ddlApplication"
                                                                        SetFocusOnError="true" InitialValue="0"
                                                                        Display="Static"
                                                                        ValidationGroup="OnSubmit">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="col-md-4 text-center mb-3"
                                                                    style="visibility: hidden;">
                                                                    <asp:DropDownList ID="DropDownList2"
                                                                        runat="server" CssClass="form-control"
                                                                        AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlApplication_SelectedIndexChanged" />
                                                                    <asp:RequiredFieldValidator
                                                                        ID="RequiredFieldValidator2"
                                                                        runat="server"
                                                                        ControlToValidate="ddlApplication"
                                                                        SetFocusOnError="true" InitialValue="0"
                                                                        Display="Static"
                                                                        ValidationGroup="OnSubmit">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <span id="spnRoleMessage"
                                                                            runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 text-center mb-3">
                                                                    <asp:Button ID="btnUpdateRole"
                                                                        runat="server" Text="Update"
                                                                        CssClass="btn btn-primary"
                                                                        OnClick="btnUpdateRole_Click"
                                                                        ValidationGroup="OnSubmit" />
                                                                    <asp:Button ID="btnCancelRole"
                                                                        runat="server" Text="Cancel"
                                                                        CssClass="btn btn-danger"
                                                                        OnClick="btnCancelRole_Click" />
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <asp:GridView ID="gvShowSelectedRole"
                                                                            runat="server"
                                                                            AutoGenerateColumns="False"
                                                                            Width="550px"
                                                                            DataKeyNames="RoleAccessible_YN"
                                                                            class="GridStyle table table-bordered table-condensed table-hover">
                                                                            <Columns>
                                                                                <asp:TemplateField
                                                                                    HeaderText="Roles"
                                                                                    HeaderStyle-CssClass="PaddingLeft_5">
                                                                                    <ItemTemplate>
                                                                                     <asp:CheckBox ID="chkSelectedRole" runat="server" onclick="SingleRoleSelection(this);" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField
                                                                                    HeaderText="Role Code"
                                                                                    DataField="RoleCode"
                                                                                    HeaderStyle-CssClass="PaddingLeft_5" />
                                                                                <asp:BoundField
                                                                                    HeaderText="Role Name"
                                                                                    DataField="RoleName"
                                                                                    HeaderStyle-CssClass="PaddingLeft_5" />
                                                                            </Columns>
                                                                            <RowStyle CssClass="NorRow" />
                                                                            <AlternatingRowStyle
                                                                                CssClass="AltRowTable" />
                                                                            <HeaderStyle
                                                                                CssClass="SectionHeader" />
                                                                            <PagerStyle HorizontalAlign="Center"
                                                                                Font-Bold="true" />
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
                                    <div class="container-fluid p-0 DefaultTableStyle d-inline-block p-all-area"
                                        id="divManageUserDetails" runat="server" visible="false">
                                        <!-- Form -->
                                        <div class="card card-default">
                                            <div class="card-header text-center p-2 bg-primary">
                                                <h3 class="card-title col-md-12 text-center m-0 SectionHeader">Update User Details:
                                                            <span id="spnUserFullforDetails" runat="server" /><span
                                                                id="spnUsernameforDetails" runat="server" />
                                                </h3>
                                            </div>
                                            <!-- /.card-header -->
                                            <form action="#">
                                                <div class="card-body p-4">
                                                    <div class="row">
                                                        <div class="col-md-12 card-body-box">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <span id="SpnUserDetailMessage"
                                                                            runat="server" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <asp:ValidationSummary ID="valSummary"
                                                                            runat="server" ShowSummary="true"
                                                                            DisplayMode="BulletList"
                                                                            EnableClientScript="true"
                                                                            ValidationGroup="OnSubmitDetails" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <span
                                                                            class="font-weight-bold BoldLabel">User
                                                                                    Name <code>*</code></span>
                                                                        <asp:TextBox ID="txtNickName"
                                                                            runat="server"
                                                                            CssClass="form-control"
                                                                            Width="200px" MaxLength="30"
                                                                            placeholder="Enter User Name"
                                                                            Style="width: 100%;" />
                                                                        <asp:RequiredFieldValidator
                                                                            ID="valReqNickName" runat="server"
                                                                            ControlToValidate="txtNickName"
                                                                            SetFocusOnError="true"
                                                                            Display="None"
                                                                            ValidationGroup="OnSubmitDetails">
                                                                        </asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator
                                                                            ID="valrexNickName" runat="server"
                                                                            ControlToValidate="txtNickName"
                                                                            SetFocusOnError="true"
                                                                            Display="None"
                                                                            ValidationGroup="OnSubmitDetails"
                                                                            ValidationExpression="[^\s]+">
                                                                        </asp:RegularExpressionValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <span
                                                                            class="font-weight-bold BoldLabel">Holding
                                                                                    Amount <code>*</code></span>
                                                                        <asp:TextBox ID="txtholdingAmt"
                                                                            runat="server"
                                                                            CssClass="form-control"
                                                                            Width="100px" MaxLength="5"
                                                                            onkeypress="return /^[0-9./]/i.test(event.key)"
                                                                            placeholder="Enter Holding Amount"
                                                                            Style="width: 100%;" />
                                                                        <asp:RequiredFieldValidator
                                                                            ID="valReqholdingamt" runat="server"
                                                                            ControlToValidate="txtholdingAmt"
                                                                            SetFocusOnError="true"
                                                                            Display="None"
                                                                            ValidationGroup="OnSubmitDetails">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <span
                                                                            class="font-weight-bold BoldLabel">Remarks</span>
                                                                        <asp:TextBox ID="txtholdimngRemarks"
                                                                            runat="server"
                                                                            CssClass="form-control"
                                                                            Width="400px" MaxLength="300"
                                                                            placeholder="Enter Remarks"
                                                                            Style="width: 100%;" />
                                                                        <asp:RequiredFieldValidator
                                                                            ID="ReqfilValtxtremarks"
                                                                            runat="server"
                                                                            ControlToValidate="txtholdimngRemarks"
                                                                            SetFocusOnError="true"
                                                                            ValidationGroup="OnSubmitDetails">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <span class="font-weight-bold BoldLabel"
                                                                            sytle="display: none;">Allowed days
                                                                                    to view Report <code>*</code></span>
                                                                        <asp:TextBox
                                                                            ID="txtAllowedDaysToViewReport"
                                                                            runat="server" Text="0" Width="50px"
                                                                            MaxLength="4"
                                                                            Style="width: 100%; display: none;"
                                                                            placeholder="Enter Allowed days to view Report"
                                                                            class="TextBox form-control" />
                                                                        <%--<asp:RequiredFieldValidator
                                                                                    ID="valReqAllowedDaysToViewReport"
                                                                                    runat="server"
                                                                                    ControlToValidate="txtAllowedDaysToViewReport"
                                                                                    SetFocusOnError="true"
                                                                                    Display="None"
                                                                                    ValidationGroup="OnSubmitDetails">
                                                                                    </asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator
                                                                                        ID="valRegExpAllowedDaysToViewReport"
                                                                                        runat="server"
                                                                                        SetFocusOnError="true"
                                                                                        ValidationGroup="OnSubmitDetails"
                                                                                        Display="None"
                                                                                        ControlToValidate="txtAllowedDaysToViewReport"
                                                                                        ValidationExpression="\d+" />
                                                                        --%>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 text-center mb-3">
                                                                    <asp:Button ID="btnUpdateUserDetails"
                                                                        runat="server" Text="Update"
                                                                        CssClass="btn btn-primary"
                                                                        ValidationGroup="OnSubmitDetails"
                                                                        OnClick="btnUpdateUserDetails_Click" />
                                                                    <asp:Button ID="btnCancelUserDetails"
                                                                        runat="server" Text="Cancel"
                                                                        CssClass="btn btn-danger"
                                                                        OnClick="btnCancelUserDetails_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </form>
                                        </div>
                                    </div>
                                    <div class="container-fluid p-0" id="dvManageReport" runat="server"
                                        visible="false">
                                        <!-- Form -->
                                        <div class="card card-default">
                                            <div class="card-header text-center p-2 bg-primary">
                                                <h3 class="card-title col-md-12 text-center m-0">Manage Report
                                                            for User: <span id="spnReportName" runat="server" />
                                                    <span id="spnUsernames" runat="server" />
                                                </h3>
                                            </div>
                                            <!-- /.card-header -->
                                            <form action="#">
                                                <div class="card-body p-4">
                                                    <div class="row">
                                                        <div class="col-md-12 card-body-box">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <asp:GridView ID="gvShowSelectedReport"
                                                                            runat="server"
                                                                            AutoGenerateColumns="False"
                                                                            Width="100%"
                                                                            DataKeyNames="Active_YN"
                                                                            class="GridStyle table table-bordered table-condensed table-hover">
                                                                            <Columns>
                                                                                <asp:TemplateField
                                                                                    HeaderText="Reports"
                                                                                    HeaderStyle-CssClass="PaddingLeft_5">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox
                                                                                            ID="chkSelectedReport"
                                                                                            runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField
                                                                                    HeaderText="Report Name"
                                                                                    DataField="Report"
                                                                                    HeaderStyle-CssClass="PaddingLeft_5" />
                                                                                <%--<asp:BoundField
                                                                                            HeaderText="Application Name"
                                                                                            DataField="AppName"
                                                                                            HeaderStyle-CssClass="PaddingLeft_5" />--%>
                                                                            </Columns>
                                                                            <RowStyle CssClass="NorRow" />
                                                                            <AlternatingRowStyle
                                                                                CssClass="AltRowTable" />
                                                                            <HeaderStyle
                                                                                CssClass="SectionHeader" />
                                                                            <PagerStyle HorizontalAlign="Center"
                                                                                Font-Bold="true" />
                                                                        </asp:GridView>
                                                                        <span id="spnMessageReport"
                                                                            runat="server"
                                                                            class="BigText_Green" />
                                                                        <span id="spnMessageReportsError"
                                                                            runat="server"
                                                                            class="BigText_Red" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12 text-center mb-3">
                                                                    <asp:Button ID="btnUpdateReports"
                                                                        runat="server" Text="Update"
                                                                        CssClass="btn btn-primary"
                                                                        OnClick="btnUpdateReports_Click" />
                                                                    <asp:Button ID="btnCancelReports"
                                                                        runat="server" Text="Cancel"
                                                                        CssClass="btn btn-danger"
                                                                        OnClick="btnCancelApplication_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </form>
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
         <!-- Modal -->
    <div id="MyPopup" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header" style="background-color:cornflowerblue">
                <h4 class="modal-title">Balance Details</h4>
                <button type="button" class="close" data-dismiss="modal">
                    &times;</button>
            </div>
            <div class="modal-body">
                  <b> <asp:Label ID="lbldebitmessages" runat="server" Text="Debit Balance:"></asp:Label></b>
                  <b><asp:Label ID="lbldebitBal" runat="server" Text="" style="color:navy"></asp:Label></b><br /> <br />
                <b><asp:Label ID="lblcreditmessage" runat="server" Text="Credit Balance:"></asp:Label></b>
                  <b> <asp:Label ID="lblCreditbal" runat="server" Text="" style="color:navy"></asp:Label></b>
                

                  

            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">
                    Close</button>
            </div>
        </div>
    </div>
</div>


  <div id="MyChangePass" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header" style="background-color:cornflowerblue">
                <h4 class="modal-title">Reset Password</h4>
                <button type="button" class="close" data-dismiss="modal">&times;</button>
            </div>
            <div class="modal-body">
                <b>
                    <asp:Label ID="lblChnagePassmess" runat="server" 
                        Text="Password has been reset. New Password is:" 
                        ClientIDMode="Static"></asp:Label>
                </b>
                <br />
                <b>
                    <asp:Label ID="lblnewpass" runat="server" 
                        Text="" 
                        style="color:green; font-size:larger" 
                        ClientIDMode="Static"></asp:Label>
                </b>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
            </div>
        </div>
    </div>
</div>

    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js'></script>
<script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/js/bootstrap.bundle.min.js'></script>

 <script type="text/javascript">
     function ShowChangepass(message, newpassword) {
         $("#lblChnagePassmess").html(message);
         $("#lblnewpass").html(newpassword);
         $("#MyChangePass").modal("show");
     }
 </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scriptContent" runat="Server">
    <script type="text/javascript" language="javascript">
        function ConfirmOnDelete() {
            if (confirm("Are you sure?") == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        if (<%= dvManageApplication.Visible.ToString().ToLower() %>) {
            document.getElementById('<%= dvManageApplication.ClientID %>').scrollIntoView({ behavior: 'smooth', block: 'start' });
    }

    else if (<%= dvManageRole.Visible.ToString().ToLower() %>) {
        document.getElementById('<%= dvManageRole.ClientID %>').scrollIntoView({ behavior: 'smooth', block: 'start' });
    }

    else if (<%= dvManageReport.Visible.ToString().ToLower() %>) {
        document.getElementById('<%= dvManageReport.ClientID %>').scrollIntoView({ behavior: 'smooth', block: 'start' });
    }

    else if (<%= divManageUserDetails.Visible.ToString().ToLower() %>) {
        document.getElementById('<%= divManageUserDetails.ClientID %>').scrollIntoView({ behavior: 'smooth', block: 'start' });
        }


    </script>

    <script type="text/javascript">
        function OnlyOneCheck(chk) {
            var grid = document.getElementById('<%= gvShowSelectedRole.ClientID %>');
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i] != chk && inputs[i].type == "checkbox") {
                    inputs[i].checked = false;
                }
            }
        }
    </script>

    <script type="text/javascript">
        function SingleRoleSelection(chk) {
            var gv = document.getElementById('<%= gvShowSelectedRole.ClientID %>');
            var checkBoxes = gv.getElementsByTagName("input");

            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i] != chk) {
                    checkBoxes[i].checked = false;
                }
            }
        }
    </script>
    

</asp:Content>
