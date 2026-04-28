<%@ Page Title="Manage Disbursement" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="ManageDisburshment.aspx.cs" Inherits="TheEMIClubApplication.Admin.ManageDisburshment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
         <div class="container-fluid mt-4">
      <!-- 🔹 Filter Section -->
      <div class="card shadow-lg border-0 mb-4">
          <div class="card-header bg-gradient-primary text-white d-flex justify-content-between align-items-center">
              <h5 class="mb-0"><i class="fas fa-search mr-2"></i> Manage Disburshment</h5>
          </div>
<div class="card-body">
                <asp:GridView ID="gvDisbursement" runat="server" CssClass="table table-bordered table-hover text-center"
                    AutoGenerateColumns="False" DataKeyNames="LoanCode"  OnRowCommand="gvDisbursement_RowCommand">
                    
                    <Columns>
                        <asp:BoundField DataField="LoanCode" HeaderText="Loan Code" />
                        <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                        <asp:BoundField DataField="FullName" HeaderText="Customer Name" />
                        <asp:BoundField DataField="BrandName" HeaderText="Brand" />
                        <asp:BoundField DataField="ModelName" HeaderText="Model" />
                        <asp:BoundField DataField="LoanAmount" HeaderText="Loan Amount"/>
                        <asp:BoundField DataField="Tenure" HeaderText="Tenure (Months)" />
                        <asp:BoundField DataField="EMIAmount" HeaderText="EMI Amount"/>
                        <asp:BoundField DataField="ProcessingFees" HeaderText="Processing Fees"/>
                        <asp:BoundField DataField="MemberShip" HeaderText="MemberShip Fees"/>
                        <asp:BoundField DataField="RecordStatus" HeaderText="Status" />

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDisburse" runat="server" 
                                    Text="Disburse" CssClass="btn btn-success btn-sm"
                                    CommandName="DisburseLoan"
                                    CommandArgument='<%# Eval("LoanCode") %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:Label ID="lblMessage" runat="server" CssClass="text-center text-danger fw-bold d-block mt-2"></asp:Label>
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
