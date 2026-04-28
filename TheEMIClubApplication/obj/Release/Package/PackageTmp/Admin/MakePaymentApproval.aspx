<%@ Page Title="Make Payment Approval" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="MakePaymentApproval.aspx.cs" Inherits="TheEMIClubApplication.MasterPage.MakePaymentApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <style>
         .form-group label {
     font-weight: 500;
 }

 label::before,
 label::after {
     content: none !important;
 }

        /* Table Styling */
        .table {
            border-radius: 8px;
            overflow: hidden;
            font-size: 0.85rem;
            table-layout: auto;
            width: 100%;
        }
        .table thead th {
            white-space: nowrap;
            background: #007bff;
            color: #fff;
            text-align: center;
            font-weight: 600;
            padding: 8px;
        }
        .table tbody td {
            white-space: nowrap;
            text-overflow: ellipsis;
            overflow: hidden;
            vertical-align: middle;
            padding: 6px 8px;
        }
        .table tbody tr:hover {
            background-color: #f1f5ff;
        }

        /* Image Zoom */
        .image-hover-wrapper {
            position: relative;
            display: inline-block;
        }
        .hover-thumbnail {
            width: 150px;
            height: 150px;
            object-fit: cover;
            border: 1px solid #ccc;
            cursor: zoom-in;
        }
        .zoom-center {
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 400px;
            height: 400px;
            z-index: 1050;
            display: none;
            border: 1px solid #ddd;
            background: #fff;
            box-shadow: 0 0 20px rgba(0,0,0,0.4);
        }
        .zoomed-image {
            width: 100%;
            height: 100%;
            object-fit: contain;
        }
    </style>

    <section class="content">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container-fluid">

            <!-- Payment Approval Card -->
            <div class="card shadow mb-4" runat="server" id="DivMakePayment" style="display:none;">
                <div class="card-header bg-primary text-white">
                    <h5 class="mb-0"><i class="fas fa-credit-card mr-2"></i> Make Payment Approval</h5>
                </div>
                <div class="card-body">
                    <asp:HiddenField ID="hfRID" runat="server" />
                    <asp:Label ID="lblRID" runat="server" Visible="false"></asp:Label>

                    <!-- Row 1 -->
                    <div class="form-row">
                        <div class="form-group col-md-3">
                            <label>Customer Code</label>
                            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>
                        <div class="form-group col-md-3">
                            <label>Loan Code</label>
                            <asp:TextBox ID="txtLoanCode" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>
                        <div class="form-group col-md-3">
                            <label>EMI Amount</label>
                            <asp:TextBox ID="txtEMIAmount" runat="server" CssClass="form-control" ReadOnly="true" Text="0" />
                        </div>
                        <div class="form-group col-md-3">
                            <label>Monthly EMI Amount</label>
                            <asp:TextBox ID="txtMonthlyEMIAmount" runat="server" CssClass="form-control" ReadOnly="true" Text="0" />
                        </div>
                    </div>
                         <!-- Row 2 -->
     <div class="form-row">
         <div class="form-group col-md-3">
             <label>InterestAmt</label>
             <asp:TextBox ID="txtInterestAmt" runat="server" CssClass="form-control" ReadOnly="true" />
         </div>
         <div class="form-group col-md-3">
             <label>Fine</label>
             <asp:TextBox ID="txtFine" runat="server" CssClass="form-control" ReadOnly="true" />
         </div>
         <div class="form-group col-md-3">
             <label>Total EMI Amount</label>
             <asp:TextBox ID="txtTotalEMIAmt" runat="server" CssClass="form-control" ReadOnly="true" Text="0" />
         </div>
         <div class="form-group col-md-3">
             <label>Paid Amount</label>
             <asp:TextBox ID="txtPaidAmount" runat="server" CssClass="form-control" ReadOnly="true" Text="0" />
         </div>
     </div>
                    <!-- Row 3 -->
                    <div class="form-row">
                        <div class="form-group col-md-3">
                            <label>No. of Paid EMI</label>
                            <asp:TextBox ID="txtNoofpaidEMI" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>
                        <div class="form-group col-md-3">
                            <label>Transaction No</label>
                            <asp:TextBox ID="txtTxnNo" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>
                        <div class="form-group col-md-3">
                            <label>Active Status</label>
                            <asp:DropDownList ID="ddlActiveStatus" runat="server" CssClass="form-control" Enabled="false">
                                <asp:ListItem Text="Active" Value="ACTIVE" />
                                <asp:ListItem Text="Inactive" Value="INACTIVE" />
                            </asp:DropDownList>
                        </div>
                        <div class="form-group col-md-3">
                            <label>Record Status</label>
                            <asp:DropDownList ID="ddlRecordStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Pending" Value="PENDING" />
                                <asp:ListItem Text="Approved" Value="APPROVED" />
                                <asp:ListItem Text="Rejected" Value="REJECTED" />
                            </asp:DropDownList>
                        </div>

                              <div class="form-group col-md-12">
          <label>Remarks</label>
           <asp:TextBox ID="txtremarks" runat="server" CssClass="form-control"  TextMode="MultiLine" line="3" />
      </div>
                        <asp:HiddenField ID="hfdretailercode" runat="server" />
                    </div>

                    <!-- Receipt Image -->
                    <div class="form-group">
                        <label>Receipt Image</label>
                        <div class="image-hover-wrapper">
                            <asp:Image ID="imgReceiptPreview" runat="server" Height="120" Width="120" CssClass="img-thumbnail hover-thumbnail" />
                            <div class="zoom-center">
                                <img runat="server" id="Invoiceimgzoom" class="zoomed-image" />
                            </div>
                        </div>
                        <div class="mt-2">
                            <asp:LinkButton ID="btnDownloadReceipt" runat="server" Text="Download" CssClass="btn btn-sm btn-outline-primary" OnClick="btnDownloadReceipt_Click" />
                        </div>
                    </div>

                    <!-- Buttons -->
                    <div class="text-center mt-3">
                        <span id="lblErrormsg" runat="server" class="text-danger d-block mb-2"></span>
                        <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="btn btn-success mr-2" OnClick="btnSave_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Close" CssClass="btn btn-secondary" OnClick="btnClear_Click" />
                    </div>
                </div>
            </div>

            <!-- Search Card -->
            <div class="card shadow mb-4" runat="server" id="DivSearchMakePayment">
                <div class="card-header bg-info text-white">
                    <h5 class="mb-0"><i class="fas fa-search mr-2"></i> Search Make Payment Details</h5>
                </div>
                <div class="card-body">
                    <div class="form-row align-items-end">
       <div class="form-group col-md-3">
     <label>Type</label>
     <asp:DropDownList ID="ddlCutomerType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCutomerType_SelectedIndexChanged">
          <asp:ListItem Text="Retailer" Value="Retailer" />
         <asp:ListItem Text="Customer" Value="Customer" /> 
     </asp:DropDownList>
     </div>
                        <div class="form-group col-md-3">
                            <label>Criteria</label>
                            <asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="form-control">
                   <%--             <asp:ListItem Text="All" Value="All" />
                                <asp:ListItem Text="Pending" Value="Pending" />
                                <asp:ListItem Text="Approved" Value="Approved" />
                                <asp:ListItem Text="Rejected" Value="Rejected" />
                                <asp:ListItem Text="CustomerCode" Value="CustomerCode" />
                                <asp:ListItem Text="LoanCode" Value="LoanCode" />--%>
                            </asp:DropDownList>
                        </div>
                        <div class="form-group col-md-3">
                            <label>Value</label>
                            <asp:TextBox ID="txtSearchValue" runat="server" CssClass="form-control" placeholder="Enter Search Value" />
                        </div>
                        <div class="form-group col-md-3">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary btn-block" OnClick="btnSearch_Click" />
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
                            AutoGenerateColumns="false" DataKeyNames="RID,CustomerCode,LoanCode"
                            AllowPaging="true" PageSize="10"
                            OnRowCommand="gvMakePayment_RowCommand"
                            OnDataBound="gvMakePayment_DataBound"
                            OnPageIndexChanging="gvMakePayment_PageIndexChanging"
                            PagerSettings-Mode="Numeric"
                            PagerSettings-Position="Bottom"
                            PagerStyle-HorizontalAlign="Center"
                            PagerStyle-CssClass="pagination-container"
                            UseAccessibleHeader="true" GridLines="None">

                            <Columns>
                                <asp:BoundField DataField="RetailerCode" HeaderText="Retailer Code" />
                                <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                                <asp:BoundField DataField="LoanCode" HeaderText="Loan Code" />
                                <asp:BoundField DataField="UTRNumber" HeaderText="Txn No" />
                                <asp:BoundField DataField="DivideNo" HeaderText="No. of EMI" />
                                <asp:BoundField DataField="Monthly EMI" HeaderText="Monthly EMI" />
                                 <asp:BoundField DataField="EMIAmount" HeaderText="EMI Amt" />
                                 <asp:BoundField DataField="InterestAmt" HeaderText="InterestAmt" />
                                <asp:BoundField DataField="Fine" HeaderText="Fine" />
                                <asp:BoundField DataField="Total EMIAmt" HeaderText="Total EMIAmt" />
                                <asp:BoundField DataField="ActiveStatus" HeaderText="Active Status" />
                                <asp:BoundField DataField="RecordStatus" HeaderText="Record Status" />                               
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server"
                                            CommandName="EditRow"
                                            CommandArgument='<%# Eval("RID") %>'
                                            Text="Edit"
                                            CssClass="btn btn-sm btn-primary"
                                            Visible='<%# Eval("RecordStatus").ToString().ToUpper() != "APPROVED" %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:Label ID="lblMakePaymentRecordCount" runat="server" CssClass="mt-2 font-weight-bold"></asp:Label>
                    </div>
                </div>
            </div>

        </div>
    </section>

    <!-- Modals (Error & Success) -->
    <div id="ErrorPage" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content border-danger">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title">Error</h5>
                    <button type="button" class="close text-white" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <span id="lblerror" class="text-danger font-weight-bold"></span>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-outline-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div id="MyPopups" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content border-success">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title">Confirmation</h5>
                    <button type="button" class="close text-white" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <span id="lblMessages" class="text-success"></span><br />
                    <span id="lblName" class="text-primary"></span>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-success">Ok</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Scripts -->
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/js/bootstrap.bundle.min.js"></script>

                 <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" rel="stylesheet"/>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
	

    <script>
        function ShowPopup(username, messages) {
            $("#MyPopups #lblName").html(username);
            $("#MyPopups #lblMessages").html(messages);
            $("#MyPopups").modal("show");
        }
        function ShowError(ErrorMessages) {
            $("#ErrorPage #lblerror").html(ErrorMessages);
            $("#ErrorPage").modal("show");
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
