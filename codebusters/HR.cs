using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace codebusters
{
    public partial class HR : Form
    {
        MySqlConnection connection;
        private string userfname;
        private string userlname;
        private int HRid;

        DateTime tempdate = new DateTime();

        int agentIDadded;
        int agentedited;

        XmlTextReader reader = new XmlTextReader("data.xml");

        private void connect()
        {
            string connectionString = "";

            string[] names = { "SERVER", "database", "uid", "password" };
            string[] values = new string[3];
            for (int i = 0; i < names.Length; i++)
            {
                reader.ReadToFollowing(names[i]);
                reader.Read();
                connectionString += names[i] + "=" + reader.Value + ";";
            }
            connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
            }
            if (connection != null)
                connection.Close();
        }


        private void HR_Load(object sender, EventArgs e)
        {
            connect();
        }


        public HR(string HRfirstname, string HRlastname, int HRloggedinid)
        {
            InitializeComponent();

            userfname = HRfirstname;
            userlname = HRlastname;
            HRid = HRloggedinid;
            loggedinHR.Text = userfname + " " + userlname;
        }

        private void exitform(object sender, FormClosingEventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
        }

        private void Agentadd_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                 //validation here
                //-------------------------------
                if ((hragentfn.Text.Trim() == "") ||
                    (hragentln.Text.Trim() == "") ||
                    (hragentphone.Text.Trim() == "") ||
                    (hragentgovid.Text.Trim() == "") ||
                    (hragentaddress.Text.Trim() == ""))
                {
                    MessageBox.Show("All fields must be filled");
                    return;
                }
                //validation completed

                //when validation is passed continue here:

                connection.Open();
                string nameOfProcedure = "add_agent";
                MySqlCommand cmd = new MySqlCommand(nameOfProcedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("agentfirstname", hragentfn.Text.Trim());
                cmd.Parameters["agentfirstname"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("agentlastname", hragentln.Text.Trim());
                cmd.Parameters["agentlastname"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("agentphone", hragentphone.Text.Trim());
                cmd.Parameters["agentphone"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("agentgovid", hragentgovid.Text.Trim());
                cmd.Parameters["agentgovid"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("agentaddress", hragentaddress.Text.Trim());
                cmd.Parameters["agentaddress"].Direction = ParameterDirection.Input;
                                
                cmd.Parameters.AddWithValue("agentemployed", "Y");
                cmd.Parameters["agentemployed"].Direction = ParameterDirection.Input;                 

                cmd.Parameters.AddWithValue("agentpw", hragentpw.Text.Trim());
                cmd.Parameters["agentpw"].Direction = ParameterDirection.Input;

                DateTime thisDay = DateTime.Today;

                cmd.Parameters.AddWithValue("agentemployeddate", thisDay);
                cmd.Parameters["agentemployeddate"].Direction = ParameterDirection.Input;

                // output the id of the added agent.

                MySqlParameter retId = new MySqlParameter("returnId", MySqlDbType.Int16);
                retId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retId);

                cmd.ExecuteNonQuery();

                connection.Close();

                agentIDadded = 0;
                agentIDadded = int.Parse(cmd.Parameters["returnId"].Value.ToString());
                if(agentIDadded > 0)
                {
                    MessageBox.Show("Agent added with id: " + agentIDadded);
                }
                else
                {
                    MessageBox.Show("An Error Occurred");
                }
                HRreset_Click(sender, e);
            }
        }

        private void Agentsearch_Click(object sender, EventArgs e)
        {
            int searchparamcount = 0;            
            string query = "select * from Agent where ";

            if(!String.IsNullOrEmpty(hragentfn.Text))
            {
                query += "Agent_fname =" + "\"" + hragentfn.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(hragentln.Text))
            {
                if(searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Agent_lname =" + "\"" + hragentln.Text.ToString() + "\"";
                searchparamcount++;
            }            
            if (!String.IsNullOrEmpty(hragentphone.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Agent_phone =" + "\"" + hragentphone.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(hragentgovid.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Agent_govid =" + "\"" + hragentgovid.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(hragentaddress.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Agent_address =" + "\"" + hragentaddress.Text.ToString() + "\"";
                searchparamcount++;
            }

            if(searchparamcount == 0)
            {
                if(showall.Checked)
                {
                    query = "select * from Agent";                    
                }
                else
                {
                    query = "select * from Agent where Employed = \"Y\"";
                }
            }
            else
            {
                if (showall.Checked)
                {
                    //query += " and Employed = \"Y\"";
                }
                else
                {
                    query += " and Employed = \"Y\"";
                }

            }
           
            //end validation + query composing

            try 
            { 
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter mcmd = new MySqlDataAdapter();
            MySqlDataReader reader;
            mcmd.SelectCommand = cmd;

            reader = cmd.ExecuteReader();

            HRdatagrid.Rows.Clear();

                while (reader.Read())
                {
                    this.HRdatagrid.Rows.Add(
                         reader["Agent_ID"].ToString(),
                         reader["Agent_fname"].ToString(),
                         reader["Agent_lname"].ToString(),
                         reader["Agent_phone"].ToString(),
                         reader["Agent_govid"].ToString(),
                         reader["Agent_address"].ToString(),
                         reader["Employed"].ToString(),
                         reader["Agent_PW"].ToString(),
                         ((DateTime)reader["Agent_Date_Employed"]).ToString("d")
                         );          
                } 
            }
            catch
            {
                MessageBox.Show("Insert less parameters for more results");
            }           

            connection.Close();            
        }

        private void HRdatagridcell(object sender, DataGridViewCellEventArgs e)
        {
            HRagentID.Clear();

            try
            {
                if (e.RowIndex >= 0)
                {
                HRagentID.Text = HRdatagrid.Rows[e.RowIndex].Cells["AgentID"].Value.ToString();
                hragentfn.Text = HRdatagrid.Rows[e.RowIndex].Cells["AgentFirstName"].Value.ToString();
                hragentln.Text = HRdatagrid.Rows[e.RowIndex].Cells["AgentLastName"].Value.ToString();
                hragentphone.Text = HRdatagrid.Rows[e.RowIndex].Cells["AgentPhone"].Value.ToString();
                hragentgovid.Text = HRdatagrid.Rows[e.RowIndex].Cells["Agentgovid"].Value.ToString();
                hragentaddress.Text = HRdatagrid.Rows[e.RowIndex].Cells["AgentAddress"].Value.ToString();
                hragentpw.Text = HRdatagrid.Rows[e.RowIndex].Cells["PW"].Value.ToString();

                    if (HRdatagrid.Rows[e.RowIndex].Cells["Employed"].Value.ToString() == "Y")
                    {
                        hragentemployed.Checked = true;
                    }
                    else
                    {
                        hragentemployed.Checked = false;
                    }
                //tempdate = HRdatagrid.Rows[e.RowIndex].Cells["DateEmployed"].Value.ToString();
                }
            }
            catch
            {

            }
            Agentadd.Hide();
            
        }

        private void HRreset_Click(object sender, EventArgs e)
        {
            HRagentID.Clear();
            hragentfn.Clear();
            hragentln.Clear();
            hragentphone.Clear();
            hragentgovid.Clear();
            hragentaddress.Clear();
            hragentpw.Clear();
            hragentemployed.Checked = false;

            HRdatagrid.Rows.Clear();

            Agentadd.Show();

        }

        private void Agentedit_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                //validation here
                //-------------------------------
                if ((hragentfn.Text.Trim() == "") ||
                    (hragentln.Text.Trim() == "") ||
                    (hragentphone.Text.Trim() == "") ||
                    (hragentgovid.Text.Trim() == "") ||
                    (hragentaddress.Text.Trim() == ""))
                {
                    MessageBox.Show("All fields must be filled");
                    return;
                }
                //validation completed

                //when validation is passed continue here:

                connection.Open();
                string nameOfProcedure = "edit_agent";
                MySqlCommand cmd = new MySqlCommand(nameOfProcedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("agentid", HRagentID.Text.Trim());
                cmd.Parameters["agentid"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("agentfirstname", hragentfn.Text.Trim());
                cmd.Parameters["agentfirstname"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("agentlastname", hragentln.Text.Trim());
                cmd.Parameters["agentlastname"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("agentphone", hragentphone.Text.Trim());
                cmd.Parameters["agentphone"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("agentgovid", hragentgovid.Text.Trim());
                cmd.Parameters["agentgovid"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("agentaddress", hragentaddress.Text.Trim());
                cmd.Parameters["agentaddress"].Direction = ParameterDirection.Input;

                if (hragentemployed.Checked)
                {
                    cmd.Parameters.AddWithValue("agentemployed", "Y");
                    cmd.Parameters["agentemployed"].Direction = ParameterDirection.Input;

                    DateTime thisDay = DateTime.Today;

                    cmd.Parameters.AddWithValue("agentemployeddate", thisDay);
                    cmd.Parameters["agentemployeddate"].Direction = ParameterDirection.Input;
                }
                else
                {
                    cmd.Parameters.AddWithValue("agentemployed", "N");
                    cmd.Parameters["agentemployed"].Direction = ParameterDirection.Input;

                    DateTime thisDay = DateTime.Today;

                    cmd.Parameters.AddWithValue("agentemployeddate", thisDay);
                    cmd.Parameters["agentemployeddate"].Direction = ParameterDirection.Input;
                }

                cmd.Parameters.AddWithValue("agentpw", hragentpw.Text.Trim());
                cmd.Parameters["agentpw"].Direction = ParameterDirection.Input;

                // output the id of the added agent.

                MySqlParameter retId = new MySqlParameter("rowcount", MySqlDbType.Int16);
                retId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retId);

                cmd.ExecuteNonQuery();

                connection.Close();

                agentedited = 0;
                agentedited = int.Parse(cmd.Parameters["rowcount"].Value.ToString());
                if (agentedited > 0)
                {
                    MessageBox.Show(agentedited + " Agent Record(s) updated");
                }
                else
                {
                    MessageBox.Show("no records updated");
                }
                HRreset_Click(sender, e);
            }
        }              
    }
}
