<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="ManageCustomer.aspx.cs" Inherits="TheEMIClubApplication.Admin.ManageCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <style>
        .form-group label {
            font-weight: 500;
        }

        label::before, label::after {
            content: none !important;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <!-- Customer Details Card -->
    <div id="divcustomer" runat="server" class="card shadow-sm my-3">
        <div class="card-header bg-primary text-white">
            <h4 class="mb-0">Customer Details</h4>
        </div>

        <div class="card-body">

            <!-- Search Filters -->
            <div class="row mb-3">
                <div class="col-md-3 mb-2">
                    <label class="font-weight-bold">Criteria</label>
                    <asp:DropDownList ID="ddlCriteria" runat="server" CssClass="form-control">
                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Name"></asp:ListItem>
                        <asp:ListItem Text="MobileNo"></asp:ListItem>
                        <asp:ListItem Text="EmailId"></asp:ListItem>
                        <asp:ListItem Text="AadharNo"></asp:ListItem>
                        <asp:ListItem Text="PanNo"></asp:ListItem>
                        <asp:ListItem Text="AccountNo"></asp:ListItem>
                        <asp:ListItem Text="IMEINo 1"></asp:ListItem>
                        <asp:ListItem Text="Active" Value="Active"></asp:ListItem>
                        <asp:ListItem Text="Inactive" Value="Inactive"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-3 mb-2">
                    <label class="font-weight-bold">Value</label>
                    <asp:TextBox ID="txtValues" runat="server" CssClass="form-control" Placeholder="Enter Value"></asp:TextBox>
                </div>

                <div class="col-md-6 d-flex align-items-end mb-2">
                    <asp:Button ID="btnCustSearch" runat="server" Text="Search" CssClass="btn btn-success ml-auto" OnClick="btnCustSearch_Click" />
                </div>
            </div>

            <!-- Customer Grid -->
            <div class="table-responsive">
               <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">--%>
                    <ContentTemplate>
                        <asp:GridView ID="gvDevices" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-bordered table-hover table-striped"
                            Width="100%" AllowPaging="true" PageSize="15"
                            OnRowCommand="gvDevices_RowCommand" OnDataBound="gvDevices_DataBound"
                            OnPageIndexChanging="gvDevices_PageIndexChanging"
                            DataKeyNames="RID" PagerSettings-Mode="Numeric"
                            PagerSettings-Position="Bottom"
                            PagerStyle-HorizontalAlign="Center"
                            PagerStyle-CssClass="pagination justify-content-center"
                            UseAccessibleHeader="true"
                            GridLines="None">

                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="S.No" />
                                <asp:BoundField DataField="CustomerCode" HeaderText="UserId" />
                                <asp:BoundField DataField="FirstName" HeaderText="Name" />
                                <asp:BoundField DataField="PrimaryMobileNumber" HeaderText="Mobile" />
                                <asp:BoundField DataField="EMailID" HeaderText="Email" />
                                <asp:BoundField DataField="BrandName" HeaderText="Brand" />
                                <asp:BoundField DataField="ModelName" HeaderText="Model" />
                                <asp:BoundField DataField="ModelVariant" HeaderText="Variant" />
                                <asp:BoundField DataField="IMEINumber1" HeaderText="IMEI 1" />
                                <asp:BoundField DataField="AadharNumber" HeaderText="Aadhar" />
                                <asp:BoundField DataField="PANNumber" HeaderText="Pan" />
                                <asp:BoundField DataField="BankName" HeaderText="Bank" />
                                <asp:BoundField DataField="AccountNumber" HeaderText="A/c No" />
                                <asp:BoundField DataField="BankIFSCCode" HeaderText="IFSC" />
                                <asp:BoundField DataField="ActiveStatus" HeaderText="Status" />
                                <asp:BoundField DataField="CibilScore" HeaderText="Cibil Score" />
                                <asp:TemplateField HeaderText="Cibil Report ">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdownload" runat="server" Text="Download" CssClass="btn btn-sm btn-primary"
                                            CommandName="DownloadRow" CommandArgument='<%# Eval("RID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="View" CssClass="btn btn-sm btn-primary"
                                            CommandName="EditRow" CommandArgument='<%# Eval("RID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>


                            <%--  <PagerStyle CssClass="pagination justify-content-center mt-2" Mode="Numeric" />--%>
                        </asp:GridView>

                        <asp:Label ID="lblCustomerNoData" runat="server" CssClass="text-danger font-weight-bold mt-2 d-block"></asp:Label>

                        
                        <Triggers>
  <asp:AsyncPostBackTrigger ControlID="lnkdownload" />
</Triggers>
                    </ContentTemplate>
               <%-- </asp:UpdatePanel>--%>
            </div>

        </div>
    </div>



    <!-- Image Zoom Modal (single modal used for all images) -->
<div class="modal fade" id="imageZoomModal" tabindex="-1" role="dialog" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
    <div class="modal-content">
      <div class="modal-body text-center p-0">
        <img id="zoomedImage" src="#" alt="Zoom" style="max-width:100%; max-height:80vh; display:block; margin:0 auto;" />
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>

<!-- Generic Error / Message Modal (re-usable) -->
<div id="ErrorPage" class="modal fade" role="dialog">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Message</h5>
      </div>
      <div class="modal-body">
        <!-- changed to server-side Label so you can set it from code-behind, client id remains 'lblerror' -->
        <asp:Label ID="lblerror" runat="server" ClientIDMode="Static" CssClass="text-danger"></asp:Label>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>

<!-- jQuery and Bootstrap JS -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/js/bootstrap.bundle.min.js"></script>

<script type="text/javascript">
    $(document).ready(function () {
        // open image in modal when clicked
        $('.gridImage').on('click', function () {
            var src = $(this).attr('src') || $(this).prop('src');
            if (!src) return;
            $('#zoomedImage').attr('src', src);
            $('#imageZoomModal').modal('show');
        });
    });

    // function to show message from server-side
    function ShowError(msg) {
        $('#lblerror').text(msg);
        $('#ErrorPage').modal('show');
    }
</script>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
