<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="ManageEmployee.aspx.cs"
    Inherits="TheEMIClubApplication.MasterPage.ManageEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container-fluid py-3">
        <div class="card shadow-sm">
<%--   <div class="card-header bg-dark text-white d-flex justify-content-between align-items-center">
    <h5 class="mb-0">Employee Master</h5>
    
    <!-- Redirect Button on the right -->
    <a href="another-page.html" class="btn btn-primary">
        Go to Another Page
    </a>
</div>--%>

                  <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
    <h6 class="mb-0 font-weight-bold">
        <i class="fas fa-search mr-2"></i> Manage Employee
    </h6>
      <a href="CreateEmployee.aspx" class="btn btn-success btn-sm font-weight-bold shadow-sm">
    <i class="fas fa-plus mr-1"></i> Create Employee
</a>
</div>

            <div class="card-body">
                <asp:HiddenField ID="hfModelID" runat="server" />
                        <div class="card shadow-sm mt-4">
            <div class="card-header bg-dark text-white">
                Employee Records
            </div>
            <div class="card-body">
                <!-- Search Section -->
                <div class="form-row mb-3">
                    <div class="form-group col-md-4">
                        <asp:Label runat="server" CssClass="font-weight-bold">Criteria</asp:Label>
                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="form-control">
                            <asp:ListItem Text="All" Value="All" />
                            <asp:ListItem Text="Employee Code" Value="EmployeeCode" />
                            <asp:ListItem Text="Full Name" Value="FullName" />
                            <asp:ListItem Text="Mobile No" Value="MobileNo" />
                            <asp:ListItem Text="Email id" Value="Emailid" />
                            <asp:ListItem Text="Aadhar No" Value="AadharNo" />
                            <asp:ListItem Text="Pan No" Value="PanNo" />
                        </asp:DropDownList>
                    </div>
                    <div class="form-group col-md-5">
                        <asp:Label  runat="server" CssClass="font-weight-bold">Value</asp:Label>
                        <asp:TextBox ID="txtvalues" runat="server"
                            CssClass="form-control" placeholder="Enter Value" />
                    </div>
                    <div class="form-group col-md-3 d-flex align-items-end">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary"
                            Text="Search" CausesValidation="false" OnClick="btnSearch_Click" />
                    </div>
                </div>

                <!-- Grid -->
                <div class="table-responsive">
                    <asp:GridView ID="gvEmployee" runat="server" AutoGenerateColumns="false"
                        CssClass="GridStyle table table-bordered table-condensed table-hover" 
 AllowPaging="true" PageSize="10"
                        OnRowCommand="gvEmployee_RowCommand"
                        DataKeyNames="Empid,ActiveStatus"
                         OnPageIndexChanging="gvEmployee_PageIndexChanging"   OnRowDataBound="gvEmployee_RowDataBound"
     PagerSettings-Mode="Numeric"
     PagerSettings-Position="Bottom"
     PagerStyle-HorizontalAlign="Center"
     PagerStyle-CssClass="pagination-container"
     UseAccessibleHeader="true"
     GridLines="None">
                        <Columns>
  <asp:TemplateField HeaderText="S.No">
            <ItemTemplate>
                <asp:Label ID="lblSNo" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
                            <asp:BoundField HeaderText="Employee Code" DataField="EmployeeCode" />
                            <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                            <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                            <asp:BoundField HeaderText="Mobile No" DataField="MobileNo" />
                            <asp:BoundField HeaderText="Email Id" DataField="EmailID" />
                            <asp:BoundField HeaderText="Aadhar No" DataField="AadharNumber" />
                            <asp:BoundField HeaderText="PAN No" DataField="PANNumber" />
                            <asp:BoundField HeaderText="Status" DataField="ActiveStatus" />

                    <%--        <asp:TemplateField HeaderText="Change Status">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkActiveStatus" runat="server"
                                        Text='<%# Eval("ActiveStatus").ToString() == "Active" ? "Deactivate" : "Activate" %>'
                                        CommandName="ActiveStatusRow"
                                        CommandArgument='<%# Eval("Empid") %>'
                                        CssClass="btn btn-warning btn-sm"
                                        CausesValidation="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit"
                                        CommandName="EditRow"
                                        CommandArgument='<%# Eval("Empid") %>'
                                        CssClass="btn btn-info btn-sm mr-1"
                                        CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <asp:Label ID="lblModelRecordCount" runat="server"
                    CssClass="mt-2 font-weight-bold"></asp:Label>
            </div>
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

             <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" rel="stylesheet"/>
<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
