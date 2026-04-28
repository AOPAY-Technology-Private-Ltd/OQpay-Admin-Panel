<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="LoanFollowUp.aspx.cs" Inherits="TheEMIClubApplication.Admin.LoanFollowUp" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar.Net.2008" Namespace="RJS.Web.WebControl" TagPrefix="rjs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
            <style>
        .form-group label {
            font-weight: 500;
        }
        label::before, label::after {
    content: none !important;
}
    </style>
    <asp:ScriptManager ID="smWallet" runat="server"></asp:ScriptManager>

    <div class="container-fluid mt-4">
        <div class="card shadow-sm">
            <div class="card-header bg-primary text-white">
                <h3 class="mb-0">Manage EMI Loan Follow-Ups</h3>
            </div>

            <div class="card-body">
                <!-- Validation message -->
                <div class="row mb-3">
                    <div class="col-12">
                        <span id="spnMessage" runat="server"></span>
                    </div>
                </div>

              <!-- Loan Details -->
                <div class="form-group row">
                    <label class="col-md-2 col-form-label">Loan ID</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtLoanID" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <label class="col-md-2 col-form-label">Customer Name</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        <asp:HiddenField  runat="server" ID="hdfcustomecode"/>
                    </div>
                </div>

                <div class="form-group row">
                    <label class="col-md-2 col-form-label">Product Name</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <label class="col-md-2 col-form-label">Contact No</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group row">
                    <label class="col-md-2 col-form-label">Email Address</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <label class="col-md-2 col-form-label">Loan EMI Amount</label>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtLoanEMIAmount" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>

                </div>

                <!-- Status & Next Follow-Up -->
                <div class="form-group row">
                    <label class="col-md-2 col-form-label">Follow-Up Status</label>
                    <div class="col-md-4">
<asp:DropDownList ID="ddlFollowUpStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFollowUpStatus_SelectedIndexChanged">
           <asp:ListItem Text="-- Select Status --" Value=""/>
    <asp:ListItem Text="Pending – EMI follow-up not yet done" Value="Pending" />
    <asp:ListItem Text="In Progress – staff currently following up" Value="In Progress" />
    <asp:ListItem Text="PTP (Promise to Pay) – customer committed to pay later" Value="PTP" />
    <asp:ListItem Text="Paid – EMI cleared" Value="Paid" />
    <asp:ListItem Text="Overdue – EMI missed/not paid" Value="Overdue" />
    <asp:ListItem Text="Rescheduled – EMI date changed" Value="Rescheduled" />
    <asp:ListItem Text="Escalated – moved to collections/legal" Value="Escalated" />
    <asp:ListItem Text="No Response – customer unreachable" Value="No Response" />
</asp:DropDownList>
                         <asp:RequiredFieldValidator 
            ID="rfvFollowUpStatus" 
            runat="server" 
            ControlToValidate="ddlFollowUpStatus" 
            InitialValue="" 
            ErrorMessage="Please select a follow-up status." 
            CssClass="text-danger small"
            Display="Dynamic" ValidationGroup="OnSubmit" />
                    </div>
                    <label class="col-md-2 col-form-label">Next Follow-Up Date</label>
                    <div class="col-md-4">
                                                            <div class="has-feedback">
    <asp:TextBox 
        ID="txtNextFollowUpDate" 
        runat="Server" 
        CssClass="form-control list-items-column-inner" 
        MaxLength="20" 
        Placeholder="Select Date and Time" Enabled="false"></asp:TextBox>
</div>
                      <%--  <asp:TextBox ID="txtNextFollowUpDate" runat="server" CssClass="form-control"
                            Placeholder="yyyy-MM-dd hh:mm tt"></asp:TextBox>--%>
                    </div>
                </div>
                      <div class="form-group row">
         <label class="col-md-2 col-form-label">Loan TenureNo </label>
          <div class="col-md-4">
              <asp:TextBox ID="txtLoanTenureNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
          </div>
       

      </div>
                <!-- Remarks -->
                <div class="form-group row">
                    <label class="col-md-2 col-form-label">Remarks</label>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtFollowUpRemarks" runat="server" CssClass="form-control"
                            TextMode="MultiLine" Rows="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqRemarks" runat="server" 
                            ControlToValidate="txtFollowUpRemarks" ErrorMessage="Remarks required"
                            CssClass="text-danger small" ValidationGroup="OnSubmit" />
                    </div>
                </div>

                <!-- Buttons -->
                <div class="text-right mb-3">
                    <asp:Button ID="btnSave" runat="server" ValidationGroup="OnSubmit"
                        CssClass="btn btn-primary" Text="Save Follow-Up" OnClick="btnSave_Click" />
                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-secondary ml-2" Text="Reset" />
                </div>

                <!-- Follow-up Grid -->
                <div class="table-responsive">
                    <asp:GridView ID="gvFollowUp" runat="server" CssClass="GridStyle table table-bordered table-condensed table-hover" 
                        AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
                        DataKeyNames="FollowUpID" OnPageIndexChanging="gvFollowUp_PageIndexChanging"   PagerSettings-Mode="Numeric"
  PagerSettings-Position="Bottom"
  PagerStyle-HorizontalAlign="Center"
  PagerStyle-CssClass="pagination-container"
  UseAccessibleHeader="true"
  GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="FollowUpID" HeaderText="ID" />
                            <asp:BoundField DataField="LoanCode" HeaderText="LoanCode" />
                            <asp:BoundField DataField="FollowUpDate" HeaderText="Follow-Up Date" />
                            <asp:BoundField DataField="NextFollowUpDate" HeaderText="Next Follow-Up"
                                DataFormatString="{0:yyyy-MM-dd hh:mm tt}" HtmlEncode="false" />
                            <asp:BoundField DataField="FollowUpStatus" HeaderText="Status" />
                                      <asp:BoundField DataField="LoanTenureNo" HeaderText="Loan Tenure No" />
                                         <asp:BoundField DataField="LoanEmiAmount" HeaderText="Loan Emi Amount" />
                            <asp:BoundField DataField="FollowUpRemarks" HeaderText="Remarks" />
                            <asp:BoundField DataField="FollowUpBy" HeaderText="Followed By" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <!-- Chat Modal -->
    <div class="modal fade" id="chatModal" tabindex="-1" role="dialog" aria-labelledby="chatModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
          <div class="modal-header bg-info text-white">
            <h5 class="modal-title" id="chatModalLabel">Product's Details</h5>
            <button type="button" class="close text-white" data-dismiss="modal">&times;</button>
          </div>
          <div class="modal-body">
            <div id="chatHistory" class="mb-3" style="max-height:400px; overflow-y:auto;">
              <asp:Repeater ID="rptChatHistory" runat="server">
                <ItemTemplate>
                  <div class="mb-2">
                    <strong><%# Eval("Sender") %>:</strong>
                    <div><%# Eval("Message") %></div>
                    <small class="text-muted"><%# Eval("Timestamp", "{0:yyyy-MM-dd HH:mm:ss}") %></small>
                  </div>
                </ItemTemplate>
              </asp:Repeater>
            </div>
            <asp:TextBox runat="server" ID="newMessage" CssClass="form-control" TextMode="MultiLine" Rows="3"
                placeholder="Type a new message..."></asp:TextBox>
          </div>
          <div class="modal-footer">
            <asp:Button ID="btnClosed" runat="server" Text="Close" CssClass="btn btn-secondary" />
            <asp:Button ID="btnSendMessage" runat="server" Text="Save" CssClass="btn btn-primary" />
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
       function validateMessage() {
           var message = document.getElementById('<%= newMessage.ClientID %>').value.trim();
           if (message.length === 0) {
               toastr.error("Product's Details is required.", "");
               return false; // Prevent form submission
           }
           return true; // Allow form submission
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


   

<!-- Flatpickr CSS (Bootstrap 4 theme) -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/themes/material_blue.css">

<!-- jQuery + Bootstrap 4.6 JS -->



<!-- Flatpickr JS -->
<script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="scriptContent" Runat="Server">
    <script type="text/javascript" language="javascript">

        // var IsShiftDown = false;
        function BlockingHtmlOnKey(Sender, e) {


            if ((e.shiftKey && (e.which == 188 || e.which == 190))) {
                return false;
            }
        }

        function BlockHTMLOnPaste(Sender, e) {


            try {
                var txt = e.clipboardData.getData('text/plain');
                if (txt.match("<") || txt.match(">")) {
                    return false;
                }
            } catch (err) {
            }

        }



        function expandControl(sender, eventArgs) {
            //This will expand the dropdown on focus (with tab or otherwise)
            sender.showDropDown();
        }
    </script>

<script type="text/javascript">
    flatpickr("#<%= txtNextFollowUpDate.ClientID %>", {
        enableTime: true,
        dateFormat: "d M Y H:i",
        time_24hr: true,
        allowInput: true,
        altInput: true,
        altFormat: "d M Y H:i"
    });
</script>
    <script type="text/javascript">

        function numberwithDecimal(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                //Check if the text already contains the . character
                if (txt.value.indexOf('.') === -1) {
                    return true;
                } else {
                    return false;
                }
            } else {
                if (charCode > 31 &&
                    (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
        }
    </script>


        <script>
            function scrollToBottom(elementId) {
                const chatHistory = document.getElementById(elementId);
                if (chatHistory) {
                    chatHistory.scrollTop = chatHistory.scrollHeight;
                }
            }
</script>




   <script type="text/javascript">
       // Function to open the chat modal
       function openChatModal() {
           // Open the modal using Bootstrap's modal method
           $('#chatModal').modal('show');

           // Wait for the modal to fully open, then scroll to the bottom of the chat history
           $('#chatModal').on('shown.bs.modal', function () {
               // Scroll to the bottom of the chat history if it is scrollable
               scrollToBottom('chatHistory');
           });
       }

       // Function to scroll the chat history container to the bottom
       function scrollToBottom(elementId) {
           const chatHistory = document.getElementById(elementId);
           if (chatHistory) {
               // Set scrollTop to scrollHeight to ensure we scroll to the bottom
               chatHistory.scrollTop = chatHistory.scrollHeight;
           }
       }
</script>





<!-- Bootstrap JS (Required for modal functionality) -->

</asp:Content>      
