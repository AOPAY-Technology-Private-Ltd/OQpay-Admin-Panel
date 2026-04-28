<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Collection.aspx.cs" Inherits="TheEMIClubApplication.Collection.Collection" %>

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
                                              <div class="col-md-4">
                                                  <div class="form-group">
                                                      <span class="font-weight-bold">Customer Name</span>
                                                      <asp:TextBox ID="txtcustmerName" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>


                                                  </div>
                                              </div>
                                              <div class="col-md-4">
                                                  <div class="form-group">
                                                      <span class="font-weight-bold">Mobile No</span>
                                                      <asp:TextBox ID="txtMobileNo" runat="server" class="form-control" ReadOnly="true"></asp:TextBox>


                                                  </div>
                                              </div>




                                          </div>
                                      </div>
                                  </div>
                              </div>
                          </div>

                      </div>
                      <!-- Grid View -->
                      <div class="card card-default">
                          <div class="card-header form-header-bar">
                              <h3 class="card-title">Collection Details</h3>
                          </div>
                          <!-- /.card-header -->
                          <div class="col-md-12 text-center mb-3 w-100 overflow-auto">
                              <span id="spnMessage" runat="server"></span>

                              <asp:GridView ID="gvcollection" runat="server"  AutoGenerateColumns="False"  Width="100%" CssClass="table table-bordered table-condensed table-hover"  >
                                  <Columns>
                                    
                                        <asp:BoundField HeaderText="EMI.No" DataField="InsttalmentNo" />
                                      <asp:BoundField HeaderText="Cust.Code" DataField="CustCode" />
                                      <asp:BoundField HeaderText="L.ID" DataField="LoanID" />
                                    
                                      <asp:BoundField HeaderText="Due.Dt." DataField="EMIStartDate" DataFormatString="{0:d}" />
                                    

                                     
                                      <asp:BoundField HeaderText="EMI Amt." DataField="TotalAmount" />
                                      <asp:BoundField HeaderText="Paid Amount" DataField="PaidAmount" />
                                    

                                      <asp:BoundField HeaderText="EMI Status" DataField="EMIStatus" />
                                      <asp:BoundField HeaderText="Approved By" DataField="ApprovedBy" />
                                      
                                      <asp:BoundField HeaderText="ApprovedDate" DataField="ApprovedDate" DataFormatString="{0:d}" />
                                    
                         
           
          


                                  </Columns>
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
