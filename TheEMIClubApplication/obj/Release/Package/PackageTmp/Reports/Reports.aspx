<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="TheEMIClubApplication.Reports.Reports" %>

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
                          <div class="card card-default">
                              <div class="card-header form-header-bar">
                                  <h3 class="card-title">Collection</h3>
                              </div>
                              <!-- /.card-header -->
                              <div class="card-body ">
                                  <div class="row">
                                      <div class="col-md-12 card-body-box">

                                          <div class="row">
                                              <div class="col-md-6">
                                                  <div class="form-group">
                                                      <span class="font-weight-bold">From Date</span>
                                                      <asp:TextBox ID="txtfromdate" runat="server" TextMode="Date" CssClass="form-control" AutoCompleteType="Enabled" Width="100%"></asp:TextBox>

                                                  </div>
                                              </div>
                                              <div class="col-md-6">
                                                  <div class="form-group">
                                                      <span class="font-weight-bold">To Date</span>
                                                      <asp:TextBox ID="txttodate" runat="server" TextMode="Date" CssClass="form-control" AutoCompleteType="Enabled" Width="100%"></asp:TextBox>
                                                  </div>

                                              </div>

                                              <div class="col-md-3">
                                                  <div class="form-group">
                                                      <span class="font-weight-bold">Select Report</span>
                                                      <asp:DropDownList ID="dtpmode" runat="server" OnClientFocus="expandControl"
                                                          Filter="Contains" Style="width: 100%;" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="dtpmode_SelectedIndexChanged">
                                                      </asp:DropDownList>

                                                  </div>

                                              </div>

                                              <div class="col-md-3">
                                                  <div class="form-group">
                                                      <span class="font-weight-bold">Status</span>
                                                      <asp:DropDownList ID="ddlMode" runat="server" CssClass="form-control" Enabled="false">
                                                          <asp:ListItem id="All" Text="All" Value="All" Enabled="false"></asp:ListItem>
                                                          <asp:ListItem id="Approved" Text="Approved" Value="Approved" Enabled="false"></asp:ListItem>

                                                          <asp:ListItem id="Pending" Text="Pending" Value="Pending" Enabled="false"></asp:ListItem>
                                                          <asp:ListItem id="Assign" Text="Assign" Value="Assign" Enabled="false"></asp:ListItem>
                                                          <asp:ListItem id="Rejected" Text="Rejected" Value="Rejected" Enabled="false"></asp:ListItem>
                                                          <asp:ListItem id="Due" Text="Due" Value="Due" Enabled="false"></asp:ListItem>
                                                          <asp:ListItem id="Paid" Text="Paid" Value="Paid" Enabled="false"></asp:ListItem>
                                                          <asp:ListItem id="Disbursements" Text="Disbursement" Value="Disbursement" Enabled="false"></asp:ListItem>


                                                      </asp:DropDownList>
                                                  </div>

                                              </div>

                                             <%-- <div class="col-md-3">
                                                  <div class="form-group">
                                                      <span class="font-weight-bold">Select User</span>
                                                      <asp:DropDownList ID="ddlcustomer" runat="server" OnClientFocus="expandControl"
                                                          Filter="Contains" Style="width: 100%;" AutoPostBack="true" CssClass="form-control">
                                                      </asp:DropDownList>

                                                  </div>

                                              </div>--%>

                                              <div class="col-md-3">
                                                  <div class="form-group">
                                                      <span class="font-weight-bold">Records</span>
                                                      <asp:DropDownList ID="ddlNoofRecords" runat="server" CssClass="form-control">
                                                          <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                          <asp:ListItem Text="50" Value="50"></asp:ListItem>

                                                          <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                                          <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                                          <asp:ListItem Text="1000" Value="1000"></asp:ListItem>
                                                          <asp:ListItem Text="5000" Value="5000"></asp:ListItem>


                                                      </asp:DropDownList>

                                                  </div>

                                              </div>

                                          </div>
                                           <div class="col-md-12 text-center mb-3">
     <asp:Button ID="btnshow" runat="server" Text="Show Report" ValidationGroup="OnSubmit" class="btn btn-primary" OnClick="btnshow_click" />
     <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-danger" />

 </div>




                                      </div>
                                  </div>
                                  </div>
                              </div>
                          </div>
                          <!-- Grid View -->
    <div class="card card-default">
        <div class="card-header form-header-bar">
            <h3 class="card-title">Report Details</h3>
        </div>
        <!-- /.card-header -->
        <div class="col-md-12 text-center mb-3 w-100 overflow-auto">
            <span id="spnMessage" runat="server"></span>

            <asp:GridView ID="gvreport" runat="server" AutoGenerateColumns="true"  Width="100%" CssClass="table table-bordered table-condensed table-hover">
               
                <RowStyle CssClass="NorRow" />
                <AlternatingRowStyle CssClass="AltRowTable" />
                <HeaderStyle CssClass="SectionHeader" />
                <PagerStyle HorizontalAlign="Center" Font-Bold="true" />
            </asp:GridView>
        </div>
    </div>
                      </div>
              
              </section>
                  </div>
          
          </div>
          
      </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">

    
    <script type="text/javascript">
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

     <script type = "text/javascript">
         function Confirm() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Do you want to proceed?")) {
                 confirm_value.value = "Yes";
             } else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
         }
     </script>



</asp:Content>
