<%@ Page Title="Payout Payment Approval" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="PayoutPaymentApproval.aspx.cs"
    Inherits="TheEMIClubApplication.Admin.PayoutPaymentApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <style>
        .form-group label {
            font-weight: 500;
        }

        label::before, label::after {
            content: none !important;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />

    <div class="container-fluid mt-4">

        <!-- 🔹 Filter Section -->
        <div class="card shadow-lg border-0 mb-4">
            <div class="card-header bg-gradient-primary text-white d-flex justify-content-between align-items-center">
                <h5 class="mb-0"><i class="fas fa-search mr-2"></i>Search Payouts & HoldAmt.</h5>
            </div>
            <div class="card-body">
                <div class="form-row">

                    <div class="form-group col-md-3">
                        <label for="ddlApproveType">Select Approve Type</label>
                        <asp:DropDownList ID="ddlApproveType" runat="server" CssClass="form-control">
                            <asp:ListItem Text="-- Select Type --" Value=""></asp:ListItem>
                            <asp:ListItem Text="Payout Amt." Value="Payout"></asp:ListItem>
                            <asp:ListItem Text="Hold Amt." Value="HoldAmt"></asp:ListItem>
                        </asp:DropDownList>


                    </div>
                    <!-- Retailer Dropdown -->
                    <div class="form-group col-md-3">
                        <label for="ddlRetailer">Retailer Code</label>
                        <asp:DropDownList ID="ddlRetailer" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>

                    <!-- From Date -->
                    <div class="form-group col-md-2">
                        <label for="txtFromDate">From Date</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                            </div>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>

                    <!-- To Date -->
                    <div class="form-group col-md-2">
                        <label for="txtToDate">To Date</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                            </div>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Search Button -->
                    <div class="form-group col-md-2 d-flex align-items-end">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-block"
                            Text="Search" OnClick="btnSearch_Click" />
                        <span id="spnMessage" runat="server"></span><span id="spnpayout" runat="server" style="color: red; font-size: larger; position: page"></span>
                    </div>

                </div>
            </div>
        </div>

        <!-- 🔹 Results Section inside UpdatePanel -->

        <asp:Panel ID="pnlResults" runat="server" Visible="false">
            <div class="card shadow-lg border-0">
                <div class="card-header bg-gradient-secondary text-white d-flex justify-content-between align-items-center">
                    <h5 class="mb-0"><i class="fas fa-table mr-2"></i>Search Results</h5>
                </div>
                <div class="card-body table-responsive">
                    <asp:GridView ID="gvResults" runat="server" CssClass="table table-striped table-hover table-bordered"
                        AutoGenerateColumns="False" EmptyDataText="No records found."
                        AllowPaging="true" PageSize="10" OnPageIndexChanging="gvResults_PageIndexChanging"
                        GridLines="None" OnRowCommand="gvResults_RowCommand"
                        DataKeyNames="ReferenceID,RegistrationId,PayoutAmount,ServiceChargeAmt,GSTAmt,TransferAmt,PaymentMode,PaymentDate,AccountHolder,
                        DepositBankName,BranchName,IFSCCode,BeneId,MobileNo,EmailID ">

                        <HeaderStyle CssClass="thead-dark" />

                        <Columns>

                            <asp:TemplateField HeaderText="Select">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="toggleSelectAll(this)" Checked="true" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" Checked="true" CssClass="rowCheckbox" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:BoundField DataField="ReferenceID" HeaderText="Reference ID" />
                            <asp:BoundField DataField="RegistrationId" HeaderText="Registration ID" />    
                             <asp:BoundField DataField="PayoutAmount" HeaderText="TransferAmt" DataFormatString="₹{0:N2}" />
                             <asp:BoundField DataField="ServiceChargeAmt" HeaderText="ServiceChargeAmt" DataFormatString="₹{0:N2}" />
                             <asp:BoundField DataField="GSTAmt" HeaderText="GSTAmt" DataFormatString="₹{0:N2}" />
                            <asp:BoundField DataField="TransferAmt" HeaderText="Amount" DataFormatString="₹{0:N2}" />
                            <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" />
                            <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="AccountHolder" HeaderText="Account Holder" />
                            <asp:BoundField DataField="MobileNo" HeaderText="MobileNo" />
                            <asp:BoundField DataField="EmailID" HeaderText="EmailID" />
                            <asp:BoundField DataField="DepositBankName" HeaderText="Bank Name" />
                            <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                            <asp:BoundField DataField="IFSCCode" HeaderText="IFSC Code" />
                            <asp:BoundField DataField="BranchCode_ChecqueNo" HeaderText="Branch Code / Cheque No" />
                            <asp:BoundField DataField="BeneId" HeaderText="Accounts No" />
                            <asp:BoundField DataField="ApprovedStatus" HeaderText="Status" />

                            <%--        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:Button ID="btnApprove" runat="server" 
                    Text="Approve/Reject" 
                    CommandName="ApprovePayout"
                    CommandArgument="<%# Container.DataItemIndex %>"
                    CssClass="btn btn-primary btn-sm" />
            </ItemTemplate>
        </asp:TemplateField>--%>
                          
                        </Columns>

                        <PagerStyle CssClass="pagination-outer" HorizontalAlign="Center" />
                    </asp:GridView>
                    <div class="col-md-9 form-group">
                        <asp:Label ID="Label20" runat="server" CssClass="font-weight-bold" AssociatedControlID="txtpayoutremaeksremarks">
      Remarks <span style="color:red">*</span>
                        </asp:Label>
                        <asp:TextBox ID="txtpayoutremaeksremarks" runat="server" CssClass="form-control" TextMode="MultiLine" />
                        <asp:RequiredFieldValidator
                            ID="rfvRemarks"
                            runat="server"
                            ControlToValidate="txtpayoutremaeksremarks"
                            ErrorMessage="Remarks are required."
                            ForeColor="Red"
                            Display="Dynamic"
                            ValidationGroup="remarksvalidation" />


                    </div>
                    <asp:Button ID="btnApproveSelected" runat="server" Text="Approve Selected"
                        CssClass="btn btn-success mb-3" OnClick="btnApproveSelected_Click" ValidationGroup="remarksvalidation" />

                      <asp:Button ID="btnRejectSelected" runat="server" Text="Reject Selected"
      CssClass="btn btn-danger mb-3" OnClick="BtnRejectSelected_Click" ValidationGroup="remarksvalidation" />

                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="holdingpanel" runat="server" Visible="false">
            <div class="card shadow-lg border-0">
                <div class="card-header bg-gradient-secondary text-white d-flex justify-content-between align-items-center">
                    <h5 class="mb-0"><i class="fas fa-table mr-2"></i>Pending Hold Amounts</h5>
                </div>
                <div class="card-body table-responsive">
                    <asp:GridView ID="gvHoldingamt" runat="server" CssClass="table table-striped table-hover table-bordered"
                        AutoGenerateColumns="False" EmptyDataText="No records found."
                        AllowPaging="true" PageSize="10"
                        OnPageIndexChanging="gvHoldingamt_PageIndexChanging"
                        OnRowCommand="gvHoldingamt_RowCommand"
                        DataKeyNames="Amt_Transfer_TransID,TransferAmt,TransactionStatus">

                        <HeaderStyle CssClass="thead-dark" />
                        <Columns>
                            <asp:BoundField DataField="Amt_Transfer_TransID" HeaderText="Transaction ID" />
                            <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date"
                                DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:BoundField DataField="TransferFrom" HeaderText="From" />
                            <asp:BoundField DataField="TransferTo" HeaderText="To" />
                            <asp:BoundField DataField="TransferAmt" HeaderText="Amount"
                                DataFormatString="₹{0:N2}" />
                            <asp:BoundField DataField="Remark" HeaderText="Remark" />
                            <asp:BoundField DataField="TransactionStatus" HeaderText="Status" />


                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:Button ID="btnApprove" runat="server"
                                        Text="Approve/Reject"
                                        CommandName="ApproveHoldAmt"
                                        CommandArgument="<%# Container.DataItemIndex %>"
                                        CssClass="btn btn-primary btn-sm" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <PagerStyle CssClass="pagination-outer" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>

        <div class="modal fade" id="failedPayoutsModal" tabindex="-1" aria-labelledby="failedPayoutsModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header bg-danger text-white">
                        <h5 class="modal-title" id="failedPayoutsModalLabel">Failed Payouts</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <ul id="failedPayoutsList"></ul>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>



        <div id="approvalModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <!-- Modal content-->
                <div class="modal-content shadow-sm border-0">
                    <div class="modal-header bg-primary text-white" style="border-top-left-radius: 10px; border-top-right-radius: 10px;">
                        <h5 class="modal-title">Approve / Reject Payout</h5>
                        <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <asp:HiddenField ID="hdnRefrenceID" runat="server" />

                        <!-- Bootstrap grid for two columns layout -->
                        <div class="row">
                            <!-- Column 1 -->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="font-weight-bold text-primary">Reference ID:</label>
                                    <span id="spnRefId" runat="server" class="d-block text-dark"></span>
                                </div>
                                <div class="form-group">
                                    <label class="font-weight-bold text-primary">Registration ID:</label>
                                    <span id="spnRegId" runat="server" class="d-block text-dark"></span>
                                </div>

                                <div class="form-group">
                                    <label class="font-weight-bold text-primary">PayoutAmount:</label>
                                    <span id="spnPayoutAmount" runat="server" class="d-block text-dark"></span>
                                </div>

                                <div class="form-group">
                                    <label class="font-weight-bold text-primary">ServiceChargeAmt:</label>
                                    <span id="spnServiceChargeAmt" runat="server" class="d-block text-dark"></span>
                                </div>

                                <div class="form-group">
                                    <label class="font-weight-bold text-primary">GSTAmt:</label>
                                    <span id="spnGSTAmt" runat="server" class="d-block text-dark"></span>
                                </div>

                                <div class="form-group">
                                    <label class="font-weight-bold text-primary">Amount:</label>
                                    <span id="spnAmount" runat="server" class="d-block text-dark"></span>
                                </div>


                                <div class="form-group">
                                    <label class="font-weight-bold text-primary">Payment Mode:</label>
                                    <span id="spnPayMode" runat="server" class="d-block text-dark"></span>
                                </div>

                                <div class="form-group">
                                    <label class="font-weight-bold text-primary">Account No:</label>
                                    <span id="spnAccountNo" runat="server" class="d-block text-dark"></span>
                                </div>
                            </div>

                            <!-- Column 2 -->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="font-weight-bold text-primary">Payment Date:</label>
                                    <span id="spnPayDate" runat="server" class="d-block text-dark"></span>
                                </div>
                                <div class="form-group">
                                    <label class="font-weight-bold text-primary">Account Holder:</label>
                                    <span id="spnAccHolder" runat="server" class="d-block text-dark"></span>
                                </div>
                                <div class="form-group">
                                    <label class="font-weight-bold text-primary">Bank Name:</label>
                                    <span id="spnBank" runat="server" class="d-block text-dark"></span>
                                </div>
                                <div class="form-group">
                                    <label class="font-weight-bold text-primary">Branch Name:</label>
                                    <span id="spnBranch" runat="server" class="d-block text-dark"></span>
                                </div>
                                <div class="form-group">
                                    <label class="font-weight-bold text-primary">IFSC Code:</label>
                                    <span id="spnIFSC" runat="server" class="d-block text-dark"></span>
                                </div>
                            </div>
                        </div>

                        <!-- Status and Remarks Fields -->
                        <div class="form-group">
                            <label class="font-weight-bold text-primary" for="ddlApprovalStatus">Status</label>
                            <asp:DropDownList ID="ddlApprovalStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
                                <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold text-primary" for="txtApprovalRemarks">Remarks</label>
                            <asp:TextBox ID="txtApprovalRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <asp:Button ID="btnSubmitApproval" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnSubmitApproval_Click" />
                        <button type="button" data-dismiss="modal" class="btn btn-secondary">Cancel</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- HoldAmt Approval Modal -->
        <div id="holdAmtApprovalModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <!-- Modal content-->
                <div class="modal-content shadow-sm border-0">
                    <div class="modal-header bg-info text-white" style="border-top-left-radius: 10px; border-top-right-radius: 10px;">
                        <h5 class="modal-title">Approve / Reject Hold Amount</h5>
                        <button type="button" class="close text-white" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <asp:HiddenField ID="hdnHoldAmtId" runat="server" />

                        <div class="row">
                            <!-- Column 1 -->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="font-weight-bold text-info">Transaction ID:</label>
                                    <span id="spnHoldTxnId" runat="server" class="d-block text-dark"></span>
                                </div>
                                <div class="form-group">
                                    <label class="font-weight-bold text-info">Transaction Date:</label>
                                    <span id="spnHoldTxnDate" runat="server" class="d-block text-dark"></span>
                                </div>
                                <div class="form-group">
                                    <label class="font-weight-bold text-info">From:</label>
                                    <span id="spnHoldFrom" runat="server" class="d-block text-dark"></span>
                                </div>
                            </div>

                            <!-- Column 2 -->
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="font-weight-bold text-info">To:</label>
                                    <span id="spnHoldTo" runat="server" class="d-block text-dark"></span>
                                </div>
                                <div class="form-group">
                                    <label class="font-weight-bold text-info">Amount:</label>
                                    <span id="spnHoldAmt" runat="server" class="d-block text-dark"></span>
                                </div>
                                <div class="form-group">
                                    <label class="font-weight-bold text-info">Status:</label>
                                    <span id="spnHoldStatus" runat="server" class="d-block text-dark"></span>
                                </div>
                            </div>
                        </div>

                        <!-- Status and Remarks Fields -->
                        <div class="form-group">
                            <label class="font-weight-bold text-info" for="ddlHoldApprovalStatus">Action</label>
                            <asp:DropDownList ID="ddlHoldApprovalStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
                                <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="form-group">
                            <label class="font-weight-bold text-info" for="txtHoldApprovalRemarks">Remarks</label>
                            <asp:TextBox ID="txtHoldApprovalRemarks" runat="server" CssClass="form-control"
                                TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <asp:Button ID="btnSubmitHoldApproval" runat="server" CssClass="btn btn-success"
                            Text="Submit" OnClick="btnSubmitHoldApproval_Click" />
                        <button type="button" data-dismiss="modal" class="btn btn-secondary">Cancel</button>
                    </div>
                </div>
            </div>
        </div>




        <div id="ErrorPage" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Error Message
                        </h5>
                    </div>
                    <div class="modal-body">
                        <span id="lblerror" style="font-family: Georgia, 'Times New Roman', Times, serif; font-size: medium; color: red; font-weight: bold"></span>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal" id="">
                            Close</button>
                    </div>
                </div>
            </div>
        </div>



        <div id="MyPopups" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="background-color: cornflowerblue">

                        <h5 class="modal-title">Confirmation Messages
                        </h5>
                    </div>
                    <div class="modal-body">


                        <span id="lblMessages" style="font-family: Georgia, 'Times New Roman', Times, serif; font-size: medium; color: forestgreen"></span>
                        <br />
                        <span id="lblName" style="color: navy"></span>

                    </div>
                    <div class="modal-footer">
                        <button type="button" data-dismiss="modal" class="btn-success" id="btnsucess" runat="server" causesvalidation="false">
                            Ok</button>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            function ShowPopup(username, messages) {
                $("#MyPopups #lblName").html(username);
                $("#MyPopups #lblMessages").html(messages);
                $("#MyPopups").modal("show");
            }
        </script>
        <%--        <script type="text/javascript">
            function ShowApprovalModal(referenceId, transactionId, status, remarks) {
                // Set hidden field value
                $("#approvalModal #<%= hdnRefrenceID.ClientID %>").val(referenceId);

        // Set transaction ID span
        $("#approvalModal #spnTransactionID").html(transactionId);

        // Set dropdown value (if provided)
        if (status) {
            $("#approvalModal #<%= ddlApprovalStatus.ClientID %>").val(status);
        } else {
            $("#approvalModal #<%= ddlApprovalStatus.ClientID %>").val("");
        }

        // Set remarks textbox
        if (remarks) {
            $("#approvalModal #<%= txtApprovalRemarks.ClientID %>").val(remarks);
        } else {
            $("#approvalModal #<%= txtApprovalRemarks.ClientID %>").val("");
                }

                // Show the modal
                $("#approvalModal").modal("show");
            }
        </script>--%>


        <script type="text/javascript">
            function ShowError(ErrorMessages) {

                $("#ErrorPage #lblerror").html(ErrorMessages);
                $("#ErrorPage").modal("show");
            }
        </script>

     
    </div>

    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js'></script>
    <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/js/bootstrap.bundle.min.js'></script>
    <script type="text/javascript">
        // Rebind modal after UpdatePanel async postback
        Sys.Application.add_load(function () {
            // ensure modal can be triggered after async postback
            $('#approvalModal').modal({ show: false });
        });
    </script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <%--    <script>
        toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-top-right",

            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "6000",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
</script>--%>
    <script>
        function toggleSelectAll(source) {
            var checkboxes = document.querySelectorAll("#<%= gvResults.ClientID %> .rowCheckbox");
            checkboxes.forEach(function (cb) {
                cb.checked = source.checked; // true = select all, false = unselect all
            });
        }

       <%-- function toggleSelectAll(source) {
            var checkboxes = document.querySelectorAll("#<%= gvResults.ClientID %> .rowCheckbox");
            checkboxes.forEach(function (cb) {
                cb.checked = source.checked;
            });
        }--%>
    </script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">

    <!-- Bootstrap 5 JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
