<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="ManageHoldAmount.aspx.cs" Inherits="TheEMIClubApplication.Admin.ManageHoldAmount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <style>
        /* 🔹 General Styles */
        body {
            background: #f4f6f9;
        }

        .card {
            border-radius: 12px;
            border: none;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.08);
        }

        .card-header {
            border-bottom: none;
            background: linear-gradient(90deg, #007bff, #0056b3);
            color: white;
            padding: 0.75rem 1rem;
            border-radius: 12px 12px 0 0 !important;
        }

        .card-header h6,
        .card-body h4 {
            font-weight: 600;
        }

        .form-label {
            font-weight: 600;
            color: #344767;
        }

        .input-group-text {
            background-color: #f1f3f5;
            border-right: none;
        }

        .form-control {
            border-left: none;
            border-color: #dee2e6;
            box-shadow: none !important;
            font-size: 0.9rem;
        }

        .form-control:focus {
            border-color: #007bff;
        }

        .btn-primary {
            background: linear-gradient(90deg, #007bff, #0056b3);
            border: none;
            border-radius: 30px;
            font-weight: 600;
            transition: all 0.3s ease;
        }

        .btn-primary:hover {
            background: linear-gradient(90deg, #0056b3, #004094);
            transform: translateY(-1px);
        }

        /* 🔹 Table Styles */
        .table-container {
            overflow-x: auto;
        }

        .table {
            width: 100%;
            border-radius: 8px;
            overflow: hidden;
            font-size: 0.85rem;
        }

        .table thead th {
            background: linear-gradient(90deg, #007bff, #0056b3);
            color: #fff;
            font-weight: 600;
            text-align: center;
            padding: 10px;
            white-space: nowrap;
        }

        .table tbody td {
            vertical-align: middle;
            white-space: nowrap;
            text-overflow: ellipsis;
            overflow: hidden;
            padding: 8px 10px;
        }

        .table tbody tr:hover {
            background: #f1f5ff;
            transition: 0.2s ease;
        }

        /* 🔹 Pagination Styling */
        .pagination-container td {
            text-align: center !important;
            padding: 10px;
        }

        .pagination a, .pagination span {
            margin: 0 4px;
            padding: 6px 10px;
            border-radius: 6px;
            background: #f8f9fa;
            border: 1px solid #dee2e6;
            color: #007bff;
            text-decoration: none;
            transition: all 0.3s ease;
        }

        .pagination a:hover {
            background: #007bff;
            color: white;
        }

        /* 🔹 Checkbox alignment */
        .form-check {
            display: flex;
            align-items: center;
            gap: 6px;
        }

        .form-check label {
            margin-bottom: 0;
            font-weight: 600;
            color: #495057;
        }

        /* 🔹 Responsive tweaks */
        @media (max-width: 768px) {
            .row.g-3 > [class*='col-'] {
                margin-bottom: 1rem;
            }
        }
    </style>

    <div class="container-fluid mt-4">

              <!-- Payment Approval Card style="display:none;"-->
      <div class="card shadow mb-4" runat="server" id="DivMakePayment" >
          <div class="card-header bg-primary text-white">
              <h5 class="mb-0"><i class="fas fa-credit-card mr-2"></i> Hold Amount Request Detail</h5>
          </div>
          <div class="card-body">
              <asp:HiddenField ID="hfRID" runat="server" />
              <asp:Label ID="lblRID" runat="server" Visible="false"></asp:Label>

              <!-- Row 1 -->
              <div class="form-row">
                  <div class="form-group col-md-3">
                      <label>Retailer Code</label>
                      <asp:TextBox ID="txtRetailerCode" runat="server" CssClass="form-control" ReadOnly="true" />
                  </div>
                  <div class="form-group col-md-3">
                      <label>Retailer Name</label>
                      <asp:TextBox ID="txtRetailerName" runat="server" CssClass="form-control" ReadOnly="true" />
                  </div>
                  <div class="form-group col-md-3">
                      <label>Mobile No</label>
                      <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" ReadOnly="true" />
                  </div>
                  <div class="form-group col-md-3">
                      <label>Emailid</label>
                      <asp:TextBox ID="txtEmailid" runat="server" CssClass="form-control" ReadOnly="true" />
                  </div>
              </div>

              <!-- Row 2 -->
              <div class="form-row">
                   <div class="form-group col-md-3">
     <label>Store Name</label>
     <asp:TextBox ID="txtStoreName" runat="server" CssClass="form-control" ReadOnly="true" />
 </div>
                  <div class="form-group col-md-3">
                      <label>Hold Amount</label>
                      <asp:TextBox ID="txtHoldamount" runat="server" CssClass="form-control" ReadOnly="true" />
                  </div>
                  <div class="form-group col-md-3">
                      <label>Wallet Balance</label>
                      <asp:TextBox ID="txtWalletBalance" runat="server" CssClass="form-control" ReadOnly="true" />
                  </div>
                  <%--<div class="form-group col-md-3">
                      <label>Active Status</label>
                      <asp:DropDownList ID="ddlActiveStatus" runat="server" CssClass="form-control" Enabled="false">
                          <asp:ListItem Text="Active" Value="ACTIVE" />
                          <asp:ListItem Text="Inactive" Value="INACTIVE" />
                      </asp:DropDownList>
                  </div>--%>
                                          <div class="form-group col-md-3">
    <label>Remarks</label>
     <asp:TextBox ID="txtremarks" runat="server" CssClass="form-control"  TextMode="MultiLine" line="2" />
</div>
              

                  <asp:HiddenField ID="hfdretailercode" runat="server" />
              </div>

            

              <!-- Buttons -->
              <div class="text-center mt-3">
                  <span id="Span1" runat="server" class="text-danger d-block mb-2"></span>
                  <asp:Button ID="btnApproved" runat="server" Text="Approved" CssClass="btn btn-success mr-2"  />
                   <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-danger mr-2"  />
                  <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-secondary"  />
              </div>
          </div>
      </div>



        <!-- Page Header -->
        <div class="card mb-4">
            <div class="card-header d-flex justify-content-between align-items-center">
                <h4 class="mb-0">
                    <i class="fas fa-chart-bar me-2"></i>Hold Amount Request Report
                </h4>
            </div>
        </div>

        <!-- Filters Section -->
        <div class="card mb-4">
            <div class="card-header">
                <h6 class="mb-0"><i class="fas fa-filter me-2"></i>Filters</h6>
            </div>
            <div class="card-body">
                <asp:Label ID="lblErrorMsg" runat="server" Text="" CssClass="form-label"></asp:Label>
                <div class="row g-3 align-items-end">
                    <div class="col-md-2">
                        <div class="form-check">
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true"
                               OnCheckedChanged="CheckBox1_CheckedChanged" CssClass="form-check-input" />
                            <label for="CheckBox1"> Date Filter</label>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label">From Date</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="far fa-calendar"></i></span>
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" TextMode="Date" />
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label">To Date</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" TextMode="Date" />
                        </div>
                    </div>

                                        <div class="col-md-3">
    <label class="form-label">User</label>
    <div class="input-group">
        <span class="input-group-text"><i class="fas fa-user"></i></span>
        <asp:DropDownList ID="ddluser" runat="server" CssClass="form-control"></asp:DropDownList>
    </div>
</div>
                    
                </div>

                <div class="row g-3 mt-3 align-items-end">
                    
                   <%-- <div class="col-md-3">
                        <label class="form-label">Search Parameter</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-search"></i></span>
                            <asp:DropDownList ID="ddlSearchdiffrentParams" runat="server" CssClass="form-control">
                              <%--  <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Customercode" Value="Customercode"></asp:ListItem>
                                <asp:ListItem Text="LoanCode" Value="LoanCode"></asp:ListItem>
                                <asp:ListItem Text="Name" Value="Name"></asp:ListItem>
                                <asp:ListItem Text="ReceiptNo" Value="ReceiptNo"></asp:ListItem>
                                <asp:ListItem Text="PaymentMode" Value="PaymentMode"></asp:ListItem>
                                <asp:ListItem Text="RecordStatus" Value="RecordStatus"></asp:ListItem>
                                <asp:ListItem Text="Status" Value="Status"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label">Value</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="fas fa-keyboard"></i></span>
                            <asp:TextBox ID="txtSearchValues" runat="server" CssClass="form-control" placeholder="Enter search value" />
                        </div>
                    </div>--%>

                    <div class="col-md-12 text-center mt-3">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary px-4 py-2"/>

                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-primary px-4 py-2"  />
                    </div>
                </div>
            </div>
        </div>

        <!-- Report Table  -->
        <div class="card">
            <div class="card-header">
                <h6 class="mb-0"><i class="fas fa-table me-2"></i> Report Results</h6>
            </div>
            <div class="table-container">
                <asp:GridView ID="gvAllReport" runat="server"
                    CssClass="table table-bordered table-striped table-hover mb-0"
                    OnRowCommand="gvAllReport_RowCommand"
                    DataKeyNames="RetailerCode"
                    AutoGenerateColumns="true"
                    AllowPaging="true"
                    PageSize="10"
                    EmptyDataText="⚠️ No records found for the selected filters."
                    UseAccessibleHeader="true"
                    OnPageIndexChanging="gvAllReport_PageIndexChanging"
                    GridLines="None"
                    PagerSettings-Mode="Numeric"
                    PagerStyle-HorizontalAlign="Center"
                    PagerStyle-CssClass="pagination-container">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtn_HoldAmountPay" runat="server" Text="Transfer" CssClass="btn btn-primary"
                                 CommandName="PayHoldAmount"  CommandArgument='<%# Container.DataItemIndex %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
