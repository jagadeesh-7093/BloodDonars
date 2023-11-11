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
    public partial class search : System.Web.UI.Page
    {
        void getstates()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
            string q = "proc_DisplayStates";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "state");
            DropDownList1.DataSource = ds;
            DropDownList1.DataTextField = "sname";
            DropDownList1.DataValueField = "sid";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "--select");
        }
        void getcities()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
            string q = "proc_Displaycities";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@a", DropDownList1.SelectedItem.Value);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "city");
            DropDownList2.DataSource = ds;
            DropDownList2.DataTextField = "cname";
            //DropDownList1.DataValueField = "sid";
            DropDownList2.DataBind();
            //  DropDownList2.Items.Insert(0, "--select");
        }
        void getBloodgroup()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
            string q = "proc_Displayblood";
            SqlDataAdapter da = new SqlDataAdapter(q, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Blood");
            DropDownList3.DataSource = ds;
            DropDownList3.DataTextField = "bname";
            //DropDownList1.DataValueField = "sid";
            DropDownList3.DataBind();
            DropDownList3.Items.Insert(0, "--select");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                getstates();
                getBloodgroup();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
            
            String q = "proc_SearchDonar";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@a", DropDownList3.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@b", DropDownList1.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@c", DropDownList2.SelectedItem.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getcities();
            DropDownList2.Items.Insert(0, "--select");
        }
    }
}