<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="PersonalDetails.aspx.cs" Inherits="TheEMIClubApplication.CustomerDetails.PersonalDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

     <div class="hold-transition sidebar-mini">
    <div class="wrapper">
        <!-- Content Wrapper. Contains page content -->
        <div class="main-box-container" style="width: 100%; position: relative; left: 0px; top: 0px; margin: 0px; padding: 25px 0px;">
            <!-- Main content -->
            <section class="content">
                <div class="container-fluid">
                    <!-- Form -->
                    <div class="card card-default">
                        <div class="card-header form-header-bar">
                            <h3 class="card-title">Customer Details</h3>
                        </div>
                        <!-- /.card-header -->
                        <div class="card-body ">
                            <div class="row">
                              
                                <div class="col-md-12 card-body-box">

                                    <div class="row">
    <asp:HiddenField ID="hfCustomerID" runat="server" />

    <div class="col-md-4">
        <div class="form-group">
            <span class="font-weight-bold">Customer Code</span>
            <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" placeholder="Enter Customer Code"></asp:TextBox>
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <span class="font-weight-bold">First Name</span>
            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="Enter First Name"></asp:TextBox>
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <span class="font-weight-bold">Last Name</span>
            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Enter Last Name"></asp:TextBox>
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <span class="font-weight-bold">Mobile Number</span>
            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="Enter Mobile Number"></asp:TextBox>
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <span class="font-weight-bold">Email ID</span>
            <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control" placeholder="Enter Email ID"></asp:TextBox>
        </div>
    </div>

    <div class="col-md-8">
        <div class="form-group">
            <span class="font-weight-bold">Address</span>
            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Enter Address"></asp:TextBox>
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <span class="font-weight-bold">Aadhar Number</span>
            <asp:TextBox ID="txtAadharNumber" runat="server" CssClass="form-control" placeholder="Enter Aadhar Number"></asp:TextBox>
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <span class="font-weight-bold">PAN Number</span>
            <asp:TextBox ID="txtPANNumber" runat="server" CssClass="form-control" placeholder="Enter PAN Number"></asp:TextBox>
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <span class="font-weight-bold">Active Status</span>
            <asp:DropDownList ID="ddlActiveStatus" runat="server" CssClass="form-control">
                <asp:ListItem Text="Active" Value="Active" />
                <asp:ListItem Text="Inactive" Value="Inactive" />
            </asp:DropDownList>
        </div>
    </div>
</div>

                                    <div class="col-md-12 text-center">
    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Update" OnClick="btnSave_Click"/>  
    <%-- <asp:Button ID="btnReject" runat="server" CssClass="btn btn-danger" Text="Reject" OnClick="btnReject_Click"/> --%>
    <%--<asp:Button ID="Button2" runat="server" CssClass="btn btn-success" Text="Reject" />  --%>
    <asp:Button ID="btnClose" runat="server" CssClass="btn btn-secondary" Text="Close" OnClick="btnClose_Click" />   
</div>

                                 

                                </div>
                            </div>
                        </div>

                        <!-- /.card-body -->
      
                        <!-- /.card-body -->
                    </div>
                </div>
           
                <!-- /.container-fluid -->
            </section>
            <!-- /.content -->
        </div>
        </div>
        <!-- /.content-wrapper -->
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
                <div class="modal-body"  style="max-height: 300px; overflow-y: auto;">
                               <asp:Label ID="lblnorecords" runat="server" Visible="false"></asp:Label>
                    <asp:ImageButton ID="btnExportExcel" runat="server" ImageUrl="~/Images/ExcelIcon.png"  OnClick="btnExportExcel_Click" />
                    <asp:GridView ID="gvContactList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                  
                        <Columns>
                            <asp:BoundField DataField="ContactName" HeaderText="Contact Name"  />
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
            <div class="modal-header" style="background-color:cornflowerblue">
              
                <h5 class="modal-title"> Confirmation Messages
                </h5>
            </div>
            <div class="modal-body">
                
           
                  <span id="lblMessages" style="font-family:Georgia, 'Times New Roman', Times, serif;font-size:medium;color:forestgreen" ></span>
                  <br />
                <span id="lblName" style="color:navy"></span>
              
            </div>
            <div class="modal-footer">
                <button type="button"  data-dismiss="modal" class="btn-success" id="btnsucess" runat="server" causesvalidation="false" >
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
