<%@ Page Title="Loan EMI With Location"
    Language="C#"
    MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="LoanEmiDueswithlocationReport.aspx.cs"
    Inherits="TheEMIClubApplication.Reports.LoanEmiDueswithlocationReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">

    <!-- ================= CSS & JS ================= -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons/font/bootstrap-icons.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>

    <!-- Leaflet MAP -->
    <link rel="stylesheet"
          href="https://unpkg.com/leaflet@1.9.4/dist/leaflet.css" />
    <script src="https://unpkg.com/leaflet@1.9.4/dist/leaflet.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <style>
        .filter-card {
            border-radius: 14px;
            box-shadow: 0 10px 25px rgba(0,0,0,.08);
        }
        .table-modern th {
            background: #f8fafc;
            font-weight: 600;
            white-space: nowrap;
        }
        .table-modern td {
            white-space: nowrap;
            vertical-align: middle;
        }
    </style>

    <div class="container-fluid px-4 py-4">

        <!-- ================= HEADER ================= -->
        <h4 class="fw-bold mb-3">
            <i class="bi bi-geo-alt-fill text-primary me-2"></i>
            Loan EMI With Location Report
        </h4>

        <!-- ================= FILTER ================= -->
        <div class="card filter-card mb-4">
            <div class="card-header bg-primary text-white">
                <i class="bi bi-funnel-fill me-2"></i> Filter
            </div>
            <div class="card-body">
                <div class="row g-3">

                    <div class="col-md-4">
                        <label class="form-label fw-semibold">Retailer Code</label>
                        <asp:DropDownList ID="ddlRetailer"
                            runat="server"
                            CssClass="form-control form-control-lg">
                            <asp:ListItem Text="Select Retailer" Value="" />
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label fw-semibold">Report Type</label>
                        <asp:DropDownList ID="ddlReportType"
                            runat="server"
                            CssClass="form-control form-control-lg">
                            <asp:ListItem Text="All" Value="All" />
                                      <asp:ListItem Text="Today Due" Value="TodayDue" />
                                      <asp:ListItem Text="OverDue" Value="OverDue" />
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4 d-flex align-items-end">
                        <asp:Button ID="btnSearch"
                            runat="server"
                            Text="Search"
                            CssClass="btn btn-primary btn-lg w-100"
                            OnClick="btnSearch_Click" />
                    </div>

                </div>
            </div>
        </div>

        <!-- ================= GRID ================= -->
        <div class="card shadow-sm">
            <div class="card-header bg-dark text-white">
                <i class="bi bi-table me-2"></i> Loan Records
            </div>

            <div class="table-responsive">
                <asp:GridView ID="gvLoanEmiReport"
                    runat="server"
                    CssClass="table table-bordered table-hover table-modern mb-0"
                    AutoGenerateColumns="False"
                    DataKeyNames="loanCode"
                    OnRowCommand="gvLoanEmiReport_RowCommand"
                    EmptyDataText="No records found">

                    <Columns>

                     
                        <asp:TemplateField HeaderText="Map">
                            <ItemTemplate>
                                <asp:LinkButton runat="server"
                                    CssClass="btn btn-sm btn-outline-primary"
                                    CommandName="ViewMap"
                                    CommandArgument='<%# Eval("loanCode") %>'>
                                    <i class="bi bi-geo-alt-fill"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField HeaderText="Retailer"
                            DataField="centerName" />

                        <asp:BoundField HeaderText="Loan Code"
                            DataField="loanCode" />

                        <asp:BoundField HeaderText="Customer"
                            DataField="customerName" />

                        <asp:BoundField HeaderText="Mobile"
                            DataField="primaryMobileNumber" />

                        <asp:BoundField HeaderText="Loan Amount"
                            DataField="loanAmount"
                            DataFormatString="{0:N2}" />

                        <asp:BoundField HeaderText="EMI Amount"
                            DataField="emiAmount"
                            DataFormatString="{0:N2}" />

                        <asp:BoundField HeaderText="Paid EMI"
                            DataField="paidEMI" />

                        <asp:BoundField HeaderText="Due EMI"
                            DataField="dueEMI" />

                        <asp:BoundField HeaderText="Next Due Date"
                            DataField="dueDate"
                            DataFormatString="{0:dd-MMM-yyyy}" />

                        <asp:BoundField HeaderText="Status"
                            DataField="loanStatus" />

                    </Columns>

                </asp:GridView>
            </div>
        </div>

    </div>

    <!-- ================= MAP MODAL ================= -->
<!-- ================= MAP MODAL ================= -->
<div class="modal fade" id="mapModal" tabindex="-1" aria-hidden="true">
    <div class="modal-dialog modal-lg modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content">

            <!-- HEADER -->
            <div class="modal-header bg-primary text-white">
                <h5 class="modal-title">
                    <i class="bi bi-geo-alt-fill me-2"></i>
                    Customer Location
                </h5>
                <button type="button"
                    class="btn-close btn-close-white"
                    data-bs-dismiss="modal"></button>
            </div>

            <!-- BODY -->
            <div class="modal-body">

                <!-- CUSTOMER INFO -->
                <div class="row align-items-center mb-3">

                    <!-- PHOTO -->
                    <div class="col-md-3 text-center">
                        <asp:Image ID="imgCustomerPhoto"
                            runat="server"
                            CssClass="img-thumbnail shadow-sm"
                            Style="width:120px;height:120px;object-fit:cover;border-radius:50%;"
                            ImageUrl="~/assets/no-photo.png" />
                    </div>

                    <!-- DETAILS -->
                    <div class="col-md-9">
                        <div class="row">
                            <div class="col-md-6 mb-1">
                                <strong>Loan Code:</strong><br />
                                <asp:Label ID="lblLoanCode"
                                    runat="server"
                                    CssClass="fw-semibold text-primary" />
                            </div>
                            <div class="col-md-6 mb-1">
                                <strong>Customer Name:</strong><br />
                                <asp:Label ID="lblCustomerName"
                                    runat="server"
                                    CssClass="fw-semibold" />
                            </div>
                            <div class="col-md-6 mt-1">
                                <strong>Last Location Date:</strong><br />
                                <asp:Label ID="lblLastLocationDate"
                                    runat="server"
                                    CssClass="text-muted fw-semibold" />
                            </div>

                                           <div class="col-md-6 mt-1">
                   <strong>Last IP Address :</strong><br />
                   <asp:Label ID="lblipaddress"
                       runat="server"
                       CssClass="text-muted fw-semibold" />
               </div>
                        </div>
                    </div>

                </div>

                <!-- MAP -->
                <div id="map"
                    style="height:350px;border-radius:12px;"></div>

            </div>

            <!-- FOOTER -->
            <div class="modal-footer">
                <button type="button"
                    class="btn btn-secondary"
                    data-bs-dismiss="modal">
                    Close
                </button>
            </div>

        </div>
    </div>
</div>


    <!-- ================= MAP SCRIPT ================= -->
<script>
    var mapInstance;

    function showMap(lat, lng) {

        setTimeout(function () {

            // Remove old map (important when reopening modal)
            if (mapInstance) {
                mapInstance.remove();
            }

            mapInstance = L.map('map').setView([lat, lng], 14);

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '© OpenStreetMap contributors'
            }).addTo(mapInstance);

            L.marker([lat, lng])
                .addTo(mapInstance)
                .bindPopup("Customer Location")
                .openPopup();

        }, 300);

        var modal = new bootstrap.Modal(
            document.getElementById('mapModal'));
        modal.show();
    }
</script>

</asp:Content>
