using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using HRM.Common.DAL;
using Telerik.Web.UI;

public partial class UserRights_AssignLevelWiseRightsToUser : System.Web.UI.Page
{
    #region Security

    private const string ApplicationName = "Common";
    private const string PageName = "LevelWiseRights";

    MksSecurity objSecurity = new MksSecurity();

    #endregion

    #region Varible Declarations

    DataTable dtDesig;
    DataTable dtOrg;

    Organization objOrganization = new Organization();
    MksSecurity objMksSecurity = new MksSecurity();

    List<MksSecurityDAL.OrgStructureToSave> orgStruct = new List<MksSecurityDAL.OrgStructureToSave>();
    List<MksSecurityDAL.DesigStructureToSave> desigStruct = new List<MksSecurityDAL.DesigStructureToSave>();

    #endregion

    #region User Defined Methods

    #region Tree View Methods

    private void PopulateDesignationTreeView()
    {
        if (radtreeDesignationLevel.Nodes.Count > 0)
        {
            radtreeDesignationLevel.Nodes.Clear();
        }

        PopulateDesignationRootLevel();
    }

    private void PopulateOrganizationTreeView()
    {
        if (radtreeOrgLevel.Nodes.Count > 0)
        {
            radtreeOrgLevel.Nodes.Clear();
        }

        PopulateOrganizationRootLevel();
    }

    private void PopulateOrganizationRootLevel()
    {
        dtOrg = objMksSecurity.GetUserLevelOrganizationalStucture(0, radcmbUsers.SelectedValue.ToString().Trim(), radcmbApplications.SelectedValue.ToString().Trim(), Convert.ToInt32(radcmbCompanySelect.SelectedValue));
        PopulateOrganizationNodes(dtOrg, radtreeOrgLevel.Nodes);
    }

    private void PopulateDesignationRootLevel()
    {
        dtDesig = objMksSecurity.GetUserLevelDesignationStucture(0, radcmbUsers.SelectedValue.ToString().Trim(), radcmbApplications.SelectedValue.ToString().Trim());
        PopulateDesignationNodes(dtDesig, radtreeDesignationLevel.Nodes);
    }

    private void PopulateOrganizationNodes(DataTable Table, RadTreeNodeCollection Nodes)
    {
        foreach (DataRow row in Table.Rows)
        {
            RadTreeNode NewNode = new RadTreeNode();
            NewNode.Text = row["OrganizationTypeName"].ToString();
            NewNode.Value = row["OrgStructureID"].ToString();
            NewNode.Checked = Convert.ToBoolean(row["HaveRights"].ToString());
            Nodes.Add(NewNode);
        }
    }

    private void PopulateDesignationNodes(DataTable Table, RadTreeNodeCollection Nodes)
    {
        foreach (DataRow row in Table.Rows)
        {
            RadTreeNode NewNode = new RadTreeNode();
            NewNode.Text = row["DesignationName"].ToString();
            NewNode.Value = row["DesignationStuctureID"].ToString();
            NewNode.Checked = Convert.ToBoolean(row["HaveRights"].ToString());
            Nodes.Add(NewNode);
        }
    }

    private void PopulateOrganizationSubLevel(int ParentId, RadTreeNode ParentNode)
    {
        dtOrg = objMksSecurity.GetUserLevelOrganizationalStucture(ParentId, radcmbUsers.SelectedValue.ToString().Trim(), radcmbApplications.SelectedValue.ToString().Trim(), Convert.ToInt32(radcmbCompanySelect.SelectedValue));
        PopulateOrganizationNodes(dtOrg, ParentNode.Nodes);
    }

    private void PopulateDesignationSubLevel(int ParentId, RadTreeNode ParentNode)
    {
        dtDesig = objMksSecurity.GetUserLevelDesignationStucture(ParentId, radcmbUsers.SelectedValue.ToString().Trim(), radcmbApplications.SelectedValue.ToString().Trim());
        PopulateDesignationNodes(dtDesig, ParentNode.Nodes);
    }

    #endregion

    #region Build Organization & Department Hierarchy to Save

    private void GetSubLevelOrgLevels(RadTreeNode orgParentLevel)
    {
        if (orgParentLevel.Nodes.Count > 0)
        {
            foreach (RadTreeNode orgSubLevels in orgParentLevel.Nodes)
            {
                if (orgSubLevels.Checked == true)
                {
                    orgStruct.Add(new MksSecurityDAL.OrgStructureToSave { OrgStructureID = Convert.ToInt32(orgSubLevels.Value.ToString().Trim()), ParentID = Convert.ToInt32(orgParentLevel.Value.ToString()) });
                }

                GetSubLevelOrgLevels(orgSubLevels); 
            }
        }
    }
    
    private void GetSubLevelDesigLevels(RadTreeNode desigParentLevels)
    {
        if (desigParentLevels.Nodes.Count > 0)
        {
            foreach (RadTreeNode desigSubLevels in desigParentLevels.Nodes)
            {
                if (desigSubLevels.Checked == true)
                {
                    desigStruct.Add(new MksSecurityDAL.DesigStructureToSave { DesignationStuctureID = Convert.ToInt32(desigSubLevels.Value.ToString().Trim()), ParentID = Convert.ToInt32(desigParentLevels.Value.ToString()) });
                }

                GetSubLevelDesigLevels(desigSubLevels);
            }
        }
    }

    #endregion

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }
        else
        {
            if (IsPostBack == false)
            {
                if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.ViewOnly) == false)
                {
                    Response.Redirect("~/Common/NoPermissions.aspx");
                    return;
                }
                else
                {
                    radbtnSave.Visible = false;
                    radbtnUpdate.Visible = false;

                    if (objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.FullControl) == true)
                    {
                        radbtnSave.Visible = true;
                        radbtnUpdate.Visible = true;
                    }
                    else
                    {
                        radbtnSave.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.AddOnly);
                        radbtnUpdate.Visible = objSecurity.GetSpecificAccessRights(Session["UserName"].ToString(), ApplicationName, PageName, HRM.AccessRights.EditOnly);
                    }
                }
            }
        }

        if (!IsPostBack)
        {
            if (Session["UserName"] != null)
            {
                ViewState["IsLoading"] = true;
                hfCurrentUser.Value = Session["UserName"].ToString().Trim();
            }
        }
    }

    protected void radtreeDesignationLevel_NodeCreated(object sender, RadTreeNodeEventArgs e)
    {
        if (ViewState["IsCreated"] != null)
        {
            if (Convert.ToBoolean(ViewState["IsCreated"].ToString()) == true)
            {
                return;
            }
        }

        PopulateDesignationSubLevel(Convert.ToInt32(e.Node.Value), e.Node);
        radtreeDesignationLevel.ExpandAllNodes();
    }

    protected void radtreeOrgLevel_NodeCreated(object sender, RadTreeNodeEventArgs e)
    {
        if (ViewState["IsCreated"] != null)
        {
            if (Convert.ToBoolean(ViewState["IsCreated"].ToString()) == true)
            {
                return;
            }
        }

        PopulateOrganizationSubLevel(Convert.ToInt32(e.Node.Value), e.Node);
        radtreeOrgLevel.ExpandAllNodes();
    }

    protected void radcmbApplications_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (ViewState["IsLoading"] != null)
        {
            if (Convert.ToBoolean(ViewState["IsLoading"].ToString()) == false)
            {
                ViewState["IsCreated"] = false;
                
                radtreeOrgLevel.Nodes.Clear();
                radtreeDesignationLevel.Nodes.Clear();

                PopulateOrganizationRootLevel();
                PopulateDesignationRootLevel();

                ViewState["IsCreated"] = true;
            }
        }
    }

    protected void radcmbUsers_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (ViewState["IsLoading"] != null)
        {
            if (Convert.ToBoolean(ViewState["IsLoading"].ToString()) == false)
            {
                ViewState["IsCreated"] = false;
                
                radtreeOrgLevel.Nodes.Clear();
                radtreeDesignationLevel.Nodes.Clear();

                PopulateOrganizationRootLevel();
                PopulateDesignationRootLevel();

                ViewState["IsCreated"] = true;
            }
        }
    }

    protected void radbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";

            if (Session["UserName"] == null)
            {
                Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
            }

            if (radcmbCompanySelect.SelectedValue.Trim() == string.Empty)
            {
                lblError.Text = "Please select a valid company!";
                return;
            }
            else
            {
                if (Convert.ToInt32(radcmbCompanySelect.SelectedValue.Trim()) == 0)
                {
                    lblError.Text = "Please select a valid company!";
                    return;
                }
            }

            foreach (RadTreeNode orgLevels in radtreeOrgLevel.Nodes)
            {
                if (orgLevels.Checked == true)
                {
                    orgStruct.Add(new MksSecurityDAL.OrgStructureToSave { OrgStructureID = Convert.ToInt32(orgLevels.Value.ToString().Trim()), ParentID = 0 });
                }

                GetSubLevelOrgLevels(orgLevels);
            }

            foreach (RadTreeNode desigLevels in radtreeDesignationLevel.Nodes)
            {
                if (desigLevels.Checked == true)
                {
                    desigStruct.Add(new MksSecurityDAL.DesigStructureToSave { DesignationStuctureID = Convert.ToInt32(desigLevels.Value.ToString().Trim()), ParentID = 0 });
                }

                GetSubLevelDesigLevels(desigLevels);
            }

            if (objMksSecurity.AllowNewLevelRightsSave(radcmbUsers.SelectedValue.ToString().Trim(), radcmbApplications.SelectedValue.ToString().Trim(), Convert.ToInt32(radcmbCompanySelect.SelectedValue.Trim())) == true)
            {
                if (objMksSecurity.SaveDesignationAndOrganizationRights(radcmbUsers.SelectedValue.ToString().Trim(), radcmbApplications.SelectedValue.ToString().Trim(), orgStruct, desigStruct, Session["UserName"].ToString().Trim(), Convert.ToInt32(radcmbCompanySelect.SelectedValue.Trim())) == 1)
                {
                    lblError.ForeColor = Color.Green;
                    lblError.Text = "Rights successfully assigned to selected user.";

                    if (Convert.ToBoolean(ViewState["IsLoading"].ToString()) == false)
                    {
                        ViewState["IsCreated"] = false;

                        radtreeOrgLevel.Nodes.Clear();
                        radtreeDesignationLevel.Nodes.Clear();

                        PopulateOrganizationRootLevel();
                        PopulateDesignationRootLevel();

                        ViewState["IsCreated"] = true;
                    }
                }
                else
                {
                    if (objMksSecurity.IsError == true)
                    {
                        lblError.ForeColor = Color.Red;
                        lblError.Text = objMksSecurity.ErrorMsg;
                    }
                }
            }
            else
            {
                if (objMksSecurity.IsError == true)
                {
                    lblError.ForeColor = Color.Red;
                    lblError.Text = objMksSecurity.ErrorMsg;

                    return;
                }

                lblError.ForeColor = Color.Red;
                lblError.Text = "Rights already assigned for application, user & company combination(s); Only updating is allowed!";
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }

    protected void radbtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (objMksSecurity.AllowNewLevelRightsSave(radcmbUsers.SelectedValue.ToString().Trim(), radcmbApplications.SelectedValue.ToString().Trim(), Convert.ToInt32(radcmbCompanySelect.SelectedValue.Trim())) == false)
            {
                lblError.Text = "";

                foreach (RadTreeNode orgLevels in radtreeOrgLevel.Nodes)
                {
                    if (orgLevels.Checked == true)
                    {
                        orgStruct.Add(new MksSecurityDAL.OrgStructureToSave { OrgStructureID = Convert.ToInt32(orgLevels.Value.ToString().Trim()), ParentID = 0 });
                    }

                    GetSubLevelOrgLevels(orgLevels);
                }

                foreach (RadTreeNode desigLevels in radtreeDesignationLevel.Nodes)
                {
                    if (desigLevels.Checked == true)
                    {
                        desigStruct.Add(new MksSecurityDAL.DesigStructureToSave { DesignationStuctureID = Convert.ToInt32(desigLevels.Value.ToString().Trim()), ParentID = 0 });
                    }

                    GetSubLevelDesigLevels(desigLevels);
                }

                if (objMksSecurity.UpdateDesignationAndOrganizationRights(radcmbUsers.SelectedValue.ToString().Trim(), radcmbApplications.SelectedValue.ToString().Trim(), orgStruct, desigStruct, Session["UserName"].ToString().Trim(), Convert.ToInt32(radcmbCompanySelect.SelectedValue.Trim())) == 1)
                {
                    lblError.ForeColor = Color.Green;
                    lblError.Text = "Rights successfully updated to selected user.";

                    if (Convert.ToBoolean(ViewState["IsLoading"].ToString()) == false)
                    {
                        ViewState["IsCreated"] = false;

                        radtreeOrgLevel.Nodes.Clear();
                        radtreeDesignationLevel.Nodes.Clear();

                        PopulateOrganizationRootLevel();
                        PopulateDesignationRootLevel();

                        ViewState["IsCreated"] = true;
                    }
                }
                else
                {
                    if (objMksSecurity.IsError == true)
                    {
                        lblError.ForeColor = Color.Red;
                        lblError.Text = objMksSecurity.ErrorMsg;
                    }
                }
            }
            else
            {
                if (objMksSecurity.IsError == true)
                {
                    lblError.ForeColor = Color.Red;
                    lblError.Text = objMksSecurity.ErrorMsg;

                    return;
                }

                lblError.ForeColor = Color.Red;
                lblError.Text = "No Rights found for updating!";
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"] == null)
            {
                Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
            }

            lblError.ForeColor = Color.Green;
            lblError.Text = "";

            if (Convert.ToBoolean(ViewState["IsLoading"].ToString()) == false)
            {
                try
                {
                    if (Convert.ToInt32(radcmbCompanySelect.SelectedValue) == 0)
                    {
                        return;
                    }
                }
                catch
                {
                    return;
                }

                ViewState["IsCreated"] = false;

                radtreeOrgLevel.Nodes.Clear();
                radtreeDesignationLevel.Nodes.Clear();

                PopulateOrganizationRootLevel();
                PopulateDesignationRootLevel();

                ViewState["IsCreated"] = true;
                ViewState["IsLoading"] = false;
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }
    protected void radcmbCompanySelect_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }

        ViewState["IsCreated"] = false;

        radtreeOrgLevel.Nodes.Clear();
        radtreeDesignationLevel.Nodes.Clear();

        try
        {
            if (Convert.ToInt32(radcmbCompanySelect.SelectedValue) == 0)
            {
                return;
            }
        }
        catch
        {
            return;
        }
        
        PopulateOrganizationRootLevel();
        PopulateDesignationRootLevel();

        ViewState["IsCreated"] = true;
        ViewState["IsLoading"] = false;
    }

    protected void radcmbCompanySelect_DataBound(object sender, EventArgs e)
    {
        RadComboBoxItem newItem = new RadComboBoxItem("<< Select Company >>", "0");
        radcmbCompanySelect.Items.Insert(0, newItem);
    }
}