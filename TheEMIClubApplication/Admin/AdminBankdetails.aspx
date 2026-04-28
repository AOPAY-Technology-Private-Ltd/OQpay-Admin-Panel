<%@ Page Title="Admin Bank Details" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdminBankdetails.aspx.cs" Inherits="TheEMIClubApplication.Admin.AdminBankdetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
<%--    <style>
    .qr-hover {
        transition: transform 0.3s ease;
        cursor: pointer;
        transform-origin: top right;  
    }

    .qr-hover:hover {
        transform: scale(3);   /* Increase size on hover */
        z-index: 9999;
        position: relative;
    }
</style>--%>

    <style>
    .qr-thumbnail {
        width: 80px;
        height: 80px;
        cursor: pointer;
    }

    /* Hidden zoom image (center of screen) */
    .qr-preview {
        display: none;
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        z-index: 9999;
        width: 300px;
        height: 300px;
        border: 3px solid #fff;
        box-shadow: 0 0 15px rgba(0,0,0,0.6);
        background: #fff;
    }
</style>

    <div class="container-fluid mt-4">
        <!-- 🔹 Filter Section -->
        <div class="card shadow-lg border-0 mb-4">
            <div class="card-header bg-gradient-primary text-white d-flex justify-content-between align-items-center">
                <h5 class="mb-0"><i class="fas fa-search mr-2"></i>Admin Bank Details</h5>
            </div>
            <div class="card-body">
                <asp:Label ID="lblMessage" runat="server" CssClass="text-success mb-2 d-block"></asp:Label>


                <asp:ValidationSummary ID="vsBankForm" runat="server"
    ValidationGroup="BankForm"
    CssClass="text-danger"
    HeaderText="Please fix the following errors:" />

                <div class="row g-3">
                    <!-- Account Number -->
                    <div class="col-md-3">
    <label class="form-label fw-bold">Account Number</label>
    <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="form-control" MaxLength="18"></asp:TextBox>

    <asp:RequiredFieldValidator ID="rfvAccountNumber" runat="server"
        ControlToValidate="txtAccountNumber"
        ErrorMessage="Enter Account Number"
        ValidationGroup="BankForm"
        CssClass="text-danger small" Display="Dynamic" />

    <!-- Only Numbers -->
    <asp:RegularExpressionValidator ID="revAccountNumber" runat="server"
        ControlToValidate="txtAccountNumber"
        ValidationExpression="^[0-9]{9,18}$"
        ErrorMessage="Account Number must be 9-18 digits"
        ValidationGroup="BankForm"
        CssClass="text-danger small" Display="Dynamic" />
</div>

                    <!-- Account Name -->
                    <div class="col-md-3">
    <label class="form-label fw-bold">Account Name</label>
    <asp:TextBox ID="txtAccountName" runat="server" CssClass="form-control"></asp:TextBox>

    <asp:RequiredFieldValidator ID="rfvAccountName" runat="server"
        ControlToValidate="txtAccountName"
        ErrorMessage="Enter Account Name"
        ValidationGroup="BankForm"
        CssClass="text-danger small" Display="Dynamic" />

    <asp:RegularExpressionValidator ID="revAccountName" runat="server"
        ControlToValidate="txtAccountName"
        ValidationExpression="^[a-zA-Z ]+$"
        ErrorMessage="Only alphabets allowed"
        ValidationGroup="BankForm"
        CssClass="text-danger small" Display="Dynamic" />
</div>

                    <!-- Bank Name -->
                    <div class="col-md-3">
    <label class="form-label fw-bold">Bank Name</label>
    <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control"></asp:TextBox>

    <asp:RequiredFieldValidator ID="rfvBankName" runat="server"
        ControlToValidate="txtBankName"
        ErrorMessage="Enter Bank Name"
        ValidationGroup="BankForm"
        CssClass="text-danger small" Display="Dynamic" />

    <asp:RegularExpressionValidator ID="revBankName" runat="server"
        ControlToValidate="txtBankName"
        ValidationExpression="^[a-zA-Z ]+$"
        ErrorMessage="Only alphabets allowed"
        ValidationGroup="BankForm"
        CssClass="text-danger small" Display="Dynamic" />
</div>

                    <!-- IFSC Code -->
                    <div class="col-md-3">
    <label class="form-label fw-bold">IFSC Code</label>
    <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control text-uppercase" MaxLength="11" style="text-transform:uppercase;"></asp:TextBox>

    <asp:RequiredFieldValidator ID="rfvIFSC" runat="server"
        ControlToValidate="txtIFSCCode"
        ErrorMessage="Enter IFSC Code"
        ValidationGroup="BankForm"
        CssClass="text-danger small"
        Display="Dynamic" />

    <asp:RegularExpressionValidator ID="revIFSC" runat="server"
        ControlToValidate="txtIFSCCode"
        ValidationExpression="^[A-Z]{4}0[A-Z0-9]{6}$"
        ErrorMessage="Invalid IFSC Code format"
        ValidationGroup="BankForm"
        CssClass="text-danger small"
        Display="Dynamic" />
</div>

                    <!-- Branch Name -->
                    <div class="col-md-3">
    <label class="form-label fw-bold">Branch Name</label>
    <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control"></asp:TextBox>

    <asp:RequiredFieldValidator ID="rfvBranchName" runat="server"
        ControlToValidate="txtBranchName"
        ErrorMessage="Enter Branch Name"
        ValidationGroup="BankForm"
        CssClass="text-danger small" Display="Dynamic" />
</div>

                    <!-- Branch Address -->
                   <div class="col-md-3">
    <label class="form-label fw-bold">Branch Address</label>
    <asp:TextBox ID="txtBranchAddress" runat="server" CssClass="form-control"></asp:TextBox>

    <asp:RequiredFieldValidator ID="rfvBranchAddress" runat="server"
        ControlToValidate="txtBranchAddress"
        ErrorMessage="Enter Branch Address"
        ValidationGroup="BankForm"
        CssClass="text-danger small" Display="Dynamic" />
</div>

                    <!-- Active Status -->
                    <div class="col-md-3">
                        <label class="form-label fw-bold">Active Status</label>
                        <asp:DropDownList ID="ddlActiveStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Select" Value="" />
                            <asp:ListItem Text="Active" Value="Active" />
                            <asp:ListItem Text="Inactive" Value="Inactive" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvStatus" runat="server"
                            ControlToValidate="ddlActiveStatus" InitialValue=""
                            ErrorMessage="Select status" ValidationGroup="BankForm"
                            CssClass="text-danger small" Display="Dynamic" />
                    </div>

                    <!-- Generate QR Checkbox -->
                    <div class="col-md-12 mt-2">
                        <asp:CheckBox ID="chk_GenerateQR" runat="server"
                            AutoPostBack="true"
                            OnCheckedChanged="chk_GenerateQR_CheckedChanged" />
                        <label class="fw-bold ms-1">Generate Static QR</label>
                    </div>

                    <!-- Extra Fields (hidden until checkbox checked) -->
                    <div class="row g-3 mt-1 col-md-12" id="qrRow" runat="server" visible="false">

                        <div class="col-md-3">
                            <label class="fw-bold">Business Name</label>
                            <asp:TextBox ID="txtbusinessName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                ControlToValidate="txtbusinessName" ErrorMessage="Enter Business Name"
                                ValidationGroup="BankForm"
                                CssClass="text-danger small" Display="Dynamic" />
                        </div>
                        <div class="col-md-3">
                            <label class="fw-bold">Seller Identifier</label>
                            <asp:TextBox ID="txtsellerIdentifier" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                ControlToValidate="txtsellerIdentifier" ErrorMessage="Enter Seller Identifier"
                                ValidationGroup="BankForm"
                                CssClass="text-danger small" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="revSellerIdentifier" runat="server"
                                ControlToValidate="txtsellerIdentifier"
                                ValidationGroup="BankForm"
                                ErrorMessage="Seller Identifier must be 3 to 10 characters."
                                CssClass="text-danger small" Display="Dynamic"
                                ValidationExpression="^.{3,10}$">
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-3">
                            <label class="fw-bold">Mobile Number</label>
                           <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txtMobileNo" ErrorMessage="Enter Mobile Number"
                                ValidationGroup="BankForm"
                                CssClass="text-danger small" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="revMobile" runat="server"
                                ControlToValidate="txtMobileNo"
                                ValidationGroup="BankForm"
                                ErrorMessage="Enter a valid 10-digit mobile number."
                                CssClass="text-danger small"
                                Display="Dynamic"
                                ValidationExpression="^[0-9]{10}$">
                            </asp:RegularExpressionValidator>
                        </div>

                        <div class="col-md-3">
                            <label class="fw-bold">Email </label>
                            <asp:TextBox ID="txtEmailid" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="txtEmailid" ErrorMessage="Enter Email id"
                                ValidationGroup="BankForm"
                                CssClass="text-danger small" Display="Dynamic" />
                        </div>

                        <!-- QR Image -->
                        <div class="col-md-3 text-center" style="display:none">
                            <label class="fw-bold">QR Image</label><br />
                            <asp:Image ID="img_qrimage" runat="server" Width="100" Height="100" CssClass="border rounded shadow-sm" />                            
                        </div>

                    </div>

                    <div class="row">
                     <asp:HiddenField ID="hdnLat" runat="server" />
                     <asp:HiddenField ID="hdnLon" runat="server" />
                    </div>

                    <div class="row">
                        <div class="col-md-12 mt-3">
                        <div class="alert alert-warning fw-bold small mt-2">
                          ⚠ Disclaimer: The QR code will be generated only once. 
                          Please enter all QR details (Business Name, Seller Identifier, Mobile Number, Email) correctly.
                          Incorrect information may prevent QR code generation.
                      </div>
                            <%--<img id="qrPreview" class="qr-preview" runat="server" ImageUrl='<%# Eval("QRCode_Path") %>' />--%>
                            <asp:Image ID="qrPreview" class="qr-preview" runat="server" ImageUrl='<%# Eval("QRCode_Path") %>'/>
                         </div>
                    </div>

                    <!-- Buttons -->
                    <div class="col-md-12 mt-3">
                        <asp:HiddenField ID="hfRID" runat="server" />
                        <asp:Button ID="btnSave" runat="server" Text="Save"
                            CssClass="btn btn-success me-2"
                            ValidationGroup="BankForm" OnClick="btnSave_Click" />

                        <asp:Button ID="btnUpdate" runat="server" Text="Update"
                            CssClass="btn bg-info me-2"
                            ValidationGroup="BankForm" OnClick="btnUpdate_Click" Visible="false" />

                        <asp:Button ID="btnClear" runat="server" Text="Clear"
                            CssClass="btn btn-secondary" CausesValidation="False" OnClick="btnClear_Click"/>
                    </div>
                </div>

            </div>

        </div>

        <!-- Grid for displaying records -->
        <div class="card shadow-sm mt-4">
            <div class="card-header bg-info text-white">
                <h5 class="mb-0">Bank Records</h5>
            </div>
            <div class="card-body">
                <asp:GridView ID="gvBanks" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                    OnRowCommand="gvBanks_RowCommand" DataKeyNames="RID">
                    <Columns>
                        <asp:BoundField DataField="RID" HeaderText="RID" ReadOnly="True" />
                        <asp:BoundField DataField="AccountNumber" HeaderText="Account Number" />
                        <asp:BoundField DataField="AccountName" HeaderText="Account Name" />
                        <asp:BoundField DataField="BankName" HeaderText="Bank Name" />
                        <asp:BoundField DataField="IFSCCode" HeaderText="IFSC Code" />
                        <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                        <asp:BoundField DataField="BranchAddress" HeaderText="Branch Address" />
                        <asp:BoundField DataField="ActiveStatus" HeaderText="Status" />
                        <asp:TemplateField HeaderText="QR Image">
                            <ItemTemplate>

                                <asp:Image ID="imgQR" runat="server"
    ImageUrl='<%# Eval("QRCode_Path") %>'
    CssClass="qr-thumbnail"
    onmouseover='<%# "showPreview(\"" + Eval("QRCode_Path") + "\")" %>'
    onmouseout="hidePreview()" />
                               <%--   <asp:Image ID="imgQR" runat="server"
            ImageUrl='<%# Eval("QRCode_Path") %>'
            Width="80" Height="80"
            CssClass="img-thumbnail"
             onmouseover="showPreview('<%# Eval("QRCode_Path") %>')"
               onmouseout="hidePreview()" />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditBank" CommandArgument='<%# Eval("RID") %>' CssClass="btn btn-sm btn-primary me-1" Visible="false">Edit</asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteBank" CommandArgument='<%# Eval("RID") %>' OnClientClick="return confirmDelete();" CssClass="btn btn-sm btn-danger">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <!-- Error Modal -->
    <div id="ErrorPage" class="modal fade" role="dialog">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title"><i class="fas fa-exclamation-triangle mr-2"></i>Error Message</h5>
                </div>
                <div class="modal-body">
                    <span id="lblerror" class="text-danger font-weight-bold"></span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Confirmation Modal -->
    <div id="MyPopups" class="modal fade" role="dialog">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title"><i class="fas fa-check-circle mr-2"></i>Confirmation</h5>
                </div>
                <div class="modal-body">
                    <span id="lblMessages" class="text-success font-weight-bold"></span>
                    <br />
                    <span id="lblName" class="text-primary"></span>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-success">OK</button>
                </div>
            </div>
        </div>
    </div>

    <!-- jQuery + Bootstrap -->
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/js/bootstrap.bundle.min.js"></script>
    <script src="https://kit.fontawesome.com/a076d05399.js" crossorigin="anonymous"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>

    <script type="text/javascript">
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


    <script>
        function getLocation() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(showPosition, showError);
            } else {
                alert("Geolocation is not supported by this browser.");
            }
        }

        function showPosition(position) {
            let lat = position.coords.latitude;
            let lon = position.coords.longitude;

            // Set values to hidden fields in ASP.NET
            document.getElementById("<%= hdnLat.ClientID %>").value = lat;
        document.getElementById("<%= hdnLon.ClientID %>").value = lon;

            console.log("Latitude: " + lat + ", Longitude: " + lon);
        }

        function showError(error) {
            switch (error.code) {
                case error.PERMISSION_DENIED:
                    alert("User denied the request for Geolocation.");
                    break;
                case error.POSITION_UNAVAILABLE:
                    alert("Location information is unavailable.");
                    break;
                case error.TIMEOUT:
                    alert("The request to get user location timed out.");
                    break;
                case error.UNKNOWN_ERROR:
                    alert("An unknown error occurred.");
                    break;
            }
        }
    </script>


    <script>
        function showPreview(imgUrl) {
            document.getElementById("qrPreview").src = imgUrl;
            document.getElementById("qrPreview").style.display = "block";
        }

        function hidePreview() {
            document.getElementById("qrPreview").style.display = "none";
        }
    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
    <script type="text/javascript">
        function confirmDelete() {
            return confirm('Are you sure you want to delete this record?');
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>


    
</asp:Content>
