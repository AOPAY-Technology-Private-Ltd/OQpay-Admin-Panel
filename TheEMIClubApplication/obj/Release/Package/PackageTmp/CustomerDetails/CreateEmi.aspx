<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"   AutoEventWireup="true" CodeBehind="CreateEmi.aspx.cs" Inherits="TheEMIClubApplication.CustomerDetails.CreateEmi" %>
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
                                 <h3 class="card-title">Create EMI Schedule</h3>
                             </div>
                             <!-- /.card-header -->
                             <div class="card-body ">
                                 <div class="row">
                                     <div class="col-md-12 card-body-box">
                                            <span id="spnMessage" runat="server"></span>
                                         <div class="row">
                                             <%--<div class="col-md-3">
                                                <div class="form-group">
                                                    <span class="font-weight-bold">Mode of Payment</span>
                                                    <asp:DropDownList ID="ddlBranch" runat="server" class='form-control'>
                                                        <asp:ListItem>:: Select Mode ::</asp:ListItem>
                                                        <asp:ListItem>Cash</asp:ListItem>
                                                        <asp:ListItem>Online</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>--%>
                                             <div class="col-md-4">
                                                 <div class="form-group">
                                                     <span class="font-weight-bold">Approved Amt.</span>
                                                     <asp:TextBox ID="txtLoanAmount" runat="server" class='form-control'> 
                                                     </asp:TextBox>
                                                 </div>
                                             </div>
                                             <div class="col-md-4">
                                                 <div class="form-group">
                                                     <span class="font-weight-bold">Processing charge</span>
                                                     <asp:TextBox ID="txtprocessingcharge" runat="server" text="0.00" CssClass="form-control" AutoCompleteType="Disabled" Width="100%"></asp:TextBox>

                                                 </div>
                                             </div>

                                             <div class="col-md-4">
                                                 <div class="form-group">
                                                     <span class="font-weight-bold">Other charge</span>
                                                     <asp:TextBox ID="txtothercharge" runat="server" Text="0.00" CssClass="form-control" AutoCompleteType="Disabled" Width="100%"></asp:TextBox>

                                                 </div>
                                             </div>
                                             <div class="col-md-4">
                                                 <div class="form-group">
                                                     <span class="font-weight-bold">ROI(%)</span>
                                                      <asp:Label runat="server" ID="lbl_custmerid" Visible="false"></asp:Label>
                                                     <asp:TextBox ID="txtAnnualInterestRate" runat="server" class="form-control" placeholder="Enter Rate of Interest"></asp:TextBox>
                                                 </div>
                                             </div>
                                             <div class="col-md-4">
                                                 <div class="form-group">
                                                     <span class="font-weight-bold">Loan Duration</span>
                                                     <asp:TextBox ID="txtLoanTermInYears" runat="server" class="form-control" placeholder="Loan Duration"></asp:TextBox>
                                                 </div>
                                             </div>

                                             <div class="col-md-4">
                                                 <div class="form-group">
                                                     <span class="font-weight-bold">EMI Type</span>
                                                     <asp:DropDownList runat="server" ID="ddlEmiType" AutoPostBack="true" class="form-control">
                                                         <asp:ListItem Text="Flat" Value="1"></asp:ListItem>
                                                         <asp:ListItem Text="Reducing" Value="2"></asp:ListItem>
                                                     </asp:DropDownList>

                                                 </div>
                                             </div>
                                             <div class="col-md-4">
                                                 <div class="form-group">
                                                     <span class="font-weight-bold">EMIType</span>
                                                     <asp:DropDownList runat="server" ID="ddlCalculationType" AutoPostBack="true" class="form-control">
                                                         <asp:ListItem Text="Monthly" Value="1"></asp:ListItem>
                                                         <asp:ListItem Text="Day" Value="2"></asp:ListItem>
                                                     </asp:DropDownList>

                                                 </div>
                                             </div>

                                             <div class="col-md-4">
                                                 <div class="form-group">
                                                     <span class="font-weight-bold">EMI Start Date</span>

                                                     <asp:TextBox ID="txtEmistartDate" runat="server" TextMode="Date" CssClass="form-control" AutoCompleteType="Disabled" Width="100%"></asp:TextBox>

                                                 </div>
                                             </div> 
                                             <%--   <div class="col-md-6 offset-md-3 card-body-box" style="box-shadow: none;">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <span class="font-weight-bold">Processing Charges (%)</span>
                                                            <asp:TextBox ID="txtProcessingCharges" runat="server" class='form-control' placeholder="Processing Charges (%)"> 
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <span class="font-weight-bold">Processing Ch. Amt.</span>
                                                            <asp:TextBox ID="txtProcessingChargesAmount" runat="server" class='form-control' placeholder="Processing Charge Amount"> 
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <span class="font-weight-bold">File Charges (%)</span>
                                                            <asp:TextBox ID="txtFileCharge" runat="server" class='form-control' placeholder="File Charges (%)">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <span class="font-weight-bold">File Ch. Amt.</span>
                                                            <asp:TextBox ID="txtFileChargeAmt" runat="server" class='form-control' placeholder="File Charge Amount">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <span class="font-weight-bold">Other Charges (%)</span>
                                                            <asp:TextBox ID="txtOtherCharges" runat="server" class='form-control' placeholder="Other Charges (%)"> 
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <span class="font-weight-bold">Other Ch. Amt.</span>
                                                            <asp:TextBox ID="txtOtherChargeAmount" runat="server" class='form-control' placeholder="Other Charges Amount"> 
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <span class="font-weight-bold">Principal Amt.</span>
                                                            <asp:TextBox ID="txtprincipalamt" runat="server" class='form-control' placeholder="Principal Amount"> 
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <span class="font-weight-bold">Total Charges</span>
                                                            <asp:TextBox ID="TextBox8" runat="server" class='form-control' placeholder="Total Charges"> 
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>
                                         </div>
                                         <div class="col-md-12 text-center mb-3">
                                             <asp:Button runat="server" ID="btnCalculate" class="btn btn-primary" Text="Calculate EMI" OnClick="btnCalculate_Click" />
                                             <%--  <asp:Button ID="btnCalculate" runat="server" class="btn btn-primary" Text="Calculate EMI" OnClick="btnCalculate_Click" />--%>
                                             <%--  // <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" />--%>
                                             <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-danger" />
                                         </div>
                                         <div class="container">
                                             <div class="row justify-content-center">
                                                 <div class="col-12 text-center" id="summary" runat="server" 
                                                     visible="false" style="box-shadow: 0 0 3px rgba(0,0,0,0.8); padding: 3px; border-radius: 3px; margin: 6px;">
                                                     <h2 class="text-center fs-6" style="font-size: 20px; font-weight: bold; padding: 3px;">EMI SUMMARY</h2>
                                                     <table class="table table-bordered borderTable" style="height: 10%;">
                                                         <tbody style="display: flex; align-items: center; justify-content: center;">
                                                             <tr>
                                                                 <td style="background: #D3D3D3;">
                                                                     <h3 style="font-size: 14px; font-weight: bold;">Loan Amount :<asp:Label runat="server" ID="lblloanAmount"></asp:Label>
                                                                     </h3>
                                                                 </td>
                                                             </tr>
                                                             <tr>
                                                                 <td style="background: #848b91; color: white;">
                                                                     <h3 style="font-size: 14px; font-weight: bold;">Loan Term : 
                                                                         <asp:Label runat="server" ID="lbllonaterm"></asp:Label>
                                                                         <small><asp:Label runat="server" ID="lbltermtype"></asp:Label></small></h3>
                                                                 </td>
                                                             </tr>
                                                             <tr>
                                                                 <td style="background: #D3D3D3;">
                                                                     <h3 style="font-size: 14px; font-weight: bold;">Interest Rate
                                                                         <asp:Label runat="server" ID="lblloaninterstrate"></asp:Label><small>%<small></small></small></h3>
                                                                 </td>
                                                             </tr>
                                                             <tr>
                                                                 <td style="background: #848b91; color: white;">
                                                                     <h3 style="font-size: 14px; font-weight: bold;">Total Interest :
                                                                         <asp:Label runat="server" ID="totalinterst"></asp:Label></h3>
                                                                 </td>
                                                             </tr>
                                                             <tr>
                                                                 <td style="background: #D3D3D3;">
                                                                     <h3 style="font-size: 14px; font-weight: bold;">Total Amt.With Processing & Other Ch.  :
                                                                         <asp:Label runat="server" ID="totalpayment"></asp:Label></h3>
                                                                 </td>
                                                             </tr>

                                                             <tr>
                                                                 <td style="background: #848b91; color: white;">
                                                                     <h3 style="font-size: 14px; font-weight: bold;">Transfer Amt. :
                                                                     <asp:Label runat="server" ID="lbltransferamt"></asp:Label></h3>
                                                                 </td>
                                                             </tr>
                                                             <tr>
                                                                 <td style="background: #D3D3D3;">
                                                                     <h4 style="font-size: 14px; font-weight: bold;">EMI
                                                                         <asp:Label runat="server" ID="lblResult"></asp:Label></h4>
                                                                 </td>
                                                             </tr>
                                                         </tbody>
                                                     </table>
                                                 </div>
                                             </div>
                                         </div>
                                         

                                         <div class="col-md-12 text-center mb-3">
                                             <asp:GridView runat="server" ID="gvPaymentSchedule" AutoGenerateColumns="false" OnRowDataBound="gvPaymentSchedule_RowDataBound" Visible="false" CssClass="table table-bordered table-condensed table-hover" ShowFooter="true">
                                                 <Columns>
                                                     <asp:BoundField DataField="EmiNumber" HeaderText="EMI Number" />
                                                     <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                     <asp:BoundField DataField="PrincipalAmount" HeaderText="Principal Amount" DataFormatString="{0:C}" />
                                                     <asp:BoundField DataField="InterestRate" HeaderText="Interest Rate" DataFormatString="{0:C}" />
                                                     <asp:BoundField DataField="TotalEmi" HeaderText="Total EMI" DataFormatString="{0:C}" />
                                                 </Columns> 
                                                 <FooterStyle BackColor="#2fb5ac" Font-Bold="True" ForeColor="White" />
                                                 <PagerStyle BackColor="#337AB7" ForeColor="White" HorizontalAlign="Center" />
                                                 <HeaderStyle BackColor="#337AB7" Font-Bold="True" ForeColor="White" />
                                             </asp:GridView>
                                         </div>
                                         <div class="col-md-12 text-center mb-3">
                                             <asp:Button runat="server" ID="btnCreateEmi" class="btn btn-primary" Text="Submit" OnClick="btnCreateEmi_Click" Visible="false" />
                                              <asp:Button runat="server" ID="btncancel" class="btn btn-danger" Text="Cancel" OnClick="btncancel_Click" Visible="false" />
                                         </div>
                                     </div>
                                 </div>
                             </div>
                                 
                         </div>
                     </div>
                        
                 </section>

             </div>
             
         </div>
             
           <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js" type="text/javascript"></script>
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
                function ShowPopup( messages) {
                 
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
