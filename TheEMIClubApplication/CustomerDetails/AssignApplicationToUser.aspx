<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="AssignApplicationToUser.aspx.cs" Inherits="TheEMIClubApplication.CustomerDetails.AssignApplicationToUser" %>
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
                                    <h3 class="card-title">Assign Application To User </h3>
                                </div>
                                <!-- /.card-header -->
                                <div class="card-body ">
                                                 <span id="spnMessage" runat="server"></span>
                                      <%--<asp:Panel ID="Panel1" runat="server" ScrollBars="Both">--%>
                                    <asp:GridView ID="gvAssignApplication" runat="server" AutoGenerateColumns="false"
                                        Width="100%" CssClass="table table-bordered table-condensed table-hover">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select Data">
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Sl No." DataField="SNo" />
                                            <asp:BoundField HeaderText="Customer Code" DataField="CustCode" />
                                            <asp:BoundField HeaderText="Application No" DataField="LoanId" />
                                            <asp:BoundField HeaderText="Name" DataField="Name" />
                                            <asp:BoundField HeaderText="LoanType" DataField="LoanType" />
                                            <asp:BoundField HeaderText="Status" DataField="LoanStatus" />
                                            <asp:BoundField HeaderText="Mobile No" DataField="MobileNo" />
                                        </Columns>
                                        <RowStyle CssClass="NorRow" />
                                        <AlternatingRowStyle CssClass="AltRowTable" />
                                        <HeaderStyle CssClass="SectionHeader" />
                                        <PagerStyle HorizontalAlign="Center" Font-Bold="true" />
                                    </asp:GridView>
                                    <div class="col-md-12">
                                        <div class="form-group d-flex align-items-center justify-content-center flex-row">
                                            <span class="font-weight-bold m-1">Employee Name</span>
                                            <asp:DropDownList ID="ddlemployee" runat="server" class='form-control w-auto m-1'>
                                            </asp:DropDownList>
                                            <asp:Button ID="btnSubmit" runat="server" Text="Assign Application" class="btn btn-primary m-1" OnClick="btnSubmit_click" />
                                            <asp:Button ID="btncancel" runat="server" Text="Cancel" class="btn btn-success m-1"  OnClick="btncancel_Click"/>
                                        </div>
                                    </div> 
                                    
                                    <div class="col-md-4">
                                  <div class="form-group">
    
</div>
    </div>
                                         <%-- </asp:Panel>--%>
                                </div>
                            </div>

                        </div>
                        <!-- Grid View -->
          <%--              <div class="card card-default">
                            <div class="card-header form-header-bar">
                                <h3 class="card-title">User List</h3>
                            </div>
                            <!-- /.card-header -->
                            <div class="card-body">
                                

                                      <div class="row">
          <div class="col-md-12 card-body-box">

              <div class="row">
                  



              </div>
          </div>
      </div>
                            </div>

                               
                        </div>--%>
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
