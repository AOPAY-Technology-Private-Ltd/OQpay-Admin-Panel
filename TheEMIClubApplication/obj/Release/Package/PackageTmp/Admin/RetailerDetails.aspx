<%@ Page Title="Retailer Details" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="RetailerDetails.aspx.cs" Inherits="TheEMIClubApplication.MasterPage.RetailerDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!-- Bootstrap 4.6 & FontAwesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" />

    <style>
        .form-group label {
            font-weight: 500;
        }

        label::before,
        label::after {
            content: none !important;
        }
    </style>
    <style>
        .form-header-bar {
            background: linear-gradient(90deg,#2b7be3,#1b6fd8);
            color: #fff;
            padding: 12px 16px;
            border-radius: .25rem .25rem 0 0;
            margin-bottom: 1rem;
        }

        .form-card {
            box-shadow: 0 0 10px rgba(0,0,0,0.05);
            border-radius: .25rem;
            margin-bottom: 1.5rem;
        }

        .field-label {
            font-weight: 600;
            margin-bottom: 4px;
            display: block;
        }

        .btn-space {
            margin-right: 10px;
        }

        .input-group-text {
            background-color: #f0f0f0;
        }

        .modal-body-scroll {
            max-height: 300px;
            overflow-y: auto;
        }
    </style>

    <style>
        /* Fix image size for preview thumbnail */
.fixed-image {
    width: 150px !important;   /* fixed width */
    height: 150px !important;  /* fixed height */
    object-fit: cover;         /* keeps aspect ratio while filling area */
    border: 1px solid #ddd;
    border-radius: 8px;
    background-color: #f8f9fa;
}

/* Optional hover zoom effect styling */
/*.image-hover-wrapper {
    position: relative;
    display: inline-block;
}

.zoom-center {
    display: none;
    position: absolute;
    top: 0;
    left: 0;
}

.image-hover-wrapper:hover .zoom-center {
    display: block;
}

.zoomed-image {
    width: 300px;
    height: 300px;
    object-fit: contain;
    border: 2px solid #ccc;
    background: #fff;
    z-index: 999;
}*/

    </style>

    <div class="container-fluid py-3">
        <div class="form-card card">
            <div class="form-header-bar">
                <h4 class="mb-0"><i class="fas fa-user-tie mr-2"></i>Dealer Details</h4>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfCustomerID" runat="server" />

                <div class="form-row">

                    <!-- Customer Code -->
                    <div class="form-group col-md-4">
                        <label class="field-label">Customer Code</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-id-badge"></i></span>
                            </div>
                            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" placeholder="Enter Customer Code"></asp:TextBox>
                        </div>
                    </div>

                    <!-- First Name -->
                    <div class="form-group col-md-4">
                        <label class="field-label">First Name</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-user"></i></span>
                            </div>
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="Enter First Name"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Last Name -->
                    <div class="form-group col-md-4">
                        <label class="field-label">Last Name</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-user"></i></span>
                            </div>
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Enter Last Name"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Mobile Number -->
                    <div class="form-group col-md-4">
                        <label class="field-label">Mobile Number</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-phone"></i></span>
                            </div>
                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="Enter Mobile Number" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Email -->
                    <div class="form-group col-md-4">
                        <label class="field-label">Email ID</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-envelope"></i></span>
                            </div>
                            <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control" placeholder="Enter Email ID" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Aadhar -->
                    <div class="form-group col-md-4">
                        <label class="field-label">Aadhar Number</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-id-card"></i></span>
                            </div>
                            <asp:TextBox ID="txtAadharNumber" runat="server" CssClass="form-control" placeholder="Enter Aadhar Number"></asp:TextBox>
                        </div>
                    </div>
                    <!-- PAN -->
                    <div class="form-group col-md-4">
                        <label class="field-label">PAN Number</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-file-alt"></i></span>
                            </div>
                            <asp:TextBox ID="txtPANNumber" runat="server" CssClass="form-control" placeholder="Enter PAN Number"></asp:TextBox>
                        </div>
                    </div>
                    <!-- Address -->
                    <div class="form-group col-md-8">
                        <label class="field-label">Address</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-map-marker-alt"></i></span>
                            </div>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Enter Address"></asp:TextBox>
                        </div>
                    </div>

                    <!-- ✅ New Store Details -->
                    <div class="form-group col-md-4">
                        <label class="field-label">Store Name</label>
                        <div class="input-group">
                            <div class="input-group-prepend"><span class="input-group-text"><i class="fas fa-store"></i></span></div>
                            <asp:TextBox ID="txtStoreName" runat="server" CssClass="form-control" placeholder="Enter Store Name"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group col-md-8">
                        <label class="field-label">Store Address</label>
                        <div class="input-group">
                            <div class="input-group-prepend"><span class="input-group-text"><i class="fas fa-map"></i></span></div>
                            <asp:TextBox ID="txtStoreAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Enter Store Address"></asp:TextBox>
                        </div>
                    </div>

                    <!-- ✅ Document Uploads -->
                    <div class="form-group col-md-4">
                        <div class="image-hover-wrapper">
                            <asp:Label ID="Label6" runat="server" CssClass="font-weight-bold">Profile Photo</asp:Label>
                            <asp:Image ID="img_ProfilePhoto"  runat="server" CssClass="img-thumbnail mt-2 d-block fixed-image"  />
                            <div class="zoom-center">
                                <img runat="server" id="Img6" class="zoomed-image" />
                            </div>
                        </div>

                        <div class="mt-2">
                            <asp:FileUpload ID="fuProfilePhoto" runat="server" CssClass="form-control" />
                            <asp:Button ID="btn_ProfilePhotoUpload" runat="server" Text="Edit" Visible="false" CssClass="btn btn-sm btn-secondary me-2"  /> <%--OnClick="btn_ProfilePhotoUpload_Click"--%>
                            <asp:LinkButton ID="lnk_ProfilePhotoDownload" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="lnk_ProfilePhotoDownload_Click" />
                            <asp:LinkButton ID="lnk_ProfilePhotoRemove" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" />
                        </div>
                    </div>

                    <div class="form-group col-md-4">

                        <div class="image-hover-wrapper">
                            <asp:Label ID="Label5" runat="server" CssClass="font-weight-bold">Aadhaar Front Photo</asp:Label>
                            <asp:Image ID="img_AadhaarFrontPhoto"  runat="server" CssClass="img-thumbnail mt-2 d-block fixed-image"  />
                            <div class="zoom-center">
                                <img runat="server" id="Img5" class="zoomed-image" />
                            </div>
                        </div>

                        <div class="mt-2">
                            <asp:FileUpload ID="fuAadhaarFront" runat="server" CssClass="form-control" />
                            <asp:Button ID="btn_AadhaarFrontPhotoUpload" runat="server" Text="Edit" Visible="false" CssClass="btn btn-sm btn-secondary me-2" /> <%--OnClick="btn_AadhaarFrontPhotoUpload_Click"--%>
                            <asp:LinkButton ID="lnk_AadhaarFrontPhotoDownload" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="lnk_AadhaarFrontPhotoDownload_Click" />
                            <asp:LinkButton ID="lnk_AadhaarFrontPhotoRemove" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" />
                        </div>
                    </div>

                    <div class="form-group col-md-4">
                        <div class="image-hover-wrapper">
                            <asp:Label ID="Label4" runat="server" CssClass="font-weight-bold">Aadhaar Back Photo</asp:Label>
                            <asp:Image ID="img_AadhaarBackPhoto"  runat="server" 
                                 CssClass="img-thumbnail mt-2 d-block fixed-image"  />
                            <div class="zoom-center">
                                <img runat="server" id="Img4" class="zoomed-image" />
                            </div>
                        </div>

                        <div class="mt-2">
                            <asp:FileUpload ID="fuAadhaarBack" runat="server" CssClass="form-control" />
                            <asp:Button ID="btn_AadhaarBackPhotoUpload" runat="server" Text="Edit" Visible="false" CssClass="btn btn-sm btn-secondary me-2"  />  <%--OnClick="btn_AadhaarBackPhotoUpload_Click"--%>
                            <asp:LinkButton ID="lnk_AadhaarBackPhotoDownload" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="lnk_AadhaarBackPhotoDownload_Click" />
                            <asp:LinkButton ID="lnk_AadhaarBackPhotoRemove" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" />
                        </div>
                    </div>

                    <div class="form-group col-md-4">

                        <div class="image-hover-wrapper">
                            <asp:Label ID="Label3" runat="server" CssClass="font-weight-bold">Cancel Cheque Photo</asp:Label>
                            <asp:Image ID="img_CancelChequePhoto"  runat="server" 
                                 CssClass="img-thumbnail mt-2 d-block fixed-image"  />
                            <div class="zoom-center">
                                <img runat="server" id="Img3" class="zoomed-image" />
                            </div>
                        </div>

                        <div class="mt-2">
                            <asp:FileUpload ID="fuCancelCheque" runat="server" CssClass="form-control" />
                            <asp:Button ID="btn_CancelChequePhotoUpload" runat="server" Text="Edit" Visible="false" CssClass="btn btn-sm btn-secondary me-2"  />  <%--OnClick="btn_CancelChequePhotoUpload_Click"--%>
                            <asp:LinkButton ID="lnk_CancelChequePhotoDownload" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="lnk_CancelChequePhotoDownload_Click" />
                            <asp:LinkButton ID="lnk_CancelChequePhotoRemove" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" />
                        </div>
                    </div>

                    <div class="form-group col-md-4">
                        <div class="image-hover-wrapper">
                            <asp:Label ID="Label2" runat="server" CssClass="font-weight-bold">Store Front Photo</asp:Label>
                            <asp:Image ID="img_StoreFrontPhoto"  runat="server" 
                                 CssClass="img-thumbnail mt-2 d-block fixed-image"  />
                            <div class="zoom-center">
                                <img runat="server" id="Img2" class="zoomed-image" />
                            </div>
                        </div>

                        <div class="mt-2">
                            <asp:FileUpload ID="fuStoreFront" runat="server" CssClass="form-control" />
                            <asp:Button ID="btn_StoreFrontPhotoUpload" runat="server" Text="Edit" Visible="false" CssClass="btn btn-sm btn-secondary me-2"  />  <%--OnClick="btn_StoreFrontPhotoUpload_Click"--%>
                            <asp:LinkButton ID="lnk_StoreFrontPhotoDownload" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="lnk_StoreFrontPhotoDownload_Click"/>
                            <asp:LinkButton ID="lnk_StoreFrontPhotoRemove" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" />
                        </div>
                    </div>

                    <div class="form-group col-md-4">
                        <div class="image-hover-wrapper">
                            <asp:Label ID="Label1" runat="server" CssClass="font-weight-bold">Company Document Photo</asp:Label>
                            <asp:Image ID="img_CompanyDocumentPhoto"  runat="server" 
                                 CssClass="img-thumbnail mt-2 d-block fixed-image"  />
                            <div class="zoom-center">
                                <img runat="server" id="Img1" class="zoomed-image" />
                            </div>
                        </div>

                        <div class="mt-2">
                            <asp:FileUpload ID="fuCompanyDoc" runat="server" CssClass="form-control" />
                            <asp:Button ID="btn_CompanyDocumentPhotoUpload" runat="server" Text="Edit" Visible="false" CssClass="btn btn-sm btn-secondary me-2"  /> <%-- OnClick="btn_CompanyDocumentPhotoUpload_Click"--%>
                            <asp:LinkButton ID="lnk_CompanyDocumentPhotoDownload" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="lnk_CompanyDocumentPhotoDownload_Click" />
                            <asp:LinkButton ID="lnk_CompanyDocumentPhotoRemove" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" />
                        </div>

                    </div>








                    <!-- Active Status -->
                    <div class="form-group col-md-4">
                        <label class="field-label">Active Status</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text"><i class="fas fa-toggle-on"></i></span>
                            </div>
                            <asp:DropDownList ID="ddlActiveStatus" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Approved" Value="Approve" />
                                 <asp:ListItem Text="Rejected" Value="Reject" />
                                <asp:ListItem Text="Inactive" Value="Inactive" />
                            </asp:DropDownList>
                        </div>
                    </div>





                </div>

                <!-- Buttons -->
                <div class="text-center mt-3">
                    <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-success btn-space" Text="Approve" OnClick="btnApprove_Click" Visible="false" />
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-warning btn-space" Text="Update" OnClick="btnSave_Click" />
                    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-danger btn-space" Text="Close" OnClick="btnClose_Click" />
                </div>
            </div>
    </div>
    </div>

    <!-- jQuery & Bootstrap JS -->
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



    <!-- Bootstrap Modal -->
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
                    <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/Images/ExcelIcon.png" />
                    <asp:GridView ID="gvContactList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">

                        <Columns>
                            <asp:BoundField DataField="ContactName" HeaderText="Contact Name" />
                            <asp:BoundField DataField="ContactNumber" HeaderText="Contact Number" />

                        </Columns>
                    </asp:GridView>

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
    <style>
        /* Define styles for the zoomed image */
        #zoomedImage {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.8);
            text-align: center;
            z-index: 9999;
        }

            #zoomedImage img {
                max-width: 90%;
                max-height: 90%;
                margin-top: 5%;
            }
    </style>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
