<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreateUsers.aspx.cs" Inherits="TheEMIClubApplication.MembershipPages.CreateUsers" %>
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
                                <h3 class="card-title">Create Team</h3>
                                <span id="spnMessage" runat="server"></span>
                            </div>
                            <!-- Card Header -->
                            <div class="card-body ">
                                <div class="row">
                                    <div class="col-md-12 card-body-box">
                                        <div class="row"> 
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <span class="font-weight-bold">First Name</span>
                                                    <asp:TextBox ID="txtfirstName" runat="server" class="form-control" placeholder="First    Name"></asp:TextBox>
                                                     
                                                    <asp:RequiredFieldValidator ID="txtfnameval" runat="server" ErrorMessage="  First Name is Required"
                                                        ControlToValidate="txtfirstName" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Onsubmit"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ID="valRextxtfirstName" ValidationGroup="Onsubmit" runat="server"
                                                        ControlToValidate="txtfirstName" CssClass="ErrorMessage" ErrorMessage="Enter Valid Name " Display="Dynamic"
                                                        ValidationExpression="^[a-zA-Z0-9'\/_\-,&.\s]{0,50}$" SetFocusOnError="true"></asp:RegularExpressionValidator>


                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <span class="font-weight-bold">Last Name</span>
                                                    <asp:TextBox ID="txtLastname" runat="server" class="form-control" placeholder="Last Name"></asp:TextBox>
                                              
                                                  <%--  <asp:RequiredFieldValidator ID="valReqAccountNumber" runat="server" ErrorMessage="Account No is Required"
                                                        ControlToValidate="txtAccountHolderName" ForeColor="Red" SetFocusOnError="true" ValidationGroup="Onsubmit"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="valRexAccountNumber" ValidationGroup="Onsubmit" runat="server"
                                                        ControlToValidate="txtAccountNumber" CssClass="ErrorMessage" ErrorMessage="Enter Valid Account No" Display="Dynamic"
                                                        ValidationExpression="^[a-zA-Z0-9'\/_\-,&.\s]{0,50}$" SetFocusOnError="true"></asp:RegularExpressionValidator>--%>

                                                    </div>
                                            </div>
                                              <div class="col-md-4">
      <div class="form-group">
                                             <span class="font-weight-bold">Phone No</span> 
    <asp:TextBox ID="txtMobileNo" runat="server" class="form-control" placeholder="Mobile No" MaxLength="10"></asp:TextBox>
    <asp:RequiredFieldValidator ID="valreqtxtMobileNo" runat="server" ControlToValidate="txtMobileNo"
        ErrorMessage="Mobile No is Required"
        SetFocusOnError="true" ValidationGroup="Onsubmit"> </asp:RequiredFieldValidator>

    <asp:RegularExpressionValidator ID="valrexMobileNo" ValidationGroup="Onsubmit" runat="server"
        ControlToValidate="txtMobileNo" CssClass="ErrorMessage" ErrorMessage="Invalid text" SetFocusOnError="true"
        ValidationExpression="^(?:\+?1[-. ]?)?\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})"></asp:RegularExpressionValidator>
    </div>
</div>

                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <span class="font-weight-bold">Email</span>
                                                    <asp:TextBox ID="txtEmailID" runat="server" class="form-control" placeholder="Email Address" MaxLength="30"></asp:TextBox>
  <asp:RequiredFieldValidator ID="valreqEmailID" runat="server" ControlToValidate="txtEmailID" ErrorMessage="Email Address is Required" SetFocusOnError="true"
      ValidationGroup="Onsubmit"></asp:RequiredFieldValidator>
  <asp:RegularExpressionValidator ID="valrexEmailID" runat="server" ControlToValidate="txtEmailID" CssClass="ErrorMessage"
      ErrorMessage="Invalid text" ValidationGroup="Onsubmit"
      ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>

                                                </div>
                                            </div>
                                        
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <span class="font-weight-bold">Password</span>
                                             <asp:TextBox ID="txtPasword" runat="server" class="form-control" placeholder="Password"></asp:TextBox>
                                                </div>
                                            </div>

                                           

                                           
                                            <div class="col-md-12 text-center mb-3">
                                                <asp:Label ID="lblrid" runat="server" Visible="false"></asp:Label>
                                                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary"    ValidationGroup="Onsubmit" OnClick="btnSave_click" CausesValidation="false"/>
                                             
                                                <asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-success" />                                                                                             
                                            </div>                                         
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
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
                function ShowPopup(userid, messages) {
                    $("#MyPopups #lblName").html(userid);
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
