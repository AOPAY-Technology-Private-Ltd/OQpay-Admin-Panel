<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreateVariantMaster.aspx.cs" Inherits="TheEMIClubApplication.Admin.CreateVariantMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">
    <style>
        .form-group label {
            font-weight: 500;
        }

        label::before, label::after {
            content: none !important;
        }

        .input-group-text {
            background-color: #f8f9fa;
        }
    </style>

    <!-- Main Card -->
    <div class="card shadow-lg border-0" runat="server" id="divSaveVariant">
        <div class="card-header bg-primary text-white">
            <h5 class="mb-0"><i class="fas fa-layer-group mr-2"></i>Create / Manage Variant</h5>
        </div>

        <div class="card-body">
            <asp:HiddenField ID="hfRID" runat="server" />

            <div class="form-row">
                <!-- Brand -->
                <div class="form-group col-md-4">
                    <label class="font-weight-bold">Brand</label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><i class="fas fa-industry text-primary"></i></span>
                        </div>
                        <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged" ValidationGroup="VG_SaveUpdate">
                            <asp:ListItem Text="-- Select Brand --" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvBrand" runat="server"
                        ControlToValidate="ddlBrand" InitialValue="0"
                        ErrorMessage="* Select Brand" Display="Dynamic"
                        ForeColor="Red" ValidationGroup="ProductGroup" />
                </div>

                <!-- Model -->
                <div class="form-group col-md-4">
                    <label class="font-weight-bold">Model</label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><i class="fas fa-mobile-alt text-success"></i></span>
                        </div>
                        <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-control"
                            ValidationGroup="VG_SaveUpdate">
                            <asp:ListItem Text="-- Select Model --" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvModel" runat="server" ErrorMessage="* Select Model"
                        Display="Dynamic" ControlToValidate="ddlModel" InitialValue="0"
                        CssClass="text-danger" ValidationGroup="ProductGroup"></asp:RequiredFieldValidator>
                </div>

            </div>

            <!-- Variant Table -->
            <!-- Variant Table -->
            <asp:ScriptManager ID="ScriptManager1" runat="server" />

            <div class="form-group col-md-12">
                <asp:UpdatePanel ID="updVariants" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <%--  <!-- Validation -->
                <asp:CustomValidator ID="cvSpec" runat="server"
ClientValidationFunction="onValidateProductGroup"
ErrorMessage="* All specification fields must be filled."
Display="Dynamic"
ForeColor="Red"
 ValidationGroup="ProductGroup"/>--%>

                        <asp:CustomValidator ID="cvSpec" runat="server"
                            ClientValidationFunction="onValidateProductGroup"
                            ErrorMessage="* All specification fields must be filled."
                            Display="Dynamic"
                            ForeColor="Red"
                            ValidationGroup="ProductGroup" />

                        <!-- Table -->
                        <asp:Table ID="tblVariants" runat="server" CssClass="table table-bordered table-sm" GridLines="Both">
                            <asp:TableHeaderRow>
                                <asp:TableHeaderCell>RID</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Variant Name</asp:TableHeaderCell>

                                <asp:TableHeaderCell>Available Colors</asp:TableHeaderCell>
                                <asp:TableHeaderCell>MRP Price</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Selling Price</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Down Payment %</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Interest %</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Tenure (Months)</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Processing Fees</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Action</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>

                        <!-- Add Row Button -->
                        <div class="mt-2 text-right">
                            <asp:Button ID="btnAddVariant" runat="server" CssClass="btn btn-info" Text="Add Variant" OnClick="btnAddVariant_Click" />
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAddVariant" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>





            <hr />

            <!-- Additional Info -->
            <div class="form-row">


                <div class="form-group col-md-8">
                    <label class="font-weight-bold">Remark</label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><i class="fas fa-comment-dots text-secondary"></i></span>
                        </div>
                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" placeholder="Additional notes..." />
                    </div>
                </div>

                <div class="form-group col-md-4">
                    <label class="font-weight-bold">Status</label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><i class="fas fa-toggle-on text-primary"></i></span>
                        </div>
                        <asp:DropDownList ID="ddlActiveStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Active" Value="Active" />
                            <asp:ListItem Text="Inactive" Value="Inactive" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <!-- Image Upload -->
            <div class="form-row">
                <div class="form-group col-md-3">
                    <label class="font-weight-bold">Upload Image</label>
                    <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control-file" ValidationGroup="VG_SaveUpdate" />
                    <div class="mt-2">
                        <asp:Button ID="btnUploadImage" runat="server" Text="Upload" CssClass="btn btn-sm btn-primary mr-2" OnClick="btnUploadImage_Click" ValidationGroup="VG_SaveUpdate" />
                        <asp:Button ID="btnimgRemove" runat="server" Text="Remove" CssClass="btn btn-sm btn-danger" Visible="false" OnClick="btnimgRemove_Click" ValidationGroup="VG_SaveUpdate" />
                    </div>
                </div>

                <!-- Preview -->
                <div class="form-group col-md-5">
                    <label class="font-weight-bold">Preview</label>
                    <div>
                        <asp:Image ID="imgPreview" runat="server" ImageUrl="~/Images/Variants/Default/defaultimg.png"
                            Width="120" Height="120" CssClass="img-thumbnail shadow-sm" />
                    </div>
                </div>
            </div>


                    <!-- Action Buttons -->
        <div class="text-center mt-3">
            <asp:HiddenField ID="hdfgroupid" runat="server" />
            <asp:Button ID="btnSave" runat="server" Text="Save"
                CssClass="btn btn-success px-4 mr-2"
                OnClick="btnSave_Click"
                ValidationGroup="ProductGroup"
                CausesValidation="true" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary px-4 mr-2" Visible="false" OnClick="btnUpdate_Click" ValidationGroup="VG_SaveUpdate" />
            <asp:Button ID="btnClear" runat="server" Text="Close" CssClass="btn btn-secondary px-4" OnClick="btnClear_Click" />
        </div>
        </div>


    </div>

    <!-- Error Modal -->
    <div id="ErrorPage" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Error Message</h5>
                </div>
                <div class="modal-body"><span id="lblerror" style="color: red; font-weight: bold"></span></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button></div>
            </div>
        </div>
    </div>

    <!-- Confirmation Modal -->
    <div id="MyPopups" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header bg-primary">
                    <h5 class="modal-title text-white">Confirmation Messages</h5>
                </div>
                <div class="modal-body">
                    <span id="lblMessages" style="font-size: medium; color: forestgreen"></span>
                    <br />
                    <span id="lblName" style="color: navy"></span>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-success" id="btnsucess">Ok</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Scripts -->
    <script type="text/javascript">
        function ShowPopup(username, messages) {
            $("#MyPopups #lblName").html(username);
            $("#MyPopups #lblMessages").html(messages);
            $("#MyPopups").modal("show");
            $("#btnsucess").off("click").on("click", function () { $("#MyPopups").modal("hide"); });
        }
        function ShowPopupAfterSave(username, messages) {
            $("#MyPopups #lblName").html(username);
            $("#MyPopups #lblMessages").html(messages);
            $("#MyPopups").modal("show");
            $("#btnsucess").off("click").on("click", function () {
                window.location.href = "ManageVariant.aspx?msg=VariantSaved";
            });
        }
        function ShowError(ErrorMessages) {
            $("#ErrorPage #lblerror").html(ErrorMessages);
            $("#ErrorPage").modal("show");
        }
    </script>

    <script>
        function validateSpecs() {
            let isValid = true;
            const rows = document.querySelectorAll("#<%= tblVariants.ClientID %> tr");
            validateSpecs.alertShown = false; // reset alert flag before loop

            for (let i = 1; i < rows.length; i++) { // Skip header row
                const inputs = rows[i].querySelectorAll("input[type='text']");
                if (inputs.length < 9) continue; // skip invalid rows

                const Rid = inputs[0];
                const variant = inputs[1];
                const color = inputs[2];
                const mrp = inputs[3];
                const Selling = inputs[4];
                const Downpayment = inputs[5];
                const intrest = inputs[6];
                const tenure = inputs[7];
                const processfee = inputs[8];

                // ✅ Validate required fields
                [Rid, variant, color, mrp, Selling, Downpayment, intrest, tenure, processfee].forEach(input => {
                    if (!input.value.trim()) {
                        input.classList.add("is-invalid");
                        isValid = false;
                    } else {
                        input.classList.remove("is-invalid");
                    }
                });

                // ✅ Validate numeric/decimal fields
                [mrp, Selling, Downpayment, intrest, tenure, processfee].forEach(input => {
                    const value = input.value.trim();
                    if (value && !/^\d+(\.\d{1,2})?$/.test(value)) {
                        input.classList.add("is-invalid");
                        isValid = false;

                        if (!validateSpecs.alertShown) {
                            alert("Please enter only numeric or decimal values (up to 2 decimals).");
                            validateSpecs.alertShown = true;
                        }
                    } else {
                        input.classList.remove("is-invalid");
                    }
                });

                // ✅ Validate Selling Price ≤ MRP
                const mrpValue = parseFloat(mrp.value) || 0;
                const sellingValue = parseFloat(Selling.value) || 0;

                if (sellingValue > mrpValue) {
                    Selling.classList.add("is-invalid");
                    mrp.classList.add("is-invalid");
                    isValid = false;

                    if (!validateSpecs.alertShown) {
                        alert("Selling Price cannot be greater than MRP.");
                        validateSpecs.alertShown = true;
                    }
                } else {
                    Selling.classList.remove("is-invalid");
                    mrp.classList.remove("is-invalid");
                }

                // ✅ Downpayment and Interest between 0–100
                const downValue = parseFloat(Downpayment.value) || 0;
                const intrestValue = parseFloat(intrest.value) || 0;
                if (downValue <= 0 || downValue > 100) {
                    Downpayment.classList.add("is-invalid");
                    isValid = false;

                    if (!validateSpecs.alertShown) {
                        alert("Downpayment must be greater than 0% and not exceed 100%.");
                        validateSpecs.alertShown = true;
                    }
                } else {
                    Downpayment.classList.remove("is-invalid");
                }

                if (intrestValue <= 0 || intrestValue > 100) {
                    intrest.classList.add("is-invalid");
                    isValid = false;

                    if (!validateSpecs.alertShown) {
                        alert("Interest must be greater than 0% and not exceed 100%.");
                        validateSpecs.alertShown = true;
                    }
                } else {
                    intrest.classList.remove("is-invalid");
                }

                // ✅ Tenure between 1–120 months
                const tenureValue = parseInt(tenure.value) || 0;
                if (tenureValue < 1 || tenureValue > 120) {
                    tenure.classList.add("is-invalid");
                    isValid = false;

                    if (!validateSpecs.alertShown) {
                        alert("Tenure must be between 1 and 120 months.");
                        validateSpecs.alertShown = true;
                    }
                } else {
                    tenure.classList.remove("is-invalid");
                }
            }

            return isValid;
        }

        function onValidateProductGroup(sender, args) {
            args.IsValid = validateSpecs();
        }

        // ✅ Real-time numeric restriction
        document.addEventListener("input", function (e) {
            if (e.target.matches("#<%= tblVariants.ClientID %> input[type='text']")) {
            const index = Array.from(e.target.parentNode.parentNode.querySelectorAll("input[type='text']")).indexOf(e.target);
            // Restrict numeric fields only
            if ([3, 4, 5, 6, 7, 8].includes(index)) {
                e.target.value = e.target.value.replace(/[^0-9.]/g, "");
            }
        }
    });
    </script>

    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/4.6.2/js/bootstrap.bundle.min.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
