<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="ComapnyMst.aspx.cs" Inherits="TheEMIClubApplication.MasterPage.ComapnyMst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>

<!-- Bootstrap 4 -->

<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <style>
        .image-hover-wrapper {
            display: inline-block;
            position: relative;
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
            z-index: 9999;
            display: none;
            border: 1px solid #ddd;
            background: white;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.4);
        }

        .zoomed-image {
            width: 100%;
            height: 100%;
            object-fit: contain;
        }

        /* Show zoom when thumbnail is hovered */
        .image-hover-wrapper:hover .zoom-center {
            display: block;
        }
    </style>
    <div class="content">
        <div class="container-fluid">
            <div class="card card-default">
                <div class="card-header form-header-bar">
                    <h3 class="card-title">Company Master</h3>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12 card-body-box">
                            <div class="row">
                                <asp:HiddenField ID="hfRID" runat="server" />

                                <!-- Company Info -->
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="font-weight-bold">Company Name</span>
                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" ReadOnly="true" placeholder="Enter Company Name" />
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="font-weight-bold">Client Code</span>
                                        <asp:TextBox ID="txtClientCode" runat="server" CssClass="form-control" placeholder="Enter Client Code" ReadOnly="true" />
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="font-weight-bold">Address</span>
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Enter Address" TextMode="MultiLine" Rows="2" ReadOnly="true" />
                                    </div>
                                </div>

                                <!-- Location -->
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="font-weight-bold">City</span>
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="font-weight-bold">State</span>
                                        <asp:TextBox ID="txtState" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="font-weight-bold">Country</span>
                                        <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="font-weight-bold">Zip</span>
                                        <asp:TextBox ID="txtZip" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                </div>

                                <!-- Contact -->
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="font-weight-bold">Phone</span>
                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="font-weight-bold">Email</span>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" ReadOnly="true" />
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="font-weight-bold">Website</span>
                                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                                <!-- People -->
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="font-weight-bold">First Name</span>
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="font-weight-bold">Last Name</span>
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                                <!-- Account Info -->
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="font-weight-bold">Username</span>
                                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" ReadOnly="true" />
                                    </div>
                                </div>



                                <!-- Status and Type -->
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="font-weight-bold">Status</span>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" ReadOnly="true" Enabled="false">
                                            <asp:ListItem Text="Active" Value="Active" />
                                            <asp:ListItem Text="Inactive" Value="Inactive" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <%-- <span class="font-weight-bold">Password</span>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" ReadOnly="true" />--%>

                                        <span class="font-weight-bold">MemberShip Fees</span>
                                        <asp:TextBox ID="txtMemberShipFees" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <%--<span class="font-weight-bold">Company Type</span>
                                    <asp:TextBox ID="txtCompanyType" runat="server" CssClass="form-control" ReadOnly="true" />--%>
                                        <span class="font-weight-bold">Late Fine</span>
                                        <asp:TextBox ID="txtLatefine" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <%--<span class="font-weight-bold">Company Type</span>
                <asp:TextBox ID="txtCompanyType" runat="server" CssClass="form-control" ReadOnly="true" />--%>
                                        <span class="font-weight-bold">Reserve Type</span>
                                        <asp:DropDownList ID="ddlReservetype" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlReservetype_SelectedIndexChanged">
                                            <asp:ListItem Text="-- Select Reserve Type --" Value="" Selected="True" Disabled="True"></asp:ListItem>
                                            <asp:ListItem Text="Flat" Value="Flat"></asp:ListItem>
                                            <%--    <asp:ListItem Text="Percentage" Value="Percentage"></asp:ListItem>--%>
                                            <asp:ListItem Text="Not Applicable" Value="Not Applicable"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <%--<span class="font-weight-bold">Company Type</span>
                <asp:TextBox ID="txtCompanyType" runat="server" CssClass="form-control" ReadOnly="true" />--%>
                                        <span class="font-weight-bold">Reserve values</span>
                                        <asp:TextBox ID="txtreservevalues" runat="server" CssClass="form-control" />
                                    </div>
                                </div>


                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="font-weight-bold">Grace Period</span>
                                        <asp:TextBox ID="txtgraceperiod" runat="server" CssClass="form-control" TextMode="Number" />

                                        <!-- Required Field Validator -->
                                        <asp:RequiredFieldValidator
                                            ID="rfvGracePeriod"
                                            runat="server"
                                            ControlToValidate="txtgraceperiod"
                                            ErrorMessage="Grace Period is required."
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            SetFocusOnError="true" />

                                        <!-- Regular Expression Validator -->
                                        <asp:RegularExpressionValidator
                                            ID="revGracePeriod"
                                            runat="server"
                                            ControlToValidate="txtgraceperiod"
                                            ValidationExpression="^\d+$"
                                            ErrorMessage="Only numeric values allowed."
                                            ForeColor="Red"
                                            Display="Dynamic"
                                            SetFocusOnError="true" />

                                    </div>
                                </div>


                                 <!-- Payout Service Charge Type -->
                                 <div class="col-md-4">
     <div class="form-group">
         <span class="font-weight-bold">Service Charge Type</span>
          <asp:DropDownList ID="ddlServiceChargeType" runat="server" CssClass="form-control" AutoPostBack="true">
     <asp:ListItem Text="-- Select Service Charge Type --" Value="" ></asp:ListItem>
     <asp:ListItem Text="Flat" Value="Flat"></asp:ListItem>
         <asp:ListItem Text="Percentage" Value="Percentage"></asp:ListItem>
     <asp:ListItem Text="Not Applicable" Value="Not Applicable"></asp:ListItem>
 </asp:DropDownList>

         <!-- Required Field Validator -->
         <asp:RequiredFieldValidator
             ID="RequiredFieldValidator1"
             runat="server"
             ControlToValidate="txtgraceperiod"
             ErrorMessage="Grace Period is required."
             ForeColor="Red"
             Display="Dynamic"
             SetFocusOnError="true" />

         <!-- Regular Expression Validator -->
         <asp:RegularExpressionValidator
             ID="RegularExpressionValidator1"
             runat="server"
             ControlToValidate="txtgraceperiod"
             ValidationExpression="^\d+$"
             ErrorMessage="Only numeric values allowed."
             ForeColor="Red"
             Display="Dynamic"
             SetFocusOnError="true" />

     </div>
 </div>

                                 <div class="col-md-4">
     <div class="form-group">
         <span class="font-weight-bold">Service Charge</span>
         <asp:TextBox ID="txtServiceCharge" runat="server" CssClass="form-control"  />

         <!-- Required Field Validator -->
         <asp:RequiredFieldValidator
             ID="RequiredFieldValidator2"
             runat="server"
             ControlToValidate="txtgraceperiod"
             ErrorMessage="Service Charge is required."
             ForeColor="Red"
             Display="Dynamic"
             SetFocusOnError="true" />

         <!-- Regular Expression Validator -->
         <asp:RegularExpressionValidator
             ID="RegularExpressionValidator2"
             runat="server"
             ControlToValidate="txtgraceperiod"
             ValidationExpression="^\d+$"
             ErrorMessage="Only numeric values allowed."
             ForeColor="Red"
             Display="Dynamic"
             SetFocusOnError="true" />

     </div>
 </div>


                                                              <!-- ================= SETTLEMENT CHARGE ================= -->
<div class="col-md-4">
    <div class="form-group">
        <span class="font-weight-bold">Settlement Charge Type</span>
        <asp:DropDownList ID="ddlSettlementChargeType" runat="server"
            CssClass="form-control" AutoPostBack="true"  OnSelectedIndexChanged="ddlSettlementChargeType_SelectedIndexChanged">
            <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
            <asp:ListItem Text="Flat" Value="Flat"></asp:ListItem>
            <asp:ListItem Text="Percentage" Value="Percentage"></asp:ListItem>
            <asp:ListItem Text="Not Applicable" Value="NA"></asp:ListItem>
        </asp:DropDownList>

        <asp:RequiredFieldValidator
            ID="rfvSettlementType"
            runat="server"
            ControlToValidate="ddlSettlementChargeType"
            InitialValue=""
            ErrorMessage="Settlement charge type is required."
            ForeColor="Red"
            Display="Dynamic" />
    </div>
</div>

<div class="col-md-4">
    <div class="form-group">
        <span class="font-weight-bold">Settlement Charge</span>
        <asp:TextBox ID="txtSettlementCharge" runat="server"
            CssClass="form-control" />

        <asp:RequiredFieldValidator
            ID="rfvSettlementCharge"
            runat="server"
            ControlToValidate="txtSettlementCharge"
            ErrorMessage="Settlement charge is required."
            ForeColor="Red"
            Display="Dynamic" />

        <asp:RegularExpressionValidator
            ID="revSettlementCharge"
            runat="server"
            ControlToValidate="txtSettlementCharge"
            ValidationExpression="^\d+(\.\d{1,2})?$"
            ErrorMessage="Enter valid amount."
            ForeColor="Red"
            Display="Dynamic" />
    </div>
</div>


                                <!-- ================= FORECLOSURE CHARGE ================= -->
<div class="col-md-4">
    <div class="form-group">
        <span class="font-weight-bold">Foreclosure Charge Type</span>
        <asp:DropDownList ID="ddlForeclosureChargeType" runat="server"
            CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlForeclosureChargeType_SelectedIndexChanged">
            <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
            <asp:ListItem Text="Flat" Value="Flat"></asp:ListItem>
            <asp:ListItem Text="Percentage" Value="Percentage"></asp:ListItem>
            <asp:ListItem Text="Not Applicable" Value="NA"></asp:ListItem>
        </asp:DropDownList>

        <asp:RequiredFieldValidator
            ID="rfvForeclosureType"
            runat="server"
            ControlToValidate="ddlForeclosureChargeType"
            InitialValue=""
            ErrorMessage="Foreclosure charge type is required."
            ForeColor="Red"
            Display="Dynamic" />
    </div>
</div>

<div class="col-md-4">
    <div class="form-group">
        <span class="font-weight-bold">Foreclosure Charge</span>
        <asp:TextBox ID="txtForeclosureCharge" runat="server"
            CssClass="form-control" />

        <asp:RequiredFieldValidator
            ID="rfvForeclosureCharge"
            runat="server"
            ControlToValidate="txtForeclosureCharge"
            ErrorMessage="Foreclosure charge is required."
            ForeColor="Red"
            Display="Dynamic" />

        <asp:RegularExpressionValidator
            ID="revForeclosureCharge"
            runat="server"
            ControlToValidate="txtForeclosureCharge"
            ValidationExpression="^\d+(\.\d{1,2})?$"
            ErrorMessage="Enter valid amount."
            ForeColor="Red"
            Display="Dynamic" />
    </div>
</div>

                                <div class="col-md-4">
    <div class="form-group">
        <span class="font-weight-bold">Mini Hold Amount Request</span>
        <asp:TextBox ID="txtminiholdamtrequest" runat="server"
            CssClass="form-control" Text="0.00" />

        <asp:RequiredFieldValidator
            ID="rfvminiholdamtrequest"
            runat="server"
            ControlToValidate="txtminiholdamtrequest"
            ErrorMessage="Mini Hold Amount Request is required."
            ForeColor="Red"
            Display="Dynamic" />

        <asp:RegularExpressionValidator
            ID="revminiholdamtrequest"
            runat="server"
            ControlToValidate="txtminiholdamtrequest"
            ValidationExpression="^\d+(\.\d{1,2})?$"
            ErrorMessage="Enter valid amount."
            ForeColor="Red"
            Display="Dynamic" />
    </div>
</div>



                                <!-- Logo Path -->
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="font-weight-bold">Logo Path</span>
                                        <asp:FileUpload ID="fileLogoPath" runat="server" />
                                        <br />
                                        <asp:Button ID="btnUploadImage" runat="server" Text="Upload Image"
                                            CssClass="btn btn-sm btn-primary mt-2" OnClick="btnUploadImage_Click" />
                                        <%--<asp:Button ID="btnimgRemove" runat="server" Text="Remove" 
                                     CssClass="btn btn-sm btn-danger mt-2" OnClick="btnimgRemove_Click" />--%>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span class="font-weight-bold">Preview</span>
                                        <br />
                                        <asp:Image ID="img_Logo" ImageUrl="../My-Files/Images/oqlogo-02-removebg-preview.png" runat="server" Width="120" Height="120" />
                                    </div>
                                </div>


                                <!-- Metadata (Optional or Hidden) -->
                                <asp:HiddenField ID="hfCreateDate" runat="server" />
                                <asp:HiddenField ID="hfLastUpdate" runat="server" />
                                <asp:HiddenField ID="hfEUser" runat="server" />
                                <asp:HiddenField ID="hfEDate" runat="server" />
                                <asp:HiddenField ID="hfMUser" runat="server" />
                                <asp:HiddenField ID="hfMDate" runat="server" />

                                <div class="col-md-12 text-center mt-3">
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success" Text="Update" OnClick="btnUpdate_Click" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-secondary" Text="Close" OnClick="btnClear_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
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
    function ShowConfirmUpdate() {
        Swal.fire({
            title: 'Are you sure?',
            text: 'Do you want to update company details?',
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Yes, Update',
            cancelButtonText: 'Cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                __doPostBack('<%= btnUpdate.UniqueID %>', '');
            }
        });

        return false; // IMPORTANT: stop default postback
    }
</script>


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
