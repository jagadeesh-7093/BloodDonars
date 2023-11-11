using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BloodDonars
{
    public partial class AddDetails : System.Web.UI.Page
    {
        void getdata()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
            string q = "proc_Displaydonr";

            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "donar");
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                getdata();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CmdDelete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Label l1 = (Label)row.FindControl("Label1");
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
                con.Open();
                string q = "porc_DeleteDonar";
                SqlCommand cmd = new SqlCommand(q, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@a", l1.Text);
                cmd.ExecuteNonQuery();
                con.Close();

                
            }
            else if (e.CommandName == "CmdEdit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Label l1 = (Label)row.FindControl("Label1");
                Label l2 = (Label)row.FindControl("Label2");
                Label l3 = (Label)row.FindControl("Label3");
                Label l4 = (Label)row.FindControl("Label4");
                Label l5 = (Label)row.FindControl("Label5");
                Label l6 = (Label)row.FindControl("Label6");
                Session["uid"] = l1.Text;
                Session["uname"] = l2.Text;
                Session["bg"] = l3.Text;
                Session["phno"] = l4.Text;
                Session["email"] = l5.Text;
                Session["city"] = l6.Text;
                Session["Button"] = "Update";
                Server.Transfer("register.aspx");
            }
        }
    }
}