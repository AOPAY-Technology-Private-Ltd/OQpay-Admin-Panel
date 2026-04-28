<%@ Page Title="Assigned EMI Details"
    Language="C#"
    MasterPageFile="~/MasterPage/MasterPage.Master"
    AutoEventWireup="true"
    CodeBehind="ShowAssignEmiDetailstoEmployee.aspx.cs"
    Inherits="TheEMIClubApplication.Admin.ShowAssignEmiDetailstoEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" />


    <div class="card shadow-sm border-0 mb-4">
        <div class="card-header filter-header">
            <i class="bi bi-funnel-fill me-2"></i> Filter Criteria
        </div>

        <div class="card-body">
            <div class="row g-3 align-items-end">

                <div class="col-md-3">
                    <label class="form-label fw-semibold">Criteria</label>
                    <asp:DropDownList ID="ddlsearch" runat="server" CssClass="form-control">
                        <asp:ListItem Text="All Record" Value="0" />
                        <asp:ListItem Text="Loan Id" Value="loanid" />
                        <asp:ListItem Text="User Code" Value="userid" />
                        <asp:ListItem Text="Active" Value="Active" />
                        <asp:ListItem Text="Inactive" Value="Inactive" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-4">
                    <label class="form-label fw-semibold">Value</label>
                    <asp:TextBox ID="TextBox1" runat="server"
                        CssClass="form-control"
                        placeholder="Enter Loan ID / User Code" />
                </div>

                <div class="col-md-3 d-flex gap-2">
                    <asp:Button ID="Button3" runat="server"
                        Text="Search"
                        CssClass="btn btn-primary w-100"
                        OnClick="Button3_Click" />

                    <asp:Button ID="btnReset" runat="server"
                        Text="Reset"
                        CssClass="btn btn-outline-secondary w-100"
                        OnClick="Button3_Click" />
                </div>

            </div>
        </div>
    </div>


    <div class="row g-4 mb-4">

        <div class="col-lg-4">
            <div class="stat-card bg-primary">
                <div class="stat-icon">
                    <i class="bi bi-list-check"></i>
                </div>
                <div class="stat-info">
                    <span>Total Loans</span>
                    <h3><asp:Label ID="lblTotalLoans" runat="server" /></h3>
                </div>
            </div>
        </div>

        <div class="col-lg-4">
            <div class="stat-card bg-success">
                <div class="stat-icon">₹</div>
                <div class="stat-info">
                    <span>Total Paid Amount</span>
                    <h3>₹ <asp:Label ID="lblTotalPaid" runat="server" /></h3>
                </div>
            </div>
        </div>

        <div class="col-lg-4">
            <div class="stat-card bg-warning text-dark">
                <div class="stat-icon dark">
                    <i class="bi bi-exclamation-circle"></i>
                </div>
                <div class="stat-info">
                    <span>Pending EMI Amount</span>
                    <h3>₹ <asp:Label ID="lblPendingEmi" runat="server" /></h3>
                </div>
            </div>
        </div>

    </div>


    <div class="card shadow-sm border-0">

        <div class="card-header table-header">
            <i class="bi bi-table me-2"></i> Loan EMI Details
        </div>

        <div class="card-body p-0">
            <div class="table-responsive">

                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>

                        <span id="spnMessage" runat="server"></span>

                        <asp:GridView ID="grdAssignView" runat="server"
                            CssClass="table table-bordered table-hover align-middle text-center emi-table mb-0"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            PageSize="5"
                        
                            OnRowCommand="grdAssignView_RowCommand"
                            OnPageIndexChanging="grdAssignView_PageIndexChanging"
                                  DataKeyNames="RID"
      PagerSettings-Mode="Numeric"
      PagerSettings-Position="Bottom"
      PagerStyle-HorizontalAlign="Center"
      PagerStyle-CssClass="pagination-container"
      UseAccessibleHeader="true"
      GridLines="None">

                            <Columns>
                                            <asp:BoundField DataField="SrNo" HeaderText="S.No" />
<asp:BoundField DataField="LoanCode" HeaderText="Loan ID" />
<asp:BoundField DataField="CustomerCode" HeaderText="User Code" />
<asp:BoundField DataField="LoanAmount" HeaderText="Loan Amt." />
<asp:BoundField DataField="DownPayment" HeaderText="DP" />
<asp:BoundField DataField="EMIAmount" HeaderText="EMI Amt." />
<asp:BoundField DataField="Tenure" HeaderText="Tenure" />
<asp:BoundField DataField="InterestRate" HeaderText="Inst.(%)" />
<asp:BoundField DataField="PaidEMI" HeaderText="PaidEMI" />
<asp:BoundField DataField="DuesEMI" HeaderText="DuesEMI" />
<asp:BoundField DataField="FirstDueDate" HeaderText="FirstDueDate" />
<asp:BoundField DataField="RecordStatus" HeaderText="RecordStatus"  />
<asp:BoundField DataField="flag_Status" HeaderText="Assigned"  />
                                      <asp:BoundField DataField="RID" HeaderText="" Visible="false" />

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <span class="badge bg-success px-3 py-2">
                                            <%# Eval("RecordStatus") %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" runat="server"
                                            CssClass="btn btn-sm btn-outline-primary"
                                            CommandName="test1"
                                            CommandArgument='<%# Eval("RID") %>'>
                                            View
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                                   <asp:TemplateField>
            <ItemTemplate>
               
                <asp:HiddenField ID="hfEMIAmt" runat="server" Value='<%# Eval("EMIAmount") %>'  />

                <asp:HiddenField ID="hfPaidEMI" runat="server" Value='<%# Eval("PaidEMI") %>' />
                ...
            </ItemTemplate>
        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="FollowUp">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkFollowup" runat="server"
                                                Text="Followup"
                                                CssClass="btn btn-sm btn-primary"
                                                CommandName="Followup"
                                                CommandArgument='<%# Eval("LoanCode") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                        <div class="px-3 py-2 text-muted fw-semibold">
                            <asp:Label ID="lblEMINoData" runat="server" />
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<style>
.filter-header{
    background:#2f6fed;
    color:#fff;
    font-weight:600;
    padding:14px 20px;
}
.table-header{
    background:linear-gradient(90deg,#1f2937,#111827);
    color:#fff;
    font-weight:600;
    padding:14px 20px;
}
.stat-card{
    display:flex;
    align-items:center;
    gap:20px;
    padding:22px;
    border-radius:18px;
    color:#fff;
    box-shadow:0 14px 30px rgba(0,0,0,.18);
}
.stat-icon{
    width:60px;
    height:60px;
    border-radius:50%;
    border:2px solid rgba(255,255,255,.6);
    display:flex;
    align-items:center;
    justify-content:center;
    font-size:26px;
}
.stat-icon.dark{
    border-color:#333;
}
.stat-info span{
    font-size:13px;
    letter-spacing:.6px;
    opacity:.9;
}
.stat-info h3{
    margin:4px 0 0;
    font-weight:700;
}
.emi-table th{
    background:#f3f4f6;
    font-weight:600;
    white-space:nowrap;
}
.emi-table td{
    vertical-align:middle;
}
.card{
    border-radius:16px;
}
</style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scriptContent" runat="server">
</asp:Content>
