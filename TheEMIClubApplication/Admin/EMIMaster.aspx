<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="EMIMaster.aspx.cs" Inherits="TheEMIClubApplication.MasterPage.EMIMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <section class="content">
    <div class="container-fluid">
        <div class="card card-default">
            <div class="card-header form-header-bar">
                <h3 class="card-title">Loan Installment Entry</h3>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfInstallmentId" runat="server" />

                <div class="row">
                    <div class="col-md-4 form-group">
                        <label>Loan ID</label>
                        <asp:TextBox ID="txtLoanId" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-4 form-group">
                        <label>Customer Code</label>
                        <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-4 form-group">
                        <label>Installment No</label>
                        <asp:TextBox ID="txtInstallmentNo" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                    <div class="col-md-4 form-group">
                        <label>Due Date</label>
                        <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-4 form-group">
                        <label>Principal Amount</label>
                        <asp:TextBox ID="txtPrincipalAmount" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                    <div class="col-md-4 form-group">
                        <label>Interest Amount</label>
                        <asp:TextBox ID="txtInterestAmount" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                    <div class="col-md-4 form-group">
                        <label>Total Amount</label>
                        <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                    <div class="col-md-4 form-group">
                        <label>Grace Period (Days)</label>
                        <asp:TextBox ID="txtGracePeriod" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                    <div class="col-md-4 form-group">
                        <label>Penalty / Day</label>
                        <asp:TextBox ID="txtPenaltyPerDay" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                </div>

                <div class="col-md-12 text-center mt-3">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
                </div>
            </div>
        </div>

        <!-- GridView -->
        <div class="card mt-4">
            <div class="card-header">
                <h3 class="card-title">Loan Installment List</h3>
            </div>
            <div class="card-body">
                <asp:GridView ID="gvInstallments" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                    DataKeyNames="LoanId,InstallmentNo" OnRowCommand="gvInstallments_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="LoanId" HeaderText="Loan ID" />
                        <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                        <asp:BoundField DataField="InstallmentNo" HeaderText="Installment No" />
                        <asp:BoundField DataField="DueDate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="PrincipalAmount" HeaderText="Principal" />
                        <asp:BoundField DataField="InterestAmount" HeaderText="Interest" />
                        <asp:BoundField DataField="TotalAmount" HeaderText="Total" />
                        <asp:BoundField DataField="GracePeriod" HeaderText="Grace Period" />
                        <asp:BoundField DataField="PenaltyPerDay" HeaderText="Penalty / Day" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRow" CommandArgument='<%# Eval("InstallmentNo") %>' Text="Edit" CssClass="btn btn-sm btn-primary" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</section>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
