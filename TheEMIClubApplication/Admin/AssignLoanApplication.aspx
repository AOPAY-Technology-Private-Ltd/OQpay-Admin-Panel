<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="AssignLoanApplication.aspx.cs" Inherits="TheEMIClubApplication.MasterPage.AssignLoanApplication" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
                <div id="DivAssignapplication" class="card card-default  p-2 m-2" runat="server" >
                <%-- Customer EMI Details--%>
                <div class="card-header form-header-bar" id="HeaderAssignapplication" style="background-color: transparent;">
                    <h3 class="card-title mb-0" style="color: black; background-color: transparent;">Assign Application</h3>
                </div>
                <div class="card-body">
                    <div class="col-md-12 card-body-box m-2">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <span class="font-weight-bold">Criteria</span>
                                    <asp:DropDownList ID="ddlEmiCriteria" runat="server" CssClass="form-control">
                                        <asp:ListItem id="EMIAll" Text="All" Value="0"></asp:ListItem>
                                        <%-- <asp:ListItem id="Emiid" Text="EMI Id" Value="Emiid"></asp:ListItem>--%>
                                        <asp:ListItem id="loanid" Text="Loan Id" Value="loanid"></asp:ListItem>
                                        <asp:ListItem id="userid" Text="User Id" Value="userid"></asp:ListItem>
                                        <asp:ListItem id="EMIActive" Text="Active" Value="Active"></asp:ListItem>
                                        <asp:ListItem id="EMIInactive" Text="Inactive" Value="Inactive"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <span class="font-weight-bold">Value</span>
                                    <asp:TextBox ID="txtEmivalue" runat="server" class="form-control" Placeholder="Enter Value"></asp:TextBox>


                                </div>
                            </div>
                            <div class="col-md-6  d-flex align-items-center">
                                <span class="font-weight-bold"></span>
                                <asp:Button ID="btnEMISearch" runat="server" Text="Search" class="btn btn-primary"  />
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid my-3">
                        <div class="table-responsive">
                            <span id="spnMessage" runat="server"></span>
<asp:UpdatePanel ID="UpdatePanel3" runat="server">
    <ContentTemplate>
        <asp:GridView ID="grdAssignApplication" runat="server"
            OnRowDataBound="grdAssignApplication_RowDataBound"
             OnRowCommand="grdAssignApplication_RowCommand"
            AutoGenerateColumns="false"
            PageSize="10"
            CssClass="table table-bordered table-striped table-hover w-100 mb-0"
            AllowPaging="true"
            DataKeyNames="RID"
            PagerSettings-Mode="Numeric"
            PagerSettings-Position="Bottom"
            PagerStyle-HorizontalAlign="Center"
            PagerStyle-CssClass="pagination-container"
            UseAccessibleHeader="true"
            GridLines="None"
            OnPageIndexChanging="grdAssignApplication_PageIndexChanging">

            <Columns>
              
                <asp:TemplateField HeaderText="Select / Reassign">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="SrNo" HeaderText="S.No" />

                <asp:BoundField DataField="LoanCode" HeaderText="Loan ID" />
                <asp:BoundField DataField="CustomerCode" HeaderText="User Code" />
                <asp:BoundField DataField="LoanAmount" HeaderText="Loan Amt." />
                <asp:BoundField DataField="DownPayment" HeaderText="DP" />
                <asp:BoundField DataField="EMIAmount" HeaderText="EMI Amt." />
                <asp:BoundField DataField="Tenure" HeaderText="Tenure" />
                <asp:BoundField DataField="InterestRate" HeaderText="Inst.(%)" />
                <asp:BoundField DataField="PaidEMI" HeaderText="PaidEMI" />
                <asp:BoundField DataField="DuesEMI" HeaderText="DuesEMI" />
                <asp:BoundField DataField="FirstDueDate" HeaderText="FirstDueDate" />
                <asp:BoundField DataField="RecordStatus" HeaderText="RecordStatus" />
                <asp:BoundField DataField="flag_Status" HeaderText="Assigned" />

               
            <asp:TemplateField HeaderText="Action">
    <ItemTemplate>
        <asp:LinkButton ID="lnkReassign" runat="server"
            Text="Reassign"
            CssClass="btn btn-sm btn-warning"
            CommandName="Reassign"
            CommandArgument='<%# Eval("LoanCode") %>'
            OnClientClick="return confirm('Are you sure you want to enable reassignment for this loan?');">
        </asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblEMINoData" runat="server"
            CssClass="mt-2 font-weight-bold d-block">
        </asp:Label>
    </ContentTemplate>
</asp:UpdatePanel>


                                        <div class="col-md-12">
                                        <div class="form-group d-flex align-items-center justify-content-center flex-row">
                                            <span class="font-weight-bold m-1">Employee Name</span>
                                            <asp:DropDownList ID="ddlemployee" runat="server" class='form-control w-auto m-1'>
                                                <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Button ID="btnSubmit" runat="server" Text="Assign Application" class="btn btn-primary m-1" OnClick="btnSubmit_click" />
                                            <asp:Button ID="btncancel" runat="server" Text="Cancel" class="btn btn-success m-1"  />
                                        </div>
                                    </div> 
                        </div>

                       <%-- <div class="row">
                          <div class="card-header form-header-bar">
                               <h3 class="card-title mb-0" style="color: black; background-color: transparent;">Assign Loan</h3>
                          </div>
                            <div class="row">
                                <div class="col-4">

                                </div>
                                <div class="col-4">

                                </div>
                            </div>
                        </div>--%>

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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
