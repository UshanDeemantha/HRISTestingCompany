using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class Organization_OrganizationStuctureCom : System.Web.UI.UserControl
{
    DataTable dt;
    Organization objOrganizationStructure = new Organization();


    #region Tree Viev Methods

    private void PopulateTreeView()
    {
        if (tvOrganizationStucture.Nodes.Count > 0)
        {
            tvOrganizationStucture.Nodes.Clear();
        }

        PopulateRootLevel();
    }


    public void PopulateTreeViewCompany()
    {
        if (tvOrganizationStucture.Nodes.Count > 0)
        {
            tvOrganizationStucture.Nodes.Clear();
        }

        PopulateRootLevel();
    }
    private void PopulateRootLevel()
    {
        //if (Session["IsOk"] == null)
        //{

        //    if (tvOrganizationStucture.Nodes.Count > 0)
        //    {
        //        tvOrganizationStucture.Nodes.Clear();
        //    }
        //}
        if(Session["StartingRecreatingOrg"] != null)
        {
            if (Convert.ToBoolean(Session["StartingRecreatingOrg"]) == true)
            {
                tvOrganizationStucture.Nodes.Clear();
                dt = objOrganizationStructure.GetOrganizationStructureRootLevel(Convert.ToInt32(Session["NewCompanyId"]), 0);
                PopulateNodes(dt, tvOrganizationStucture.Nodes);
                Session["StartingRecreatingOrg"] = false;
            }
        }
        
    }

    private void PopulateNodes(DataTable Table, RadTreeNodeCollection Nodes)
    {
        foreach (DataRow row in Table.Rows)
        {
            RadTreeNode NewNode = new RadTreeNode();
            NewNode.Text = row["OrganizationTypeName"].ToString();
            NewNode.Value = row["OrgStructureID"].ToString();
            Nodes.Add(NewNode);
            //NewNode.PopulateOnDemand = (Convert.ToInt32(row["ChildNodeCount"].ToString()) > 0);
        }
    }

    private void PopulateSubLevel(int ParentId, RadTreeNode ParentNode)
    {
        if (Session["StartingRecreatingOrg"] != null)
        {
            if (Convert.ToBoolean(Session["StartingRecreatingOrg"]) == true)
            {
                dt = objOrganizationStructure.GetOrganizationStructureRootLevel(Convert.ToInt32(Session["NewCompanyId"]), ParentId);
                PopulateNodes(dt, ParentNode.Nodes);
            }
        }
    }

    #endregion

    public void Page_Load(object sender, EventArgs e)
    {
        if (Session["CompanyId"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }

        
        {
            PopulateRootLevel();

            ViewState["IsCreated"] = true;
        }
    }

    public void reLoad()
    {
      
        PopulateRootLevel();
        tvOrganizationStucture.DataBind();  
        if (tvOrganizationStucture.Nodes.Count > 0)
        {
            tvOrganizationStucture.Nodes.Clear();
        }

             
    }

    public void reLoadNew(int CompanyId)
    {

        PopulateRootLevel();
       
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
        }
        catch
        {
            try
            {
                Label lblOrganization = (Label)Parent.FindControl("lblOrganization");
                lblOrganization.Text = tvOrganizationStucture.SelectedNode.Text;
                
                HiddenField hfOrgainzationStuctcureId = (HiddenField)Parent.FindControl("hfOrganizationStructure");
                hfOrgainzationStuctcureId.Value = tvOrganizationStucture.SelectedNode.Value;
            }
            catch{}
        }
    }

    protected void tvOrganizationStucture_NodeCreated(object sender, RadTreeNodeEventArgs e)
    {
        if (ViewState["IsCreated"] != null)
        {
            if (Convert.ToBoolean(ViewState["IsCreated"].ToString()) == true)
            {
                //return;
            }
        }

        PopulateSubLevel(Convert.ToInt32(e.Node.Value), e.Node);
        tvOrganizationStucture.ExpandAllNodes();
    }
}
