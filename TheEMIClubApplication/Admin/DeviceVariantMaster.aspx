<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="DeviceVariantMaster.aspx.cs" Inherits="TheEMIClubApplication.MasterPage.DeviceVariantMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<%--    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            
        </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gvVariants" EventName="RowCommand" />
    </Triggers>
</asp:UpdatePanel>--%>
      
    <div class="hold-transition sidebar-mini">
        <div class="wrapper">
            <!-- Content Wrapper. Contains page content -->
            <div class="main-box-container" style="width: 100%; position: relative; left: 0px; top: 0px; margin: 0px; padding: 25px 0px;">
                <section class="content">
                    <div class="container-fluid">
                        <div class="card card-default">
                            <div class="card-header form-header-bar d-flex justify-content-center position-relative">
                                <!-- Center Title -->
                                <h3 class="card-title mb-0">Device Variant Master</h3>

                                <!-- Right-Aligned Button -->
                                <div class="position-absolute" style="right: 15px; top: 50%; transform: translateY(-50%);">
                                    <asp:Button ID="btnCreateVariant" runat="server" Text="Add Variant" OnClick="btnCreateVariant_Click" CausesValidation="false" visible="false"/>
                                </div>
                            </div>

                            <%--  <div class="card-header form-header-bar">
                                <h3 class="card-title">Device Variant Master</h3> 
                              
                            </div>--%>


                            <div class="card-body" runat="server" id="divSaveVariant" style="display: none">
                                <asp:HiddenField ID="hfRID" runat="server" />
                                <div class="row">
                                    <!-- Brand -->
                                    <div class="col-md-4">
                                        <div class="form-group">

                                            <asp:Label ID="lblBrand" runat="server" Class="font-weight-bold">Brand</asp:Label>

                                            <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged" CausesValidation="false">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblErrorBarnd" runat="server" Text=""></asp:Label>
                                              <asp:RequiredFieldValidator ID="rfvBrand" runat="server" ErrorMessage="* Select Brand"
                     Display="Dynamic" ControlToValidate="ddlBrand" InitialValue="0"   >
                 </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <!-- Model -->
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="lblModel" runat="server" Class="font-weight-bold">Model</asp:Label>

                                            <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-control" CausesValidation="false">
                                            </asp:DropDownList>
                                             <asp:Label ID="lblErrorModel" runat="server" Text=""></asp:Label>
                                             <asp:RequiredFieldValidator ID="rfvModel" runat="server" ErrorMessage="* Select Model"
                     Display="Dynamic" ControlToValidate="ddlModel" InitialValue="0"  >
                 </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <!-- Variant Name -->
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Class="font-weight-bold">Variant Name</asp:Label>

                                            <asp:TextBox ID="txtVariantName" runat="server" CssClass="form-control" placeholder="e.g. 8GB+128GB (5G)" />
                                            <asp:RequiredFieldValidator ID="rfvVariantName" runat="server" ErrorMessage="* Enter Variant Name"
                                                Display="Dynamic" ControlToValidate="txtVariantName" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <!-- Selling Price -->

                                    <div class="col-md-2">
                                        <div class="form-group">

                                            <asp:Label ID="Label2" runat="server" Class="font-weight-bold">MRP Price</asp:Label>
                                            <asp:TextBox ID="txtMRPPrice" runat="server" CssClass="form-control" TextMode="Number" />
                                            <asp:RequiredFieldValidator ID="rfvMRPPrice" runat="server" ErrorMessage="* Enter MRP Price"
                                                Display="Dynamic" ControlToValidate="txtMRPPrice" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" Class="font-weight-bold">Selling Price</asp:Label>

                                            <asp:TextBox ID="txtSellingPrice" runat="server" CssClass="form-control" TextMode="Number" />
                                            <asp:Label ID="lblSellingValidation" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvSellingPrice" runat="server" ErrorMessage="* Enter Selling Price"
                                                Display="Dynamic" ControlToValidate="txtSellingPrice" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <!-- Colors -->
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" Class="font-weight-bold">Available Colors</asp:Label>

                                            <asp:TextBox ID="txtColors" runat="server" CssClass="form-control" placeholder="e.g. black, blue" />
                                            <asp:RequiredFieldValidator ID="rfvColors" runat="server" ErrorMessage="* Enter Device Color"
                                                Display="Dynamic" ControlToValidate="txtColors" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <!-- Remark -->
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" Class="font-weight-bold">Remark</asp:Label>

                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ErrorMessage="* Enter Device Remark"
                                                Display="Dynamic" ControlToValidate="txtRemark" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <!-- Down Payment -->
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label6" runat="server" Class="font-weight-bold">Down Payment %</asp:Label>

                                            <asp:TextBox ID="txtDownPaymentPerc" runat="server" CssClass="form-control" />
                                            <asp:Label ID="lblDownpaymentvalidation" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvDownPaymentPerc" runat="server" ErrorMessage="* Enter DownPayment(%)"
                                                Display="Dynamic" ControlToValidate="txtDownPaymentPerc" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <!-- Allow integer or decimal -->
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                ControlToValidate="txtDownPaymentPerc"
                                                ValidationExpression="^\d+(\.\d+)?$"
                                                ErrorMessage="* Enter valid number (e.g., 1 or 1.5)"
                                                Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <!-- Interest -->
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label7" runat="server" Class="font-weight-bold">Interest %</asp:Label>

                                            <asp:TextBox ID="txtInterestPerc" runat="server" CssClass="form-control" />
                                            <asp:Label ID="lblintrestvaliadtion" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvInterestPerc" runat="server" ErrorMessage="* Enter Interest(%)"
                                                Display="Dynamic" ControlToValidate="txtInterestPerc" ForeColor="Red"></asp:RequiredFieldValidator>

                                            <!-- Allow integer or decimal -->
                                            <asp:RegularExpressionValidator ID="revInterestPerc" runat="server"
                                                ControlToValidate="txtInterestPerc"
                                                ValidationExpression="^\d+(\.\d+)?$"
                                                ErrorMessage="* Enter valid number (e.g., 5 or 5.5)"
                                                Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <!-- Tenure -->
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label8" runat="server" Class="font-weight-bold">Tenure (Months)</asp:Label>

                                            <asp:TextBox ID="txtTenure" runat="server" CssClass="form-control" />
                                             <asp:Label ID="lbltanurevaliadtion" runat="server" ForeColor="Red" Text=""></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvTenure" runat="server" ErrorMessage="* Enter Tenure"
                                                Display="Dynamic" ControlToValidate="txtTenure" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <!-- Processing Fees -->
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label9" runat="server" Class="font-weight-bold">Processing Fees</asp:Label>

                                            <asp:TextBox ID="txtProcessingFees" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvProcessingFees" runat="server" ErrorMessage="* Enter Processing Fees"
                                                Display="Dynamic" ControlToValidate="txtProcessingFees" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <!-- Allow integer or decimal -->
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                ControlToValidate="txtProcessingFees"
                                                ValidationExpression="^\d+(\.\d+)?$"
                                                ErrorMessage="* Enter valid number (e.g., 1 or 1.5)"
                                                Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                    <!-- Active Status -->
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label10" runat="server" Class="font-weight-bold">Status</asp:Label>

                                            <asp:DropDownList ID="ddlActiveStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Active" Value="Active" />
                                                <asp:ListItem Text="Inactive" Value="Inactive" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvddlActiveStatus" runat="server" ErrorMessage="* Select Status"
                                                Display="Dynamic" ControlToValidate="ddlActiveStatus" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <!-- Image Upload -->
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label11" runat="server" Class="font-weight-bold">Upload Image</asp:Label>

                                            <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control" />
                                            <asp:Button ID="btnUploadImage" runat="server" CausesValidation="false" Text="Upload Image"
                                                CssClass="btn btn-sm btn-primary mt-2" OnClick="btnUploadImage_Click" />

                                            <asp:Button ID="btnimgRemove" CausesValidation="false" runat="server" Text="Remove" 
                                                 CssClass="btn btn-sm btn-danger mt-2"
                                                Visible="false" OnClick="btnimgRemove_Click" />
                                            <!-- Required Field Validator -->
                                            <%--<asp:RequiredFieldValidator ID="rfvImage" runat="server"
                                                ControlToValidate="fuImage"
                                                InitialValue=""
                                                ErrorMessage="* Please select an image"
                                                Display="Dynamic"
                                                ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:Label ID="Label12" runat="server" Class="font-weight-bold">Preview</asp:Label>
                                        <br />
                                        <asp:Image ID="imgPreview" ImageUrl="~/Images/Variants/Default/defaultimg.png" runat="server" Width="120" Height="120" CssClass="img-thumbnail" />
                                        <div class="col-md-3 text-right mt-1">

                                            
                                        </div>
                                    </div>

                                    <!-- Buttons -->
                                    <div class="col-md-12 text-center mt-2">
                                               <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success px-4 mr-2" OnClick="btnSave_Click" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary px-4 mr-2" CausesValidation="false" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-secondary px-4 mr-2" CausesValidation="false" OnClick="btnClear_Click" />
                                       <asp:Button ID="Button1" runat="server" Text="Close" CausesValidation="false" CssClass="btn btn-danger" OnClick="Button1_Click"/>

                                    </div>
                                </div>
                            </div>

                            <div class="card-body" runat="server" id="divSeachvariant">
                                <div class="row mt-4">
                                    <!--  Filter -->
                                    <div class="col-md-1">
                                        <asp:Label ID="lblSearchModel" runat="server" CssClass="font-weight-bold">Criteria</asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <asp:DropDownList ID="ddlSearchValues" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                                <asp:ListItem Text="Brand" Value="Brand"></asp:ListItem>
                                                <asp:ListItem Text="Model" Value="Model"></asp:ListItem>
                                                <asp:ListItem Text="Variant" Value="Variant"></asp:ListItem>
                                                <asp:ListItem Text="Color" Value="Color"></asp:ListItem>
                                                <%--<asp:ListItem Text="Price" Value="Price"></asp:ListItem>--%>
                                                <asp:ListItem Text="Status" Value="Status"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <!-- Filter Variant -->
                                    <div class="col-md-1">
                                        <asp:Label ID="lblSearchValues" runat="server" CssClass="font-weight-bold">Values</asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <asp:TextBox ID="txtSearchValues" runat="server" CssClass="form-control" placeholder="Enter Search Values" />
                                        </div>
                                    </div>

                                    <!-- Buttons -->
                                    <div class="col-md-4">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary mr-2" OnClick="btnSearch_Click" CausesValidation="false" />
                                    </div>
                                </div>



                            </div>

                            <!-- Grid -->
                            <div class="col-md-12 mt-4">
                                <div style="overflow-x: auto; white-space: nowrap;">
                                    <span id="spnMessage" runat="server"></span>
                                    <asp:GridView ID="gvVariants" runat="server" 
                                        CssClass="GridStyle table table-bordered table-condensed table-hover" 
                                        AllowPaging="true" PageSize="10"
                                        AutoGenerateColumns="false" DataKeyNames="RID,ImagePath"
                                        OnRowCommand="gvVariants_RowCommand"
                                        OnRowDataBound="gvVariants_RowDataBound"
                                        OnDataBound="gvVariants_DataBound"
                                        OnPageIndexChanging="gvVariants_PageIndexChanging"
                                        PagerSettings-Mode="Numeric"
                                        PagerSettings-Position="Bottom"
                                        PagerStyle-HorizontalAlign="Center"
                                        PagerStyle-CssClass="pagination-container"
                                        UseAccessibleHeader="true"
                                        GridLines="None">
                                        <Columns>

                                            <asp:BoundField DataField="BrandName" HeaderText="Brand" />
                                            <asp:BoundField DataField="ModelName" HeaderText="Model" />
                                            <asp:BoundField DataField="VariantName" HeaderText="Variant" />
                                            <asp:BoundField DataField="SellingPrice" HeaderText="Selling Price"  />
                                            <asp:BoundField DataField="MrpPrice" HeaderText="MRP Price"  />
                                            <asp:BoundField DataField="avlbColors" HeaderText="Color" />
                                            <asp:BoundField DataField="DownPaymentPerc" HeaderText="DP(%)" DataFormatString="{0:C}" />
                                            <asp:BoundField DataField="InterestPerc" HeaderText="Interest(%)" DataFormatString="{0:C}" />
                                            <asp:BoundField DataField="Tenure" HeaderText="Tenure" DataFormatString="{0:C}" />
                                            <asp:BoundField DataField="ProcessingFees" HeaderText="Proc.Fees" DataFormatString="{0:C}" />
                                            <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                            <asp:TemplateField HeaderText="Image">
                                                <ItemTemplate>
                                                    <asp:Image ID="productimage" runat="server" ImageUrl='<%# Eval("ImagePath") %>' Width="60px" Height="60px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ActiveStatus" HeaderText="Status" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="btn btn-info btn-sm"
                                                        CausesValidation="false" CommandName="EditRow" CommandArgument='<%# Eval("RID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                      <%--  <PagerSettings Position="Bottom" />
                                        <PagerStyle CssClass="grid-pagin" />
                                        <PagerTemplate>
                                            <asp:LinkButton ID="lnkPrevious" runat="server" CommandName="Page" CommandArgument="Prev" CausesValidation="false"
                                                CssClass="btn btn-sm btn-outline-primary">Previous</asp:LinkButton>

                                            <asp:LinkButton ID="lnkNext" runat="server" CommandName="Page" CommandArgument="Next" CausesValidation="false"
                                                CssClass="btn btn-sm btn-outline-primary">Next</asp:LinkButton>
                                        </PagerTemplate>--%>
                                    </asp:GridView>
                                    <%--<asp:Label ID="lblRecordCount" runat="server" CssClass="mt-2 font-weight-bold"></asp:Label>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
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
    <style>
        /* Define styles for the zoomed image */
        #zoomedImage {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.8);
            text-align: center;
            z-index: 9999;
        }

            #zoomedImage img {
                max-width: 90%;
                max-height: 90%;
                margin-top: 5%;
            }
    </style>

    <script>
        // jQuery function to handle click event on image
        $(document).ready(function () {
            $(".gridImage").click(function () {
                // Get the source of the clicked image
                var imgSrc = $(this).attr("src");
                // Set the source of the zoomed image
                $("#zoomedImage img").attr("src", imgSrc);
                // Show the zoomed image
                $("#zoomedImage").fadeIn();
            });

            // Hide zoomed image when clicked anywhere outside the zoomed image
            $("#zoomedImage").click(function () {
                $(this).fadeOut();
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
