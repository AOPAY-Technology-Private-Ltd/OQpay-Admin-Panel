<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TheEMIClubApplication.Login" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
<meta charset="utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1" />
            <link rel="icon" href="../My-Files/Images/NewOqpay.jpeg" />
<title>Login OQPay</title>

<!-- Bootstrap 5 CSS -->
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
<link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet">

<style>
/* Body Gradient */
 body {
    /* Gradient using your logo's colors: yellow, white, teal */
    background: linear-gradient(135deg, #f9d423 0%, #ffffff 50%, #5ac8fa 100%);
    min-height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
    font-family: "Segoe UI", Roboto, sans-serif;
    padding: 1rem;
}


/* Container */
.login-container {
    display: flex;
    min-height: 80vh;
    width: 100%;
    max-width: 1200px;
    box-shadow: 0px 10px 50px rgba(0,0,0,0.2);
    border-radius: 12px;
    overflow: hidden;
    background: rgba(255,255,255,0.05); /* subtle transparency on container */
}

/* Left Section */
.login-left {
    flex: 1;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    padding: 40px;
    text-align: center;
    color: #fff;
     background: linear-gradient(135deg, #002252, #0056a3); /* Modern gradient */
}
.login-left img {
    max-width: 50%;
    margin-bottom: 20px;
    filter: drop-shadow(0 4px 6px rgba(0,0,0,0.3));
}
.login-left h1 {
    font-weight: 700;
    font-size: 2rem;
    margin-top: 10px;
}
.login-left p {
    margin-top: 8px;
    font-size: 1rem;
    opacity: 0.9;
}

/* Right Section */
.login-right {
    flex: 1.2;
    display: flex;
    align-items: center;
    justify-content: center;
    background: #fff;
    padding: 40px;
}
.login-box {
    width: 100%;
    max-width: 420px;
    border-radius: 12px;
    padding: 35px 30px;
    box-shadow: 0px 8px 25px rgba(0,0,0,0.12);
    background-color: #fff;
}

/* Inputs */
.form-control {
    border-radius: 7px 0 0 7px;
}
.input-group-text {
    background: #1E3A8A;
    color: #fff;
    border-radius: 0 7px 7px 0;
}
.form-control:focus {
    border-color: #0d6efd;
    box-shadow: 0 0 0 0.2rem rgba(13,110,253,.25);
}

/* Login Button */
.login-btn {
    width: 100%;
    background: #1E3A8A;
    color: #fff;
    border: none;
    border-radius: 7px;
    padding: 12px;
    font-size: 16px;
    font-weight: 600;
    transition: all 0.3s ease-in-out;
}
.login-btn:hover {
    background: #0d6efd;
    transform: translateY(-2px);
}

/* Captcha */
.captcha-box {
    display: flex;
    gap: 10px;
    align-items: center;
    justify-content: center;
    margin-bottom: 15px;
}

/* Footer Links */
.form-footer {
    margin-top: 20px;
    text-align: center;
}
.form-footer a {
    text-decoration: none;
    color: #0d6efd;
    transition: color 0.2s;
}
.form-footer a:hover {
    color: #1E3A8A;
}

/* Responsive */
@media(max-width: 992px) {
    .login-left { display: none; }
    .login-container { justify-content: center; }
}
</style>
</head>
<body>
<form id="frmLogin" runat="server" class="needs-validation" novalidate>
    <div class="login-container">

        <!-- Left Section -->
        <div class="login-left">
            <img src="My-Files/Images/OQ.png" alt="Logo" />
            <h1>Welcome Back</h1>
            <p>Securely access your account</p>
        </div>

        <!-- Right Section -->
        <div class="login-right">
            <div class="login-box">

                <h3 class="text-center mb-4">Login to Your Account</h3>

                <!-- Username -->
                <div class="mb-3">
                    <div class="input-group">
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" 
                            placeholder="Username" MaxLength="50" AutoCompleteType="Disabled" required></asp:TextBox>
                        <div class="input-group-text">
                            <i class="fa fa-user"></i>
                        </div>
                        <div class="invalid-feedback">Username is required</div>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" 
                        ControlToValidate="txtUserName" CssClass="text-danger"/>
                </div>

                <!-- Password -->
                <div class="mb-3">
                    <div class="input-group">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" 
                            TextMode="Password" placeholder="Password" MaxLength="50" required></asp:TextBox>
                        <div class="input-group-text">
                            <i class="fa fa-lock"></i>
                        </div>
                        <div class="invalid-feedback">Password is required</div>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                        ControlToValidate="txtPassword" CssClass="text-danger"/>
                </div>

                <!-- Captcha -->
                <div class="captcha-box text-center">
                    <cc1:CaptchaControl ID="cptCaptcha" runat="server" 
                        CaptchaBackgroundNoise="Low" CaptchaLength="5" 
                        CaptchaHeight="40" CaptchaWidth="200" 
                        CaptchaLineNoise="None" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                        FontColor="#529E00" NoiseColor="#B1B1B1" />
                    <asp:TextBox ID="txtCaptcha" runat="server" CssClass="form-control" 
                        placeholder="Enter Captcha" MaxLength="5" style="text-transform:uppercase;" 
                        onkeyup="this.value = this.value.toUpperCase();"></asp:TextBox>

                </div>
 
                                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="text-danger d-block mt-2"></asp:Label>
                
                <div class="alert"><span id="spnLoginMsg" runat="server"></span></div>

                <!-- Remember Me -->
            <div class="form-check mb-3">
    <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="form-check-input" Checked="true" />
    <label for="chkRememberMe" class="form-check-label">Keep me signed in</label>
</div>

                <!-- Login Button -->
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn login-btn" OnClick="btnLogin_Click" />

                <!-- Links -->
               <%-- <div class="form-footer">
                    <a href="ForgotPassword.aspx">Forgot Password?</a> 
                  
                </div>--%>

                <!-- Error Message -->


            </div>
        </div>
    </div>
</form>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
<script>
    (function () {
        'use strict'
        var forms = document.querySelectorAll('.needs-validation')
        Array.prototype.slice.call(forms).forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }
                form.classList.add('was-validated')
            }, false)
        })
    })()
</script>
</body>
</html>
