<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="ManageRole.aspx.cs" Inherits="TheEMIClubApplication.MembershipPages.ManageRole" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <div class="hold-transition sidebar-mini">
        <div class="wrapper">
            <!-- Content Wrapper. Contains page content -->
            <div class="main-box-container w-100 m-0" style="position: relative; left: 0px; top: 0px; padding: 25px 0px;">
                <!-- Main content -->
                <section class="content">
                    <div class="container-fluid">
                        <!-- Form -->
                        <div class="card card-default">
                            <%--<div class="card-header form-header-bar">
                                <h3 class="card-title">Manage Role Detail</h3>
                                <a href="CreateRole.aspx">Add Role</a>
                                <asp:ImageButton ID="imgbtnPlus" Class="plusicon" runat="server" ToolTip="Add Role" ImageUrl="~/Images/icon-add.png"
                                    OnClick="imgbtnPlus_Click" />
                            </div>--%>

                                  <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
    <h6 class="mb-0 font-weight-bold">
        <i class="fas fa-search mr-2"></i>Manage Role Detail
    </h6>
      <a href="CreateRole.aspx" class="btn btn-success btn-sm font-weight-bold shadow-sm">
    <i class="fas fa-plus mr-1"></i> Add Role
</a>
</div>
                            <!-- Card Header -->
                            <form action="#">
                                <div class="card-body ">
                                    <div class="row">
                                        <div class="col-md-12 card-body-box">
                                            <div class="row">
                                                <div class="col-md-12 text-center">
                                                    <div class="form-group">
                                                        <asp:ScriptManager ID="scRoleDetail" runat="server"></asp:ScriptManager>
                                                        <span id="spnMessage" runat="server"></span>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Application</span>
                                                        <asp:DropDownList ID="ddlApplicationName" runat="server" OnClientFocus="expandControl"
                                                            placeholder="Enter Application" class="form-control" Style="width: 100%;">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Role Name</span>
                                                        <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control" MaxLength="50" placeholder="Enter Role Name" Style="width: 100%;" />
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Menu Scope</span>
                                                        <asp:DropDownList ID="ddlMenuScope" runat="server" OnClientFocus="expandControl"
                                                            CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold ">Status</span>
                                                        <asp:DropDownList ID="ddlStatus" runat="server" OnClientFocus="expandControl" class="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-12 text-center mb-3">
                                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search"
                                                        OnClick="btnSearch_Click" />
                                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger" Text="Clear"
                                                        OnClick="btnClear_Click" />
                                                </div>
                                                <div class="col-md-12 text-center">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="gvManageRole" CssClass="GridStyle table table-bordered table-condensed table-hover" runat="server"
                                                            AutoGenerateColumns="False" OnPageIndexChanging="gvManageRole_PageIndexChanging"
                                                            Width="100%" AllowPaging="True" AllowSorting="True" OnRowCommand="gvManageRole_RowCommand">
                                                            <PagerStyle CssClass="gridview"></PagerStyle>
                                                            <Columns>
                                                                <asp:BoundField DataField="SrNo" HeaderText="Sl. #">
                                                                    <ItemStyle Wrap="false" CssClass="PaddingLeft_5" />
                                                                    <HeaderStyle Wrap="false" CssClass="PaddingLeft_5" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="RoleCode" HeaderText="Role Code">
                                                                    <ItemStyle Wrap="false" CssClass="PaddingLeft_5" />
                                                                    <HeaderStyle Wrap="false" CssClass="PaddingLeft_5" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="RoleName" HeaderText="Role Name">
                                                                    <ItemStyle Wrap="false" CssClass="PaddingLeft_5" />
                                                                    <HeaderStyle Wrap="false" CssClass="PaddingLeft_5" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="MenuScope" HeaderText="Menu Scope">
                                                                    <ItemStyle Wrap="false" CssClass="PaddingLeft_5" />
                                                                    <HeaderStyle Wrap="false" CssClass="PaddingLeft_5" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Active_YN" HeaderText="Status" Visible="false">
                                                                    <ItemStyle Wrap="false" CssClass="PaddingLeft_5" />
                                                                    <HeaderStyle Wrap="false" CssClass="PaddingLeft_5" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <%--  <asp:Image ID="imgRenew" ImageUrl="~/Images/R.bmp" AlternateText="Renew" runat="server" />--%>
                                                                        <asp:LinkButton ID="lnkAction" runat="server" Text='<%#Eval("IsActive")%>' OnClientClick="return confirm('Are you sure?');"
                                                                            CommandName="ACT" CommandArgument='<%#Eval("RoleCode") +  "|" + Eval("IsActive")%>'>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="PaddingLeft_5" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit"
                                                                            CommandName="EDT" CommandArgument='<%#Eval("RoleCode") +  "|" + Eval("IsActive")%>'>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle CssClass="PaddingLeft_5" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle CssClass="NorRow" />
                                                            <AlternatingRowStyle CssClass="AltRowTable" />
                                                            <HeaderStyle CssClass="SectionHeader" />
                                                            <PagerSettings NextPageText="next" Position="Bottom" PreviousPageText="previous" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scriptContent" Runat="Server">
    <script type="text/javascript">
        function expandControl(sender, eventArgs) {
            //This will expand the dropdown on focus (with tab or otherwise)
            sender.showDropDown();
        }
    </script>
</asp:Content>