<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="LoanApproval.aspx.cs" Inherits="TheEMIClubApplication.MasterPage.LoanApproval" %>

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
                white-space: nowrap; /* ✅ keep data in one line */
                overflow: hidden; /* hide overflow text */
                text-overflow: ellipsis; /* show ... if too long */
                vertical-align: middle; /* better alignment */
                padding: 6px 8px;
            }

            .table tbody tr:hover {
                background-color: #f1f5ff;
            }

        .badge {
            padding: 0.35em 0.5em;
            font-size: 0.9rem;
            border-radius: 6px;
        }

        /* ✅ Responsive scroll on smaller screens */
        .table-container {
            overflow-x: auto;
        }

        .status-column.Pending {
            background-color: red;
            color: white;
        }

        .status-column.Approved {
            background-color: green;
            color: white;
        }
    </style>

    <style>
        /* === Zoom Overlay === */
        #zoomedImage {
            display: none;
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.85);
            text-align: center;
            z-index: 9999;
            cursor: zoom-out;
        }

            #zoomedImage img {
                max-width: 90%;
                max-height: 90%;
                margin-top: 5%;
                border: 4px solid #fff;
                border-radius: 10px;
                box-shadow: 0 0 20px rgba(255, 255, 255, 0.2);
                transition: transform 0.3s ease-in-out;
            }

                #zoomedImage img:hover {
                    transform: scale(1.02);
                }

        /* Add hover style to thumbnails */
        .zoomable {
            cursor: zoom-in;
            transition: transform 0.2s ease-in-out;
        }

            .zoomable:hover {
                transform: scale(1.05);
            }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="content">
        <div class="container-fluid">
            <div class="card card-default">


                <section class="content">

                    <div runat="server" id="Div_LaonApproval" visible="false">
                        <div class="card-header form-header-bar">
                            <h3 class="card-title">Loan Approval</h3>
                        </div>
                        <div class="card-body">
                            <asp:HiddenField ID="hfLoanRID" runat="server" />
                            <asp:HiddenField ID="hfEMIRID" runat="server" />
                            <asp:HiddenField ID="hfPaymentRID" runat="server" />

                            <!-- Dealer Info -->
                            <div class="row">
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label15" runat="server" CssClass="font-weight-bold" Text="Dealer Id"></asp:Label>
                                    <%-- <label>Customer Code</label>--%>
                                    <asp:TextBox ID="txtDealerid" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label16" runat="server" CssClass="font-weight-bold" Text="Dealer Full Name"></asp:Label>
                                    <%--<label>First Name</label>--%>
                                    <asp:TextBox ID="txtDealerFullName" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label21" runat="server" CssClass="font-weight-bold" Text="Dealer Mobile No"></asp:Label>
                                    <%-- <label>Middle Name</label>--%>
                                    <asp:TextBox ID="txtDealerMobileNo" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label22" runat="server" CssClass="font-weight-bold" Text="Dealer Emailid"></asp:Label>
                                    <%--<label>Last Name</label>--%>
                                    <asp:TextBox ID="txtDealerEmailid" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                            </div>
                            <!-- Customer Info -->
                            <div class="row">
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Lable1" runat="server" CssClass="font-weight-bold" Text="Customer Code"></asp:Label>
                                    <%-- <label>Customer Code</label>--%>
                                    <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label1" runat="server" CssClass="font-weight-bold" Text="Customer Full Name"></asp:Label>
                                    <%--<label>First Name</label>--%>
                                    <asp:TextBox ID="txtCustomerFullName" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label2" runat="server" CssClass="font-weight-bold" Text="Customer Mobile No"></asp:Label>
                                    <%-- <label>Middle Name</label>--%>
                                    <asp:TextBox ID="txtCustomerMobileNo" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label3" runat="server" CssClass="font-weight-bold" Text="Customer Emailid"></asp:Label>
                                    <%--<label>Last Name</label>--%>
                                    <asp:TextBox ID="txtCustomerEmailid" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                            </div>


                            <!-- Loan Info -->
                            <div class="row">
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label4" runat="server" CssClass="font-weight-bold" Text="Loan Code"></asp:Label>
                                    <%--<label>Loan Code</label>--%>
                                    <asp:TextBox ID="txtLoanCode" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label5" runat="server" CssClass="font-weight-bold" Text="Loan Amount"></asp:Label>
                                    <%--<label>Loan Amount</label>--%>
                                    <asp:TextBox ID="txtLoanAmount" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label6" runat="server" CssClass="font-weight-bold" Text="Down Payment"></asp:Label>
                                    <%--<label>Down Payment</label>--%>
                                    <asp:TextBox ID="txtDownPayment" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label7" runat="server" CssClass="font-weight-bold" Text="Loan EMI Amount"></asp:Label>
                                    <%--<label>Loan EMI Amount</label>--%>
                                    <asp:TextBox ID="txtLoanEMIAmount" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label8" runat="server" CssClass="font-weight-bold" Text="Tenure"></asp:Label>
                                    <%--<label>Tenure</label>--%>
                                    <asp:TextBox ID="txtTenure" runat="server" CssClass="form-control" TextMode="Number" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label9" runat="server" CssClass="font-weight-bold" Text="Interest Rate (%)"></asp:Label>
                                    <%-- <label>Interest Rate (%)</label>--%>
                                    <asp:TextBox ID="txtInterestRate" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label14" runat="server" CssClass="font-weight-bold" Text="Start Date"></asp:Label>
                                    <%--<label>Start Date</label>--%>
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label13" runat="server" CssClass="font-weight-bold" Text="End Date"></asp:Label>
                                    <%-- <label>End Date</label>--%>
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="lblCreditScore" runat="server" CssClass="font-weight-bold" Text="Credit Score"></asp:Label>
                                    <asp:TextBox ID="txtCreditScore" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label11" runat="server" CssClass="font-weight-bold" Text="Loan Status"></asp:Label>
                                    <%--<label>Loan Status</label>--%>
                                    <asp:TextBox ID="txtLoanStatus" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:HiddenField ID="hdnReserveValue" runat="server" />
                                    <asp:Label ID="Label12" runat="server" CssClass="font-weight-bold" Text="Loan Created By"></asp:Label>
                                    <%--<label>Loan Created By</label>--%>
                                    <asp:TextBox ID="txtLoanCreatedBy" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                            </div>



                            <!-- Product Info -->

                            <div class="row">

                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label17" runat="server" CssClass="font-weight-bold" Text="Brand Name"></asp:Label>
                                    <%--  <label>IMEI Number</label>--%>
                                    <asp:TextBox ID="txtBrandName" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label18" runat="server" CssClass="font-weight-bold" Text="Model Name"></asp:Label>
                                    <%--  <label>IMEI Number</label>--%>
                                    <asp:TextBox ID="txtModelName" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label19" runat="server" CssClass="font-weight-bold" Text="Model Variant"></asp:Label>
                                    <%--  <label>IMEI Number</label>--%>
                                    <asp:TextBox ID="txtModelVariant" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label10" runat="server" CssClass="font-weight-bold" Text="IMEI  Number"></asp:Label>
                                    <%--  <label>IMEI Number</label>--%>
                                    <asp:TextBox ID="txtIMEI1Number" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3 form-group">
                                    <div class="image-hover-wrapper">

                                        <%--<label>Customer Photo</label>--%>

                                        <asp:Label ID="Label43" runat="server" CssClass="font-weight-bold">Customer Photo</asp:Label>
                                        <asp:Image ID="imgCustPhoto" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                            CssClass="img-thumbnail mt-2 d-block zoomable" AlternateText="Customer Photo" />

                                    </div>
                                    <div class="mt-2">
                                        <asp:LinkButton ID="btnDownloadCustPhoto" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadCustPhoto_Click" />
                                        <asp:LinkButton ID="btnRemoveCustPhoto" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemoveCustPhoto_Click" />
                                    </div>
                                    <asp:Label ID="lblCustPhotoError" runat="server" Text=""></asp:Label>
                                    <asp:HiddenField ID="HiddenField4" runat="server" />
                                </div>

                                <div class="col-md-3 form-group">
                                    <div class="image-hover-wrapper">
                                        <asp:Label ID="Label44" runat="server" CssClass="font-weight-bold">Aadhar Front</asp:Label>
                                        <%--<label>Aadhar Front </label>--%>
                                        <asp:Image ID="imgAadharfrontphoto" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                            CssClass="img-thumbnail mt-2 d-block zoomable" />

                                    </div>
                                    <div class="mt-2">
                                        <asp:LinkButton ID="btnDownloadAadharfrontphoto" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadAadharfrontphoto_Click" />
                                        <asp:LinkButton ID="btnRemoveAadharfrontphoto" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemoveAadharfrontphoto_Click" />
                                    </div>
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                </div>
                                <div class="col-md-3 form-group">
                                    <div class="image-hover-wrapper">
                                        <asp:Label ID="Label45" runat="server" CssClass="font-weight-bold">Aadhar Back</asp:Label>
                                        <%-- <label>Aadhar Back </label>--%>
                                        <asp:Image ID="imgAadharbackphoto" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                            CssClass="img-thumbnail mt-2 d-block zoomable" />

                                    </div>
                                    <div class="mt-2">
                                        <asp:LinkButton ID="btnDownloadAadharbackphoto" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadAadharbackphoto_Click" />
                                        <asp:LinkButton ID="btnRemoveAadharbackphoto" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemoveAadharbackphoto_Click" />
                                    </div>
                                    <asp:HiddenField ID="HiddenField3" runat="server" />
                                </div>

                                <div class="col-md-3 form-group">
                                    <div class="image-hover-wrapper">
                                        <asp:Label ID="Label50" runat="server" CssClass="font-weight-bold">Customer Pan</asp:Label>
                                        <%-- <label>Invoice</label>--%>
                                        <asp:Image ID="imgPan" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                            CssClass="img-thumbnail mt-2 d-block zoomable" />
                                        <%--<div class="zoom-center">
                                            <img runat="server" id="Panimgzoom" class="zoomed-image" />
                                        </div>--%>
                                    </div>

                                    <div class="mt-2">
                                        <asp:LinkButton ID="btnDownloadPan" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadPan_Click" />
                                        <asp:LinkButton ID="btnRemovePan" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemovePan_Click" />
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-3 form-group">


                                    <div class="image-hover-wrapper">
                                        <asp:Label ID="Label46" runat="server" CssClass="font-weight-bold">IMEI 1 Seal</asp:Label>
                                        <%--<label>IMEI 1 Seal </label>--%>
                                        <asp:Image ID="imgIMEI1" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                            CssClass="img-thumbnail mt-2 d-block zoomable" />
                                        <%--  <div class="zoom-center">
                                            <img runat="server" id="IMEI1imgzoom" class="zoomed-image" />
                                        </div>--%>
                                    </div>
                                    <div class="mt-2">
                                        <asp:LinkButton ID="btnDownloadIMEI1" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadIMEI1_Click" />
                                        <asp:LinkButton ID="btnRemoveIMEI1" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemoveIMEI1_Click" />
                                    </div>
                                </div>
                                <div class="col-md-3 form-group">


                                    <div class="image-hover-wrapper">
                                        <asp:Label ID="Label47" runat="server" CssClass="font-weight-bold">IMEI 2 Seal</asp:Label>
                                        <%--<label>IMEI 2 Seal </label>--%>
                                        <asp:Image ID="imgIMEI2" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                            CssClass="img-thumbnail mt-2 d-block zoomable" />
                                        <%-- <div class="zoom-center">
                                            <img runat="server" id="IMEI2imgzoom" class="zoomed-image" />
                                        </div>--%>
                                    </div>
                                    <div class="mt-2">
                                        <asp:LinkButton ID="btnDownloadIMEI2" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadIMEI2_Click" />
                                        <asp:LinkButton ID="btnRemoveIMEI2" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemoveIMEI2_Click" />
                                    </div>
                                </div>
                                <div class="col-md-3 form-group">

                                    <div class="image-hover-wrapper">
                                        <asp:Label ID="Label48" runat="server" CssClass="font-weight-bold">IMEI Photo</asp:Label>
                                        <%--<label>IMEI Photo</label>--%>
                                        <asp:Image ID="imgIMEI" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                            CssClass="img-thumbnail mt-2 d-block zoomable" />
                                        <%-- <div class="zoom-center">
                                            <img runat="server" id="IMEIimgzoom" class="zoomed-image" />
                                        </div>--%>
                                    </div>

                                    <div class="mt-2">
                                        <asp:LinkButton ID="btnDownloadIMEI" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadIMEI_Click" />
                                        <asp:LinkButton ID="btnRemoveIMEI" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemoveIMEI_Click" />
                                    </div>
                                </div>
                                <div class="col-md-3 form-group">
                                    <div class="image-hover-wrapper">
                                        <asp:Label ID="Label49" runat="server" CssClass="font-weight-bold">Invoice</asp:Label>
                                        <%-- <label>Invoice</label>--%>
                                        <asp:Image ID="imgInvoice" ImageUrl="../Images/image icon.png" runat="server" Width="100px"
                                            CssClass="img-thumbnail mt-2 d-block zoomable" />
                                        <%--<div class="zoom-center">
                                            <img runat="server" id="Invoiceimgzoom" class="zoomed-image" />
                                        </div>--%>
                                    </div>

                                    <div class="mt-2">
                                        <asp:LinkButton ID="btnDownloadInvoice" runat="server" Text="Download" CssClass="btn btn-sm btn-primary me-2" OnClick="btnDownloadInvoice_Click" />
                                        <asp:LinkButton ID="btnRemoveInvoice" runat="server" Visible="false" Text="Remove" CssClass="btn btn-sm btn-danger" OnClick="btnRemoveInvoice_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <%-- <div class="col-md-3 form-group">
                                    <asp:Label ID="Label23" runat="server" CssClass="font-weight-bold" Text="IMEI 2 Number"></asp:Label>
                                    <asp:TextBox ID="txtIMEI2Number" runat="server" CssClass="form-control" ReadOnly="true" />
                                </div>--%>
                                <div class="col-md-3 form-group">
                                    <asp:Label ID="Label24" runat="server" CssClass="font-weight-bold" Text="Loan Approval"></asp:Label>
                                    <%--  <label>IMEI Number</label>--%>
                                    <asp:DropDownList ID="ddlLoanApproval" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                                        <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                                        <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-9 form-group">
                                    <asp:Label ID="Label20" runat="server" CssClass="font-weight-bold" AssociatedControlID="txtremarks">
        Remarks <span style="color:red">*</span>
                                    </asp:Label>
                                    <asp:TextBox ID="txtremarks" runat="server" CssClass="form-control" TextMode="MultiLine" />
                                    <asp:RequiredFieldValidator
                                        ID="rfvRemarks"
                                        runat="server"
                                        ControlToValidate="txtremarks"
                                        ErrorMessage="Remarks are required."
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="remarksvalidation" />
                                </div>

                            </div>




                            <!-- Buttons -->
                            <div class="row mt-4">
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="btn btn-success" OnClick="btnSave_Click" ValidationGroup="remarksvalidation" />
                                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger" OnClick="btnClose_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div runat="server" id="Div_LoanSearch">
                        <div class="card-header form-header-bar" id="DivEMIDetails">
                            <h3 class="card-title mb-0" style="color: black;">Loan Approval</h3>
                        </div>
                        <div class="card-body">
                            <div class="col-md-12 card-body-box m-2">
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <span class="font-weight-bold">Mode</span>
                                            <asp:DropDownList ID="ddlMode" runat="server" CssClass="form-control">
                                                <asp:ListItem id="lstitm_all" Text="All" Value=""></asp:ListItem>
                                                <asp:ListItem id="lstitm_Online" Text="Online Mode" Value="ONLINE"></asp:ListItem>
                                                <asp:ListItem id="lstitm_offline" Text="Offline Mode" Value="OFFLINE"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <span class="font-weight-bold">Criteria</span>
                                            <asp:DropDownList ID="ddlLoanCriteria" runat="server" CssClass="form-control">
                                                <asp:ListItem id="all" Text="All" Value="all"></asp:ListItem>
                                                <asp:ListItem id="loancode" Text="Loan Code" Value="Loancode"></asp:ListItem>
                                                <asp:ListItem id="customercode" Text="Customer Code" Value="Customercode"></asp:ListItem>
                                                <asp:ListItem id="dealerid" Text="Dealer id" Value="dealerid"></asp:ListItem>
                                                <asp:ListItem id="loanstatrus" Text="Loan Status" Value="loanstatrus"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <span class="font-weight-bold">Value</span>
                                            <asp:TextBox ID="txtLoanvalue" runat="server" class="form-control" Placeholder="Enter Value"></asp:TextBox>


                                        </div>
                                    </div>
                                    <div class="col-md-3  d-flex align-items-center">
                                        <span class="font-weight-bold"></span>
                                        <asp:Button ID="btnLoanSearch" runat="server" Text="Search" class="btn btn-primary" OnClick="btnLoanSearch_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="card shadow border-0">
                                <div class="card-header bg-light d-flex justify-content-between align-items-center">
                                    <h6 class="mb-0 text-primary font-weight-bold">
                                        <i class="fas fa-table mr-2"></i>Pending Loan
                                    </h6>
                                </div>
                                <div class="table-container">
                                    <span id="Span2" runat="server"></span>
                                    <span id="spnMessage" runat="server"></span>


                                    <%--      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>--%>
                                    <asp:GridView ID="grdLoanDetails" runat="server" AutoGenerateColumns="false"
                                        Width="100%" PageSize="15" CssClass="table table-bordered table-striped table-hover w-100 mb-0"
                                        DataKeyNames="LoanCode"
                                        AllowPaging="true"
                                        OnPageIndexChanging="grdLoanDetails_PageIndexChanging"
                                        OnRowCommand="grdLoanDetails_RowCommand"
                                        PagerSettings-Mode="Numeric"
                                        EmptyDataText="Record Not Found !"
                                        PagerSettings-Position="Bottom"
                                        PagerStyle-HorizontalAlign="Center"
                                        PagerStyle-CssClass="pagination-container"
                                        UseAccessibleHeader="true"
                                        GridLines="None">

                                        <Columns>
                                            <%-- <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%# ((grdLoanDetails.PageIndex * grdLoanDetails.PageSize) + Container.DataItemIndex + 1) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                            <asp:BoundField DataField="SrNo" HeaderText="S.No" />
                                            <asp:BoundField DataField="LoanCode" HeaderText="Loan id" />
                                            <asp:BoundField DataField="CustomerCode" HeaderText="Customer Code" />
                                            <asp:BoundField DataField="RetailerCode" HeaderText="Dealer Code" />
                                            <asp:BoundField DataField="LoanAmount" HeaderText="Loan Amt." />
                                            <asp:BoundField DataField="DownPayment" HeaderText="DP" />
                                            <asp:BoundField DataField="EMIAmount" HeaderText="EMI Amt." />
                                            <asp:BoundField DataField="Tenure" HeaderText="Tenure" />
                                            <asp:BoundField DataField="InterestRate" HeaderText="Inst.(%)" />
                                            <asp:BoundField DataField="StartDate" HeaderText="Loan Start Date" />
                                            <asp:BoundField DataField="EndDate" HeaderText="Loan End Date" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                            <asp:BoundField DataField="creditScore" HeaderText="Credit Score" />
                                            <asp:TemplateField HeaderText="Loan Status">
                                                <ItemTemplate>
                                                    <%# Eval("RecordStatus") %>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="status-column" />
                                                <HeaderStyle CssClass="header-column" />
                                            </asp:TemplateField>


                                            <asp:BoundField DataField="RID" HeaderText="" Visible="false" />

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="btn btn-sm btn-primary"
                                                        CommandName="EditRow" CommandArgument='<%# Eval("LoanCode") %>'
                                                        Visible='<%# Eval("RecordStatus").ToString().ToUpper() != "APPROVED" 
                                                  && Eval("RecordStatus").ToString().ToUpper() != "REJECTED" %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                        <%--  <PagerSettings Position="Bottom" />
                                        <PagerStyle CssClass="grid-pagin" />
                                        <PagerTemplate>
                                            <asp:LinkButton ID="lnkPrevious" runat="server" CommandName="Page" CommandArgument="Prev"
                                                CssClass="btn btn-sm btn-outline-primary">Previous</asp:LinkButton>

                                            <asp:LinkButton ID="lnkNext" runat="server" CommandName="Page" CommandArgument="Next"
                                                CssClass="btn btn-sm btn-outline-primary">Next</asp:LinkButton>
                                        </PagerTemplate>--%>
                                    </asp:GridView>
                                </div>
                                <%--                                    <asp:Label ID="lblLoanRcordCount" runat="server" Text="" CssClass="mt-2 font-weight-bold"></asp:Label>--%>
                                <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                            </div>

                        </div>

                    </div>
                </section>




            </div>

            <!-- GridView for Loan Details -->

        </div>
    </section>

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
    <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <%--    <style>
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
    </script>--%>

    <script>
        $(document).ready(function () {
            // Show zoomed image on click
            $(".zoomable").click(function () {
                var imgSrc = $(this).attr("src");
                $("#zoomedImage img").attr("src", imgSrc);
                $("#zoomedImage").fadeIn();
            });

            // Hide zoomed image on click outside
            $("#zoomedImage").click(function () {
                $(this).fadeOut();
            });
        });
    </script>

    <!-- === Overlay Container === -->
    <div id="zoomedImage">
        <img src="" alt="Zoomed Image" />
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
