<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AMDateLoginHeader.ascx.cs" Inherits="TheEMIClubApplication.UserControls.AMDateLoginHeader" %> 
<style>
    .main-table-content {
        background: white;
    }

    .main-table-content-tr {
        display: flex;
        align-items: center;
        justify-content: center;
        flex-direction: column;
        padding: 0px;
        margin: 0px;
        width: auto;
    }

    .main-table-content-td {
        margin: 0px;
        width: 100%;
    }

    .main-table-content-td-one {
        margin: 0px;
        padding: 0px;
    }

    .main-table-content-one {
        margin: 0px;
        position: initial;
        display: flex;
        align-items: center;
        justify-content: center;
        gap: 40px;
        flex-direction: column;
    }

    .main-table-content-td-one-bar {
        text-align: right;
        display: flex;
        align-items: center;
        justify-content: center;
        flex-direction: column;
        margin: 0px;
    }

    .main-table-content-td-two-bar {
        display: flex;
        align-items: center;
        justify-content: center;
        margin: 0px;
        padding: 7px;
    }

    .main-table-content-td-three-bar {
        display: flex;
        align-items: center;
        justify-content: center;
        flex-direction: column;
        width: 100%;
        position: initial;
        margin: 0px;
        padding: 0px;
    }

    .main-table-content-td-one-bar-date-bar {
        font-weight: bold;
        color: black;
        padding: 8px;
    }

    #spnTime {
        width: 100%;
        display: flex;
        align-items: center;
        justify-content: center;
        padding: 3px 5px;
        background: white;
    }

    .spnUserName-bar {
        color: #286090;
    }

    .main-table-content-td-three-bar-box-one {
        color: black;
        width: 100%;
        font-weight: 600;
        text-align: center;
        padding: 8px;
        border: none;
        outline: none;
    }

    td {
        cursor: pointer;
    }
</style>
<style>
    .time-bar{
        background: #F4F4F4;
    }
    .change-hover:hover{
            background: #ccc;
            color: black;
    }
</style>

<table class="main-table-content rounded w-100" style="background: #F4F4F4;">
    <tr class="main-table-content-tr">
        <td class="main-table-content-td">
            <!--Application Title Here-->
            <big><%=AVFramework.Common.GetMessageFromXMLFile("AppName")%></big>
        </td>
        <td class="main-table-content-td-one w-100">
            <!--Place current Date, Time and welcome user message-->
            <table cellpadding="0" cellspacing="0" class="DateLoginHeader main-table-content-one w-100">
                <%--Day Month Date and Year--%>
                <tr class="w-100 m-0 text-center" style="font-size: 11px;">
                    <td class="main-table-content-td-one-bar text-center">
                        <span id="spnDate" runat="server" class="main-table-content-td-one-bar-date-bar hover-bar-button text-black m-0 w-100 change-hover"></span>
                        <span id="spnTime" class="SectionHeader main-table-content-td-one-bar-time-bar change-hover time-bar"></span>
                    </td>
                </tr>   
                <%--Holder--%>
                <tr>
                    <td class="main-table-content-td-two-bar hover-bar-button change-hover">Welcome:&nbsp;<span id="spnUserName" class="spnUserName-bar color-primary fw-bold" 
                        runat="server"></span><span id="spnBranchName" style="display: none;" runat="server"></span>
                    </td>
                </tr>
                <%--Change password and Logout--%>
                <tr> 
                    <td class="main-table-content-td-three-bar">
                        <asp:LinkButton ID="lnkHome" runat="server"  CausesValidation="false"  OnClick="GoToHomePage" CssClass="nontab_link main-table-content-td-three-bar-box-one  change-hover">Home</asp:LinkButton>
                        <asp:LinkButton ID="lnkProfile" runat="server"  CausesValidation="false"  CssClass="nontab_link main-table-content-td-three-bar-box-one hover-bar-button  change-hover" OnClick="lnkProfile_Click">Profile</asp:LinkButton>
                        <asp:LinkButton ID="lnkChangePassword" runat="server"  CausesValidation="false"  OnClick="ChangePassword" CssClass="nontab_link main-table-content-td-three-bar-box-one hover-bar-button  change-hover">Change Password</asp:LinkButton>
                        <asp:LinkButton ID="lnkLogout" runat="server"  CausesValidation="false"  OnClick="Logout" CssClass="nontab_link main-table-content-td-three-bar-box-one hover-bar-button  change-hover">Logout</asp:LinkButton>
                        <div id="divLoginPreserved" runat="server" style="display: none;">
                            <span id="spnLoginPreserved" runat="server" visible="false" class="LoginPreserved">Login Preserved</span>
                            <asp:LinkButton ID="lnkPreserveMyLogin" CausesValidation="false" runat="server" CssClass="PreserveMyLogin" OnClick="PreserveMyLogin"
                                Visible="false" Text="Preserve My Login" OnClientClick="javascript:return confirm('Are you sure to Preserve your Login?');"></asp:LinkButton>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<!--Code for Digital Clock-->
<script language="javascript" type="text/javascript">
    function showDigitalClock() {
        //if (!document.all)
        // return

        var Digital = new Date()
        var hours = Digital.getHours()
        var minutes = Digital.getMinutes()
        var seconds = Digital.getSeconds()
        var dn = "AM"

        if (hours >= 12) {
            dn = "PM"
            hours = hours - 12
        }
        if (hours == 0)
            hours = 12

        if (minutes <= 9)
            minutes = "0" + minutes

        if (seconds <= 9)
            seconds = "0" + seconds

        var ctime = hours + ":" + minutes + ":" + seconds + " " + dn
        document.getElementById('spnTime').innerHTML = ctime
        setTimeout("showDigitalClock()", 1000)
    } //Function End
    //window.onload=showDigitalClock //Call the function to display digital clock
    showDigitalClock();

</script>