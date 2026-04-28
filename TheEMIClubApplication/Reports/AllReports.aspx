<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="AllReports.aspx.cs" Inherits="TheEMIClubApplication.Reports.AllReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <style>
        /* 🔹 General Styles */
        body {
            background: #f4f6f9;
        }

        .card {
            border-radius: 12px;
            border: none;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.08);
        }

        .card-header {
            border-bottom: none;
            background: linear-gradient(90deg, #007bff, #0056b3);
            color: white;
            padding: 0.75rem 1rem;
            border-radius: 12px 12px 0 0 !important;
        }

        .card-header h6,
        .card-body h4 {
            font-weight: 600;
        }

        .form-label {
            font-weight: 600;
            color: #344767;
        }

        .input-group-text {
            background-color: #f1f3f5;
            border-right: none;
        }

        .form-control {
            border-left: none;
            border-color: #dee2e6;
            box-shadow: none !important;
            font-size: 0.9rem;
        }

        .form-control:focus {
            border-color: #007bff;
        }

        .btn-primary {
            background: linear-gradient(90deg, #007bff, #0056b3);
            border: none;
            border-radius: 30px;
            font-weight: 600;
            transition: all 0.3s ease;
        }

        .btn-primary:hover {
            background: linear-gradient(90deg, #0056b3, #004094);
            transform: translateY(-1px);
        }

        /* 🔹 Table Styles */
        .table-container {
            overflow-x: auto;
        }

        .table {
            width: 100%;
            border-radius: 8px;
            overflow: hidden;
            font-size: 0.85rem;
        }

        .table thead th {
            background: linear-gradient(90deg, #007bff, #0056b3);
            color: #fff;
            font-weight: 600;
            text-align: center;
            padding: 10px;
            white-space: nowrap;
        }

        .table tbody td {
            vertical-align: middle;
            white-space: nowrap;
            text-overflow: ellipsis;
            overflow: hidden;
            padding: 8px 10px;
        }

        .table tbody tr:hover {
            background: #f1f5ff;
            transition: 0.2s ease;
        }

        /* 🔹 Pagination Styling */
        .pagination-container td {
            text-align: center !important;
            padding: 10px;
        }

        .pagination a, .pagination span {
            margin: 0 4px;
            padding: 6px 10px;
            border-radius: 6px;
            background: #f8f9fa;
            border: 1px solid #dee2e6;
            color: #007bff;
            text-decoration: none;
            transition: all 0.3s ease;
        }

        .pagination a:hover {
            background: #007bff;
            color: white;
        }

        /* 🔹 Checkbox alignment */
        .form-check {
            display: flex;
            align-items: center;
            gap: 6px;
        }

        .form-check label {
            margin-bottom: 0;
            font-weight: 600;
            color: #495057;
        }

        /* 🔹 Responsive tweaks */
        @media (max-width: 768px) {
            .row.g-3 > [class*='col-'] {
                margin-bottom: 1rem;
            }
        }
    </style>

    <div class="container-fluid mt-4">

        <!-- Page Header -->
        <div class="card mb-4">
            <div class="card-header d-flex justify-content-between align-items-center">
                <h4 class="mb-0">
                    <i class="fas fa-chart-bar me-2"></i>All Transaction Report
                </h4>
            </div>
        </div>

        <!-- Filters Section -->
        <div class="card mb-4">
            <div class="card-header">
                <h6 class="mb-0"><i class="fas fa-filter me-2"></i>Filters</h6>
            </div>
            <div class="card-body">
                <asp:Label ID="lblErrorMsg" runat="server" Text="" CssClass="form-label"></asp:Label>
                <div class="row g-3 align-items-end">
                    <div class="col-md-2">
                        <div class="form-check">
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true"
                                OnCheckedChanged="CheckBox1_CheckedChanged" CssClass="form-check-input" />
                            <label for="CheckBox1"> Date Filter</label>
                        </div>
                    </div>

                    <div class="col-md-2">
                        <label class="form-label">From Date</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="far fa-calendar"></i></span>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date" />
                        </div>
                    </div>

                    <div class="col-md-2">
                        <label class="form-label">To Date</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date" />
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label">Report Type</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-file-alt"></i></span>
                            <asp:DropDownList ID="ddlReports" runat="server" CssClass="form-control"   AutoPostBack="true" OnSelectedIndexChanged="ddlReports_SelectedIndexChanged">
                               <%-- <asp:ListItem Text="Auto Approved Loan" Value="AutoApprovedLoan" />--%>
                               <%-- <asp:ListItem Text="EMI Collection Report" Value="EMICollection" />
                                <asp:ListItem Text="Loan Amount Created Report" Value="LoanAmountCreated" />--%>
                               <%-- <asp:ListItem Text="Processing Charge Report" Value="ProcessingCharge" />--%>
                               <%-- <asp:ListItem Text="Membership Charge Report" Value="MembershipCharge" />--%>
                               <%-- <asp:ListItem Text="Hold Amount Report" Value="HoldAmountReport" />--%>
                                <asp:ListItem Text="Wallet Amount Report" Value="WalletAmount" />
                                 <asp:ListItem Text="Loan Status Report" Value="loanstatusreport" />
                                <%--<asp:ListItem Text="Late Payment Charge Report" Value="LatePaymentCharge" />--%>
                               <%-- <asp:ListItem Text="Interest Amount Report" Value="InterestAmount" />--%>
                               <%-- <asp:ListItem Text="Service Charge Report" Value="ServiceCharge" />--%>
                                <asp:ListItem Text="Payout Report" Value="PayoutReport" />
                                <asp:ListItem Text="Retailer-wise Loan Report" Value="RetailerwiseLoanReport" />
                                <asp:ListItem Text="Retailer Settlement Report" Value="RetailerSettlementReport" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label">User</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-user"></i></span>
                            <asp:DropDownList ID="ddluser" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row g-3 mt-3 align-items-end">
                    <div class="col-md-3">
                        <label class="form-label">Search Parameter</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-search"></i></span>
                            <asp:DropDownList ID="ddlSearchdiffrentParams" runat="server" CssClass="form-control">
                              <%--  <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Customercode" Value="Customercode"></asp:ListItem>
                                <asp:ListItem Text="LoanCode" Value="LoanCode"></asp:ListItem>
                                <asp:ListItem Text="Name" Value="Name"></asp:ListItem>
                                <asp:ListItem Text="ReceiptNo" Value="ReceiptNo"></asp:ListItem>
                                <asp:ListItem Text="PaymentMode" Value="PaymentMode"></asp:ListItem>
                                <asp:ListItem Text="RecordStatus" Value="RecordStatus"></asp:ListItem>
                                <asp:ListItem Text="Status" Value="Status"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label">Value</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-keyboard"></i></span>
                            <asp:TextBox ID="txtSearchValues" runat="server" CssClass="form-control" placeholder="Enter search value" />
                        </div>
                    </div>

                    <div class="col-md-12 text-center mt-3">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary px-4 py-2" OnClick="btnSearch_Click" />

                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary px-4 py-2" OnClick="btnClear_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Report Table -->
        <div class="card">
            <div class="card-header">
                <h6 class="mb-0"><i class="fas fa-table me-2"></i> Report Results</h6>
            </div>
            <div class="table-container">
                <asp:GridView ID="gvAllReport" runat="server"
                    CssClass="table table-bordered table-striped table-hover mb-0"
                    OnRowCommand="gvAllReport_RowCommand"
                    OnRowDataBound="gvAllReport_RowDataBound"
                    AutoGenerateColumns="true"
                    AllowPaging="true"
                    PageSize="10"
                    ShowFooter="true"
                    EmptyDataText="⚠️ No records found for the selected filters."
                    UseAccessibleHeader="true"
                    OnPageIndexChanging="gvAllReport_PageIndexChanging"
                    GridLines="None"
                    PagerSettings-Mode="Numeric"
                    PagerStyle-HorizontalAlign="Center"
                    PagerStyle-CssClass="pagination-container">
                </asp:GridView>


            </div>
        </div>

    </div>
</asp:Content>

<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

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
                white-space: nowrap; /* ✅ keep data in one line */
                overflow: hidden; /* hide overflow text */
                text-overflow: ellipsis; /* show ... if too long */
                vertical-align: middle; /* better alignment */
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
                    <i class="fas fa-file-alt mr-2"></i>All Transaction Report
                </h4>
            </div>
        </div>

        <!-- Filters Section -->
        <div class="card shadow border-0 mb-4">
            <div class="card-header bg-light">
                <h6 class="mb-0 text-primary font-weight-bold">
                    <i class="fas fa-filter mr-2"></i>Filters
                </h6>
            </div>
            <div class="card-body">
                <div class="row g-3">
                    <div class="col-md-1">
                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    </div>
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
                </div>

                <div class="row g-3 mt-3">

                    <div class="col-md-3">
                        <label class="form-label fw-bold">Criteria</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-file-invoice-dollar"></i></span>
                            <asp:DropDownList ID="ddlReports" runat="server" CssClass="form-control" >
                                <asp:ListItem Text="EMI Collection Report" Value="EMICollection" />
                                <asp:ListItem Text="Loan Amount Created Report" Value="LoanAmountCreated" />
                                <asp:ListItem Text="Processing Charge Report" Value="ProcessingCharge" />
                                <asp:ListItem Text="Membership Charge Report" Value="MembershipCharge" />
                                <asp:ListItem Text="Hold Amount Report" Value="HoldAmountReport" />
                                <asp:ListItem Text="Wallet Amount Report" Value="WalletAmount" />
                                <asp:ListItem Text="Late Payment Charge Report" Value="LatePaymentCharge" />
                                <asp:ListItem Text="Interest Amount Report" Value="InterestAmount" />
                                <asp:ListItem Text="Service Charge Report" Value="ServiceCharge" />

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

                        <div class="col-md-3">
                            <label class="form-label fw-bold">User</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-user"></i></span>
                            <asp:DropDownList ID="ddlSearchdiffrentParams" runat="server" CssClass="form-control">
                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Customercode" Value="Customercode"></asp:ListItem>
                                <asp:ListItem Text="LoanCode" Value="LoanCode"></asp:ListItem>
                                <asp:ListItem Text="Name" Value="Name"></asp:ListItem>
                                <asp:ListItem Text="ReceiptNo" Value="ReceiptNo"></asp:ListItem>
                                <asp:ListItem Text="PaymentMode" Value="PaymentMode"></asp:ListItem>
                                <asp:ListItem Text="RecordStatus" Value="RecordStatus"></asp:ListItem>
                                <asp:ListItem Text="Status" Value="Status"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label fw-bold">Values</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-user"></i></span>
                            <asp:TextBox ID="txtSearchValues" runat="server" CssClass="form-control" Palceholder="values" />
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
                    <i class="fas fa-table mr-2"></i>Report Results
                </h6>
            </div>
            <div class="table-container">
                <asp:GridView ID="gvAllReport" runat="server"
                    CssClass="table table-bordered table-striped table-hover w-100 mb-0"
                    AutoGenerateColumns="true"
                    AllowPaging="true" PageSize="10"
                    EmptyDataText="⚠️ No records found for the selected filters."
                    UseAccessibleHeader="true"
                    OnPageIndexChanging="gvAllReport_PageIndexChanging"
                    GridLines="None"
                    PagerSettings-Mode="Numeric"
                    PagerStyle-HorizontalAlign="Center"
                    PagerStyle-CssClass="pagination-container">
                </asp:GridView>
            </div>

        </div>

    </div>
</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>


