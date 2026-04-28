<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AMMenu.ascx.cs" Inherits="TheEMIClubApplication.UserControls.AMMenuOne" %>

<%--<style>
/* Menu container */
#AMRoleBasedMenu {
        display: none;
    position: relative;
    background-color: #2c3e50;
    width: 230px;
    font-family: Arial, sans-serif;
    border-radius: 5px;
}

/* Parent menu items (Level 1) */
#AMRoleBasedMenu .menuLevel1 {
    background-color: #34495e;
    color: #fff;
    padding: 10px 15px;
    display: block;
    font-weight: bold;
    border-bottom: 1px solid #2c3e50;
}
#AMRoleBasedMenu .menuLevel1:hover {
    background-color: #1abc9c;
}

/* Optional animation */
#AMRoleBasedMenu li ul.show {
    display: block;
}

/* Sub menu items (Level 2) */
#AMRoleBasedMenu .menuLevel2 {
    background-color: #3d566e;
    color: #fff;
    padding: 8px 25px;
    display: block;
    border-bottom: 1px solid #2c3e50;
}
#AMRoleBasedMenu .menuLevel2:hover {
    background-color: #16a085;
}

/* Level 3 (if needed) */
#AMRoleBasedMenu .menuLevel3 {
    background-color: #4b6a85;
    padding: 8px 35px;
}

/* Remove ASP.NET default bullets & margins */
#AMRoleBasedMenu ul {
    list-style: none;
    margin: 0;
    padding: 0;
}

/* Ensure submenus drop below parent */
#AMRoleBasedMenu li ul {
    display: none;
    position: relative;
}
#AMRoleBasedMenu li:hover > ul {
    display: block;
}
</style>--%>

<%--<asp:Menu ID="AMRoleBasedMenu" runat="server" Orientation="Vertical" EnableSubMenuScrolling="true"  StaticEnableDefaultPopOutImage="false" MaximumDynamicDisplayLevels="1"  
    DynamicVerticalOffset="1" DynamicEnableDefaultPopOutImage="false"  DataSourceID="xmlDataSource" StaticDisplayLevels="2"   > 


    <DataBindings>
        <asp:MenuItemBinding DataMember="MenuItem" NavigateUrlField="NavigateURL" TextField="MenuText" ToolTipField="ToolTip"  />
    </DataBindings>
    <LevelMenuItemStyles>
        <asp:MenuItemStyle CssClass="tab_link"  />
        <asp:MenuItemStyle CssClass="level2" />
        <asp:MenuItemStyle CssClass="level3" />
        <asp:MenuItemStyle CssClass="level4" />
    </LevelMenuItemStyles>

</asp:Menu>
<asp:XmlDataSource ID="xmlDataSource" runat="server" TransformFile="~/CSS/AppMenuXSLT.xsl" XPath="MenuItems/MenuItem" />

--%>
<style>
/* Sidebar container */
.sidebar-menu {
    width: 220px;
    background-color: #0073b7;
    color: #fff;
    padding: 0;
    border-radius: 0;
    font-family: "Segoe UI", Arial, sans-serif;
}

/* Remove table layout ASP.NET generates */
.sidebar-menu table {
    width: 100% !important;
}
.sidebar-menu td {
    display: block !important;
    width: 100% !important;
}

/* Parent menu items */
.sidebar-menu a {
    display: block;
    padding: 10px 15px;
    color: #fff;
    text-decoration: none;
    transition: background 0.2s;
}
.sidebar-menu a:hover {
    background-color: black;
}

.sidebar-menu > li > a {
    font-size: 20px;       /* bigger */
    font-weight: 900;      /* extra bold */
    background: #666;      /* darker gray */
    text-transform: uppercase; /* optional: make them all caps */
    letter-spacing: 0.5px; /* subtle spacing for emphasis */
}
/* Submenu hidden by default */
.sidebar-menu ul {
    list-style: none;
    padding-left: 0;
    margin: 0;
    display: none;   /* <--- hidden by default */
    background: #0d6efd;
}
.sidebar-menu ul li a {
    padding: 8px 25px;
    font-size: 14px;
    font-weight: normal; /* lighter so parents stand out */
    color: #f8f9fa;
}

</style>
<script>
    document.addEventListener("DOMContentLoaded", function () {
        const menu = document.getElementById("<%= AMRoleBasedMenu.ClientID %>");
    if (!menu) return;

    // Only attach toggle on parent items
    menu.querySelectorAll("li").forEach(function (li) {
        const subMenu = li.querySelector("ul"); // check if li has submenu
        const link = li.querySelector("a");

        if (subMenu && link) {
            // Initially hide submenus
            subMenu.style.display = "none";

            link.addEventListener("click", function (e) {
                e.preventDefault(); // stop navigation
                // Toggle visibility
                subMenu.style.display = (subMenu.style.display === "block") ? "none" : "block";
            });
        }
    });
});
</script>

<asp:Menu ID="AMRoleBasedMenu" runat="server"
    Orientation="Vertical"
    StaticDisplayLevels="2"
    MaximumDynamicDisplayLevels="1"
    DataSourceID="xmlDataSource"
    CssClass="sidebar-menu">
    <DataBindings>
        <asp:MenuItemBinding DataMember="MenuItem"
            NavigateUrlField="NavigateURL"
            TextField="MenuText"
            ToolTipField="ToolTip" />
    </DataBindings>
</asp:Menu>

<asp:XmlDataSource ID="xmlDataSource" runat="server"
    TransformFile="~/CSS/AppMenuXSLT.xsl"
    XPath="MenuItems/MenuItem" />

