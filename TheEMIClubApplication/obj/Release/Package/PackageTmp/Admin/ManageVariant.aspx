<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="ManageVariant.aspx.cs" Inherits="TheEMIClubApplication.Admin.ManageVariant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
<%--        <style>
    .form-group label {
        font-weight: 500;
    }

    label::before,
    label::after {
        content: none !important;
    }

    .table {
        border-radius: 8px;
        overflow: hidden;
        font-size: 0.85rem; /* slightly larger for readability */
        table-layout: auto; /* ensure natural column widths */
        width: 100%;
    }

    .table thead th {
        white-space: nowrap; /* keep header in single line */
        background: #007bff;
        color: white;
        text-align: center;
        text-overflow: ellipsis;
        font-weight: 600;
        padding: 8px;
    }

    .table tbody td {
        white-space: nowrap;       /* ✅ keep data in one line */
        overflow: hidden;          /* hide overflow text */
        text-overflow: ellipsis;   /* show ... if too long */
        vertical-align: middle;    /* better alignment */
        padding: 6px 8px;
    }

    .table tbody tr:hover {
        background-color: #f1f5ff;
    }

    .badge {
        padding: 0.35em 0.5em;
        font-size: 0.7rem;
        border-radius: 6px;
    }

    /* ✅ Responsive scroll on smaller screens */
    .table-container {
        overflow-x: auto;
    }
</style>--%>
    <!-- Search & Filter Card -->
    <div class="card shadow mb-4">
      <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
    <h6 class="mb-0 font-weight-bold">
        <i class="fas fa-search mr-2"></i>Search & Filter
    </h6>
      <a href="CreateVariantMaster.aspx" class="btn btn-success btn-sm font-weight-bold shadow-sm">
    <i class="fas fa-plus mr-1"></i> Create Variant
</a>
</div>
        <div class="card-body" runat="server" id="divSeachvariant">
            <div class="form-row align-items-end">

                <!-- Criteria -->
                <div class="form-group col-12 col-sm-6 col-md-3">
                    <asp:Label ID="lblSearchModel" runat="server" CssClass="font-weight-bold">Criteria</asp:Label>
                    <asp:DropDownList ID="ddlSearchValues" runat="server" CssClass="form-control">
                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                        <asp:ListItem Text="Brand" Value="Brand"></asp:ListItem>
                        <asp:ListItem Text="Model" Value="Model"></asp:ListItem>
                        <asp:ListItem Text="Variant" Value="Variant"></asp:ListItem>
                        <asp:ListItem Text="Color" Value="Color"></asp:ListItem>
                        <asp:ListItem Text="Status" Value="Status"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <!-- Values -->
                <div class="form-group col-12 col-sm-6 col-md-3">
                    <asp:Label ID="lblSearchValues" runat="server" CssClass="font-weight-bold">Values</asp:Label>
                    <asp:TextBox ID="txtSearchValues" runat="server" CssClass="form-control" placeholder="Enter Search Values" />
                </div>

                <!-- Button -->
                <div class="form-group col-12 col-sm-6 col-md-2">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-block" OnClick="btnSearch_Click"  CausesValidation="false" />
                </div>

            </div>
        </div>
    </div>

    <!-- Grid Card -->

    <div class="card shadow mb-4">
    <div class="card-header bg-primary text-white">
        <h6 class="mb-0 font-weight-bold">
            <i class="fas fa-list mr-2"></i>Variants List
        </h6>
    </div>

    <!-- Responsive wrapper -->
    <div class="card-body p-0">
        <div class="table-responsive">

            <span id="spnMessage" runat="server"></span>

            <asp:GridView ID="gvVariants" runat="server"
                CssClass="table table-bordered table-hover table-striped align-middle mb-0"
                AllowPaging="true" PageSize="10"
                AutoGenerateColumns="false"
                DataKeyNames="RID,ImagePath,groupid"
                OnRowCommand="gvVariants_RowCommand"
                OnPageIndexChanging="gvVariants_PageIndexChanging"
        PagerSettings-Position="Bottom"
PagerStyle-HorizontalAlign="Left"
PagerStyle-CssClass="pagination-container"
                UseAccessibleHeader="true"
                GridLines="None">

                <Columns>

                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="BrandName" HeaderText="Brand" />
                    <asp:BoundField DataField="ModelName" HeaderText="Model" />
                    <asp:BoundField DataField="VariantName" HeaderText="Variant" />


                    <asp:BoundField DataField="SellingPrice" HeaderText="Selling"
                        DataFormatString="{0:N2}" HeaderStyle-CssClass="d-none d-md-table-cell"
                        ItemStyle-CssClass="d-none d-md-table-cell" />

                    <asp:BoundField DataField="MrpPrice" HeaderText="MRP"
                        DataFormatString="{0:N2}" HeaderStyle-CssClass="d-none d-md-table-cell"
                        ItemStyle-CssClass="d-none d-md-table-cell" />

                    <asp:BoundField DataField="Tenure" HeaderText="Tenure"
                        HeaderStyle-CssClass="d-none d-lg-table-cell"
                        ItemStyle-CssClass="d-none d-lg-table-cell" />

                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <asp:Image ID="productimage" runat="server"
                                ImageUrl='<%# Eval("ImagePath") %>'
                                CssClass="img-fluid rounded"
                                Style="max-width:60px;" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="ActiveStatus" HeaderText="Status" />

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server"
                                CssClass="btn btn-info btn-sm"
                                CommandName="EditRow"
                                CommandArgument='<%# Eval("groupid") %>'
                                CausesValidation="false">
                                <i class="fas fa-edit"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

        </div>

        <asp:Label ID="lblModelRecordCount" runat="server"
            CssClass="mt-2 font-weight-bold text-danger d-block text-end"></asp:Label>
    </div>
</div>

<%--    <div class="card shadow mb-4">
        <div class="card-header bg-primary text-white">
            <h6 class="mb-0 font-weight-bold"><i class="fas fa-list mr-2"></i>Variants List</h6>
        </div>
       <div class="table-container">
                <span id="spnMessage" runat="server"></span>
                <asp:GridView ID="gvVariants" runat="server" 
                    CssClass="table table-bordered table-hover table-striped mb-0"
                    AllowPaging="true" PageSize="10"
                    AutoGenerateColumns="false" DataKeyNames="RID,ImagePath,groupid"
           OnRowCommand="gvVariants_RowCommand" 
                    OnPageIndexChanging="gvVariants_PageIndexChanging"
                    PagerSettings-Mode="Numeric"
                    PagerSettings-Position="Bottom"
                    PagerStyle-HorizontalAlign="Center"
                    PagerStyle-CssClass="pagination justify-content-center"
                    UseAccessibleHeader="true"
                    GridLines="None">

                    <Columns>
     <asp:TemplateField HeaderText="S.No">
        <ItemTemplate>
            <%# Container.DataItemIndex + 1 %>
        </ItemTemplate>
    </asp:TemplateField>

<asp:BoundField DataField="BrandName" HeaderText="Brand" />
<asp:BoundField DataField="ModelName" HeaderText="Model" />
<asp:BoundField DataField="VariantName" HeaderText="Variant" />
<asp:BoundField DataField="SellingPrice" HeaderText="Selling Price" DataFormatString="{0:N2}" />
<asp:BoundField DataField="MrpPrice" HeaderText="MRP Price" DataFormatString="{0:N2}" />
<asp:BoundField DataField="avlbColors" HeaderText="Color" />
<asp:BoundField DataField="DownPaymentPerc" HeaderText="DP(%)" DataFormatString="{0:N2}" />
<asp:BoundField DataField="InterestPerc" HeaderText="Interest(%)" DataFormatString="{0:N2}" />
<asp:BoundField DataField="Tenure" HeaderText="Tenure" />
<asp:BoundField DataField="ProcessingFees" HeaderText="Proc. Fees" DataFormatString="{0:N2}" />
<asp:BoundField DataField="Remark" HeaderText="Remark" />

                  
                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                                <asp:Image ID="productimage" runat="server" ImageUrl='<%# Eval("ImagePath") %>' CssClass="img-thumbnail" Width="60" Height="60" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="ActiveStatus" HeaderText="Status" />

                      
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="btn btn-info btn-sm"
                                    CausesValidation="false" CommandName="EditRow" CommandArgument='<%# Eval("groupid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
                   <asp:Label ID="lblModelRecordCount" runat="server"
            CssClass="mt-2 font-weight-bold text-red"></asp:Label>
            </div>
        </div>--%>

   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">

          <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>

    <script>
        // Optional: Highlight GridView rows on hover
        $(document).ready(function () {
            $('.table tbody tr').hover(
                function () { $(this).addClass('table-primary'); },
                function () { $(this).removeClass('table-primary'); }
            );
        });
    </script>
 </asp:Content>
