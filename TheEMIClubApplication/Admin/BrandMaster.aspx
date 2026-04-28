<%@ Page Title="Brand Master" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="BrandMaster.aspx.cs" Inherits="TheEMIClubApplication.MasterPage.BrandMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <style>
    .form-group label {
        font-weight: 500;
    }

    label::before,
    label::after {
        content: none !important;
    }
        </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container-fluid py-4">
        <div class="card shadow border-0">
           <div class="card-header bg-primary text-white">
    <h4 class="mb-0">
        <i class="fas fa-mobile-alt mr-2"></i> Mobile Brand Master
    </h4>
</div>

            <div class="card-body">
                   <asp:HiddenField ID="hfRID" runat="server" />

    <div class="form-row">
        <!-- Brand Name -->
        <div class="form-group col-md-4">
            <label class="font-weight-bold">Brand Name</label>
            <div class="input-group">
                <div class="input-group-prepend">
                    <span class="input-group-text bg-primary text-white">
                        <i class="fas fa-mobile-alt"></i>
                    </span>
                </div>
                <asp:TextBox ID="txtBrandName" runat="server" CssClass="form-control" placeholder="Enter Brand Name"></asp:TextBox>
            </div>
            <asp:RequiredFieldValidator ID="rfvBrandName" runat="server" ErrorMessage="* Enter Brand Name"
                Display="Dynamic" ControlToValidate="txtBrandName" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>

        <!-- Remark -->
        <div class="form-group col-md-4">
            <label class="font-weight-bold">Remark</label>
            <div class="input-group">
                <div class="input-group-prepend">
                    <span class="input-group-text bg-info text-white">
                        <i class="fas fa-comment-dots"></i>
                    </span>
                </div>
                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" placeholder="Enter Remark"></asp:TextBox>
            </div>
            <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ErrorMessage="* Enter Brand Remark"
                Display="Dynamic" ControlToValidate="txtRemark" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>

        <!-- Active Status -->
        <div class="form-group col-md-4">
            <label class="font-weight-bold">Active Status</label>
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
            <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ErrorMessage="* Select Status"
                Display="Dynamic" ControlToValidate="ddlActiveStatus" InitialValue="0"></asp:RequiredFieldValidator>
        </div>
    </div>
                <div class="text-center mt-3">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-sm px-4 mr-2" Text="Save" OnClick="btnSave_Click" />
                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-secondary btn-sm px-4" Text="Clear" OnClick="btnClear_Click" CausesValidation="false" />
                </div>
            </div>
        </div>

        <!-- Brand Records Section -->
        <div class="card shadow border-0 mt-4">
<div class="card-header bg-light d-flex justify-content-between align-items-center">
    <h5 class="mb-0 font-weight-bold text-primary">
        <i class="fas fa-mobile-alt mr-2"></i> Brand Records
    </h5>
                 <div class="col-md-3">
                                        <asp:Label ID="lblgridError" runat="server" Text=""></asp:Label>
                                    </div>
                <div class="input-group input-group-sm w-50">
                    <asp:TextBox ID="txtSearchBrand" runat="server" CssClass="form-control" placeholder="Search by Brand Name" />
                    <div class="input-group-append">
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-outline-primary" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>

            <div class="card-body">
                <span id="spnMessage" runat="server"></span>
                <div class="table-responsive">
                    <asp:GridView ID="gvBrands" runat="server" AutoGenerateColumns="false"
                        Width="100%" CssClass="table table-bordered table-hover table-sm"
                        OnRowCommand="gvBrands_RowCommand" DataKeyNames="RID,ActiveStatus" AllowPaging="true"
                        OnDataBound="gvBrands_DataBound"
                        OnPageIndexChanging="gvBrands_PageIndexChanging"
                        PagerSettings-Mode="Numeric"
                        PagerSettings-Position="Bottom"
                        PagerStyle-HorizontalAlign="Center"
                        PagerStyle-CssClass="pagination-container"
                        UseAccessibleHeader="true"
                        GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="S.No" />
                            <asp:BoundField HeaderText="RID" DataField="RID" Visible="false" />
                            <asp:BoundField HeaderText="Brand Name" DataField="BrandName" />
                            <asp:BoundField HeaderText="Remark" DataField="Remark" />
      <asp:TemplateField HeaderText="Active Status">
    <ItemTemplate>
        <asp:LinkButton ID="lnkManageStatus" runat="server"
            CssClass='<%# Eval("ActiveStatus").ToString() == "Active" ? "btn btn-sm btn-success" : "btn btn-sm btn-danger" %>'
            Text='<%# Eval("ActiveStatus").ToString() %>'
            CommandName="DeActiveRow"
            CommandArgument='<%# Eval("RID") %>'
            CausesValidation="false"
            OnClientClick='<%# "return confirm(\"Are you sure you want to change status of " + Eval("BrandName") + "?\");" %>'>
        </asp:LinkButton>
    </ItemTemplate>
</asp:TemplateField>


                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkManageEdit" runat="server" Text="Edit" CssClass="btn btn-info btn-sm"
                                        CommandName="EditRow" CommandArgument='<%#Eval("RID") %>' CausesValidation="false">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Label ID="lblBrandRecordCount" runat="server" CssClass="mt-2 font-weight-bold d-block"></asp:Label>
            </div>
        </div>
    </div>

    <!-- Confirmation Modal -->
    <div class="modal fade" id="MyPopups" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-success text-white">
                    <h5 class="modal-title">Confirmation</h5>
                    <button type="button" class="close text-white" data-dismiss="modal">
                        <span>&times;</span>
                    </button>
                </div>
                <div class="modal-body text-center">
                    <span id="lblMessages" class="text-success font-weight-bold"></span>
                    <br />
                    <span id="lblName" class="text-primary"></span>
                </div>
                <div class="modal-footer justify-content-center">
                    <button type="button" class="btn btn-success px-4" data-dismiss="modal">Ok</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Error Modal -->
    <div class="modal fade" id="ErrorPage" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger text-white">
                    <h5 class="modal-title">Error</h5>
                    <button type="button" class="close text-white" data-dismiss="modal">
                        <span>&times;</span>
                    </button>
                </div>
                <div class="modal-body text-center">
                    <span id="lblerror" class="text-danger font-weight-bold"></span>
                </div>
                <div class="modal-footer justify-content-center">
                    <button type="button" class="btn btn-danger px-4" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <!-- JS for Bootstrap -->
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.5.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/js/bootstrap.bundle.min.js"></script>

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

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server"></asp:Content>
