<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="loandisburshment.aspx.cs" Inherits="TheEMIClubApplication.Admin.loandisburshment" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
             <div class="container-fluid mt-4">
      <!-- 🔹 Filter Section -->
      <div class="card shadow-lg border-0 mb-4">
          <div class="card-header bg-gradient-primary text-white d-flex justify-content-between align-items-center">
              <h5 class="mb-0"><i class="fas fa-search mr-2"></i> Loan Disburshment</h5>
          </div>
                     <div class="card-body">
                         <asp:HiddenField ID="hfLoanRID" runat="server" />
                         <asp:HiddenField ID="hfEMIRID" runat="server" />
                         <asp:HiddenField ID="hfPaymentRID" runat="server" />
                               <span id="spnMessage" runat="server"></span>
                         <!-- Dealer Info -->
                         <div class="row">
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label15" runat="server" CssClass="font-weight-bold" Text="Dealer Id"></asp:Label>
                                 <%-- <label>Customer Code</label>--%>
                                 <asp:TextBox ID="txtDealerid" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label16" runat="server" CssClass="font-weight-bold" Text="Dealer Full Name"></asp:Label>
                                 <%--<label>First Name</label>--%>
                                 <asp:TextBox ID="txtDealerFullName" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label21" runat="server" CssClass="font-weight-bold" Text="Dealer Mobile No"></asp:Label>
                                 <%-- <label>Middle Name</label>--%>
                                 <asp:TextBox ID="txtDealerMobileNo" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label22" runat="server" CssClass="font-weight-bold" Text="Dealer Emailid"></asp:Label>
                                 <%--<label>Last Name</label>--%>
                                 <asp:TextBox ID="txtDealerEmailid" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                         </div>
                         <!-- Customer Info -->
                         <div class="row">
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Lable1" runat="server" CssClass="font-weight-bold" Text="Customer Code"></asp:Label>
                                 <%-- <label>Customer Code</label>--%>
                                 <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label1" runat="server" CssClass="font-weight-bold" Text="Customer Full Name"></asp:Label>
                                 <%--<label>First Name</label>--%>
                                 <asp:TextBox ID="txtCustomerFullName" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label2" runat="server" CssClass="font-weight-bold" Text="Customer Mobile No"></asp:Label>
                                 <%-- <label>Middle Name</label>--%>
                                 <asp:TextBox ID="txtCustomerMobileNo" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label3" runat="server" CssClass="font-weight-bold" Text="Customer Emailid"></asp:Label>
                                 <%--<label>Last Name</label>--%>
                                 <asp:TextBox ID="txtCustomerEmailid" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                         </div>


                         <!-- Loan Info -->
                         <div class="row">
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label4" runat="server" CssClass="font-weight-bold" Text="Loan Code"></asp:Label>
                                 <%--<label>Loan Code</label>--%>
                                 <asp:TextBox ID="txtLoanCode" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label5" runat="server" CssClass="font-weight-bold" Text="Loan Amount"></asp:Label>
                                 <%--<label>Loan Amount</label>--%>
                                 <asp:TextBox ID="txtLoanAmount" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label6" runat="server" CssClass="font-weight-bold" Text="Down Payment"></asp:Label>
                                 <%--<label>Down Payment</label>--%>
                                 <asp:TextBox ID="txtDownPayment" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label7" runat="server" CssClass="font-weight-bold" Text="Loan EMI Amount"></asp:Label>
                                 <%--<label>Loan EMI Amount</label>--%>
                                 <asp:TextBox ID="txtLoanEMIAmount" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
                             </div>

                             <div class="col-md-3 form-group">
                                 <asp:Label ID="lblmembershipfees" runat="server" CssClass="font-weight-bold" Text="Membership Fees"></asp:Label>
                                 <%--<label>Loan Code</label>--%>
                                 <asp:TextBox ID="txtMembershipfees" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="lblprocessingfees" runat="server" CssClass="font-weight-bold" Text="Processing Fees"></asp:Label>
                                 <%--<label>Loan Amount</label>--%>
                                 <asp:TextBox ID="txtprocessingFees" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                         </div>                       



                         <div class="row">
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label8" runat="server" CssClass="font-weight-bold" Text="Tenure"></asp:Label>
                                 <%--<label>Tenure</label>--%>
                                 <asp:TextBox ID="txtTenure" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label9" runat="server" CssClass="font-weight-bold" Text="Interest Rate (%)"></asp:Label>
                                 <%-- <label>Interest Rate (%)</label>--%>
                                 <asp:TextBox ID="txtInterestRate" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label14" runat="server" CssClass="font-weight-bold" Text="Start Date"></asp:Label>
                                 <%--<label>Start Date</label>--%>
                                 <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label13" runat="server" CssClass="font-weight-bold" Text="End Date"></asp:Label>
                                 <%-- <label>End Date</label>--%>
                                 <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                         </div>

                         <div class="row">
                              <div class="col-md-3 form-group">
     <asp:Label ID="lblCreditScore" runat="server" CssClass="font-weight-bold" Text="Credit Score"></asp:Label>
     <asp:TextBox ID="txtCreditScore" runat="server" CssClass="form-control" ReadOnly="true" />
 </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label11" runat="server" CssClass="font-weight-bold" Text="Loan Status"></asp:Label>
                                 <%--<label>Loan Status</label>--%>
                                 <asp:TextBox ID="txtLoanStatus" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                  <asp:HiddenField ID="hdnReserveValue" runat="server" />
                                 <asp:Label ID="lblHoldAmt" runat="server" Visible="false"></asp:Label>
                                 <asp:Label ID="Label12" runat="server" CssClass="font-weight-bold" Text="Loan Created By"></asp:Label>
                                 <%--<label>Loan Created By</label>--%>
                                 <asp:TextBox ID="txtLoanCreatedBy" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                         </div>



                         <!-- Product Info -->

                         <div class="row">

                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label17" runat="server" CssClass="font-weight-bold" Text="Brand Name"></asp:Label>
                                 <%--  <label>IMEI Number</label>--%>
                                 <asp:TextBox ID="txtBrandName" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label18" runat="server" CssClass="font-weight-bold" Text="Model Name"></asp:Label>
                                 <%--  <label>IMEI Number</label>--%>
                                 <asp:TextBox ID="txtModelName" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label19" runat="server" CssClass="font-weight-bold" Text="Model Variant"></asp:Label>
                                 <%--  <label>IMEI Number</label>--%>
                                 <asp:TextBox ID="txtModelVariant" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                             <div class="col-md-3 form-group">
                                 <asp:Label ID="Label10" runat="server" CssClass="font-weight-bold" Text="IMEI  Number"></asp:Label>
                                 <%--  <label>IMEI Number</label>--%>
                                 <asp:TextBox ID="txtIMEI1Number" runat="server" CssClass="form-control" ReadOnly="true" />
                             </div>
                         </div>

           
<div class="row">
    <!-- IMEI 1 -->
    <div class="col-md-3 form-group">
        <div class="image-hover-wrapper">
            <asp:Label ID="Label46" runat="server" CssClass="font-weight-bold">IMEI 1 Seal</asp:Label>
            <asp:Image ID="imgIMEI1" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                CssClass="img-thumbnail mt-2 d-block" />
            <div class="zoom-center">
                <img runat="server" id="IMEI1imgzoom" class="zoomed-image" />
            </div>
        </div>

        <div class="mt-2">
            <asp:FileUpload ID="fuIMEI1" runat="server" CssClass="form-control form-control-sm mb-2" OnClick="btnUploadIMEI1_Click" />
            <asp:Button ID="btnUploadIMEI1" runat="server" Text="Change Image" CssClass="btn btn-sm btn-secondary me-2" OnClick="btnUpdateIMEI1_Click"  />
            <asp:LinkButton ID="btnDownloadIMEI1" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" />
            <asp:LinkButton ID="btnRemoveIMEI1" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" />
        </div>
    </div>

    <!-- IMEI 2 -->
    <div class="col-md-3 form-group">
        <div class="image-hover-wrapper">
            <asp:Label ID="Label47" runat="server" CssClass="font-weight-bold">IMEI 2 Seal</asp:Label>
            <asp:Image ID="imgIMEI2" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                CssClass="img-thumbnail mt-2 d-block" />
            <div class="zoom-center">
                <img runat="server" id="IMEI2imgzoom" class="zoomed-image" />
            </div>
        </div>

        <div class="mt-2">
            <asp:FileUpload ID="fuIMEI2" runat="server" CssClass="form-control form-control-sm mb-2" />
            <asp:Button ID="btnUploadIMEI2" runat="server" Text="Change Image" CssClass="btn btn-sm btn-secondary me-2" OnClick="btnUploadIMEI2_Click" />
            <asp:LinkButton ID="btnDownloadIMEI2" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" />
            <asp:LinkButton ID="btnRemoveIMEI2" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" />
        </div>
    </div>

    <!-- IMEI Photo -->
    <div class="col-md-3 form-group">
        <div class="image-hover-wrapper">
            <asp:Label ID="Label48" runat="server" CssClass="font-weight-bold">IMEI Photo</asp:Label>
            <asp:Image ID="imgIMEI" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                CssClass="img-thumbnail mt-2 d-block" />
            <div class="zoom-center">
                <img runat="server" id="IMEIimgzoom" class="zoomed-image" />
            </div>
        </div>

        <div class="mt-2">
            <asp:FileUpload ID="fuIMEI" runat="server" CssClass="form-control form-control-sm mb-2" />
            <asp:Button ID="btnUploadIMEI" runat="server" Text="Change Image" CssClass="btn btn-sm btn-secondary me-2" OnClick="btnUploadIMEI_Click"/>
            <asp:LinkButton ID="btnDownloadIMEI" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" />
            <asp:LinkButton ID="btnRemoveIMEI" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" />
        </div>
    </div>

    <!-- Invoice -->
    <div class="col-md-3 form-group">
        <div class="image-hover-wrapper">
            <asp:Label ID="Label49" runat="server" CssClass="font-weight-bold">Invoice</asp:Label>
            <asp:Image ID="imgInvoice" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                CssClass="img-thumbnail mt-2 d-block" />
            <div class="zoom-center">
                <img runat="server" id="Invoiceimgzoom" class="zoomed-image" />
            </div>
        </div>

        <div class="mt-2">
            <asp:FileUpload ID="fuInvoice" runat="server" CssClass="form-control form-control-sm mb-2" />
            <asp:Button ID="btnUploadInvoice" runat="server" Text="Change Image" CssClass="btn btn-sm btn-secondary me-2" OnClick="btnUploadInvoice_Click" />
            <asp:LinkButton ID="btnDownloadInvoice" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" />
            <asp:LinkButton ID="btnRemoveInvoice" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" />
        </div>
    </div>
</div>

                         <div class="row">
                             <div class="col-md-3 form-group">
    <asp:Label ID="Label20" runat="server" CssClass="font-weight-bold" Text="Disbustment Remark"></asp:Label>
    <%--  <label>IMEI Number</label>--%>
    <asp:TextBox ID="txtDisbustmentRemark" runat="server" CssClass="form-control" />
</div>
                         </div>


     

                         <!-- Buttons -->
                         <div class="row mt-4">
                             <div class="col-md-12 text-center">
                                  <asp:Button ID="btnRejectDisburshmenmt" runat="server" Text="Reject Disburshmenmt Loan" CssClass="btn btn-danger" OnClick="btnRejectDisburshmenmt_Click"  />
                                 <asp:Button ID="btnSave" runat="server" Text="Disburshmenmt Loan" CssClass="btn btn-success"  OnClick="btnSave_Click" />
                                 <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger" />
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

    <script type="text/javascript">
        function ShowError(ErrorMessages) {

            $("#ErrorPage #lblerror").html(ErrorMessages);
            $("#ErrorPage").modal("show");
        }
    </script>

          <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js'></script>
<script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/js/bootstrap.bundle.min.js'></script>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
