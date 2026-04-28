<%@ Page Title="Customer Master" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="CustomerMaster.aspx.cs"
    Inherits="TheEMIClubApplication.MasterPage.CustomerMaster" %>

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
        </style>
    <!-- Bootstrap 4.6 & FontAwesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" />

    <style>
        .section-card { margin-bottom: 1rem; }
        .field-label { font-weight: 600; display:block; margin-bottom:6px; }
        .hover-thumbnail { width:110px; height:110px; object-fit:cover; border-radius:6px; cursor:pointer; }
        .img-actions { margin-top:8px; }
        .form-header-bar { background: linear-gradient(90deg,#2b7be3,#1b6fd8); color:#fff; padding:12px 16px; border-radius: .25rem .25rem 0 0; }
        .card-title { margin:0; font-size:1.1rem; font-weight:700; color:#fff; }
        .readonly { background: transparent; border: none; padding-left:0; }
        .label-icon { width:18px; text-align:center; display:inline-block; }
    </style>

    <div class="container-fluid p-3">
        <div class="card shadow-sm">
            <div class="form-header-bar d-flex justify-content-between align-items-center">
                <div class="card-title"><i class="fas fa-id-card mr-2"></i> Customer Device Registration</div>
                <div>
                    <!-- top-right actions if needed -->
                </div>
            </div>

            <div class="card-body">

                <!-- Hidden fields (added hfRID and kept others you had) -->
                <asp:HiddenField ID="hfRID" runat="server" />          <!-- ADDED: often referenced in code-behind -->
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
                <asp:HiddenField ID="HiddenField3" runat="server" />
                <asp:HiddenField ID="HiddenField4" runat="server" />

                <!-- CUSTOMER INFO -->
                <div class="card section-card">
                    <div class="card-body">
                        <h5 class="mb-3"><i class="fas fa-user mr-2 text-primary"></i> Customer Info</h5>
                        <div class="row">
                            <div class="col-md-3">
                                <label class="field-label"><span class="label-icon"><i class="fas fa-key"></i></span> Customer Code</label>
                                <asp:TextBox ID="txtCustomercode" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3">
                                <label class="field-label"><span class="label-icon"><i class="fas fa-user"></i></span> First Name</label>
                                <asp:TextBox ID="txtFirstname" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3">
                                <label class="field-label"><span class="label-icon"><i class="fas fa-user-check"></i></span> Middle Name</label>
                                <asp:TextBox ID="txtMiddlename" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3">
                                <label class="field-label"><span class="label-icon"><i class="fas fa-user-tie"></i></span> Last Name</label>
                                <asp:TextBox ID="txtLastname" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>

                            <div class="col-md-3 mt-3">
                                <label class="field-label"><span class="label-icon"><i class="fas fa-phone"></i></span> Primary Mobile</label>
                                <asp:TextBox ID="txtPrimaruMobileno" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                                        <div class="col-md-3 mt-3">
                                                                      <label class="field-label"><span class="label-icon"><i class="fas fa-phone"></i></span> Alternate Mobile</label>
                                      
                                        <asp:TextBox ID="txtAltMobile" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                         <%--   <div class="col-md-3 mt-3">
                                <label class="field-label"><span class="label-icon"><i class="fas fa-shield-alt"></i></span> Primary OTP</label>
                                <asp:TextBox ID="txtPrimaryOTP" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>--%>
                            <div class="col-md-3 mt-3">
                                <label class="field-label"><span class="label-icon"><i class="fas fa-check-circle"></i></span> Primary Mobile Verified</label>
                                <asp:TextBox ID="txtPrimaryVerified" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 mt-3">
                                <label class="field-label"><span class="label-icon"><i class="fas fa-envelope"></i></span> Email</label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- ADDRESS -->
                <div class="card section-card">
                    <div class="card-body">
                        <h5 class="mb-3"><i class="fas fa-map-marker-alt mr-2 text-success"></i> Address</h5>
                        <div class="row">
                            <div class="col-md-2">
                                <label class="field-label">Flat No</label>
                                <asp:TextBox ID="txtFlatno" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3">
                                <label class="field-label">Area / Sector</label>
                                <asp:TextBox ID="txtAreaSector" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-7">
                                <label class="field-label">Current Address</label>
                                <asp:TextBox ID="txtCurrentAddress" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>

                            <div class="col-md-2 mt-3">
                                <label class="field-label">Pin Code</label>
                                <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 mt-3">
                                <label class="field-label">Country</label>
                                <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 mt-3">
                                <label class="field-label">State</label>
                                <asp:TextBox ID="txtState" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-4 mt-3">
                                <label class="field-label">City</label>
                                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- IDs -->
                <div class="card section-card">
                    <div class="card-body">
                        <h5 class="mb-3"><i class="fas fa-id-badge mr-2 text-warning"></i> Identification</h5>
                        <div class="row">
                            <div class="col-md-4">
                                <label class="field-label">Aadhar Number</label>
                                <asp:TextBox ID="txtAaadharno" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-4">
                                <label class="field-label">Aadhar Verified</label>
                                <asp:TextBox ID="txtAadharVerified" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-4">
                                <label class="field-label">PAN Number</label>
                                <asp:TextBox ID="txtPan" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-4 mt-3">
                                <label class="field-label">PAN Verified</label>
                                <asp:TextBox ID="txtPANVerified" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-4 mt-3">
  <label class="field-label">Cibil Score </label>
   <asp:TextBox ID="txtCibilScore" runat="server" CssClass="form-control" ReadOnly="true" />
  </div>

                             <div class="col-md-4 mt-3">
                               <label class="field-label">Cibil Score Report (pdf)</label>
                                 <asp:Button ID="btnCibilDownload" runat="server" Text="Download" CssClass="btn btn-primary" OnClick="btnCibilDownload_Click"/>
                               </div>

                        </div>
                    </div>
                </div>

                <!-- DEVICE -->
                <div class="card section-card">
                    <div class="card-body">
                        <h5 class="mb-3"><i class="fas fa-mobile-alt mr-2 text-info"></i> Device Info</h5>
                        <div class="row">
                            <div class="col-md-3">
                                <label class="field-label">Brand</label>
                                <asp:TextBox ID="txtBrand" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3">
                                <label class="field-label">Model</label>
                                <asp:TextBox ID="txtmodel" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3">
                                <label class="field-label">Variant</label>
                                <asp:TextBox ID="txtVariant" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3">
                                <label class="field-label">Color</label>
                                <asp:TextBox ID="txtColor" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>

                            <div class="col-md-3 mt-3">
                                <label class="field-label">Selling Price</label>
                                <asp:TextBox ID="txtSellingPrice" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 mt-3">
                                <label class="field-label">Down Payment</label>
                                <asp:TextBox ID="txtdownPayment" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 mt-3">
                                <label class="field-label">Tenure (Months)</label>
                                <asp:TextBox ID="txtTenure" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 mt-3">
                                <label class="field-label">EMI Amount</label>
                                <asp:TextBox ID="txtEMIAmount" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- IMEI & BANK -->
                <div class="card section-card">
                    <div class="card-body">
                        <h5 class="mb-3"><i class="fas fa-keyboard mr-2 text-secondary"></i> IMEI & Bank Info</h5>
                        <div class="row">
                            <div class="col-md-4">
                                <label class="field-label">IMEI 1</label>
                                <asp:TextBox ID="txtIMEIOne" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-4">
                                <label class="field-label">IMEI 2</label>
                                <asp:TextBox ID="txtIMEITwo" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-4">
                                <label class="field-label">Account No</label>
                                <asp:TextBox ID="txtAccountno" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 mt-3">
                                <label class="field-label">IFSC</label>
                                <asp:TextBox ID="txtIFSC" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 mt-3">
                                <label class="field-label">Bank Name</label>
                                <asp:TextBox ID="txtBank" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 mt-3">
                                <label class="field-label">Account Type</label>
                                <asp:TextBox ID="txtAccountType" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-3 mt-3">
                                <label class="field-label">Branch Name</label>
                                <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- REFERENCE -->
                <div class="card section-card">
                    <div class="card-body">
                        <h5 class="mb-3"><i class="fas fa-users mr-2"></i> Reference</h5>
                        <div class="row">
                            <div class="col-md-4">
                                <label class="field-label">Ref Name</label>
                                <asp:TextBox ID="txtRefName" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-4">
                                <label class="field-label">Ref Relationship</label>
                                <asp:TextBox ID="txtRefRelationship" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-4">
                                <label class="field-label">Ref Mobile No</label>
                                <asp:TextBox ID="txtRefMobileno" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                            <div class="col-md-12 mt-3">
                                <label class="field-label">Ref Address</label>
                                <asp:TextBox ID="txtRefAddress" runat="server" CssClass="form-control" ReadOnly="true" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- GRACE PERIOD -->
<div class="card section-card mt-3">
    <div class="card-body">
        <h5 class="mb-3">
            <i class="fas fa-hourglass-half mr-2 text-warning"></i>
            Grace Period
        </h5>

        <div class="row align-items-end">
            <!-- Grace Period Dropdown -->
            <div class="col-md-6">
                <label class="field-label">Select Grace Period</label>
                <asp:DropDownList 
                    ID="ddlGracePeriod" 
                    runat="server" 
                    CssClass="form-control">
                    <asp:ListItem Text="-- 0 Days --" Value="0" />
                  <asp:ListItem Text="1 Day" Value="1" />
    <asp:ListItem Text="2 Days" Value="2" />
    <asp:ListItem Text="3 Days" Value="3" />
    <asp:ListItem Text="4 Days" Value="4" />
    <asp:ListItem Text="5 Days" Value="5" />
    <asp:ListItem Text="6 Days" Value="6" />
    <asp:ListItem Text="7 Days" Value="7" />
    <asp:ListItem Text="8 Days" Value="8" />
    <asp:ListItem Text="9 Days" Value="9" />
    <asp:ListItem Text="10 Days" Value="10" />
    <asp:ListItem Text="11 Days" Value="11" />
    <asp:ListItem Text="12 Days" Value="12" />
    <asp:ListItem Text="13 Days" Value="13" />
    <asp:ListItem Text="14 Days" Value="14" />
    <asp:ListItem Text="15 Days" Value="15" />
    
                </asp:DropDownList>
            </div>

            <!-- Update Button -->
            <div class="col-md-6">
                <asp:Button 
                    ID="btnUpdateGracePeriod" 
                    runat="server" 
                    Text="Update Grace Period"
                    CssClass="btn btn-warning btn-block"
                    OnClick="btnUpdateGracePeriod_Click" />
            </div>
        </div>
    </div>
</div>


                <!-- UPLOADS (images) -->
                <div class="card section-card">
                    <div class="card-body">
                        <h5 class="mb-3"><i class="fas fa-file-image mr-2 text-dark"></i> Uploads & Documents</h5>

                        <div class="row">
                            <!-- Customer Photo -->
                            <div class="col-md-3 text-center">
                                <label class="field-label">Customer Photo</label>
                                <asp:Image ID="imgCustPhoto" runat="server" ImageUrl="~/Images/image icon.png" CssClass="hover-thumbnail gridImage img-thumbnail" />
                                <div class="img-actions">
                                    <asp:LinkButton ID="btnDownloadCustPhoto" runat="server" CssClass="btn btn-sm btn-outline-primary" OnClick="btnDownloadCustPhoto_Click">
                                        <i class="fas fa-download"></i> Download
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnRemoveCustPhoto" runat="server" CssClass="btn btn-sm btn-outline-danger" Visible="false" OnClick="btnRemoveCustPhoto_Click">
                                        <i class="fas fa-trash"></i> Remove
                                    </asp:LinkButton>
                                </div>
                                <asp:Label ID="lblCustPhotoError" runat="server" CssClass="text-danger"></asp:Label>
                            </div>

                            <!-- Aadhar Front -->
                            <div class="col-md-3 text-center">
                                <label class="field-label">Aadhar Front</label>
                                <asp:Image ID="imgAadharfrontphoto" runat="server" ImageUrl="~/Images/image icon.png" CssClass="hover-thumbnail gridImage img-thumbnail" />
                                <div class="img-actions">
                                    <asp:LinkButton ID="btnDownloadAadharfrontphoto" runat="server" CssClass="btn btn-sm btn-outline-primary" OnClick="btnDownloadAadharfrontphoto_Click">
                                        <i class="fas fa-download"></i> Download
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnRemoveAadharfrontphoto" runat="server" CssClass="btn btn-sm btn-outline-danger" Visible="false" OnClick="btnRemoveAadharfrontphoto_Click">
                                        <i class="fas fa-trash"></i> Remove
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <!-- Aadhar Back -->
                            <div class="col-md-3 text-center">
                                <label class="field-label">Aadhar Back</label>
                                <asp:Image ID="imgAadharbackphoto" runat="server" ImageUrl="~/Images/image icon.png" CssClass="hover-thumbnail gridImage img-thumbnail" />
                                <div class="img-actions">
                                    <asp:LinkButton ID="btnDownloadAadharbackphoto" runat="server" CssClass="btn btn-sm btn-outline-primary" OnClick="btnDownloadAadharbackphoto_Click">
                                        <i class="fas fa-download"></i> Download
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnRemoveAadharbackphoto" runat="server" CssClass="btn btn-sm btn-outline-danger" Visible="false" OnClick="btnRemoveAadharbackphoto_Click">
                                        <i class="fas fa-trash"></i> Remove
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <!-- IMEI 1 Seal -->
                            <div class="col-md-3 text-center">
                                <label class="field-label">IMEI 1 Seal</label>
                                <asp:Image ID="imgIMEI1" runat="server" ImageUrl="~/Images/image icon.png" CssClass="hover-thumbnail gridImage img-thumbnail" />
                                <div class="img-actions">
                                    <asp:LinkButton ID="btnDownloadIMEI1" runat="server" CssClass="btn btn-sm btn-outline-primary" OnClick="btnDownloadIMEI1_Click">
                                        <i class="fas fa-download"></i> Download
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnRemoveIMEI1" runat="server" CssClass="btn btn-sm btn-outline-danger" Visible="false" OnClick="btnRemoveIMEI1_Click">
                                        <i class="fas fa-trash"></i> Remove
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div class="row mt-3">
                            <!-- IMEI 2 Seal -->
                            <div class="col-md-3 text-center">
                                <label class="field-label">IMEI 2 Seal</label>
                                <asp:Image ID="imgIMEI2" runat="server" ImageUrl="~/Images/image icon.png" CssClass="hover-thumbnail gridImage img-thumbnail" />
                                <div class="img-actions">
                                    <asp:LinkButton ID="btnDownloadIMEI2" runat="server" CssClass="btn btn-sm btn-outline-primary" OnClick="btnDownloadIMEI2_Click">
                                        <i class="fas fa-download"></i> Download
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnRemoveIMEI2" runat="server" CssClass="btn btn-sm btn-outline-danger" Visible="false" OnClick="btnRemoveIMEI2_Click">
                                        <i class="fas fa-trash"></i> Remove
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <!-- IMEI Photo -->
                            <div class="col-md-3 text-center">
                                <label class="field-label">IMEI Photo</label>
                                <asp:Image ID="imgIMEI" runat="server" ImageUrl="~/Images/image icon.png" CssClass="hover-thumbnail gridImage img-thumbnail" />
                                <div class="img-actions">
                                    <asp:LinkButton ID="btnDownloadIMEI" runat="server" CssClass="btn btn-sm btn-outline-primary" OnClick="btnDownloadIMEI_Click">
                                        <i class="fas fa-download"></i> Download
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnRemoveIMEI" runat="server" CssClass="btn btn-sm btn-outline-danger" Visible="false" OnClick="btnRemoveIMEI_Click">
                                        <i class="fas fa-trash"></i> Remove
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <!-- Invoice -->
                            <div class="col-md-3 text-center">
                                <label class="field-label">Invoice</label>
                                <asp:Image ID="imgInvoice" runat="server" ImageUrl="~/Images/image icon.png" CssClass="hover-thumbnail gridImage img-thumbnail" />
                                <div class="img-actions">
                                    <asp:LinkButton ID="btnDownloadInvoice" runat="server" CssClass="btn btn-sm btn-outline-primary" OnClick="btnDownloadInvoice_Click">
                                        <i class="fas fa-download"></i> Download
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnRemoveInvoice" runat="server" CssClass="btn btn-sm btn-outline-danger" Visible="false" OnClick="btnRemoveInvoice_Click">
                                        <i class="fas fa-trash"></i> Remove
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <!-- PAN -->
                            <div class="col-md-3 text-center">
                                <label class="field-label">Customer PAN</label>
                                <asp:Image ID="imgPan" runat="server" ImageUrl="~/Images/image icon.png" CssClass="hover-thumbnail gridImage img-thumbnail" />
                                <div class="img-actions">
                                    <asp:LinkButton ID="btnDownloadPan" runat="server" CssClass="btn btn-sm btn-outline-primary" OnClick="btnDownloadPan_Click">
                                        <i class="fas fa-download"></i> Download
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnRemovePan" runat="server" CssClass="btn btn-sm btn-outline-danger" Visible="false" OnClick="btnRemovePan_Click">
                                        <i class="fas fa-trash"></i> Remove
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <!-- Action Buttons -->
                <div class="text-right mt-3">
              <%--      <asp:Button ID="btnApproveed" runat="server" Text="Approve" CssClass="btn btn-success" OnClick="btnApproveed_Click" Visible="false" />
                           <asp:Button ID="btnRejected" runat="server" Text="Rejected" CssClass="btn btn-success" OnClick="btnRejected_Click" Visible="false" />--%>

                    <asp:Button 
    ID="btnAction" 
    runat="server" 
    Text="Verify & Update" 
    CssClass="btn btn-primary"
    OnClientClick="openApprovalModal(); return false;"  Visible="false" />


                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-warning" OnClick="btnUpdate_Click" Visible="false" />
                    <asp:Button ID="btnDelete" runat="server" Text="Close" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
                </div>

            </div>
        </div>
    </div>

    <!-- Approval Modal -->
<div class="modal fade" id="approvalModal" tabindex="-1">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">

            <div class="modal-header">
                <h5 class="modal-title">Confirm Action</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
            </div>

            <div class="modal-body text-center">
                <p>Do you want to approve or reject this request?</p>

                <asp:Button 
                    ID="btnApprove" 
                    runat="server"
                    Text="Approve"
                    CssClass="btn btn-success me-2"
                    OnClick="Action_Click"
                    CommandArgument="APPROVED" />

                <asp:Button 
                    ID="btnReject" 
                    runat="server"
                    Text="Reject"
                    CssClass="btn btn-danger"
                    OnClick="Action_Click"
                    CommandArgument="REJECTED" />
            </div>

        </div>
    </div>
</div>

    <!-- Image Zoom Modal (single modal used for all images) -->
    <div class="modal fade" id="imageZoomModal" tabindex="-1" role="dialog" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
        <div class="modal-content">
          <div class="modal-body text-center p-0">
            <img id="zoomedImage" src="#" alt="Zoom" style="max-width:100%; max-height:80vh; display:block; margin:0 auto;" />
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Close</button>
          </div>
        </div>
      </div>
    </div>

    <!-- Generic Error / Message Modal (re-usable) -->
    <div id="ErrorPage" class="modal fade" role="dialog">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">Message</h5>
          </div>
          <div class="modal-body">
            <!-- changed to server-side Label so you can set it from code-behind, client id remains 'lblerror' -->
            <asp:Label ID="lblerror" runat="server" ClientIDMode="Static" CssClass="text-danger" Style="font-weight:bold;" ></asp:Label>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Close</button>
          </div>
        </div>
      </div>
    </div>

    <!-- jQuery and Bootstrap JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            // open image in modal when clicked
            $('.gridImage').on('click', function () {
                var src = $(this).attr('src') || $(this).prop('src');
                if (!src) return;
                $('#zoomedImage').attr('src', src);
                $('#imageZoomModal').modal('show');
            });
        });

        // function to show message from server-side
        function ShowErrors(msg) {
            $('#lblerror').text(msg);
            $('#ErrorPage').modal('show');
        }
    </script>

    <script>
        function openApprovalModal() {
            var modal = new bootstrap.Modal(document.getElementById('approvalModal'));
            modal.show();
        }
    </script>

    <script>
        function showSuccess(msg) {
            Swal.fire({
                icon: 'success',
                title: 'Success',
                text: msg,
                confirmButtonColor: '#198754'
            });
        }

        function showError(msg) {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: msg,
                confirmButtonColor: '#dc3545'
            });
        }

        function showWarning(msg) {
            Swal.fire({
                icon: 'warning',
                title: 'Warning',
                text: msg,
                confirmButtonColor: '#ffc107'
            });
        }
    </script>




</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
    <!-- Place additional page-specific scripts here if needed -->
</asp:Content>
