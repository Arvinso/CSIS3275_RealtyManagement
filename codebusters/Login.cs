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
    public partial class Login : Form
    {
        private MySqlConnection connection;
        //private DataSet ds;
        private MySqlDataAdapter mcmd;
        private List<person> personlist;
        XmlTextReader reader = new XmlTextReader("data.xml");

        public Login()
        {
            InitializeComponent();
            connect();
            //Image.FromFile(logo);

        }

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

        private void Login_click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(pw.Text.ToString()))
            {
                MessageBox.Show("Please Fill in Password");
            }
            else
            {
                if (usn_type.Text.ToString() == "HR" || usn_type.Text.ToString() == "Agent" || usn_type.Text.ToString() == "Manager")
                {
                    string usertype = usn_type.Text.ToString();
                    string username = usn_select.Text.ToString();
                    string[] stringSeparators = new string[] { " " };
                    string[] nametokens;

                    nametokens = username.Split(stringSeparators,
                        StringSplitOptions.RemoveEmptyEntries);

                    string paswoord = pw.Text.ToString();

                    string query = "";

                    if(usertype.ToString() == "HR" || usertype.ToString() == "Manager")
                    {
                        query = "select user_ID from users where user_fname = \"" + nametokens[0] + "\" and user_lname = \"" + nametokens[1] +
                        "\" and user_pass = \"" + paswoord + "\"";
                    }
                    else if(usertype.ToString() == "Agent")
                    {
                        query = "select Agent_ID from Agent where Agent_fname = \"" + nametokens[0] + "\" and Agent_lname = \"" + nametokens[1] +
                        "\" and Agent_PW = \"" + paswoord + "\"";
                    }
                    
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter mcmd = new MySqlDataAdapter();
                    MySqlDataReader reader;
                    mcmd.SelectCommand = cmd;
                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {                        
                        switch (usertype)
                        {
                            case "HR": this.Hide();
                                HR newHRform = new HR(nametokens[0], nametokens[1], (int)reader.GetValue(0));
                                newHRform.ShowDialog();
                                break;
                            case "Agent": this.Hide();
                                Agent newAgentform = new Agent(nametokens[0],nametokens[1], (int)reader.GetValue(0));
                                newAgentform.ShowDialog();
                                break;
                            case "Manager": this.Hide();
                                Manager newManagerform = new Manager(nametokens[0], nametokens[1], (int)reader.GetValue(0));
                                newManagerform.ShowDialog();
                                break;
                            default: MessageBox.Show("invalid usertype");
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("password incorrect");
                    }
                    connection.Close();                    
                }
                else
                {
                    MessageBox.Show("Invalid usertype");
                }
            }     
        }
        private void Search_Member(object sender, EventArgs e)
        {
            try
            {
                string selectedtype = usn_type.SelectedItem.ToString();

                if(selectedtype != "Agent")
                {
                    usn_select.Items.Clear();
                    connection.Open();
                    string query = "select user_ID,user_fname,user_lname from users where user_Type ='" + selectedtype + "'";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter mcmd = new MySqlDataAdapter();
                    MySqlDataReader reader;
                    mcmd.SelectCommand = cmd;

                    //DataSet ds = new DataSet();
                    //mcmd.Fill(ds);
                
                    reader = cmd.ExecuteReader();
                
                    string[] data = new string[3];
                
                    while(reader.Read())
                    {
                        data[0] = reader.GetString(0);
                        data[1] = reader.GetString(1);
                        data[2] = reader.GetString(2);

                        usn_select.Items.Add(data[1]+" "+data[2]);
                    }
                }
                else
                {
                    usn_select.Items.Clear();
                    connection.Open();
                    string query = "select Agent_ID,Agent_fname,Agent_lname from Agent where Employed =\"Y\"";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter mcmd = new MySqlDataAdapter();
                    MySqlDataReader reader;
                    mcmd.SelectCommand = cmd;

                    //DataSet ds = new DataSet();
                    //mcmd.Fill(ds);

                    reader = cmd.ExecuteReader();

                    string[] data = new string[3];

                    while (reader.Read())
                    {
                        data[0] = reader.GetString(0);
                        data[1] = reader.GetString(1);
                        data[2] = reader.GetString(2);

                        usn_select.Items.Add(data[1] + " " + data[2]);
                    }
                }           


                /*
                DataSet orig = new DataSet();
                DataSet made = new DataSet();

                made.Tables.Add("Userid");
                made.Tables.Add("Name");
                
                //var dict = new Dictionary<Guid, string>();
                mcmd.Fill(orig, "user_ID");
                              

                usn_select.DataSource = orig.Tables["user_ID"];
                usn_select.DisplayMember = "user_fname";     
                usn_select.ValueMember = "user_ID";                
                */
                connection.Close();
            }
            catch(Exception ex)
            {

                MessageBox.Show("Error occured!");
            }
        }

        private void clearbutton_Click(object sender, EventArgs e)
        {
            usn_type.ResetText();
            usn_select.ResetText();            
        }

    }
}
