<%@ Page Title="Loan Settlement / Foreclosure Report" Language="C#"
    MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="LoanSettlementForeclosureReport.aspx.cs"
    Inherits="TheEMIClubApplication.Reports.LoanSettlementForeclosureReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">

    <!-- Bootstrap Icons -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <style>
        .loan-report .filter-card {
            border-radius: 16px;
            box-shadow: 0 12px 30px rgba(0,0,0,.08);
        }
        .gradient-primary {
            background: linear-gradient(135deg, #2563eb, #3b82f6);
        }
        .gradient-dark {
            background: linear-gradient(135deg, #111827, #374151);
        }
        .kpi-card {
            display: flex;
            align-items: center;
            gap: 20px;
            padding: 24px;
            border-radius: 20px;
            box-shadow: 0 14px 35px rgba(0,0,0,.15);
        }
        .kpi-icon { font-size: 44px; opacity: .9; }
        .kpi-title {
            font-size: 13px;
            text-transform: uppercase;
            letter-spacing: 1px;
        }
        .kpi-value { font-size: 28px; font-weight: 700; }

        .table-modern th {
            background-color: #f8fafc;
            font-weight: 600;
        }
        .table-modern th,
        .table-modern td {
            white-space: nowrap;
            vertical-align: middle;
        }
        .table-responsive { overflow-x: auto; }
    </style>

    <div class="container-fluid px-4 py-4 loan-report">

        <!-- HEADER -->
        <div class="mb-4">
            <h3 class="fw-bold">
                <i class="bi bi-cash-stack text-primary me-2"></i>
                Loan Settlement / Foreclosure Report
            </h3>
            <small class="text-muted">Reports / Loan / Settlement & Foreclosure</small>
        </div>

        <!-- FILTER -->
        <div class="card filter-card mb-4">
            <div class="card-header gradient-primary text-white">
                <i class="bi bi-funnel-fill me-2"></i> Filter Criteria
            </div>
            <div class="card-body">

                <div class="row g-3">

                    <div class="col-md-3">
                        <label class="form-label fw-semibold">Settlement Type</label>
                        <asp:DropDownList ID="ddlSettlementType" runat="server"
                            CssClass="form-control form-control-lg">
                            <asp:ListItem Text="Settlement" Value="settlement" />
                            <asp:ListItem Text="Foreclosure" Value="foreclosure" />
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label fw-semibold">Retailer Code</label>
                        <asp:DropDownList ID="ddluser" runat="server"
                            CssClass="form-control form-control-lg"
                            AppendDataBoundItems="true">
                            <asp:ListItem Text="All Record" Value="" />
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label fw-semibold">Customer Code</label>
                        <asp:TextBox ID="txtCustomerCode" runat="server"
                            CssClass="form-control form-control-lg" />
                    </div>

                    <div class="col-md-3">
                        <label class="form-label fw-semibold">Loan Code</label>
                        <asp:TextBox ID="txtLoanCode" runat="server"
                            CssClass="form-control form-control-lg" />
                    </div>

                </div>

                <div class="d-flex justify-content-center gap-4 mt-4">
                    <asp:Button ID="btnSearch" runat="server"
                        Text="Search"
                        CssClass="btn btn-primary btn-lg px-4"
                        OnClick="btnSearch_Click" />

                    <asp:Button ID="btnReset" runat="server"
                        Text="Reset"
                        CssClass="btn btn-outline-secondary btn-lg px-4"
                        OnClick="btnReset_Click" />
                </div>

            </div>
        </div>

        <!-- KPI CARDS -->
        <div class="row g-4 mb-4">
            <div class="col-md-4">
                <div class="kpi-card bg-primary text-white">
                    <i class="bi bi-journal-text kpi-icon"></i>
                    <div>
                        <div class="kpi-title">Total Records</div>
                        <div class="kpi-value">
                            <asp:Label ID="lblTotalLoans" runat="server" Text="0" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="kpi-card bg-success text-white">
                    <i class="bi bi-currency-rupee kpi-icon"></i>
                    <div>
                        <div class="kpi-title">Total Paid Amount</div>
                        <div class="kpi-value">
                            ₹ <asp:Label ID="lblTotalPaid" runat="server" Text="0.00" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="kpi-card bg-warning text-dark">
                    <i class="bi bi-exclamation-circle kpi-icon"></i>
                    <div>
                        <div class="kpi-title">Total Fine Amount</div>
                        <div class="kpi-value">
                            ₹ <asp:Label ID="lblPendingAmount" runat="server" Text="0.00" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- GRID -->
<div class="card shadow-lg">
    <div class="card-header gradient-dark text-white">
        <i class="bi bi-table me-2"></i> Settlement / Foreclosure Details
    </div>

    <div class="table-responsive">
        <asp:GridView ID="gvLoanEmiReport" runat="server"
            CssClass="table table-hover table-bordered table-modern align-middle mb-0"
            AutoGenerateColumns="False"
            EmptyDataText="No settlement / foreclosure records found"

            AllowPaging="true"
            PageSize="15"
     PagerSettings-Position="Bottom"
PagerStyle-HorizontalAlign="Left"
PagerStyle-CssClass="pagination-container"
            OnPageIndexChanging="gvLoanEmiReport_PageIndexChanging" OnRowDataBound="gvLoanEmiReport_RowDataBound">

            <Columns>

<asp:TemplateField HeaderText="Sr No" ItemStyle-Width="60px">
    <ItemTemplate>
        <asp:Label ID="lblSrNo" runat="server"></asp:Label>
    </ItemTemplate>
</asp:TemplateField>
    

                <asp:BoundField HeaderText="Retailer Code" DataField="DealerCode" />
                <asp:BoundField HeaderText="Customer Name" DataField="CustomerFullName" />
                <asp:BoundField HeaderText="Mobile" DataField="CustomerMobileNo" />
                <asp:BoundField HeaderText="Loan Code" DataField="LoanCode" />
                <asp:BoundField HeaderText="Product Model" DataField="ModelName" />
                <asp:BoundField HeaderText="Loan Amount" DataField="LoanAmount" DataFormatString="{0:N2}" />
                <asp:BoundField HeaderText="EMI Amount" DataField="EMIAmount" DataFormatString="{0:N2}" />
                <asp:BoundField HeaderText="Paid EMI" DataField="PaidEMI" />
                <asp:BoundField HeaderText="Due EMI" DataField="DuesEMI" />




                                     <asp:BoundField HeaderText="Closure Charge" DataField="SettlementOrForeclosureCharg" DataFormatString="{0:N2}" />
                     <asp:BoundField HeaderText="FineAmount" DataField="FineAmount" DataFormatString="{0:N2}" />
                <asp:BoundField HeaderText="Final Paid Amount" DataField="FinalPaidAmount" DataFormatString="{0:N2}" />



                <asp:BoundField HeaderText="Payment Mode" DataField="PaymentMode" />
                <asp:BoundField HeaderText="Txn/Cheq/UPI No" DataField="TxnNo" />
                <asp:BoundField HeaderText="Bank Name" DataField="BankName" />
                <asp:BoundField HeaderText="Txn/Cheq/UPI Date" DataField="TxnDate" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField HeaderText="Settlement Date"
                    DataField="SettlementOrForeclosureDate"
                    DataFormatString="{0:dd-MMM-yyyy HH:mm}" />
  

                                <asp:TemplateField HeaderText="Record Status">
    <ItemTemplate>
        <asp:Label ID="lblRecordStatus" runat="server"
            Text='<%# Eval("RecordStatus") %>'
            CssClass="badge rounded-pill px-3 py-2">
        </asp:Label>
    </ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Closure Status">
    <ItemTemplate>
        <asp:Label ID="lblClosureStatus" runat="server"
            Text='<%# Eval("SettlementOrForeclosureStatus") %>'
            CssClass="badge rounded-pill px-3 py-2">
        </asp:Label>
    </ItemTemplate>
</asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
</div>


    </div>

             <script>
                 function showAlert(type, message) {
                     Swal.fire({
                         icon: type,          // success | warning | error | info
                         title: message,
                         confirmButtonText: "OK"
                     });
                 }
             </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server" />
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server" />
