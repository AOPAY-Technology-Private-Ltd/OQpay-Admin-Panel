<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="LoanMaster.aspx.cs" Inherits="TheEMIClubApplication.MasterPage.LoanMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <section class="content">
    <div class="container-fluid">
        <div class="card card-default">
            <div class="card-header form-header-bar">
                <h3 class="card-title">Loan Master Entry</h3>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfLoanId" runat="server" />

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
                        <label>Loan Amount</label>
                        <asp:TextBox ID="txtLoanAmount" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                    <div class="col-md-4 form-group">
                        <label>Interest Rate (%)</label>
                        <asp:TextBox ID="txtInterestRate" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                    <div class="col-md-4 form-group">
                        <label>Tenure (Months)</label>
                        <asp:TextBox ID="txtTenureMonths" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                    <div class="col-md-4 form-group">
                        <label>EMI Amount</label>
                        <asp:TextBox ID="txtEMIAmount" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                    <div class="col-md-4 form-group">
                        <label>Start Date</label>
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-4 form-group">
                        <label>End Date</label>
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div class="col-md-4 form-group">
                        <label>Loan Status</label>
                        <asp:DropDownList ID="ddlLoanStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Text="-- Select --" Value="" />
                            <asp:ListItem Text="Pending" Value="Pending" />
                            <asp:ListItem Text="Active" Value="Active" />
                            <asp:ListItem Text="Closed" Value="Closed" />
                            <asp:ListItem Text="Defaulted" Value="Defaulted" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4 form-group">
                        <label>Created At</label>
                        <asp:TextBox ID="txtCreatedAt" runat="server" CssClass="form-control" TextMode="DateTimeLocal" />
                    </div>
                </div>

                <div class="col-md-12 text-center mt-3">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
                </div>
            </div>
        </div>

        <!-- GridView to display saved loans -->
        <div class="card mt-4">
            <div class="card-header">
                <h3 class="card-title">Loan Master Records</h3>
            </div>
            <div class="card-body">
                <asp:GridView ID="gvLoanMaster" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                    DataKeyNames="LoanId" OnRowCommand="gvLoanMaster_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="LoanId" HeaderText="Loan ID" />
                        <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                        <asp:BoundField DataField="LoanAmount" HeaderText="Loan Amount" />
                        <asp:BoundField DataField="InterestRate" HeaderText="Interest Rate (%)" />
                        <asp:BoundField DataField="TenureMonths" HeaderText="Tenure (Months)" />
                        <asp:BoundField DataField="EMIAmount" HeaderText="EMI Amount" />
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="LoanStatus" HeaderText="Status" />
                        <asp:BoundField DataField="CreatedAt" HeaderText="Created At" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRow" CommandArgument='<%# Eval("LoanId") %>' Text="Edit" CssClass="btn btn-sm btn-primary" />
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
