<%@ Page Title="Pending EMI Report" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="EmiPendingReports.aspx.cs" Inherits="TheEMIClubApplication.Reports.EmiPendingReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <div class="container-fluid mt-4">

                <div class="card shadow-sm mb-4">
            <div class="card-header bg-warning text-white d-flex align-items-center">
                <i class="fas fa-file-alt fa-lg me-2"></i>
                <h5 class="mb-0">Report Type</h5>
            </div>
        <div class="card-body ce d-flex justify-content-center align-items-center">
                <div class="col-md-4 mb-3">
                    <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                        <asp:ListItem Text="-- Select Report Type --" Value="" />
                        <asp:ListItem Text="Pending EMI Report" Value="PendingEMI" />
                        <asp:ListItem Text="Make Payment Report" Value="MakePayment" />
     
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <!-- Search Criteria Card -->
                <asp:Panel ID="pnlemiSearchCriteria" runat="server" Visible="false">
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-info text-white d-flex align-items-center">
                <i class="fas fa-search fa-lg me-2"></i>
                <h5 class="mb-0"> Search Criteria</h5>
            </div>
            <div class="card-body">
                <div class="row">
                    <!-- Customer Name -->
                    <div class="col-md-3 mb-3">
                        <label class="form-label"><i class="fas fa-user me-1"></i> Customer Name</label>
                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" Placeholder="Enter Customer Name"></asp:TextBox>
                    </div>
                    <!-- Customer Phone -->
                    <div class="col-md-3 mb-3">
                        <label class="form-label"><i class="fas fa-phone me-1"></i> Customer Phone</label>
                        <asp:TextBox ID="txtCustomerPhone" runat="server" CssClass="form-control" Placeholder="Enter Phone No."></asp:TextBox>
                    </div>
                    <!-- Retailer Code -->
                    <div class="col-md-3 mb-3">
                        <label class="form-label"><i class="fas fa-store me-1"></i> Retailer Code</label>
                         <asp:DropDownList ID="ddluser" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                    <!-- Month & Year -->
            <div class="col-md-3 mb-3">
    <label class="form-label"><i class="fas fa-calendar-alt me-1"></i> Select Date</label>
    <asp:TextBox ID="txtEmiDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
</div>
                </div>
                <div class="text-end mt-3">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary me-2" Text="Search" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-secondary" Text="Clear" OnClick="btnClear_Click" />
                </div>
            </div>
        </div>

        <!-- Results Card -->
        <div class="card shadow-sm">
            <div class="card-header bg-primary text-white d-flex align-items-center">
                <i class="fas fa-file-invoice-dollar fa-lg me-2"></i>
                <h5 class="mb-0"> Pending EMI Report Results</h5>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <asp:GridView ID="gvPendingEMI" runat="server" CssClass="table table-striped table-bordered table-hover align-middle"
                        AutoGenerateColumns="False" EmptyDataText="⚠ No pending EMI records found.">
                        <Columns>
                            <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                            <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                            <asp:BoundField DataField="CustomerPhone" HeaderText="Phone" />
                            <asp:BoundField DataField="RetailerCode" HeaderText="Retailer Code" />
                            <asp:BoundField DataField="RetailerName" HeaderText="Retailer Name" />
                            <asp:BoundField DataField="Pending_EMI_Count" HeaderText="Pending EMI" />
                            <asp:BoundField DataField="EMI_Per_Installment" HeaderText="EMI/Installment" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="Total_Pending_Amount" HeaderText="Total Pending Amount" DataFormatString="{0:N2}" />
                            <asp:BoundField DataField="Last_Pending_EMI_Date" HeaderText="Last Pending EMI Date" DataFormatString="{0:dd-MMM-yyyy}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
                    </asp:Panel>


                    <!-- Search Card -->
           <asp:Panel ID="panelmakepayment" runat="server" Visible="false">
            <div class="card shadow mb-4" runat="server" id="DivSearchMakePayment">
                <div class="card-header bg-info text-white">
                    <h5 class="mb-0"><i class="fas fa-search mr-2"></i> Search Make Payment Details</h5>
                </div>
                <div class="card-body">
                    <div class="form-row align-items-end">
                        <div class="form-group col-md-3">
                            <label>Criteria</label>
                            <asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="form-control">
                                <asp:ListItem Text="All" Value="All" />
                                <asp:ListItem Text="Pending" Value="Pending" />
                                <asp:ListItem Text="Approved" Value="Approved" />
                                <asp:ListItem Text="Rejected" Value="Rejected" />
                                <asp:ListItem Text="CustomerCode" Value="CustomerCode" />
                                <asp:ListItem Text="LoanCode" Value="LoanCode" />
                            </asp:DropDownList>
                        </div>
                        <div class="form-group col-md-3">
                            <label>Value</label>
                            <asp:TextBox ID="txtSearchValue" runat="server" CssClass="form-control" placeholder="Enter Search Value" />
                        </div>
                        <div class="form-group col-md-3">
                            <asp:Button ID="btnmakepayment" runat="server" Text="Search" CssClass="btn btn-primary btn-block" OnClick="btnmakepayment_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Records Table -->
            <div class="card shadow border-0">
                <div class="card-header bg-light">
                    <h6 class="mb-0 text-primary font-weight-bold"><i class="fas fa-table mr-2"></i> Make Payment Records</h6>
                </div>
                <div class="card-body">
                    <span id="spnMessage" runat="server"></span>
                    <div class="table-container">
                        <asp:GridView ID="gvMakePayment" runat="server"
                            CssClass="table table-bordered table-striped table-hover mb-0"
                            AutoGenerateColumns="false" DataKeyNames="CustomerCode,LoanCode"
                            AllowPaging="true" PageSize="10"
                            OnPageIndexChanging="gvMakePayment_PageIndexChanging"
                            PagerSettings-Mode="Numeric"
                            PagerSettings-Position="Bottom"
                            PagerStyle-HorizontalAlign="Center"
                            PagerStyle-CssClass="pagination-container"
                            UseAccessibleHeader="true" GridLines="None"
                            EmptyDataText="No Record Found!">

                            <Columns>
                                <asp:BoundField DataField="CustomerCode" HeaderText="CustomerCode" />
                                <asp:BoundField DataField="RetailerCode" HeaderText="RetailerCode" />
                                <asp:BoundField DataField="LoanCode" HeaderText="LoanCode" />
                                 <asp:BoundField DataField="TotalPaidAmount" HeaderText="TotalPaidAmount" />
                                 <asp:BoundField DataField="TotalEMIAmount" HeaderText="TotalEMIAmount" />
                                 <asp:BoundField DataField="TotalEMIPaid" HeaderText="TotalEMIPaid" />
                                 <asp:BoundField DataField="TotalTransactions" HeaderText="TotalTransactions" />
                              <%--  <asp:BoundField DataField="txnNumber" HeaderText="Txn No" />
                                <asp:BoundField DataField="DivideNo" HeaderText="No. of EMI" />
                                <asp:BoundField DataField="PaidAmount" HeaderText="Paid Amount" />
                                <asp:BoundField DataField="EMIAmt" HeaderText="EMI Amt" />
                                <asp:BoundField DataField="ActiveStatus" HeaderText="Active Status" />
                                <asp:BoundField DataField="RecordStatus" HeaderText="Record Status" />--%>
                                
                      
                            </Columns>
                        </asp:GridView>
                       
                    </div>
                </div>
            </div>
               </asp:Panel>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
    <!-- Font Awesome & Bootstrap JS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
