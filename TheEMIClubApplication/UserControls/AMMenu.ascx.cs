using TheEMIClubApplication.AppCode;
using TheEMIClubApplication.BussinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheEMIClubApplication.UserControls
{
    public partial class AMMenuOne : System.Web.UI.UserControl
    {
        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    APPMenuBase objAppMenuBase = new APPMenuBase(); //Object of Business Class
                    objAppMenuBase.LoginId = AppSessions.SessionLoginId;
                    objAppMenuBase.ApplicationCode = Constants.ApplicationCode;
                    DataSet dsAMMenu = objAppMenuBase.GetMenu();
                    RenderMenu_XML(dsAMMenu);

                    //MenuItem mnuItem = AMRoleBasedMenu.FindItem("Home");
                    //if (mnuItem.ToString() == "Home")
                    //{
                    //    mnuItem.ImageUrl = "~/Images/Icon/AddRecord.png";
                    //}
                    //DataSet dsAMMenu1 = new DataSet();
                    //dsFWMenu = (DataSet)AppSessions.SessionAppMenuData;
                    //RenderMenu_XML(dsAMMenu);
                    //dsAMMenu.Dispose();
                    //dsAMMenu = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RenderMenu_XML(DataSet dsMenu)
        {
            try
            {
                if (dsMenu.Tables.Count > 0 && dsMenu.Tables[0].Rows.Count > 0)
                {
                    dsMenu.DataSetName = "AMMenus";
                    dsMenu.Tables[0].TableName = "AMMenu";
                    DataRelation relation = new DataRelation("ParentChildMenu",
                            dsMenu.Tables["AMMenu"].Columns["ChildMenuCode"],
                            dsMenu.Tables["AMMenu"].Columns["ParentMenuCode"],
                            true);
                    relation.Nested = true;
                    
                    dsMenu.Relations.Add(relation);
     

                    xmlDataSource.EnableCaching = false; //Do not cache the rendered XML.
                    xmlDataSource.Data = dsMenu.GetXml();
                    AMRoleBasedMenu.Visible = true;
                   //AMRoleBasedMenu.Attributes.Add("CssClass", "fas fa-home");

                }
                else
                {
                    //Menu data not available
                    AMRoleBasedMenu.Visible = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dsMenu.Dispose();
                dsMenu = null;
            }
        }


       
        #endregion Page_Load


      protected void OnMenuItemDataBound(object sender, MenuEventArgs e)
        {
            //if (SiteMap.CurrentNode != null)
            //{
            //    if (e.Item.Text == SiteMap.CurrentNode.Title)
            //    {
            //        if (e.Item.Parent != null)
            //        {
            //            e.Item.Parent.Selected = true;
            //        }
            //        else
            //        {
            //            e.Item.Selected = true;
            //        }
            //    }
            //}
        }
    }
}