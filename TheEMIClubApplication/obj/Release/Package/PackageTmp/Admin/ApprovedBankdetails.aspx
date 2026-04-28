<%@ Page Title="Approved Bank Details" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="ApprovedBankdetails.aspx.cs" Inherits="TheEMIClubApplication.Admin.ApprovedBankdetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <style>
        .form-group label {
            font-weight: 500;
        }

        label::before, label::after {
            content: none !important;
        }
    </style>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />

    <div class="container-fluid mt-4">
        <!-- 🔹 Filter Section -->
        <div class="card shadow-lg border-0 mb-4">
            <div class="card-header bg-gradient-primary text-white d-flex justify-content-between align-items-center">
                <h5 class="mb-0"><i class="fas fa-search mr-2"></i>Search Approved Bank Details</h5>
            </div>
            <div class="card-body">
                <div class="form-row">
                    <!-- Mobile No Filter -->
                    <div class="form-group col-md-3">
                        <label for="txtMobileNo">Mobile No</label>
                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" />
                    </div>

                    <!-- Retailer ID Filter -->
                    <div class="form-group col-md-3">
                        <label for="ddlRetailer">Retailer Code</label>
                        <asp:DropDownList ID="ddlRetailer" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>

                    <!-- Approved Status Filter -->
                    <div class="form-group col-md-3">
                        <label for="ddlApprovedStatus">Approved Status</label>
                        <asp:DropDownList ID="ddlApprovedStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Text="-- Select --" Value=""></asp:ListItem>
                            <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                            <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>


                        </asp:DropDownList>
                    </div>

                    <!-- Search Button -->
                    <div class="form-group col-md-3 d-flex align-items-end">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-block"
                            Text="Search" OnClick="btnSearch_Click" />
                    </div>

                    <asp:Label ID="lblModelRecordCount" runat="server"
                        CssClass="mt-2 font-weight-bold text-red"></asp:Label>
                </div>
            </div>
        </div>

        <!-- 🔹 Results Section -->
        <asp:Panel ID="pnlResults" runat="server" Visible="false">
            <div class="card shadow-lg border-0">
                <div class="card-header bg-gradient-secondary text-white d-flex justify-content-between align-items-center">
                    <h5 class="mb-0"><i class="fas fa-table mr-2"></i>Approved Bank Details</h5>
                </div>
                <div class="card-body table-responsive">
                    <asp:GridView ID="gvBankDetails" runat="server" CssClass="table table-striped table-bordered table-sm"
                        AutoGenerateColumns="False" EmptyDataText="No records found."
                        AllowPaging="true" PageSize="10"
                        OnRowCommand="gvBankDetails_RowCommand" PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom" OnPageIndexChanging="gvBankDetails_PageIndexChanging"
                        PagerStyle-HorizontalAlign="Center"
                        PagerStyle-CssClass="pagination-container"
                        UseAccessibleHeader="true"
                        GridLines="None">
                        <HeaderStyle CssClass="thead-dark" />
                        <Columns>

                            <asp:BoundField DataField="RID" HeaderText="RID" SortExpression="RID" />
                            <asp:BoundField DataField="RetailerID" HeaderText="RetailerID" SortExpression="RetailerID" />
                            <asp:BoundField DataField="MobileNumber" HeaderText="Mobile No" SortExpression="MobileNumber" />
                            <asp:BoundField DataField="AccountName" HeaderText="Account Holder Name" SortExpression="AccountName" />
                            <asp:BoundField DataField="AccountNumber" HeaderText="Account No" SortExpression="AccountNumber" />
                            <asp:BoundField DataField="IFSCCode" HeaderText="IFSC Code" SortExpression="IFSCCode" />
                            <asp:BoundField DataField="BranchName" HeaderText="Branch Name" SortExpression="BranchName" />
                            <asp:BoundField DataField="BranchAddress" HeaderText="Branch Addresss" SortExpression="BranchAddress" />
                            <asp:BoundField DataField="EmailID" HeaderText="Email" SortExpression="EmailID" />


                            <asp:TemplateField HeaderText="Approved Status">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkApprovedStatus" runat="server"
                                        Text='<%# (Eval("Approved_YN").ToString() == "Active") ? "Active" : "Inactive" %>'
                                        CommandName="UpdateStatus"
                                        CommandArgument='<%# Eval("RID") + "|" + Eval("Approved_YN") + "|" + Eval("RetailerID") %>'
                                        CssClass='<%# (Eval("Approved_YN").ToString() == "Active") 
                    ? "btn btn-sm btn-success"   // green if Active
                    : "btn btn-sm btn-danger"    // red if Inactive
                 %>'
                                        OnClientClick="return confirm('Are you sure you want to change the status?');">
                                    </asp:LinkButton>
                                </ItemTemplate>


                            </asp:TemplateField>


                        </Columns>
                        <%--   <PagerStyle CssClass="pagination-outer" HorizontalAlign="Center" />--%>
                    </asp:GridView>

                </div>
            </div>
        </asp:Panel>




        <!-- Modal for Error Messages -->
        <div id="ErrorPage" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Error Message</h5>
                    </div>
                    <div class="modal-body">
                        <span id="lblerror" style="font-family: Georgia, 'Times New Roman', Times, serif; font-size: medium; color: red; font-weight: bold"></span>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>

        <!-- Confirmation Popup Modal -->
        <div id="MyPopups" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: cornflowerblue">
                        <h5 class="modal-title">Confirmation Messages</h5>
                    </div>
                    <div class="modal-body">
                        <span id="lblMessages" style="font-family: Georgia, 'Times New Roman', Times, serif; font-size: medium; color: forestgreen"></span>
                        <br />
                        <span id="lblName" style="color: navy"></span>
                    </div>
                    <div class="modal-footer">
                        <button type="button" data-dismiss="modal" class="btn btn-success">Ok</button>
                    </div>
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

        function ShowError(ErrorMessages) {
            $("#ErrorPage #lblerror").html(ErrorMessages);
            $("#ErrorPage").modal("show");
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
