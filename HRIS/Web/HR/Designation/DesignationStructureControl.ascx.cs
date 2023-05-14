using System;
using System.Data;
using System.Web.UI.WebControls;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class HR_Designation_DesignationStructureControl : System.Web.UI.UserControl
{
    DataTable dt;
    Designation objDesignation = new Designation();

    #region Tree View Methods

    private void PopulateTreeView()
    {
        if (tvDesignationStuctureUserControl.Nodes.Count > 0)
        {
            tvDesignationStuctureUserControl.Nodes.Clear();
        }

        PopulateRootLevel();
    }

    private void PopulateRootLevel()
    {
        dt = objDesignation.GetDesignationStructure(0, Convert.ToInt32(Session["CompanyId"]));
        PopulateNodes(dt, tvDesignationStuctureUserControl.Nodes);
    }

    private void PopulateNodes(DataTable Table, RadTreeNodeCollection Nodes)
    {
        foreach (DataRow row in Table.Rows)
        {
            RadTreeNode NewNode = new RadTreeNode();
            NewNode.Text = row["DesignationName"].ToString();
            NewNode.Value = row["DesignationStuctureID"].ToString();
            Nodes.Add(NewNode);
        }
    }

    private void PopulateSubLevel(int ParentId, RadTreeNode ParentNode)
    {
        dt = objDesignation.GetDesignationStructure(ParentId, Convert.ToInt32(Session["CompanyId"]));
        PopulateNodes(dt, ParentNode.Nodes);
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CompanyId"] == null)
        {
            Response.Redirect("~/SessionTimeout.aspx?DoRedirect=" + System.Web.HttpContext.Current.Request.Url.AbsolutePath);
        }

        if (!IsPostBack)
        {
            PopulateRootLevel();

            ViewState["IsCreated"] = true;
        }
        else
        {
            if (Session["IsDesignationStructureChanged"] != null)
            {
                if (Convert.ToBoolean(Session["IsDesignationStructureChanged"]) == true)
                {
                    if (tvDesignationStuctureUserControl.Nodes.Count > 0)
                    {
                        tvDesignationStuctureUserControl.Nodes.Clear();
                        ViewState["IsCreated"] = false;
                    }
                    PopulateRootLevel();
                    ViewState["IsCreated"] = true;
                    Session["IsDesignationStructureChanged"] = null;
                }
            }
        }
    }

    public void TreeViewReLoad()
    {
        if (tvDesignationStuctureUserControl.Nodes.Count > 0)
        {
            tvDesignationStuctureUserControl.Nodes.Clear();
        }

        PopulateRootLevel();
        tvDesignationStuctureUserControl.DataBind();
    }

    protected void tvDesignationStuctureUserControl_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        hfDesignationId.Value = tvDesignationStuctureUserControl.SelectedNode.Value;
        hfDesignationName.Value = tvDesignationStuctureUserControl.SelectedNode.Text;

        try
        {
            TextBox txtParentStructureType = (TextBox)Parent.FindControl("txtRootLevel");
            txtParentStructureType.Text = tvDesignationStuctureUserControl.SelectedNode.Text;

            HiddenField hfParentId = (HiddenField)Parent.FindControl("hfParentId");
            hfParentId.Value = tvDesignationStuctureUserControl.SelectedNode.Value;
        }
        catch
        {
            try
            {
                Label lblDesignation = (Label)Parent.FindControl("lblDesignation");
                lblDesignation.Text = tvDesignationStuctureUserControl.SelectedNode.Text;

                HiddenField hfDesignationStucture = (HiddenField)Parent.FindControl("hfDesignationStucture");
                hfDesignationStucture.Value = tvDesignationStuctureUserControl.SelectedNode.Value;
            }
            catch { }
        }
    }

    protected void tvDesignationStuctureUserControl_NodeCreated(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        if (ViewState["IsCreated"] != null)
        {
            if (Convert.ToBoolean(ViewState["IsCreated"].ToString()) == true)
            {
                return;
            }
        }

        PopulateSubLevel(Convert.ToInt32(e.Node.Value), e.Node);
        tvDesignationStuctureUserControl.ExpandAllNodes();
    }

    public void reLoad()
    {
        ViewState["IsCreated"] = false;
        PopulateTreeView();
        ViewState["IsCreated"] = true;
        //tvDesignationStuctureUserControl.DataBind();
    }
}