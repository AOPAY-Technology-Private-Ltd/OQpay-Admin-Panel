<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="ManageRetailer.aspx.cs" Inherits="TheEMIClubApplication.Admin.ManageRetailer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="divretailer" runat="server" class="card shadow-sm my-4">
        <!-- Card Header -->
        <div class="card-header bg-primary text-white">
            <h4 class="mb-0"><i class="fas fa-store mr-2"></i>Retailers Details</h4>
        </div>

        <!-- Card Body -->
        <div class="card-body">
            <!-- Search Filters -->
            <div class="row mb-3">
                <div class="col-md-3 mb-2">
                    <label class="font-weight-bold">Search By Name</label>
                    <asp:TextBox ID="txtname" runat="server" CssClass="form-control" Placeholder="Enter name"></asp:TextBox>
                </div>
                <div class="col-md-3 mb-2">
                    <label class="font-weight-bold">Search By Mobile No</label>
                    <asp:TextBox ID="txtmobileNo" runat="server" CssClass="form-control" Placeholder="Enter mobile number"></asp:TextBox>
                </div>
                <div class="col-md-3 mb-2">
                    <label class="font-weight-bold">Search By Dealer Code</label>
                    <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" Placeholder="Enter dealer code"></asp:TextBox>
                </div>
                <div class="col-md-3 mb-2">
                    <label class="font-weight-bold">Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <!-- Search Button -->
            <div class="row mb-4">
                <div class="col text-center">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary px-4" OnClick="btnSearch_click" />
                </div>
            </div>

            <!-- Grid -->
            <div class="table-responsive">
       <span id="spnMessage" runat="server" style="color: red;"></span>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvRecentApplication" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-bordered table-striped table-hover mb-0"
                            OnRowCommand="gvRecentApplication_RowCommand" AllowPaging="true" PageSize="15"
                            OnPageIndexChanging="gvRecentApplication_PageIndexChanging"
                            OnDataBound="gvRecentApplication_DataBound"
                            PagerSettings-Mode="Numeric"
                            PagerSettings-Position="Bottom"
                            PagerStyle-HorizontalAlign="Center"
                            PagerStyle-CssClass="pagination justify-content-center"
                            UseAccessibleHeader="true"
                            GridLines="None">

                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="S.No" />
                                <asp:BoundField HeaderText="Dealer Code" DataField="CustomerCode" />
                                <asp:BoundField HeaderText="Name" DataField="UserName" />
                                <asp:BoundField HeaderText="Email id" DataField="EmailID" />
                                <asp:BoundField HeaderText="Mobile No" DataField="MobileNo" />
                                <asp:BoundField HeaderText="Address" DataField="Address" />
                                <asp:BoundField HeaderText="Pan No." DataField="PANNumber" />
                                <asp:BoundField HeaderText="Aadhar No." DataField="AadharNumber" />
                                <asp:BoundField HeaderText="Status" DataField="ActiveStatus" />
                                <asp:BoundField HeaderText="Credit Balance" DataField="CreditBalance" />
                                <asp:BoundField HeaderText="Holding Amount" DataField="HoldingAmount" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkManageview" runat="server" CssClass="btn btn-sm btn-info"
                                            Text="Edit" CommandName="VIEW" CommandArgument='<%#Eval("CustomerCode") %>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblDealerNoData" runat="server" CssClass="mt-2 font-weight-bold d-block text-center"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
    <script>
        $(document).ready(function () {
            // Example: Show the retailer div
            $('#<%= divretailer.ClientID %>').show();

            // Optional: Add hover effect for grid rows
            $('.table-hover tbody tr').hover(function () {
                $(this).addClass('table-primary');
            }, function () {
                $(this).removeClass('table-primary');
            });
        });
    </script>
</asp:Content>
