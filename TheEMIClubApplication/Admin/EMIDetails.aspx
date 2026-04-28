<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="EMIDetails.aspx.cs" Inherits="TheEMIClubApplication.MasterPage.EMIDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <section class="content">
        <div class="container-fluid">

            <!-- Card Wrapper -->
            <div class="card shadow-lg border-0 rounded-lg mb-4">
                <div class="card-header bg-primary text-white">
                    <h3 class="card-title mb-0">EMI Details</h3>
                </div>
                <div class="card-body">

                    <asp:HiddenField ID="hfLoanRID" runat="server" />
                    <asp:HiddenField ID="hfEMIRID" runat="server" />
                    <asp:HiddenField ID="hfPaymentRID" runat="server" />

                    <!-- Personal Details -->
              <h5 class="text-secondary mt-3 mb-3 border-bottom pb-2"> Personal Details</h5>
<div class="row">
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Customer Code" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="First Name" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Middle Name" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Last Name" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
</div>

<div class="row">
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Email Address" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Phone Number" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
</div>


<!-- ================= Product Details ================= -->
<h5 class="text-secondary mt-4 mb-3 border-bottom pb-2"> Product Details</h5>
<div class="row">
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Product Code" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtProductCode" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Brand" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtBrand" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Model" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtModel" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Variant" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtVariant" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
</div>

<div class="row">
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Color" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtColor" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="IMEI Number" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtIMEINumber" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
</div>

<!-- ================= Loan Details ================= -->
<h5 class="text-secondary mt-4 mb-3 border-bottom pb-2"> Loan Details</h5>
<div class="row">
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Loan Code" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtLoanCode" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Loan Amount" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtLoanAmount" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
    </div>
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Down Payment" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtDownPayment" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
    </div>
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Loan EMI Amount" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtLoanEMIAmount" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
    </div>
</div>

<div class="row">
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Tenure (Months)" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtTenure" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
    </div>
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Interest Rate (%)" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtInterestRate" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Start Date" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="End Date" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
</div>

<div class="row">
    <div class="col-md-3 form-group">
        <asp:Label runat="server" Text="Loan Status" CssClass="font-weight-bold" />
        <asp:TextBox ID="txtLoanStatus" runat="server" CssClass="form-control" ReadOnly="true" />
    </div>
</div>



                    <!-- EMI Details -->
                    <h5 class="text-secondary mt-4 mb-3 border-bottom pb-2"> EMI Details</h5>
                    <div class="row">
                        <div class="col-md-3 form-group">
                            <asp:Label runat="server" Text="Paid EMI" CssClass="font-weight-bold" />
                            <asp:TextBox ID="txtPaidEMI" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>
                        <div class="col-md-3 form-group">
                            <asp:Label runat="server" Text="Due EMI" CssClass="font-weight-bold" />
                            <asp:TextBox ID="txtDuesEMI" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>
                        <div class="col-md-3 form-group">
                            <asp:Label runat="server" Text="Received EMI Amount" CssClass="font-weight-bold" />
                            <asp:TextBox ID="txtReceivedEMIAmount" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>
                        <div class="col-md-3 form-group">
                            <asp:Label runat="server" Text="Payment Date" CssClass="font-weight-bold" />
                            <asp:TextBox ID="txtPaymentDate" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>
                    </div>

                    <!-- Payment Info -->
                    <h5 class="text-secondary mt-4 mb-3 border-bottom pb-2"> Payment Information</h5>
                    <div class="row">
                        <div class="col-md-3 form-group">
                            <asp:Label runat="server" Text="Paid Amount" CssClass="font-weight-bold" />
                            <asp:TextBox ID="txtPaidAmount" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>
                        <div class="col-md-3 form-group">
                            <asp:Label runat="server" Text="Payment Mode" CssClass="font-weight-bold" />
                            <asp:TextBox ID="txtPaymentMode" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>
                        <div class="col-md-3 form-group">
                            <asp:Label runat="server" Text="Remarks" CssClass="font-weight-bold" />
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" ReadOnly="true" />
                        </div>
                        <div class="col-md-3 form-group">
                            <asp:Label runat="server" Text="Payment Status" CssClass="font-weight-bold" />
                            <asp:DropDownList ID="ddlPaymentStatus" runat="server" CssClass="form-control" Enabled="false">
                                <asp:ListItem Text="Active" Value="Active" />
                                <asp:ListItem Text="Inactive" Value="Inactive" />
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Record Status -->
                    <h5 class="text-secondary mt-4 mb-3 border-bottom pb-2"> Record Details</h5>
                    <div class="row">
                        <div class="col-md-3 form-group">
                            <asp:Label runat="server" Text="Record Status" CssClass="font-weight-bold" />
                            <asp:DropDownList ID="ddlRecordStatus" runat="server" CssClass="form-control" Enabled="false">
                                <asp:ListItem Text="Approved" Value="APPROVED" />
                                <asp:ListItem Text="Pending" Value="PENDING" />
                                <asp:ListItem Text="Rejected" Value="REJECTED" />
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 form-group">
                            <asp:Label runat="server" Text="Receipt Image" CssClass="font-weight-bold" />
                            <asp:FileUpload ID="fuReceiptImage" runat="server" CssClass="form-control" Visible="false" />
                            <asp:Image ID="imgReceiptPreview" runat="server" Width="100" Height="100" CssClass="mt-2 border" />
                                                        <div class="mt-2">
                                                            <asp:Button ID="btnDownloadReceipt" runat="server" Text="Download" CssClass="btn btn-sm btn-primary" OnClick="btnDownloadReceipt_Click1" />
                                                            </div>

                            <asp:Label ID="lblimgError" runat="server"  CssClass="text-danger" style="font-weight:bold;"/>

                        </div>
   
                    </div>

                    <!-- Action Buttons -->
                    <div class="row mt-4">
                        <div class="col-md-12 text-center">
                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger px-4" OnClick="btnClose_Click" />
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
