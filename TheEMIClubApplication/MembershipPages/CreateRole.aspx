<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreateRole.aspx.cs" Inherits="TheEMIClubApplication.MembershipPages.CreateRole" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="Server">
    <div class="hold-transition sidebar-mini">
        <div class="wrapper">
            <!-- Content Wrapper. Contains page content -->
            <div class="main-box-container" style="width: 100%; position: relative; left: 0px; top: 0px; margin: 0px; padding: 25px 0px;">
                <!-- Main content -->
                <section class="content">
                    <div class="container-fluid">
                        <!-- Form -->
                        <div class="card card-default">
                            <div class="card-header form-header-bar">
                                <h3 class="card-title"><span id="spnUpdateRole" runat="server" /></h3>
                            </div>
                            <!-- Card Header -->
                            <asp:Panel CssClass="box-body box-body-inner" ID="pnlCreateRoleSection" runat="server">                               
                                    <div class="card-body ">
                                        <div class="row">
                                            <div class="col-md-12 card-body-box">
                                                <div class="row">
                                                    <div class="col-md-12 text-center mb-2">
                                                        <div class="form-group">
                                                            <asp:ScriptManager ID="smRoleDetail" runat="server"></asp:ScriptManager>
                                                            <asp:ValidationSummary ID="valSummary" runat="server" ShowSummary="true" ValidationGroup="OnSubmit" />
                                                            <span id="spnMessage" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <span class="font-weight-bold">Application Name <code>*</code></span>
                                                            <asp:DropDownList ID="ddlApplicationName" runat="server" OnClientFocus="expandControl"
                                                                placeholder="Enter Application Name" class="form-control" Style="width: 100%;">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="valReqApplicationName" runat="server" ControlToValidate="ddlApplicationName"
                                                                SetFocusOnError="true" Display="None" InitialValue="" ValidationGroup="OnSubmit"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <span class="font-weight-bold">Role Name <code>*</code></span>
                                                            <asp:TextBox ID="txtRoleName" autocomplete="off" onpaste="return BlockHTMLOnPaste(this,event);"
                                                                onkeydown="return BlockingHtmlOnKey(this,event);" Style="width: 100%;" placeholder="Enter Role Name"
                                                                runat="server" CssClass="form-control" MaxLength="50" />
                                                            <asp:RequiredFieldValidator ID="valReqRoleName" runat="server" ControlToValidate="txtRoleName"
                                                                SetFocusOnError="true" Display="None" ValidationGroup="OnSubmit" />
                                                            <asp:RegularExpressionValidator ID="valRexRoleName" ValidationGroup="OnSubmit" runat="server"
                                                                ControlToValidate="txtRoleName" CssClass="ErrorMessage" ErrorMessage="Invalid text" Display="Dynamic"
                                                                ValidationExpression="^[a-zA-Z0-9'\/_\-,.\s]{0,50}$"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <span class="font-weight-bold">Role Description</span>
                                                            <asp:TextBox ID="txtRoleDescription" autocomplete="off" onpaste="return BlockHTMLOnPaste(this,event);" onkeydown="return BlockingHtmlOnKey(this,event);"
                                                                runat="server" CssClass="form-control" MaxLength="50" placeholder="Enter Role Description" />
                                                            <asp:RegularExpressionValidator ID="valRegRoleDesc" ValidationGroup="OnSubmit" runat="server"
                                                                ControlToValidate="txtRoleDescription" CssClass="ErrorMessage" ErrorMessage="Invalid text" Display="Dynamic"
                                                                ValidationExpression="^[a-zA-Z0-9'\/_\-,.\s]{0,50}$"></asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <span class="font-weight-bold">Menu Scope <code>*</code></span>
                                                            <asp:DropDownList ID="ddlMenuScope" runat="server" OnClientFocus="expandControl"
                                                                class="form-control" Style="width: 100%;">
                                                            </asp:DropDownList>

                                                            <asp:RequiredFieldValidator ID="valReqMenuScope" runat="server"
                                                                ControlToValidate="ddlMenuScope"
                                                                InitialValue=""
                                                                Display="Dynamic"
                                                                SetFocusOnError="true"
                                                                ValidationGroup="OnSubmit">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12 text-center mb-3">
                                                        <asp:HiddenField ID="hdnRoleCode" runat="server" />
                                                        <div id="divImage" style="display: none;">
                                                            <asp:Image ID="imgLoading" runat="server" ImageUrl="~/Images/loading.gif" />
                                                        </div>
                                                    <asp:Button ID="btnSaveRole" runat="server"
    Text="Save"
    CssClass="btn btn-primary"
    OnClick="btnSaveRole_Click"
    CausesValidation="false"
    UseSubmitBehavior="false" />
                                                        <asp:Button ID="btnClearRole" runat="server" Text="Clear" CssClass="btn btn-danger"
                                                            OnClick="btnClearRole_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn btn-success"
                                                            Text="Cancel" OnClick="btnCancel_Click1" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>                                
                            </asp:Panel>
                            <div class="hold-transition sidebar-mini" id="dvMenuAssignmentSection" visible="false" runat="server">
                                <div class="wrapper">
                                    <!-- Content Wrapper Contains page content -->
                                    <div class="main-box-container w-100 m-0" style="position: relative; left: 0px; top: 0px; padding: 25px 0px;">
                                        <!-- Main content -->
                                        <section class="content">
                                            <div class="container-fluid">
                                                <!-- Form -->
                                                <div class="card card-default">
                                                    <div class="card-header form-header-bar">
                                                        <h3 class="card-title">Menu For Role: <span id="spnRoleNameAndCode" runat="server" /></h3>
                                                    </div>
                                                    <!-- Card Header -->
                                                    <div class="card-body" id="Div1" runat="server">
                                                        <div class="row">
                                                            <div class="col-md-12 card-body-box">
                                                                <div class="row">
                                                                    <div class="col-md-12 text-center mb-3">
                                                                        <span id="spnMessageMenu" runat="server" />
                                                                    </div>
                                                                    <div class="col-md-12 text-center mb-3">
                                                                        <asp:Button ID="btnSave" runat="server" Text="Save Role Detail" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                                                        <asp:Button ID="btnCancelMenu" runat="server" Text="Cancel" CssClass="btn btn-danger" OnClick="btnCancel_Click" />
                                                                    </div>
                                                                    <div class="col-md-12 text-center">
                                                                        <div class="table-responsive">
                                                                            <div id="dvCheckBoxList" runat="server" visible="false">
                                                                                <asp:CheckBoxList ID="cblMenu" runat="server" RepeatLayout="Table" RepeatColumns="6"
                                                                                    RepeatDirection="Vertical" Font-Size="small" class="GridStyle table table-bordered table-condensed table-hover checkbox-bars" />
                                                                            </div>
                                                                            <div id="dvGridView" runat="server" style="overflow: scroll; height: 250px; width: 100%;"
                                                                                visible="false">
                                                                                <asp:GridView ID="gvShowSelectedMenus" runat="server" AutoGenerateColumns="False"
                                                                                    Width="100%" DataKeyNames="Assigned_YN" CssClass="GridStyle table table-bordered table-condensed table-hover">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Menu" HeaderStyle-CssClass="PaddingLeft_5">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkSelectedMenus" runat="server" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField HeaderText="Menu Code" DataField="MenuCode" HeaderStyle-CssClass="PaddingLeft_5" />
                                                                                        <asp:BoundField HeaderText="Menu Name" DataField="MenuName" HeaderStyle-CssClass="PaddingLeft_5" />
                                                                                    </Columns>
                                                                                    <RowStyle CssClass="NorRow" />
                                                                                    <AlternatingRowStyle CssClass="AltRowTable" />
                                                                                    <HeaderStyle CssClass="SectionHeader" />
                                                                                    <PagerStyle HorizontalAlign="Center" Font-Bold="true" />
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </section>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scriptContent" runat="Server">
  <script type="text/javascript">
      function Loading() {

          if (typeof (Page_ClientValidate) == "function") {
              if (!Page_ClientValidate('OnSubmit')) {
                  return false;
              }
          }

          var div = document.getElementById('divImage');
          if (div) {
              div.style.display = 'block';
          }

          var btn = document.getElementById('<%= btnSaveRole.ClientID %>');
          if (btn) {
              btn.value = "Please Wait...";
              setTimeout(function () {
                  btn.disabled = true;
              }, 100);
          }

          return true; // IMPORTANT: allow postback
      }
  </script>
</asp:Content>
