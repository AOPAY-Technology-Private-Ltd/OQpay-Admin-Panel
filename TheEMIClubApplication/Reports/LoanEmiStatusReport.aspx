<%@ Page Title="Loan EMI Status Report" Language="C#"
    MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="LoanEmiStatusReport.aspx.cs"
    Inherits="TheEMIClubApplication.Reports.LoanEmiStatusReport" %>

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

        .kpi-icon {
            font-size: 44px;
            opacity: .9;
        }

        .kpi-title {
            font-size: 13px;
            text-transform: uppercase;
            letter-spacing: 1px;
        }

        .kpi-value {
            font-size: 28px;
            font-weight: 700;
        }

        .table-modern th {
            background-color: #f8fafc;
            font-weight: 600;
        }

        .table-modern tr:hover {
            background-color: #f1f5f9;
        }

        /* 🔹 Force GridView columns to single line */
.table-modern th,
.table-modern td {
    white-space: nowrap;   /* no line break */
    vertical-align: middle;
}

/* 🔹 Allow horizontal scroll instead of wrapping */
.table-responsive {
    overflow-x: auto;
}

    </style>

    <div class="container-fluid px-4 py-4 loan-report">

        <!-- HEADER -->
        <div class="mb-4">
            <h3 class="fw-bold">
                <i class="bi bi-cash-stack text-primary me-2"></i>
                Loan EMI & Disbursed Report
            </h3>
            <small class="text-muted">Reports / Loan EMI Status /Disbursed</small>
        </div>

        <!-- FILTER -->
        <div class="card filter-card mb-4">
            <div class="card-header gradient-primary text-white">
                <i class="bi bi-funnel-fill me-2"></i> Filter Criteria
            </div>
            <div class="card-body">
                <div class="row g-3">
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
                        <label class="form-label fw-semibold">From Date</label>
                        <asp:TextBox ID="txtFromDate" runat="server"
                            TextMode="Date"
                            CssClass="form-control form-control-lg" />
                    </div>

                    <div class="col-md-3">
                        <label class="form-label fw-semibold">To Date</label>
                        <asp:TextBox ID="txtToDate" runat="server"
                            TextMode="Date"
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

         <!-- ================= KPI CARDS ================= -->
        <div class="row g-4 mb-4">

            <div class="col-md-4">
                <div class="kpi-card bg-primary text-white">
                    <i class="bi bi-journal-text kpi-icon"></i>
                    <div>
                        <div class="kpi-title">Total Loans</div>
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
                        <div class="kpi-title">Pending EMI Amount</div>
                        <div class="kpi-value">
                            ₹ <asp:Label ID="lblPendingAmount" runat="server" Text="0.00" />
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <!-- MAIN GRID -->
        <div class="card shadow-lg">
            <div class="card-header gradient-dark text-white">
                <i class="bi bi-table me-2"></i> Loan EMI Details
            </div>

            <div class="table-responsive">
            <asp:GridView ID="gvLoanEmiReport" runat="server"
    CssClass="table table-hover table-bordered table-modern align-middle mb-0"
    AutoGenerateColumns="False"
    DataKeyNames="LoanCode"
    OnRowCommand="gvLoanEmiReport_RowCommand"
    EmptyDataText="No loan records found">

    <Columns>

         
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="btnViewEMI"
                    runat="server"
                    Text=""
                    CssClass="btn btn-sm btn-outline-primary"
                    CommandName="ViewEMI"
                    CommandArgument='<%# Eval("LoanCode") %>'>
                    <i class="bi bi-eye"></i> 
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>

               <asp:BoundField HeaderText="RetailerCode" DataField="RetailerCode" />
        <asp:BoundField HeaderText="Customer" DataField="CustomerName" />


        <asp:BoundField HeaderText="Loan Code" DataField="LoanCode" />

  
        <asp:BoundField HeaderText="Product" DataField="ProductName" />

        <asp:BoundField HeaderText="Loan Amount" DataField="LoanAmount" DataFormatString="{0:N2}" />
                <asp:BoundField HeaderText="Principal + Interest Amt." DataField="LoanAmountWithInt" DataFormatString="{0:N2}" />
        <asp:BoundField HeaderText="EMI Amount" DataField="EMIAmount" DataFormatString="{0:N2}" />
        <asp:BoundField HeaderText="Total EMI" DataField="TotalInstallment" />
        <asp:BoundField HeaderText="Paid EMI" DataField="PaidInstallment" />
        <asp:BoundField HeaderText="Remaining EMI" DataField="RemainingInstallment" />
        <asp:BoundField HeaderText="Total Paid Amt." DataField="TotalPaidAmount" />
        <asp:BoundField HeaderText="Pending Amount" DataField="PendingEMIAmount" DataFormatString="{0:N2}" />
        <asp:BoundField HeaderText="Loan Date" DataField="CreatedAt" DataFormatString="{0:dd-MMM-yyyy}" />
        <asp:BoundField HeaderText="Next EMI Date" DataField="NextEMIDate" DataFormatString="{0:dd-MMM-yyyy}" />
        <asp:BoundField HeaderText="Status" DataField="LoanStatus" />



    </Columns>

</asp:GridView>

            </div>
        </div>

    </div>

    <!-- EMI DETAILS MODAL -->
    <div class="modal fade" id="emiModal" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-xl modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">

                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">
                        <i class="bi bi-receipt-cutoff me-2"></i> Loan EMI Details
                    </h5>
                    <button type="button" class="btn-close btn-close-white"
                        data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body">

                    <div class="row mb-3">
                        <div class="col-md-4">
                            <strong>Loan Code:</strong>
                            <asp:Label ID="lblModalLoanCode" runat="server"
                                CssClass="text-primary fw-semibold" />
                        </div>
                        <div class="col-md-4">
                            <strong>Total Paid:</strong>
                            <asp:Label ID="lblpaid" runat="server"  CssClass="fw-bold text-success"/>
                        </div>
                        <div class="col-md-4">
                            <strong>Total Pending:</strong>
                            ₹ <asp:Label ID="lblpending" runat="server"  CssClass="fw-bold text-danger" />
                        </div>
                    </div>

                    <asp:GridView ID="gvEmiDetails" runat="server"
                        CssClass="table table-bordered table-hover align-middle"
                        AutoGenerateColumns="False"
                        EmptyDataText="No EMI details available"
                        OnRowDataBound="gvEmiDetails_RowDataBound">

                        <Columns>
                            <asp:BoundField HeaderText="EMI No" DataField="SrNo" />
                            <asp:BoundField HeaderText="Receipt No" DataField="ReceiptNo" />
                            <asp:BoundField HeaderText="EMI Due Date" DataField="EMI_DueDate"
                                DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField HeaderText="Payment Date" DataField="PaymentDate"
                                DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField HeaderText="EMI Amount" DataField="EMIAmount"
                                DataFormatString="{0:N2}" />
                            <asp:BoundField HeaderText="Fine" DataField="Fine"
                                DataFormatString="{0:N2}" />
                            <asp:BoundField HeaderText="Status" DataField="EMIStatus" />
                            <asp:BoundField HeaderText="Payment Mode" DataField="PaymentMode" />
                        </Columns>

                    </asp:GridView>

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary"
                        data-bs-dismiss="modal">Close</button>
                </div>

            </div>
        </div>
    </div>

    <script>
        function openEmiModal() {
            var modal = new bootstrap.Modal(document.getElementById('emiModal'));
            modal.show();
        }
    </script>
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
