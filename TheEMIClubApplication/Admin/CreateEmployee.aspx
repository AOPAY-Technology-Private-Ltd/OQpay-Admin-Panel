<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="CreateEmployee.aspx.cs"
    Inherits="TheEMIClubApplication.MasterPage.CreateEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container-fluid py-3">
        <div class="card shadow-sm">
            <div class="card-header bg-dark text-white d-flex justify-content-between align-items-center">
                <h5 class="mb-0">Employee Master</h5>

            </div>

            <div class="card-body">
                <asp:HiddenField ID="hfModelID" runat="server" />

                <!-- Personal Information -->
                <div class="card mb-3">
                    <div class="card-header bg-primary text-white">Personal Information</div>
                    <div class="card-body">

                        <asp:ValidationSummary ID="vsBankForm" runat="server"
                            ValidationGroup="EmployeeForm"
                            CssClass="text-danger"
                            HeaderText="Please fix the following errors:" />

                        <div class="form-row">
                            <div class="form-group col-md-4">
                                <asp:Label runat="server" CssClass="font-weight-bold">Employee Code</asp:Label>
                                <asp:TextBox ID="txtCustomerCode" runat="server"
                                    CssClass="form-control" placeholder="Auto Generated"
                                    ReadOnly="true" />
                            </div>
                            <div class="form-group col-md-4">
                                <asp:Label runat="server" CssClass="font-weight-bold">First Name</asp:Label>
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"
                                    placeholder="Enter First Name" />
                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server"
                                    ControlToValidate="txtFirstName"
                                    ErrorMessage="* First Name is required"
                                    Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="CustomerGroup" />

                                <asp:RegularExpressionValidator ID="revFirstName" runat="server"
                                    ControlToValidate="txtFirstName"
                                    ValidationExpression="^[a-zA-Z ]+$"
                                    ErrorMessage="Only alphabets allowed"
                                    ValidationGroup="EmployeeForm"
                                    CssClass="text-danger small" Display="Dynamic" />
                            </div>
                            <div class="form-group col-md-4">
                                <asp:Label runat="server" CssClass="font-weight-bold">Last Name</asp:Label>
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"
                                    placeholder="Enter Last Name" />
                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server"
                                    ControlToValidate="txtLastName"
                                    ErrorMessage="* Last Name is required"
                                    Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="CustomerGroup" />

                                <asp:RegularExpressionValidator ID="revLastName" runat="server"
                                    ControlToValidate="txtLastName"
                                    ValidationExpression="^[a-zA-Z ]+$"
                                    ErrorMessage="Only alphabets allowed"
                                    ValidationGroup="EmployeeForm"
                                    CssClass="text-danger small" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="font-weight-bold">Address</asp:Label>
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control"
                                TextMode="MultiLine" Rows="3" placeholder="Enter Address" />

                            <asp:RegularExpressionValidator ID="revAddress" runat="server"
                                ControlToValidate="txtAddress"
                                ValidationExpression="^[a-zA-Z0-9\s,.\-\/#]+$"
                                ErrorMessage="Enter valid address"
                                ValidationGroup="EmployeeForm"
                                CssClass="text-danger small"
                                Display="Dynamic" />
                        </div>
                    </div>
                </div>

                <!-- Contact Information -->
                <div class="card mb-3">
                    <div class="card-header bg-info text-white">Contact Information</div>
                    <div class="card-body">
                        <div class="form-row">
                            <div class="form-group col-md-6">
                                <asp:Label runat="server" CssClass="font-weight-bold">Mobile No</asp:Label>
                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"
                                    MaxLength="10" placeholder="Enter Mobile Number" />
                                <asp:RequiredFieldValidator ID="rfvMobileNo" runat="server"
                                    ControlToValidate="txtMobileNo"
                                    ErrorMessage="* Mobile No is required"
                                    Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="CustomerGroup" />
                                <asp:RegularExpressionValidator ID="revMobile" runat="server"
                                    ControlToValidate="txtMobileNo"
                                    ValidationExpression="^[6-9]\d{9}$"
                                    ErrorMessage="* Enter valid 10-digit mobile number"
                                    Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="CustomerGroup" />
                            </div>
                            <div class="form-group col-md-6">
                                <asp:Label runat="server" CssClass="font-weight-bold">Email ID</asp:Label>
                                <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control"
                                    placeholder="Enter Email" />
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                                    ControlToValidate="txtEmailID"
                                    ErrorMessage="* Email is required"
                                    Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="CustomerGroup" />
                                <asp:RegularExpressionValidator ID="revEmail" runat="server"
                                    ControlToValidate="txtEmailID"
                                    ErrorMessage="* Invalid Email format"
                                    Display="Dynamic" ForeColor="Red"
                                    ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                                    ValidationGroup="CustomerGroup" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Identity Information -->
                <div class="card mb-3">
                    <div class="card-header bg-warning text-dark">Identity Information</div>
                    <div class="card-body">
                        <div class="form-row">
                            <div class="form-group col-md-6">
                                <asp:Label runat="server" CssClass="font-weight-bold">Aadhar Number</asp:Label>
                                <asp:TextBox ID="txtAadhar" runat="server" CssClass="form-control"
                                    MaxLength="12" placeholder="Enter Aadhar Number" />
                                <asp:RequiredFieldValidator ID="rfvAadhar" runat="server"
                                    ControlToValidate="txtAadhar"
                                    ErrorMessage="* Aadhar is required"
                                    Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="CustomerGroup" />
                                <asp:RegularExpressionValidator ID="revAadhar" runat="server"
                                    ControlToValidate="txtAadhar"
                                    ValidationExpression="^\d{12}$"
                                    ErrorMessage="* Enter valid 12-digit Aadhar number"
                                    Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="CustomerGroup" />
                            </div>
                            <div class="form-group col-md-6">
                                <asp:Label runat="server" CssClass="font-weight-bold">PAN Number</asp:Label>
                                <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control"
                                    MaxLength="10" placeholder="Enter PAN Number" />
                                <asp:RequiredFieldValidator ID="rfvPAN" runat="server"
                                    ControlToValidate="txtPAN"
                                    ErrorMessage="* PAN is required"
                                    Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="CustomerGroup" />
                                <asp:RegularExpressionValidator ID="revPAN" runat="server"
                                    ControlToValidate="txtPAN"
                                    ValidationExpression="^[A-Z]{5}[0-9]{4}[A-Z]{1}$"
                                    ErrorMessage="* Enter valid PAN (e.g., ABCDE1234F)"
                                    Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="CustomerGroup" />
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Login & Status -->
                <div class="card mb-3">
                    <div class="card-header bg-secondary text-white">Login & Status</div>
                    <div class="card-body">
                        <div class="form-row">
                            <div class="form-group col-md-6">
                                <asp:Label runat="server" CssClass="font-weight-bold">Password</asp:Label>
                                <asp:TextBox ID="txtPassword" runat="server"
                                    CssClass="form-control" placeholder="Enter Password" />
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                                    ControlToValidate="txtPassword"
                                    ErrorMessage="* Enter Password"
                                    Display="Dynamic" ForeColor="Red"/>
                            </div>
                            <div class="form-group col-md-6">
                                <asp:Label runat="server" CssClass="font-weight-bold">Active Status</asp:Label>
                                <asp:DropDownList ID="ddlActiveStatus" runat="server"
                                    CssClass="form-control">
                                    <asp:ListItem Text="Active" Value="Active" />
                                    <asp:ListItem Text="Inactive" Value="Inactive" />
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Action Buttons -->
                <div class="text-center">
                    <asp:Button ID="btnSave" runat="server" Text="Save"
                        CssClass="btn btn-success mr-2"
                       OnClick="btnSave_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update"
                        CssClass="btn btn-primary mr-2"
                       OnClick="btnUpdate_Click" Visible="false" />
                    <asp:Button ID="btnClear" runat="server" Text="Close"
                        CssClass="btn btn-secondary" CausesValidation="false"
                        OnClick="btnClear_Click" />
                </div>
            </div>
        </div>

        <!-- Employee Grid -->

    </div>

    <!-- Bootstrap/JS already included -->
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
                    <%--  <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/Images/ExcelIcon.png"  OnClick="btnExportExcel_Click" />--%>
                    <%-- <asp:GridView ID="gvContactList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                  
                        <Columns>
                            <asp:BoundField DataField="ContactName" HeaderText="Contact Name"  />
                            <asp:BoundField DataField="ContactNumber" HeaderText="Contact Number" />
                       
                        </Columns>
                    </asp:GridView>--%>
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

    <script>
        // jQuery function to handle click event on image
        $(document).ready(function () {
            $(".gridImage").click(function () {
                // Get the source of the clicked image
                var imgSrc = $(this).attr("src");
                // Set the source of the zoomed image
                $("#zoomedImage img").attr("src", imgSrc);
                // Show the zoomed image
                $("#zoomedImage").fadeIn();
            });

            // Hide zoomed image when clicked anywhere outside the zoomed image
            $("#zoomedImage").click(function () {
                $(this).fadeOut();
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
