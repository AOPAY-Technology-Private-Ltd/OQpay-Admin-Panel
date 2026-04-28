<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="TheEMIClubApplication.ForgotPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="../../Scripts/plugins/fontawesome-free/css/all.min.css" />
    <link rel="stylesheet" href="../../Scripts/All-css/styleBar.css" />

    <!-- Bootstrap -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />

    <!-- Theme and other plugins -->
    <link rel="stylesheet" href="../../Scripts/dist/css/adminlte.min.css" />

    <title>Forgot Password</title>

    <script src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js'></script>
    <script src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/js/bootstrap.bundle.min.js'></script>
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



    <!-- Custom Styles -->
    <style>
        .main-container-box {
            height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .login-box {
            width: 100%;
            max-width: 600px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <div id="ErrorPage" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
               <h5 class="modal-title"> Error Message
    </h5>
            </div>
            <div class="modal-body">
                 <span id="lblerror" style="font-family:Georgia, 'Times New Roman', Times, serif;font-size:medium;color:red;font-weight:bold"  ></span>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal" id="" >
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
                
           
                  <span id="lblMessages" style="font-family:Georgia, 'Times New Roman', Times, serif;font-size:medium;color:forestgreen"  ></span>
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

        <!-- Main Body -->
        <div class="main-container-box">
            <div class="login-box">
                <div class="card card-outline card-primary">
                    <div class="card-header text-center">
                        <a href="Login.aspx" class="h1">
                            <b> 
                          <asp:Label ID="lblCompanyName" runat="server"  Text="OQ Pay"></asp:Label>
                            </b></a>
                    </div>
                    <div class="card-body">
                        <p class="login-box-msg">You forgot your password? Here you can easily retrieve a new password.</p>

                        <!-- Email Input -->
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" Placeholder="Email" TextMode="Email"></asp:TextBox>
                            <div class="input-group-append">
                                <div class="input-group-text">
                                    <span class="fas fa-envelope"></span>
                                </div>
                            </div>
                        </div>

                        <!-- UserID Input -->
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtUserid" runat="server" CssClass="form-control" Placeholder="User Name"></asp:TextBox>
                            <div class="input-group-append">
                                <div class="input-group-text">
                                    <span class="fas fa-user"></span>
                                </div>
                            </div>
                        </div>

                          <!-- New Password Input -->
  <div class="input-group mb-3">
      <asp:TextBox ID="txtNewpassword" runat="server" CssClass="form-control" Placeholder="New Password" TextMode="Password"></asp:TextBox>
      <div class="input-group-append">
          <div class="input-group-text">
              <span class="fas fa-user"></span>
          </div>
      </div>
  </div>

                        <!-- Submit Button -->
                        <div class="row">
                            <div class="col-12">
                              <asp:Button ID="btnRequestPassword" runat="server" Text="Forget Password" CssClass="btn btn-primary btn-block" OnClick="btnRequestPassword_Click" />
                            </div>
                        </div>

                        <!-- Login Link -->
                        <p class="mt-3 mb-1">
                            <a href="Login.aspx">Login</a>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Scripts -->
    <script src="../../Scripts/plugins/jquery/jquery.min.js"></script>
    <script src="../../Scripts/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../../Scripts/dist/js/adminlte.js"></script>

</body>
</html>
