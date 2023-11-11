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
    public partial class register : System.Web.UI.Page
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
            SqlCommand cmd =new SqlCommand(q, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@a", DropDownList1.SelectedItem.Value);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "city");
            DropDownList2.DataSource = ds;
            DropDownList2.DataTextField = "cname";
            //DropDownList1.DataValueField = "sid";
            DropDownList2.DataBind();
            DropDownList2.Items.Insert(0, "--select");
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
            string b = "";
            
            if (IsPostBack == false)
            {
                getstates();
                getBloodgroup();
                b = Session["Button"].ToString();
                if (b == "Update")
                {
                    Button1.Text = "Update";
                    TextBox1.Text = Session["uname"].ToString();
                    DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByText(Session["city"].ToString()));
                    DropDownList3.SelectedIndex = DropDownList3.Items.IndexOf(DropDownList3.Items.FindByText(Session["bg"].ToString()));
                    TextBox4.Text = Session["phno"].ToString();
                    TextBox5.Text = Session["email"].ToString();
                }
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cs"].ToString());
            con.Open();
            string s1 = "";
            string s2 = "";
            string s3 = "";
            string s4 = "";
            string s5 = "";
            if (RadioButton1.Checked == true)
            {
                s1 = RadioButton1.Text;
            }
            else
            {
                s1 = RadioButton2.Text;
            }
            if (CheckBox1.Checked == true)
            {
                s2 = CheckBox1.Text;
            }
            if (CheckBox2.Checked == true)
            {
                s2 = s2 + " " + CheckBox2.Text;
            }
            if (CheckBox3.Checked == true)
            {
                s2 = s2 + " " + CheckBox3.Text;
            }
            //s3 = DropDownList1.SelectedItem.Text;
           // s4 = DropDownList2.SelectedItem.Text;
            //s5 = DropDownList3.SelectedItem.Text;
            string q = "";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.CommandType = CommandType.StoredProcedure;

            if (Button1.Text == "Register")
            {
                q = "proc_InsertDonar";
                cmd.Parameters.AddWithValue("@a", TextBox1.Text);
                cmd.Parameters.AddWithValue("@b", TextBox2.Text);
                cmd.Parameters.AddWithValue("@c", TextBox3.Text);
                cmd.Parameters.AddWithValue("@d", s1);
                cmd.Parameters.AddWithValue("@j", s2);
                cmd.Parameters.AddWithValue("@e", DropDownList1.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@f", DropDownList2.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@g", DropDownList3.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@h", TextBox4.Text);
                cmd.Parameters.AddWithValue("@i", TextBox5.Text);


            }
            else
            {
                q = "porc_Update1Don";
                cmd.Parameters.AddWithValue("@a",Session["uid"].ToString());
                cmd.Parameters.AddWithValue("@b", TextBox1.Text);
                cmd.Parameters.AddWithValue("@c",DropDownList3.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@d", TextBox4.Text);
                cmd.Parameters.AddWithValue("@e", TextBox5.Text);
                cmd.Parameters.AddWithValue("@f",DropDownList2.SelectedItem.Text);

                
            }
           cmd.CommandText = q;
               cmd.ExecuteNonQuery();
            if (Button1.Text == "Update")
            {
                Server.Transfer("AddDetails.aspx");
            }
            
            con.Close();
            Button1.Text = "Register";
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            getcities();
            DropDownList2.Items.Insert(0, "--select");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
            CheckBox1.Checked = CheckBox2.Checked = CheckBox3.Checked = false;
            DropDownList1.SelectedItem.Text = "--select--";
            DropDownList2.SelectedItem.Text = "--select";
            DropDownList3.SelectedItem.Text = "--select--";
        }
    }
}