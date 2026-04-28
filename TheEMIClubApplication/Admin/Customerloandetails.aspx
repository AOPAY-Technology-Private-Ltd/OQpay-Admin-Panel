<%@ Page Title="Customer Loan Details" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Customerloandetails.aspx.cs" Inherits="TheEMIClubApplication.Admin.Customerloandetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <div class="container-fluid mt-4">
        <!-- Page Heading -->
        <div class="row mb-3">
            <div class="col">
                <h3 class="text-primary"><i class="fas fa-file-invoice-dollar"></i> Customer Loan Details</h3>
                <hr />
            </div>
        </div>

        <!-- Search Filters Card -->
        <div class="card shadow-sm mb-4">
            <div class="card-header bg-primary text-white">
                <i class="fas fa-search"></i> Search Loans
            </div>
            <div class="card-body">
                <div class="form-row">
                    <div class="form-group col-md-3">
                        <label><i class="fas fa-hashtag"></i> Loan Code</label>
                        <asp:TextBox ID="txtLoanCode" CssClass="form-control" runat="server" placeholder="Enter Loan Code"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label><i class="fas fa-user"></i> Customer Code</label>
                        <asp:TextBox ID="txtCustomerCode" CssClass="form-control" runat="server" placeholder="Enter Customer Code"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
      <label><i class="fas fa-store"></i> Retailer Code</label>
 <asp:DropDownList ID="ddlRetailer" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                   
 </asp:DropDownList>
                      
                    </div>
             <%--       <div class="form-group col-md-3">
                        <label><i class="fas fa-briefcase"></i> Employee Code</label>
                        <asp:TextBox ID="txtEmployeeCode" CssClass="form-control" runat="server" placeholder="Enter Employee Code"></asp:TextBox>
                    </div>--%>
                </div>

                <div class="form-row">
                    <div class="form-group col-md-3">
                        <label><i class="fas fa-mobile-alt"></i> Brand</label>
                        <asp:TextBox ID="txtBrandName" CssClass="form-control" runat="server" placeholder="Samsung / Apple"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label><i class="fas fa-tag"></i> Model</label>
                        <asp:TextBox ID="txtModelName" CssClass="form-control" runat="server" placeholder="Galaxy A15 / iPhone 15"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label><i class="fas fa-cube"></i> Variant</label>
                        <asp:TextBox ID="txtVariantName" CssClass="form-control" runat="server" placeholder="6GB+128GB"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label><i class="fas fa-toggle-on"></i> Status</label>
                        <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                            <asp:ListItem Text="-- All --" Value=""></asp:ListItem>
                            <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                            <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="form-row">
                    <div class="form-group col-md-3">
                        <label><i class="fas fa-calendar-alt"></i> From Date</label>
                        <asp:TextBox ID="txtFromDate" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label><i class="fas fa-calendar-check"></i> To Date</label>
                        <asp:TextBox ID="txtToDate" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                </div>

                <div class="text-right">
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-secondary ml-2" Text="Reset"  OnClick="btnReset_Click" />
                </div>
            </div>
        </div>

        <!-- Results Table -->
        <div class="card shadow-sm">
            <div class="card-header bg-info text-white">
                <i class="fas fa-list"></i> Loan Results
            </div>
            <div class="card-body table-responsive">
                <asp:GridView ID="gvLoanDetails" runat="server" CssClass="table table-bordered table-hover"
                    AutoGenerateColumns="False" GridLines="None" AllowPaging="true"   PagerSettings-Mode="Numeric" OnPageIndexChanging="gvLoanDetails_PageIndexChanging"
  PagerSettings-Position="Bottom"
  PagerStyle-HorizontalAlign="Center"
  PagerStyle-CssClass="pagination-container"
  UseAccessibleHeader="true">
                    <Columns>
                      <asp:BoundField DataField="LoanCode" HeaderText="Loan Code" />
<asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
<asp:BoundField DataField="RetailerCode" HeaderText="Retailer Code" />
<asp:BoundField DataField="BrandName" HeaderText="Brand" />
<asp:BoundField DataField="ModelName" HeaderText="Model" />
<asp:BoundField DataField="VariantName" HeaderText="Variant" />

<asp:BoundField DataField="LoanAmount" HeaderText="Loan Amount" DataFormatString="₹ {0:N2}" HtmlEncode="False" />
<asp:BoundField DataField="TotalEMI" HeaderText="Total EMI" DataFormatString="₹ {0:N2}" HtmlEncode="False" />
<asp:BoundField DataField="CollectedEMI" HeaderText="Collected EMI" DataFormatString="₹ {0:N2}" HtmlEncode="False" />
<asp:BoundField DataField="PendingEMI" HeaderText="Pending EMI" DataFormatString="₹ {0:N2}" HtmlEncode="False" />

<asp:BoundField DataField="LoanStatus" HeaderText="Status" />
<asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:dd-MMM-yyyy}" />
<asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:dd-MMM-yyyy}" />

                    </Columns>
                </asp:GridView>
                         <asp:Label ID="lblModelRecordCount" runat="server"
             CssClass="mt-2 font-weight-bold text-red"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
             <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" rel="stylesheet"/>
<script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
</asp:Content>
