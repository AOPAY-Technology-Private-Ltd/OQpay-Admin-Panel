<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="ManageApplication.aspx.cs" 
Inherits="TheEMIClubApplication.MembershipPages.ManageApplication" Title="Untitled Page"  %>

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
                                <h3 class="card-title col-md-12 text-center m-0">Manage Application</h3>
                            </div>
                            <!-- Card Header -->
                            <form action="#">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-12 card-body-box">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Application Code/Name</span>
                                                        <asp:TextBox ID="txtApplicationName" runat="server" CssClass="form-control" MaxLength="50" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Application Type</span>
                                                        <asp:DropDownList ID="ddlApplicationType" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Status</span>
                                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-center mb-3">
                                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-danger" Text="Clear" OnClick="btnClear_Click" />
                                                    <div class="text-center">
                                                        <span id="spnMessage" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-center mb-3 mt-2 table-responsive">
                                                    <asp:GridView ID="gvManageApplication" runat="server" AutoGenerateColumns="False" AllowPaging="True" CssClass="GridStyle" Width="100%" OnPageIndexChanging="gvManageApplication_PageIndexChanging"
                                                        OnRowCommand="gvManageApplication_RowCommand">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="SNo." DataField="SrNo" HeaderStyle-CssClass="PaddingLeft_5" />
                                                            <asp:BoundField HeaderText="Application Code" DataField="ApplicationCode" HeaderStyle-CssClass="PaddingLeft_5" />
                                                            <asp:BoundField HeaderText="Application Name" DataField="ApplicationName" HeaderStyle-CssClass="PaddingLeft_5" />
                                                            <asp:BoundField HeaderText="Deploy Location" DataField="DeployLocation" HeaderStyle-CssClass="PaddingLeft_5" Visible="false" />
                                                            <asp:BoundField HeaderText="Application Type" DataField="AppType" HeaderStyle-CssClass="PaddingLeft_5" />
                                                            <asp:BoundField HeaderText="Status" DataField="Active_YN" HeaderStyle-CssClass="PaddingLeft_5" />
                                                            <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="PaddingLeft_5">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkAction" runat="server" Text='<%#Eval("IsActive")%>' OnClientClick="return confirm('Are you sure?');"
                                                                        CommandName="ACT" CommandArgument='<%#Eval("ApplicationCode") +  "|" + Eval("IsActive")%>'>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="PaddingLeft_5">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit"
                                                                        CommandName="EDT" CommandArgument='<%#Eval("ApplicationCode") +  "|" + Eval("IsActive")%>'>
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
                            </form>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scriptContent" Runat="Server">
</asp:Content>



