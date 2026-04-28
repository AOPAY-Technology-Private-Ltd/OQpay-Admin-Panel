<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Loansettlement.aspx.cs" Inherits="TheEMIClubApplication.MasterPage.Loansettlement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
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
            font-size: 0.9rem;
            border-radius: 6px;
        }

        /* ✅ Responsive scroll on smaller screens */
        .table-container {
            overflow-x: auto;
        }

        .status-column.Pending {
            background-color: red;
            color: white;
        }

        .status-column.Approved {
            background-color: green;
            color: white;
        }
    </style>

    <style>
        /* === Zoom Overlay === */
        #zoomedImage {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.85);
            text-align: center;
            z-index: 9999;
            cursor: zoom-out;
        }

            #zoomedImage img {
                max-width: 90%;
                max-height: 90%;
                margin-top: 5%;
                border: 4px solid #fff;
                border-radius: 10px;
                box-shadow: 0 0 20px rgba(255, 255, 255, 0.2);
                transition: transform 0.3s ease-in-out;
            }

                #zoomedImage img:hover {
                    transform: scale(1.02);
                }

        /* Add hover style to thumbnails */
        .zoomable {
            cursor: zoom-in;
            transition: transform 0.2s ease-in-out;
        }

            .zoomable:hover {
                transform: scale(1.05);
            }
    </style>

    <style>
        .info-card {
            border: 1px solid #e3e6f0;
            border-radius: 8px;
            padding: 15px;
            margin-bottom: 20px;
            background: #fff;
        }

            .info-card h5 {
                font-weight: 600;
                border-left: 4px solid #007bff;
                padding-left: 10px;
                margin-bottom: 15px;
                color: #333;
            }

        .form-group label {
            font-size: 0.85rem;
            color: #555;
            margin-bottom: 3px;
        }

        .highlight {
            background: #fff3cd;
            font-weight: 600;
            color: #856404;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="card card-default">


        <section class="content">

            <div id="Div_Loansettelmentform" runat="server" visible="false">
                <div class="card-header form-header-bar">
                    <h3 class="card-title">Loan Settlement</h3>
                </div>
                <div class="card-body">
                    <asp:HiddenField ID="hfLoanRID" runat="server" />
                    <asp:HiddenField ID="hfEMIRID" runat="server" />
                    <asp:HiddenField ID="hfPaymentRID" runat="server" />

                    <!-- Dealer Info -->
                    <div class="info-card">
                        <h5>Retailer Info</h5>
                        <div class="row">
                            <div class="col-md-3 form-group">
                                <asp:Label ID="Label15" runat="server" CssClass="font-weight-bold" Text="Dealer Id"></asp:Label>

                                <asp:TextBox ID="txtDealerid" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 form-group">
                                <asp:Label ID="Label16" runat="server" CssClass="font-weight-bold" Text="Dealer Full Name"></asp:Label>

                                <asp:TextBox ID="txtDealerFullName" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 form-group">
                                <asp:Label ID="Label21" runat="server" CssClass="font-weight-bold" Text="Dealer Mobile No"></asp:Label>

                                <asp:TextBox ID="txtDealerMobileNo" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 form-group">
                                <asp:Label ID="Label22" runat="server" CssClass="font-weight-bold" Text="Dealer Emailid"></asp:Label>

                                <asp:TextBox ID="txtDealerEmailid" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                        </div>
                    </div>
                    <!-- Customer Info -->
                    <div class="info-card">
                        <h5>Customer Info</h5>

                        <div class="row">
                            <div class="col-md-3 form-group">
                                <asp:Label ID="Lable1" runat="server" CssClass="font-weight-bold" Text="Customer Code"></asp:Label>

                                <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="font-weight-bold" Text="Customer Full Name"></asp:Label>

                                <asp:TextBox ID="txtCustomerFullName" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="font-weight-bold" Text="Customer Mobile No"></asp:Label>

                                <asp:TextBox ID="txtCustomerMobileNo" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="font-weight-bold" Text="Customer Emailid"></asp:Label>

                                <asp:TextBox ID="txtCustomerEmailid" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                        </div>
                    </div>


                    <!-- Product Info -->
                    <div class="info-card">
                        <h5>Product Info</h5>


                        <div class="row">
                            <div class="col-md-3 form-group">
                                <asp:Label ID="Label17" runat="server" CssClass="font-weight-bold" Text="Brand Name"></asp:Label>

                                <asp:TextBox ID="txtBrandName" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 form-group">
                                <asp:Label ID="Label18" runat="server" CssClass="font-weight-bold" Text="Model Name"></asp:Label>

                                <asp:TextBox ID="txtModelName" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 form-group">
                                <asp:Label ID="Label19" runat="server" CssClass="font-weight-bold" Text="Model Variant"></asp:Label>

                                <asp:TextBox ID="txtModelVariant" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="font-weight-bold" Text="IMEI  Number"></asp:Label>

                                <asp:TextBox ID="txtIMEI1Number" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                        </div>

                    </div>
                    <%--          <div class="row">
                             <div class="col-md-3 form-group">
                                 <div class="image-hover-wrapper">

                             

                                     <asp:Label ID="Label43" runat="server" CssClass="font-weight-bold">Customer Photo</asp:Label>
                                     <asp:Image ID="imgCustPhoto" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                         CssClass="img-thumbnail mt-2 d-block zoomable" AlternateText="Customer Photo" />

                                 </div>
                                 <div class="mt-2">
                                     <asp:LinkButton ID="btnDownloadCustPhoto" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadCustPhoto_Click" />
                                     <asp:LinkButton ID="btnRemoveCustPhoto" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemoveCustPhoto_Click" />
                                 </div>
                                 <asp:Label ID="lblCustPhotoError" runat="server" Text=""></asp:Label>
                                 <asp:HiddenField ID="HiddenField4" runat="server" />
                             </div>

                             <div class="col-md-3 form-group">
                                 <div class="image-hover-wrapper">
                                     <asp:Label ID="Label44" runat="server" CssClass="font-weight-bold">Aadhar Front</asp:Label>
                                   
                                     <asp:Image ID="imgAadharfrontphoto" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                         CssClass="img-thumbnail mt-2 d-block zoomable" />

                                 </div>
                                 <div class="mt-2">
                                     <asp:LinkButton ID="btnDownloadAadharfrontphoto" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadAadharfrontphoto_Click" />
                                     <asp:LinkButton ID="btnRemoveAadharfrontphoto" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemoveAadharfrontphoto_Click" />
                                 </div>
                                 <asp:HiddenField ID="HiddenField2" runat="server" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <div class="image-hover-wrapper">
                                     <asp:Label ID="Label45" runat="server" CssClass="font-weight-bold">Aadhar Back</asp:Label>
                                    
                                     <asp:Image ID="imgAadharbackphoto" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                         CssClass="img-thumbnail mt-2 d-block zoomable" />

                                 </div>
                                 <div class="mt-2">
                                     <asp:LinkButton ID="btnDownloadAadharbackphoto" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadAadharbackphoto_Click" />
                                     <asp:LinkButton ID="btnRemoveAadharbackphoto" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemoveAadharbackphoto_Click" />
                                 </div>
                                 <asp:HiddenField ID="HiddenField3" runat="server" />
                             </div>

                             <div class="col-md-3 form-group">
                                 <div class="image-hover-wrapper">
                                     <asp:Label ID="Label50" runat="server" CssClass="font-weight-bold">Customer Pan</asp:Label>
                                   
                                     <asp:Image ID="imgPan" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                         CssClass="img-thumbnail mt-2 d-block zoomable" />
                                    
                                 </div>

                                 <div class="mt-2">
                                     <asp:LinkButton ID="btnDownloadPan" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadPan_Click" />
                                     <asp:LinkButton ID="btnRemovePan" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemovePan_Click" />
                                 </div>
                             </div>

                         </div>
                         <div class="row">
                             <div class="col-md-3 form-group">


                                 <div class="image-hover-wrapper">
                                     <asp:Label ID="Label46" runat="server" CssClass="font-weight-bold">IMEI 1 Seal</asp:Label>
                                     
                                     <asp:Image ID="imgIMEI1" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                         CssClass="img-thumbnail mt-2 d-block zoomable" />
                                  
                                 </div>
                                 <div class="mt-2">
                                     <asp:LinkButton ID="btnDownloadIMEI1" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadIMEI1_Click" />
                                     <asp:LinkButton ID="btnRemoveIMEI1" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemoveIMEI1_Click" />
                                 </div>
                             </div>
                             <div class="col-md-3 form-group">


                                 <div class="image-hover-wrapper">
                                     <asp:Label ID="Label47" runat="server" CssClass="font-weight-bold">IMEI 2 Seal</asp:Label>
                                    
                                     <asp:Image ID="imgIMEI2" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                         CssClass="img-thumbnail mt-2 d-block zoomable" />
                                  
                                 </div>
                                 <div class="mt-2">
                                     <asp:LinkButton ID="btnDownloadIMEI2" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadIMEI2_Click" />
                                     <asp:LinkButton ID="btnRemoveIMEI2" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemoveIMEI2_Click" />
                                 </div>
                             </div>
                             <div class="col-md-3 form-group">

                                 <div class="image-hover-wrapper">
                                     <asp:Label ID="Label48" runat="server" CssClass="font-weight-bold">IMEI Photo</asp:Label>
                                   
                                     <asp:Image ID="imgIMEI" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                         CssClass="img-thumbnail mt-2 d-block zoomable" />
                                  

                                 <div class="mt-2">
                                     <asp:LinkButton ID="btnDownloadIMEI" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadIMEI_Click" />
                                     <asp:LinkButton ID="btnRemoveIMEI" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemoveIMEI_Click" />
                                 </div>
                             </div>
                             <div class="col-md-3 form-group">
                                 <div class="image-hover-wrapper">
                                     <asp:Label ID="Label49" runat="server" CssClass="font-weight-bold">Invoice</asp:Label>
                          
                                     <asp:Image ID="imgInvoice" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                         CssClass="img-thumbnail mt-2 d-block zoomable" />

                                 </div>

                                 <div class="mt-2">
                                     <asp:LinkButton ID="btnDownloadInvoice" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadInvoice_Click" />
                                     <asp:LinkButton ID="btnRemoveInvoice" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemoveInvoice_Click" />
                                 </div>
                             </div>
                         </div>
                         </div>--%>




                    <!-- Loan Info -->
  <div class="info-card card shadow-sm mb-3">
    <div class="card-header bg-primary text-white">
        <h5 class="mb-0">Loan Information</h5>
    </div>

    <div class="card-body">

        <!-- ================= BASIC LOAN INFO ================= -->
        <div class="row">
            <div class="col-md-3 form-group">
                <label>Loan Code</label>
                <asp:TextBox ID="txtLoanCode" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                <label>Loan Amount</label>
                <asp:TextBox ID="txtLoanAmount" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                <label>Down Payment</label>
                <asp:TextBox ID="txtDownPayment" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                <label>Loan EMI Amount</label>
                <asp:TextBox ID="txtLoanEMIAmount" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
        </div>

        <!-- ================= TENURE & DATES ================= -->
        <div class="row">
            <div class="col-md-3 form-group">
                <label>Tenure (Months)</label>
                <asp:TextBox ID="txtTenure" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                <label>Interest Rate (%)</label>
                <asp:TextBox ID="txtInterestRate" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                <label>Start Date</label>
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                <label>End Date</label>
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
        </div>

        <!-- ================= STATUS & SCORES ================= -->
        <div class="row">
            <div class="col-md-3 form-group">
                <label>Loan Status</label>
                <asp:TextBox ID="txtLoanStatus" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                <label>Credit Score</label>
                <asp:TextBox ID="txtCreditScore" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                <label>Processing Fees</label>
                <asp:TextBox ID="txtProcessingFees" runat="server" CssClass="form-control text-success" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                <label>Total Due EMI Amount</label>
                <asp:TextBox ID="txtTotalDueEMIAmount" runat="server"
                    CssClass="form-control font-weight-bold text-danger"
                    ReadOnly="true" />
            </div>
        </div>

        <!-- ================= EMI STATUS ================= -->
        <div class="row">
            <div class="col-md-3 form-group">
                <label>Total Paid EMI Months</label>
                <asp:TextBox ID="txtTotalPaidEMIMonths" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                <label>Total Due EMI Months</label>
                <asp:TextBox ID="txtTotalDueEMIMonths" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                <label>Late Fine / Month</label>
                <asp:TextBox ID="txtLateFinePerMonth" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                <label>Total Late Fine</label>
                <asp:TextBox ID="txtTotalLateFine" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>
        </div>

        <!-- ================= FORECLOSURE CHARGE ================= -->
        <div class="row">
            <div class="col-md-6 form-group">
                <asp:CheckBox
                    ID="chkApplyForeclosureCharge"
                    runat="server"
                    Text="Apply Foreclosure Charge"
                    onclick="recalculateForeclosure();"  Visible="false"/>

                                           <asp:CheckBox
ID="chkApplySettlementCharge"
runat="server"
Text="Apply Settlement Charge"
onclick="recalculateSettlement();"  Visible="false" />

            </div>
            
                          
                
        </div>
                <div class="col-md-6 form-group">
        <asp:CheckBox
    ID="chkApplyLateFine"
    runat="server"
    Text="Apply Late Fine"
    onclick="recalculateSettlement();" Visible="false" />
                    </div>

        <div class="row">
            <div class="col-md-3 form-group">
                <label>Charge Type</label>
                <asp:TextBox ID="txtChargeType" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                <label>Charge Value</label>
                <asp:TextBox ID="txtChargeValue" runat="server" CssClass="form-control" ReadOnly="true" />
            </div>


                      <div class="col-md-3 form-group">
              <label>Total Fine</label>
              <asp:TextBox ID="txttotgrandefine" runat="server" CssClass="form-control" ReadOnly="true" />
          </div>
        </div>

        <!-- ================= CALCULATION SUMMARY ================= -->
        <div class="row mt-3">
            <div class="col-md-3 form-group">
                <label>Total Paid Amount</label>
                <asp:TextBox ID="txtTotalPaidAmount" runat="server" CssClass="form-control text-success" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
        <asp:Label 
        ID="lblRemainingPrincipal" 
        runat="server" 
       CssClass="font-weight-bold"
        Text="Remaining Principal" />
                <asp:TextBox ID="txtRemainingPrincipal" runat="server" CssClass="form-control text-primary" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                 <asp:Label 
        ID="lblTotalCharge" 
        runat="server" 
        CssClass="font-weight-bold"
        Text="Total ForeClouser Charge" />
                <asp:TextBox ID="txtTotalCharge" runat="server" CssClass="form-control text-danger" ReadOnly="true" />
            </div>

            <div class="col-md-3 form-group">
                <label>Final Amount To Pay</label>
                <asp:TextBox ID="txtFinalPayableAmount" runat="server"
                    CssClass="form-control font-weight-bold text-danger"
                    ReadOnly="true" />
            </div>
        </div>

        <!-- ================= HIDDEN FIELDS (JS SUPPORT) ================= -->
        <asp:HiddenField ID="hfLoanAmount" runat="server" />
        <asp:HiddenField ID="hfTenure" runat="server" />
        <asp:HiddenField ID="hfDueEMI" runat="server" />
        <asp:HiddenField ID="hfChargeType" runat="server" />
        <asp:HiddenField ID="hfChargeValue" runat="server" />
        <asp:HiddenField ID="hfActionType" runat="server" />

    </div>
</div>


                 <div class="info-card">
    <h5>Loan Settlement Decision</h5>

    <div class="row">

        <!-- Payment Mode -->
        <div class="col-md-3 form-group">
            <asp:Label ID="Label25" runat="server" CssClass="font-weight-bold" Text="Payment Mode"></asp:Label>

            <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control"
                onchange="handlePaymentModeChange(this.value)">
                <asp:ListItem Text="Cash" Value="Cash"></asp:ListItem>
                <asp:ListItem Text="UPI" Value="UPI"></asp:ListItem>
                <asp:ListItem Text="RTGS/NEFT" Value="RTGS/NEFT"></asp:ListItem>
               <%-- <asp:ListItem Text="Cheque" Value="Cheque"></asp:ListItem>--%>
            </asp:DropDownList>
        </div>

        <!-- Settlement / Foreclosure -->
        <div class="col-md-3 form-group">
            <asp:Label ID="Label24" runat="server" CssClass="font-weight-bold" Text="Loan Settlement"></asp:Label>

            <asp:DropDownList ID="ddlLoanSettlement" runat="server" CssClass="form-control">
                <asp:ListItem Text="Settlement" Value="Settlement"></asp:ListItem>
                <asp:ListItem Text="Foreclosure" Value="Foreclosure"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <!-- Remarks -->
        <div class="col-md-6 form-group">
            <asp:Label ID="Label20" runat="server" CssClass="font-weight-bold"
                AssociatedControlID="txtremarks">
                Settlement Remarks <span style="color:red">*</span>
            </asp:Label>

            <asp:TextBox ID="txtremarks" runat="server" CssClass="form-control" TextMode="MultiLine" />

            <asp:RequiredFieldValidator
                ID="rfvRemarks"
                runat="server"
                ControlToValidate="txtremarks"
                ErrorMessage="Remarks are required."
                ForeColor="Red"
                Display="Dynamic"
                ValidationGroup="remarksvalidation" />
        </div>

    </div>

    <!-- Transaction Section -->
    <div class="row mt-3" id="txnSection" style="display:none;">

        <div class="col-md-3 form-group">
            <asp:Label ID="lblTxnNo" runat="server" CssClass="font-weight-bold"></asp:Label>
            <asp:TextBox ID="txtTxnNo" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-3 form-group">
            <asp:Label ID="lblTxnDate" runat="server" CssClass="font-weight-bold"></asp:Label>
            <asp:TextBox ID="txtTxnDate" runat="server" CssClass="form-control" TextMode="Date" />
        </div>

        <div class="col-md-3 form-group" id="bankSection" style="display:none;">
            <asp:Label ID="LabelBank" runat="server" CssClass="font-weight-bold" Text="Bank Name"></asp:Label>
            <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" />
        </div>

    </div>

    <!-- Buttons -->
    <div class="row mt-4">
        <div class="col-md-12 text-center">
<asp:Button ID="btnUpdate" runat="server"
    Text="Update"
    CssClass="btn btn-success" OnClick="btnUpdate_Click"
    ValidationGroup="remarksvalidation"
   />




            <asp:Button ID="btnClose" runat="server" Text="Close"
                CssClass="btn btn-danger"
                OnClick="btnClose_Click" />
        </div>
    </div>
</div>
                        

                </div>
            </div>

            <div id="Div_LoanSettlementSearch" runat="server"  class="container-fluid">

                <!-- ===== Header ===== -->
                <div class="card mb-3">
                    <div class="card-header form-header-bar">
                        <h3 class="card-title mb-0 text-dark">Loan Settlement Details</h3>
                    </div>

                    <!-- ===== Search Section ===== -->
                    <div class="card-body">
                        <div class="row align-items-end">



                                         <div class="col-md-3">
                 <div class="form-group">
                     <label class="font-weight-bold">Select Action Type</label>
                     <asp:DropDownList ID="ddlaction" runat="server" CssClass="form-control">
                         <asp:ListItem Text="Settlement" Value="settlement" />
                         <asp:ListItem Text="fore closure" Value="foreclosure" />
                        
                     </asp:DropDownList>
                 </div>
             </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="font-weight-bold">Criteria</label>
                                    <asp:DropDownList ID="ddlLoanCriteria" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="All" Value="All" />
                                        <asp:ListItem Text="Loan Code" Value="LoanCode" />
                                        <asp:ListItem Text="Customer Code" Value="CustomerCode" />
                                        <asp:ListItem Text="Dealer ID" Value="DealerID" />
                                        <asp:ListItem Text="Loan Status" Value="LoanStatus" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="font-weight-bold">Value</label>
                                    <asp:TextBox ID="txtLoanSettlementvalue"
                                        runat="server"
                                        CssClass="form-control"
                                        Placeholder="Enter value" />
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>&nbsp;</label>
                                    <asp:Button ID="btnLoansettlementSearch"
                                        runat="server"
                                        Text="Search"
                                        CssClass="btn btn-primary btn-block"
                                        OnClick="btnLoansettlementSearch_Click" />
                                </div>
                            </div>

                        </div>



                    </div>
                </div>

                <!-- ===== Dynamic Grid ===== -->
                <div class="card shadow-sm">
                    <div class="card-header bg-light">
                        <h6 class="mb-0 text-primary font-weight-bold">
                            <i class="fas fa-table mr-2"></i>Pending Loan Settlements
                        </h6>
                    </div>

                    <div class="card-body p-0 table-responsive">
                        <asp:GridView ID="grdLoanSettlementDetails"
                            runat="server"
                            DataKeyNames="LoanCode"
                            AutoGenerateColumns="true"
                            CssClass="table table-bordered table-hover table-striped mb-0"
                            Width="100%"
                            AllowPaging="true"
                            PageSize="15"
                            EmptyDataText="No records found"
                            GridLines="None"
                            UseAccessibleHeader="true"
                            PagerSettings-Mode="Numeric"
                            PagerStyle-HorizontalAlign="Center"
                            PagerStyle-CssClass="pagination-container"
                            OnRowCommand="grdLoanSettlementDetails_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton
                                            ID="lnkbtnRowEdit"
                                            runat="server"
                                            CommandName="EditRow"
                                            CommandArgument='<%# Eval("LoanCode") %>'
                                            CssClass="btn btn-sm btn-primary"
                                            ToolTip="Edit Loan Settlement">
                    
                                         <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>

            </div>



        </section>

    </div>


    <!-- ===== Bootstrap Modal Popup ===== -->

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

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h5 class="modal-title text-white" id="exampleModalLabel">Contact List</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" style="max-height: 300px; overflow-y: auto;">
                    <asp:Label ID="lblnorecords" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
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


    <!-- === Overlay Container === -->
    <div id="zoomedImage">
        <img src="" alt="Zoomed Image" />
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
    <script type="text/javascript">
        function ShowPopup(username, messages) {
            $("#MyPopups #lblName").html(username);
            $("#MyPopups #lblMessages").html(messages);
            $("#MyPopups").modal("show");
        }
    </script>

    <script type="text/javascript">
        function ShowError(ErrorMessages) {

            $("#ErrorPage #lblerror").html(ErrorMessages);
            $("#ErrorPage").modal("show");
        }
    </script>

<script>
    function recalculateForeclosure() {

        if (document.getElementById('<%= hfActionType.ClientID %>').value !== "foreclosure")
        return;

    let loanAmount = parseFloat(document.getElementById('<%= hfLoanAmount.ClientID %>').value) || 0;
    let tenure = parseInt(document.getElementById('<%= hfTenure.ClientID %>').value) || 0;
    let dueEMI = parseInt(document.getElementById('<%= hfDueEMI.ClientID %>').value) || 0;

    let chargeType = document.getElementById('<%= hfChargeType.ClientID %>').value;
    let chargeValue = parseFloat(document.getElementById('<%= hfChargeValue.ClientID %>').value) || 0;

    let applyCharge = document.getElementById('<%= chkApplyForeclosureCharge.ClientID %>').checked;

    let principalPerEmi = loanAmount / tenure;
    let remainingPrincipal = principalPerEmi * dueEMI;

    let charge = 0;
    if (applyCharge) {
        if (chargeType === "Percentage")
            charge = remainingPrincipal * (chargeValue / 100);
        else if (chargeType === "Flat")
            charge = chargeValue;
    }

    let finalAmount = remainingPrincipal + charge;

    document.getElementById('<%= txtRemainingPrincipal.ClientID %>').value = remainingPrincipal.toFixed(2);
    document.getElementById('<%= txtTotalCharge.ClientID %>').value = charge.toFixed(2);
    document.getElementById('<%= txtFinalPayableAmount.ClientID %>').value = finalAmount.toFixed(2);
    }
</script>

<script>
    function recalculateSettlement() {

        if (document.getElementById('<%= hfActionType.ClientID %>').value !== "settlement")
        return;

    let emiAmount = parseFloat(document.getElementById('<%= txtLoanEMIAmount.ClientID %>').value) || 0;
    let dueEMI = parseInt(document.getElementById('<%= hfDueEMI.ClientID %>').value) || 0;

    let lateFinePerMonth = parseFloat(document.getElementById('<%= txtLateFinePerMonth.ClientID %>').value) || 0;
    let applyLateFine = document.getElementById('<%= chkApplyLateFine.ClientID %>').checked;

    let chargeType = document.getElementById('<%= hfChargeType.ClientID %>').value;
    let chargeValue = parseFloat(document.getElementById('<%= hfChargeValue.ClientID %>').value) || 0;
    let applyCharge = document.getElementById('<%= chkApplySettlementCharge.ClientID %>').checked;

    // 1️⃣ Base EMI Due
    let baseDue = emiAmount * dueEMI;

    // 2️⃣ Late Fine (optional)
    let totalLateFine = applyLateFine ? (lateFinePerMonth * dueEMI) : 0;

    // 3️⃣ Subtotal
    let subTotal = baseDue + totalLateFine;

    // 4️⃣ Settlement Charge (optional)
    let charge = 0;
    if (applyCharge) {
        if (chargeType === "Percentage")
            charge = subTotal * (chargeValue / 100);
        else if (chargeType === "Flat")
            charge = chargeValue;
    }

    // 5️⃣ Final Payable
    let finalAmount = subTotal + charge;

    // ================= BIND =================
    document.getElementById('<%= txttotgrandefine.ClientID %>').value = totalLateFine.toFixed(2);
    document.getElementById('<%= txtRemainingPrincipal.ClientID %>').value = baseDue.toFixed(2);
    document.getElementById('<%= txtTotalCharge.ClientID %>').value = charge.toFixed(2);
    document.getElementById('<%= txtFinalPayableAmount.ClientID %>').value = finalAmount.toFixed(2);

    }
</script>

    <script>
        function handlePaymentModeChange(mode) {

            let txnSection = document.getElementById('txnSection');
            let bankSection = document.getElementById('bankSection');

            let lblTxnNo = document.getElementById('<%= lblTxnNo.ClientID %>');
        let lblTxnDate = document.getElementById('<%= lblTxnDate.ClientID %>');

        let txtTxnNo = document.getElementById('<%= txtTxnNo.ClientID %>');
        let txtTxnDate = document.getElementById('<%= txtTxnDate.ClientID %>');
        let txtBankName = document.getElementById('<%= txtBankName.ClientID %>');

            // Reset
            txnSection.style.display = "none";
            bankSection.style.display = "none";
            txtTxnNo.value = "";
            txtTxnDate.value = "";
            txtBankName.value = "";

            if (mode === "Cash") {
                return;
            }

            txnSection.style.display = "flex";

            if (mode === "UPI") {
                lblTxnNo.innerText = "UPI No";
                lblTxnDate.innerText = "UPI Date";
            }
            else if (mode === "RTGS/NEFT") {
                lblTxnNo.innerText = "Transaction No";
                lblTxnDate.innerText = "Transaction Date";
                bankSection.style.display = "block";
            }
            //else if (mode === "Cheque") {
            //    lblTxnNo.innerText = "Cheque No";
            //    lblTxnDate.innerText = "Cheque Date";
            //    bankSection.style.display = "block";
            //}
        }
    </script>



<%--<script>
    function confirmLoanSettlement(hiddenBtnId, validationGroup) {

        //if (typeof Page_ClientValidate === 'function') {
        //    if (!Page_ClientValidate(validationGroup)) {
        //        return false;
        //    }
        //}

        Swal.fire({
            title: 'Confirm Action',
            text: 'Are you sure you want to proceed with Loan Settlement / Foreclosure? Once confirmed, this action cannot be reversed.',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, Proceed',
            cancelButtonText: 'Cancel',
            confirmButtonColor: '#d33'
        }).then((result) => {
            if (result.isConfirmed) {
                document.getElementById(hiddenBtnId).click();
            }
        });

        return false;
    }
</script>--%>




    <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js'></script>
    <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/js/bootstrap.bundle.min.js'></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>


    <script>
        $(document).ready(function () {
            // Show zoomed image on click
            $(".zoomable").click(function () {
                var imgSrc = $(this).attr("src");
                $("#zoomedImage img").attr("src", imgSrc);
                $("#zoomedImage").fadeIn();
            });

            // Hide zoomed image on click outside
            $("#zoomedImage").click(function () {
                $(this).fadeOut();
            });
        });
    </script>
</asp:Content>
