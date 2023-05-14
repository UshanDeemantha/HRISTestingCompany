using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class HR_Organization_OrganizationStucture : System.Web.UI.UserControl
{
    DataTable dt;
    Organization objOrganizationStructure = new Organization();

    #region Tree View Methods

    private void PopulateTreeView()
    {
        if (tvOrganizationStucture.Nodes.Count > 0)
        {
            tvOrganizationStucture.Nodes.Clear();
        }

        PopulateRootLevel();
    }

    private void PopulateRootLevel()
    {
        dt = objOrganizationStructure.GetOrganizationStructureRootLevel(Convert.ToInt32(Session["CompanyId"]), 0);
        PopulateNodes(dt, tvOrganizationStucture.Nodes);
    }

    private void PopulateNodes(DataTable Table, RadTreeNodeCollection Nodes)
    {
        foreach (DataRow row in Table.Rows)
        {
            RadTreeNode NewNode = new RadTreeNode();
            NewNode.Text = row["OrganizationTypeName"].ToString();
            NewNode.Value = row["OrgStructureID"].ToString();
            Nodes.Add(NewNode);
        }
    }

    private void PopulateSubLevel(int ParentId, RadTreeNode ParentNode)
    {
        dt = objOrganizationStructure.GetOrganizationStructureRootLevel(Convert.ToInt32(Session["CompanyId"]), ParentId);
        PopulateNodes(dt, ParentNode.Nodes);
    }

    #endregion

    public void Page_Load(object sender, EventArgs e)
    {
        if (Session["CompanyId"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }

        if (!IsPostBack)
        {
            tvOrganizationStucture.Nodes.Clear();
            PopulateRootLevel();

            ViewState["IsCreated"] = true;
        }
        else
        {
            if (Session["IsDesignationStructureChanged"] != null)
            {
                if (Convert.ToBoolean(Session["IsDesignationStructureChanged"]) == true)
                {
                    if (tvOrganizationStucture.Nodes.Count > 0)
                    {
                        tvOrganizationStucture.Nodes.Clear();
                        ViewState["IsCreated"] = false;
                    }
                    PopulateRootLevel();
                    ViewState["IsCreated"] = true;
                    Session["IsDesignationStructureChanged"] = null;
                }
            }
        }
    }

    protected void tvOrganizationStucture_NodeClick(object sender, RadTreeNodeEventArgs e)
    {
   
        hfOrganizationStucture.Value = tvOrganizationStucture.SelectedNode.Value;
        hfOrganizationTypeName.Value = tvOrganizationStucture.SelectedNode.Text;

        try
        {
            TextBox txtParentStructureType = (TextBox)Parent.FindControl("txtRootLevel");
            txtParentStructureType.Text = tvOrganizationStucture.SelectedNode.Text;

            HiddenField hfParentId = (HiddenField)Parent.FindControl("hfParentId");
            hfParentId.Value = tvOrganizationStucture.SelectedNode.Value;

            // add by anusha
            HiddenField hfChildLeveId = (HiddenField)Parent.FindControl("hfChildLevel");
            hfChildLeveId.Value = SetChildLevel(Convert.ToInt32(tvOrganizationStucture.SelectedNode.Value), Convert.ToInt32(hfParentId.Value)).ToString();

            RadComboBox radcboOrganizationType = (RadComboBox)Parent.FindControl("radcboOrganizationType");
            radcboOrganizationType.DataSource = objOrganizationStructure.GetOrganizationTypesByLevelIndex(Convert.ToInt32(Session["CompanyId"]), Convert.ToInt32(hfChildLeveId.Value));
            radcboOrganizationType.DataTextField = "OrganizationTypeName";
            radcboOrganizationType.DataValueField = "OrganizationTypeID";
            radcboOrganizationType.DataBind();
        }
        catch(Exception ex)
        {
            try
            {
                
                HiddenField hfOrgainzationStuctcureId = (HiddenField)Parent.FindControl("hfOrganizationStructure");
                hfOrgainzationStuctcureId.Value = tvOrganizationStucture.SelectedNode.Value;

                Label lblOrganization = (Label)Parent.FindControl("lblOrganization");
                lblOrganization.Text = tvOrganizationStucture.SelectedNode.Text;

                DataTable OrgStructure = objOrganizationStructure.GetOrganizationFlow(Convert.ToInt32(hfOrgainzationStuctcureId.Value));

                if (OrgStructure.Rows.Count > 0)
                {
                    lblOrganization.Text = OrgStructure.Rows[0]["OrgFlow"].ToString();
                }
            }
            catch(Exception exs) { }
        }
    }

    protected void tvOrganizationStucture_NodeCreated(object sender, RadTreeNodeEventArgs e)
    {
        if (ViewState["IsCreated"] != null)
        {
            if (Convert.ToBoolean(ViewState["IsCreated"].ToString()) == true)
            {
                return;
            }
        }

        PopulateSubLevel(Convert.ToInt32(e.Node.Value), e.Node);
        tvOrganizationStucture.ExpandAllNodes();
    }

    public void reLoad()
    {
        ViewState["IsCreated"] = false;
        PopulateTreeView();
        ViewState["IsCreated"] = true;
    }

    //add by anusha
    private int SetChildLevel(int TypeId, int StructureId)
    {
        int childLevel = int.MinValue;
        int childTypeId = GetOrganizationLevelIndex(TypeId);
        int parentTypeId = GetOrganizationLevelByStructureId(StructureId);
        childLevel = parentTypeId + 1;
        return childLevel;
    }

    private int GetOrganizationLevelIndex(int TypeId)
    {
        DataTable dtOrganizationIndex = objOrganizationStructure.GetOrganizationLevelIndexByOrganizationTypeId(TypeId);

        if (dtOrganizationIndex.Rows.Count > 0)
        {
            return Convert.ToInt32(dtOrganizationIndex.Rows[0]["OrganizationalIndex"].ToString());
        }
        else
        {
            return 0;
        }
    }

    private int GetOrganizationLevelByStructureId(int StructureId)
    {
        DataTable dtPatentOrganizationIndex = objOrganizationStructure.GetOrganizationStructure(StructureId, false);

        if (dtPatentOrganizationIndex.Rows.Count > 0)
        {
            int TypeId = Convert.ToInt32(dtPatentOrganizationIndex.Rows[0]["OrganizationTypeID"].ToString());
            return GetOrganizationLevelIndex(TypeId);
        }
        else
        {
            return 0;
        }
    }   
}