<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true" CodeBehind="BrandModeMaster.aspx.cs"
    Inherits="TheEMIClubApplication.MasterPage.BrandModeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <style>
    .form-group label {
        font-weight: 500;
    }

    label::before,
    label::after {
        content: none !important;
    }

    .table {
        border-radius: 8px;
        overflow: hidden;
        font-size: 0.85rem; /* slightly larger for readability */
        table-layout: auto; /* ensure natural column widths */
        width: 100%;
    }

    .table thead th {
        white-space: nowrap; /* keep header in single line */
        background: #007bff;
        color: white;
        text-align: center;
        text-overflow: ellipsis;
        font-weight: 600;
        padding: 8px;
    }

    .table tbody td {
        white-space: nowrap;       /* ✅ keep data in one line */
        overflow: hidden;          /* hide overflow text */
        text-overflow: ellipsis;   /* show ... if too long */
        vertical-align: middle;    /* better alignment */
        padding: 6px 8px;
    }

    .table tbody tr:hover {
        background-color: #f1f5ff;
    }

    .badge {
        padding: 0.35em 0.5em;
        font-size: 0.7rem;
        border-radius: 6px;
    }

    /* ✅ Responsive scroll on smaller screens */
    .table-container {
        overflow-x: auto;
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container-fluid py-4">
        <div class="card shadow border-0">
            <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                <h4 class="mb-0"><i class="fas fa-mobile-alt mr-2"></i> Device Model Master</h4>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfModelID" runat="server" />

                <!-- Form Section -->
<div class="row">
    <!-- Brand -->
    <div class="col-md-4">
        <div class="form-group">
            <label class="font-weight-bold">Brand</label>
            <div class="input-group">
                <div class="input-group-prepend">
                    <span class="input-group-text bg-primary text-white">
                        <i class="fas fa-mobile-alt"></i>
                    </span>
                </div>
                <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control">
                    <asp:ListItem Text="-- Select --" Value="0" />
                </asp:DropDownList>
            </div>
            <asp:RequiredFieldValidator ID="rfvBrand" runat="server"
                ControlToValidate="ddlBrand" InitialValue="0"
                ErrorMessage="* Select Brand" Display="Dynamic"
                ForeColor="Red" ValidationGroup="ProductGroup" />
        </div>
    </div>

    <!-- Model Name -->
    <div class="col-md-4">
        <div class="form-group">
            <label class="font-weight-bold">Model Name</label>
            <div class="input-group">
                <div class="input-group-prepend">
                    <span class="input-group-text bg-info text-white">
                        <i class="fas fa-tags"></i>
                    </span>
                </div>
                <asp:TextBox ID="txtModelName" runat="server" CssClass="form-control"
                    placeholder="Enter Model Name" />
            </div>
            <asp:RequiredFieldValidator ID="rfvModelName" runat="server"
                ControlToValidate="txtModelName"
                ErrorMessage="* Enter Model Name" Display="Dynamic"
                ForeColor="Red" ValidationGroup="ProductGroup" />
        </div>
    </div>

    <!-- Remark -->
    <div class="col-md-4">
        <div class="form-group">
            <label class="font-weight-bold">Remark</label>
            <div class="input-group">
                <div class="input-group-prepend">
                    <span class="input-group-text bg-warning text-dark">
                        <i class="fas fa-comment-dots"></i>
                    </span>
                </div>
                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control"
                    placeholder="Enter Remark" />
            </div>
            <asp:RequiredFieldValidator ID="rfvRemark" runat="server"
                ControlToValidate="txtRemark"
                ErrorMessage="* Enter Remark" Display="Dynamic"
                ForeColor="Red" ValidationGroup="ProductGroup" />
        </div>
    </div>

    <!-- Status -->
    <div class="col-md-4">
        <div class="form-group">
            <label class="font-weight-bold">Status</label>
            <div class="input-group">
                <div class="input-group-prepend">
                    <span class="input-group-text bg-success text-white">
                        <i class="fas fa-toggle-on"></i>
                    </span>
                </div>
                <asp:DropDownList ID="ddlActiveStatus" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Active" Value="Active" />
                    <asp:ListItem Text="Inactive" Value="Inactive" />
                </asp:DropDownList>
            </div>
            <asp:RequiredFieldValidator ID="rfvActiveStatus" runat="server"
                ControlToValidate="ddlActiveStatus" InitialValue="0"
                ErrorMessage="* Select Status" Display="Dynamic" ForeColor="Red" />
        </div>
    </div>
</div>


                <!-- Buttons -->
                <div class="text-center mt-3">
                    <asp:Button ID="btnSave" runat="server" Text="Save"
                        CssClass="btn btn-success btn-sm px-4 mr-2"
                        OnClick="btnSave_Click" ValidationGroup="ProductGroup" CausesValidation="true" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Update"
                        CssClass="btn btn-primary btn-sm px-4 mr-2"
                        OnClick="btnUpdate_Click" Visible="false"
                        ValidationGroup="ProductGroup" CausesValidation="true" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear"
                        CssClass="btn btn-secondary btn-sm px-4"
                        OnClick="btnClear_Click" CausesValidation="false" />
                </div>
            </div>
        </div>

        <!-- Records Section -->
    <%--    <div class="card shadow border-0 mt-4">
            <div class="card-header bg-light">
                <h5 class="mb-0 font-weight-bold text-primary">
                    <i class="fas fa-database mr-2"></i> Device Model Records
                </h5>
            </div>--%>
              <div class="card shadow border-0">
          <div class="card-header bg-light d-flex justify-content-between align-items-center">
              <h6 class="mb-0 text-primary font-weight-bold">
                       <i class="fas fa-database mr-2"></i> Device Model Records
              </h6>
          </div>

            <!-- Search -->
            <div class="card-body pb-0">
                <div class="row g-3 align-items-center">
                    <div class="col-md-3">
                        <asp:Label ID="lblgridError" runat="server" CssClass="text-danger font-weight-bold"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlSearch" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Brand Name" Value="BrandName" />
                            <asp:ListItem Text="Model Name" Value="ModelName" />
                            <asp:ListItem Text="Remark" Value="Remark" />
                            <asp:ListItem Text="Active" Value="Active" />
                            <asp:ListItem Text="Inactive" Value="Inactive" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtvalues" runat="server" CssClass="form-control"
                            placeholder="Enter Value" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-block"
                            Text="Search" CausesValidation="false" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>

            <!-- Grid -->
         <div class="table-container">
                <asp:GridView ID="gvModels" runat="server" AutoGenerateColumns="false" Width="100%"
                    CssClass="table table-bordered table-striped table-hover w-100 mb-0"
                    AllowPaging="true" PageSize="10"
                    DataKeyNames="RID,ActiveStatus"
                    OnRowCommand="gvModels_RowCommand"
                    OnDataBound="gvModels_DataBound"
                    OnPageIndexChanging="gvModels_PageIndexChanging1"
                    PagerSettings-Mode="Numeric"
                    PagerSettings-Position="Bottom"
                    PagerStyle-HorizontalAlign="Center"
                    PagerStyle-CssClass="pagination-container"
                    UseAccessibleHeader="true" GridLines="None">

                    <Columns>
                        <asp:BoundField DataField="SrNo" HeaderText="S.No" />
                        <asp:BoundField DataField="RID" HeaderText="ID" Visible="false" />
                        <asp:BoundField DataField="BrandName" HeaderText="Brand Name" />
                        <asp:BoundField DataField="ModelName" HeaderText="Model Name" />
                        <asp:BoundField DataField="Remark" HeaderText="Remark" />

                      
                        <asp:TemplateField HeaderText="Active Status">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkActiveStatus" runat="server"
                                    CssClass='<%# Eval("ActiveStatus").ToString() == "Active" ? "btn btn-sm btn-success" : "btn btn-sm btn-danger" %>'
                                    Text='<%# Eval("ActiveStatus").ToString() %>'
                                    CommandName="ActiveStatusRow"
                                    CommandArgument='<%# Eval("RID") %>'
                                    CausesValidation="false"
                                    OnClientClick='<%# "return confirm(\"Are you sure you want to change status of " + Eval("ModelName") + "?\");" %>'>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                      
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit"
                                    CommandName="EditRow" CommandArgument='<%# Eval("RID") %>'
                                    CssClass="btn btn-info btn-sm" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblModelRecordCount" runat="server"
                    CssClass="mt-2 font-weight-bold text-red"></asp:Label>
            </div>
        </div>
    </div>

    <!-- Error Modal -->
    <div id="ErrorPage" class="modal fade" role="dialog">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title"><i class="fas fa-exclamation-triangle mr-2"></i> Error Message</h5>
                </div>
                <div class="modal-body">
                    <span id="lblerror" class="text-danger font-weight-bold"></span>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Confirmation Modal -->
    <div id="MyPopups" class="modal fade" role="dialog">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title"><i class="fas fa-check-circle mr-2"></i> Confirmation</h5>
                </div>
                <div class="modal-body">
                    <span id="lblMessages" class="text-success font-weight-bold"></span><br />
                    <span id="lblName" class="text-primary"></span>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-success">OK</button>
                </div>
            </div>
        </div>
    </div>

    <!-- jQuery + Bootstrap -->
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/js/bootstrap.bundle.min.js"></script>
    <script src="https://kit.fontawesome.com/a076d05399.js" crossorigin="anonymous"></script>

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

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
