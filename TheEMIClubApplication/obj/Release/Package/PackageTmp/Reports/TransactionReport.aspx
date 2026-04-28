<%@ Page Title="Transaction Report" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="TransactionReport.aspx.cs"
    Inherits="TheEMIClubApplication.Reports.TransactionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">

<style>
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
        font-size: 0.75rem; /* slightly larger for readability */
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
</style>


    <div class="container-fluid mt-4">

        <!-- Page Header -->
        <div class="card shadow border-0 mb-4">
            <div class="card-body bg-gradient-primary text-white d-flex justify-content-between align-items-center">
                <h4 class="mb-0">
                    <i class="fas fa-file-alt mr-2"></i> Transaction Report
                </h4>
            </div>
        </div>

        <!-- Filters Section -->
  <div class="card shadow border-0 mb-4">
    <div class="card-header bg-light">
        <h6 class="mb-0 text-primary font-weight-bold">
            <i class="fas fa-filter mr-2"></i> Filters
        </h6>
    </div>
    <div class="card-body">
        <div class="row g-3">
            <div class="col-md-2">
                <label class="form-label fw-bold">From Date</label>
                <div class="input-group">
                    <span class="input-group-text"><i class="far fa-calendar"></i></span>
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date" />
                </div>
            </div>
            <div class="col-md-2">
                <label class="form-label fw-bold">To Date</label>
                <div class="input-group">
                    <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date" />
                </div>
            </div>

            <div class="col-md-2">
                <label class="form-label fw-bold">Report Type</label>
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-file-invoice-dollar"></i></span>
                    <asp:DropDownList ID="ddlReports" runat="server" CssClass="form-control">
                        <asp:ListItem Text="-Select-" Value="" />
                        <asp:ListItem Text="Pay Out" Value="Pay Out" />
                        <asp:ListItem Text="Deposit" Value="Deposit" />
                    </asp:DropDownList>
                </div>
            </div>

            <div class="col-md-3">
                <label class="form-label fw-bold">Status</label>
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-traffic-light"></i></span>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                        <asp:ListItem Text="All" Value="All" />
                        <asp:ListItem Text="Approved" Value="Approved" />
                       <%-- <asp:ListItem Text="Pending" Value="Pending" />--%>
                        <asp:ListItem Text="Rejected" Value="Rejected" />
                    </asp:DropDownList>
                </div>
            </div>

            <div class="col-md-3">
                <label class="form-label fw-bold">User</label>
                <div class="input-group">
                    <span class="input-group-text"><i class="fas fa-user"></i></span>
                    <asp:DropDownList ID="ddluser" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
        </div>

        <!-- Button Row Centered -->
        <div class="row mt-3">
            <div class="col-12 d-flex justify-content-center">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary px-4" OnClick="btnSearch_Click" />
            </div>
        </div>
    </div>
</div>


        <!-- Report Table -->
        <div class="card shadow border-0">
            <div class="card-header bg-light d-flex justify-content-between align-items-center">
                <h6 class="mb-0 text-primary font-weight-bold">
                    <i class="fas fa-table mr-2"></i> Report Results
                </h6>
            </div>
<div class="table-container">
    <asp:GridView ID="gvTransactionReport" runat="server"
        CssClass="table table-bordered table-striped table-hover w-100 mb-0"
        AutoGenerateColumns="true"
        AllowPaging="true" PageSize="10"
        OnPageIndexChanging="gvTransactionReport_PageIndexChanging"
        OnRowDataBound="gvTransactionReport_RowDataBound"
        EmptyDataText="⚠️ No records found for the selected filters."
        UseAccessibleHeader="true"
        GridLines="None"
        PagerSettings-Mode="Numeric"
        PagerStyle-HorizontalAlign="Center"
        PagerStyle-CssClass="pagination-container">
    </asp:GridView>
</div>

        </div>

    </div>

</asp:Content>
