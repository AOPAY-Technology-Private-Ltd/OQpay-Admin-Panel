<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master"  AutoEventWireup="true" CodeBehind="EditUsers.aspx.cs" Inherits="TheEMIClubApplication.MembershipPages.EditUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContent" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="hold-transition sidebar-mini">
        <div class="wrapper">
            <!-- Content Wrapper. Contains page content -->
            <div class="main-box-container" style="width: 100%; position: relative; left: 0px; top: 0px; margin: 0px; padding: 25px 0px;">
                <!-- Main content -->
                <section class="content">
                    <div class="container-fluid">
                        <!-- Form -->
                        <div class="card card-default"> 
                            <div class="card card-default">
                                <div class="card-header form-header-bar">
                                    <h3 class="card-title">Search & Edit Bank Master</h3>
                                </div>
                                <!-- /.card-header -->
                                <div class="card-body ">
                                    <div class="row">
                                        <div class="col-md-12 card-body-box">
                                       
                                             <div class="row">
     <div class="col-md-12">
         <div class="form-group">
               <span id="spnchange" runat="server" style="width: 100%; color: green;"></span>
<span id="spnMsg" runat="server" style="width: 100%; color: green;"></span>
<span id="spnerror" runat="server" style="width: 100%;"></span>
<span id="spnResetPassword" runat="server" style="background-color: #F2D0D9;" visible="false"></span>

         </div>
     </div>

                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">User Id</span>
                                                        <asp:TextBox ID="txtuserid" runat="server" class="form-control" placeholder="Account Holder Name"></asp:TextBox>


                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <span class="font-weight-bold">Mobile No</span>
                                                        <asp:TextBox ID="txtmobileno" runat="server" class="form-control" placeholder="Account Number"></asp:TextBox>


                                                    </div>
                                                </div>

                                                    <div class="col-md-4">
        <div class="form-group">
            <span class="font-weight-bold">Email Id</span>
            <asp:TextBox ID="txtEmail" runat="server" class="form-control" placeholder="Account Number"></asp:TextBox>


        </div>
    </div>


                                                <div class="col-md-12 text-center mb-3">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-primary" OnClick="btnSearch_Click" />
                                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" class="btn btn-success" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <!-- Grid View -->
                        <div class="card card-default">
                            <div class="card-header form-header-bar">
                                <h3 class="card-title">Result</h3>
                            </div>
                            <!-- /.card-header -->
                            <div class="card-body">
                                     <span id="spnMessage" runat="server"></span>
                             
                                <asp:GridView ID="EditUserDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" Width="100%" CssClass="table table-bordered table-condensed table-hover"   OnRowCommand="EditUserDetails_RowCommand">
                                    <Columns>
                                        <asp:BoundField HeaderText="Sr.No." DataField="SrNo" />
                                        <asp:BoundField HeaderText="User Id" DataField="UserCode" />
                                        <asp:BoundField HeaderText=" User Name" DataField="Name" />
                                        <asp:BoundField HeaderText="Phone No" DataField="phone" />
                                        <asp:BoundField HeaderText="Email" DataField="email" />
                                                                            
                                        <asp:TemplateField HeaderText="status">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="lnkAction" runat="server" Text='<%#Eval("Status")%>' OnClientClick="return confirm('Are you sure?');"
                                                    CommandName="ACT" CommandArgument='<%#Eval("UserCode") +  "|" + Eval("Status")+  "|" + Eval("phone")%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="PaddingLeft_5" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit"
                                                    CommandName="EDT" CommandArgument='<%#Eval("UserCode") +  "|" + Eval("Status")+  "|" + Eval("phone")%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                        </asp:TemplateField> 
                                    </Columns>
                                    <RowStyle CssClass="NorRow" />
                                    <AlternatingRowStyle CssClass="AltRowTable" />
                                    <HeaderStyle CssClass="SectionHeader" />
                                    <PagerStyle HorizontalAlign="Center" Font-Bold="true" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </div>

       
    </div>

</asp:Content>