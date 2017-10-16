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
    public partial class Agent : Form
    {
        MySqlConnection connection;
        private string userfname;
        private string userlname;
        private int agentid;

        //private int newregcustomerid;
        private int newregpropertyid;

        private int regcustomerid;

        private int customeredited;

        private int propid;

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

        private void LoadAgent(object sender, EventArgs e)
        {
            connect();

            //to initialize listview in tab3 
            
            Customer_owned_properties.View = View.Details;
            Customer_owned_properties.GridLines = true;
            Customer_owned_properties.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            Customer_owned_properties.FullRowSelect = true;

            Customer_owned_properties.Columns.Add("Property ID", 70, HorizontalAlignment.Left);
            Customer_owned_properties.Columns.Add("Property Type", 110, HorizontalAlignment.Left);
            Customer_owned_properties.Columns.Add("Property Area", 90, HorizontalAlignment.Left);
            Customer_owned_properties.Columns.Add("Property Location", 150, HorizontalAlignment.Left);
            Customer_owned_properties.Columns.Add("Property Price", 90, HorizontalAlignment.Left);
            Customer_owned_properties.Columns.Add("Property Registration Date", 150, HorizontalAlignment.Left);            
            
        }
        public Agent(string agentfirstname, string agentlastname, int loggedinagentid)
        {
            InitializeComponent();
            userfname = agentfirstname;
            userlname = agentlastname;
            agentid = loggedinagentid;
            loggedinagent.Text = userfname + " " + userlname;
            propertiesloggedinas.Text = userfname + " " + userlname;
            customersloggedinas.Text = userfname + " " + userlname;
        }

        private void exitform(object sender, FormClosingEventArgs e)
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
        }

        private void Reg_prop_click(object sender, EventArgs e)
        {
            if (connection != null)
            {                
                //validation here
                //-------------------------------
                if ((agent_customer_fn.Text.Trim() == "") ||
                    (agent_customer_ln.Text.Trim() == "") ||
                    (agent_customer_phone.Text.Trim() == "") ||
                    (agent_customer_govid.Text.Trim() == "") ||
                    (agent_customer_address.Text.Trim() == "") ||
                    
                    (agent_prop_type.Text.Trim() == "") ||
                    (agent_prop_livarea.Text.Trim() == "") ||
                    (agent_prop_bedrooms.Text.Trim() == "") ||
                    (agent_prop_bathrooms.Text.Trim() == "") ||
                    (agent_prop_yearbuilt.Text.Trim() == "") ||
                    (agent_prop_location.Text.Trim() == "") ||
                    (agent_prop_price.Text.Trim() == ""))
                {
                    MessageBox.Show("All fields must be filled");
                    return;
                }
                //end validation
                //-------------------------------
               
                else // new customer adding here.
                {
                    if(isexistingcust.Checked == true)
                    {
                        if (custid.Text.Trim() == "")
                        {
                            MessageBox.Show("Please select existing customer first");
                            return;
                        }
                        else
                        {
                            regcustomerid = int.Parse(custid.Text.ToString().Trim());
                        }                                   
                    }
                    else
                    {
                        //////////////////////
                        //////////////////////
                        //////////////////////
                        /////////////////// new customer added here
                        ///////////////////
                        ///////////////////
                        connection.Open();
                        string nameOfProcedure = "add_customer";
                        MySqlCommand add_cust_cmd = new MySqlCommand(nameOfProcedure, connection);
                        add_cust_cmd.CommandType = CommandType.StoredProcedure;
                        //complete the parameters needed to insert
                        add_cust_cmd.Parameters.AddWithValue("customerfirstname", agent_customer_fn.Text.Trim());
                        add_cust_cmd.Parameters["customerfirstname"].Direction = ParameterDirection.Input;
                        add_cust_cmd.Parameters.AddWithValue("customerlastname", agent_customer_ln.Text.Trim());
                        add_cust_cmd.Parameters["customerlastname"].Direction = ParameterDirection.Input;
                        add_cust_cmd.Parameters.AddWithValue("customerphone", agent_customer_phone.Text.Trim());
                        add_cust_cmd.Parameters["customerphone"].Direction = ParameterDirection.Input;
                        add_cust_cmd.Parameters.AddWithValue("customergovid", agent_customer_govid.Text.Trim());
                        add_cust_cmd.Parameters["customergovid"].Direction = ParameterDirection.Input;
                        add_cust_cmd.Parameters.AddWithValue("customeraddress", agent_customer_address.Text.Trim());
                        add_cust_cmd.Parameters["customeraddress"].Direction = ParameterDirection.Input;
                        //output the ID
                        MySqlParameter retcId = new MySqlParameter("returncustId", MySqlDbType.Int16);
                        retcId.Direction = ParameterDirection.Output;
                        add_cust_cmd.Parameters.Add(retcId);
                        add_cust_cmd.ExecuteNonQuery();
                        connection.Close();
                        
                        regcustomerid = int.Parse(add_cust_cmd.Parameters["returncustId"].Value.ToString()); // we need this to insert in the registration table                        
                    }
                    //output the ID
                   
                        //////////////////////
                        //////////////////////
                        //////////////////////
                    /////////////////// new property added here
                    ///////////////////
                    ///////////////////
                    if(connection != null)
                    {
                        connection.Open();
                    }                    
                    String add_prop_identifier = "add_property";
                    MySqlCommand add_prop_cmd = new MySqlCommand(add_prop_identifier, connection);
                    add_prop_cmd.CommandType = CommandType.StoredProcedure;

                    add_prop_cmd.Parameters.AddWithValue("agentid", agentid);
                    add_prop_cmd.Parameters["agentid"].Direction = ParameterDirection.Input;

                    add_prop_cmd.Parameters.AddWithValue("propertytype", agent_prop_type.Text.Trim());
                    add_prop_cmd.Parameters["propertytype"].Direction = ParameterDirection.Input;

                    add_prop_cmd.Parameters.AddWithValue("propertyarea", agent_prop_livarea.Text.Trim());
                    add_prop_cmd.Parameters["propertyarea"].Direction = ParameterDirection.Input;

                    add_prop_cmd.Parameters.AddWithValue("propertybr", agent_prop_bedrooms.Text.Trim());
                    add_prop_cmd.Parameters["propertybr"].Direction = ParameterDirection.Input;

                    add_prop_cmd.Parameters.AddWithValue("propertyba", agent_prop_bathrooms.Text.Trim());
                    add_prop_cmd.Parameters["propertyba"].Direction = ParameterDirection.Input;

                    add_prop_cmd.Parameters.AddWithValue("propertyyear", agent_prop_yearbuilt.Text.Trim());
                    add_prop_cmd.Parameters["propertyyear"].Direction = ParameterDirection.Input;

                    add_prop_cmd.Parameters.AddWithValue("propertyloc", agent_prop_location.Text.Trim());
                    add_prop_cmd.Parameters["propertyloc"].Direction = ParameterDirection.Input;

                    add_prop_cmd.Parameters.AddWithValue("propertyprice", agent_prop_price.Text.Trim());
                    add_prop_cmd.Parameters["propertyprice"].Direction = ParameterDirection.Input;

                    MySqlParameter retpId = new MySqlParameter("returnpropId", MySqlDbType.Int16);
                    retpId.Direction = ParameterDirection.Output;
                    add_prop_cmd.Parameters.Add(retpId);

                    add_prop_cmd.ExecuteNonQuery();

                    connection.Close();

                    //MessageBox.Show(add_prop_cmd.Parameters["returnpropId"].Value.ToString());

                    newregpropertyid = int.Parse(add_prop_cmd.Parameters["returnpropId"].Value.ToString());                    

                    //////////////// registration of property and customer into registration table
                    ///////////////
                    ///////////////

                    if (connection != null)
                    {
                        connection.Open();
                    }         
                    String add_reg_identifier = "add_registration";
                    MySqlCommand add_reg_cmd = new MySqlCommand(add_reg_identifier, connection);
                    add_reg_cmd.CommandType = CommandType.StoredProcedure;

                    DateTime thisDay = DateTime.Today;

                    //Console.WriteLine(thisDay.ToString("d"));

                    add_reg_cmd.Parameters.AddWithValue("thisday", thisDay);
                    add_reg_cmd.Parameters["thisday"].Direction = ParameterDirection.Input;

                    add_reg_cmd.Parameters.AddWithValue("customerid", regcustomerid);
                    add_reg_cmd.Parameters["customerid"].Direction = ParameterDirection.Input;

                    add_reg_cmd.Parameters.AddWithValue("propertyid", newregpropertyid);
                    add_reg_cmd.Parameters["propertyid"].Direction = ParameterDirection.Input;

                    MySqlParameter retrId = new MySqlParameter("returnId", MySqlDbType.Int16);
                    retrId.Direction = ParameterDirection.Output;
                    add_reg_cmd.Parameters.Add(retrId);
                    add_reg_cmd.ExecuteNonQuery();

                    connection.Close();

                    int regIDadded = 0;
                    regIDadded = int.Parse(add_reg_cmd.Parameters["returnId"].Value.ToString());
                    if (regIDadded > 0)
                    {
                        MessageBox.Show("Registration added with id: " + regIDadded);
                    }
                    else
                    {
                        MessageBox.Show("An Error Occurred");
                    }
                    Resetregistration_Click(sender, e);
                }
            }
        }

        private void grayinout(object sender, EventArgs e)
        {
            if(isexistingcust.Checked)
            {
                agent_customer_fn.ReadOnly = true;
                agent_customer_ln.ReadOnly = true;
                agent_customer_phone.ReadOnly = true;
                agent_customer_govid.ReadOnly = true;
                agent_customer_address.ReadOnly = true;

                agent_customer_id.ReadOnly = false;
                existingcustfn.ReadOnly = false;
                existingcustln.ReadOnly = false;
                
            }
            else
            {
                agent_customer_fn.ReadOnly = false;
                agent_customer_ln.ReadOnly = false;
                agent_customer_phone.ReadOnly = false;
                agent_customer_govid.ReadOnly = false;
                agent_customer_address.ReadOnly = false;

                agent_customer_id.ReadOnly = true;
                existingcustfn.ReadOnly = true;
                existingcustln.ReadOnly = true;
            }
        }


        private void searchexistingcustomers_Click(object sender, EventArgs e)
        {
            int searchparamcount = 0;
            string query = "select * from Customer where ";

            if (!String.IsNullOrEmpty(agent_customer_id.Text))
            {
                query += "Customer_ID =" + "\"" + agent_customer_id.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(existingcustfn.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Cust_fname =" + "\"" + existingcustfn.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(existingcustln.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Cust_lname =" + "\"" + existingcustln.Text.ToString() + "\"";
                searchparamcount++;
            }


            if (searchparamcount == 0)
            {                
                query = "select * from Customer";                          
            }

            //end validation part. 

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataAdapter mcmd = new MySqlDataAdapter();
                MySqlDataReader reader;
                mcmd.SelectCommand = cmd;

                reader = cmd.ExecuteReader();

                EXCust_datagrid.Rows.Clear();

                while (reader.Read())
                {
                    this.EXCust_datagrid.Rows.Add(
                         reader["Customer_ID"].ToString(),
                         reader["Cust_fname"].ToString(),
                         reader["Cust_lname"].ToString(),
                         reader["Cust_phone"].ToString(),
                         reader["Cust_govid"].ToString(),
                         reader["Cust_address"].ToString()
                         );
                }
            }
            catch
            {
                MessageBox.Show("error occured");
            }
            connection.Close();
        }

        private void EXcustsearchdatagrid(object sender, DataGridViewCellEventArgs e)
        {
            custid.Clear();

            try
            {
                if (e.RowIndex >= 0)
                {
                custid.Text = EXCust_datagrid.Rows[e.RowIndex].Cells["CustomerID"].Value.ToString();

                regcustomerid = int.Parse(custid.Text.ToString().Trim());
    
                agent_customer_fn.Text = EXCust_datagrid.Rows[e.RowIndex].Cells["CustomerFirstName"].Value.ToString();
                agent_customer_ln.Text = EXCust_datagrid.Rows[e.RowIndex].Cells["CustomerLastName"].Value.ToString();
                agent_customer_phone.Text = EXCust_datagrid.Rows[e.RowIndex].Cells["CustomerPhone"].Value.ToString();
                agent_customer_govid.Text = EXCust_datagrid.Rows[e.RowIndex].Cells["CustomerGovID"].Value.ToString();
                agent_customer_address.Text = EXCust_datagrid.Rows[e.RowIndex].Cells["CustomerAddress"].Value.ToString();
                }
            }
            catch
            {
                
            }
        }

        private void Resetregistration_Click(object sender, EventArgs e)
        {
            agent_customer_id.Clear();
            custid.Clear();
            agent_customer_fn.Clear();
            agent_customer_ln.Clear();
            agent_customer_phone.Clear();
            agent_customer_govid.Clear();
            agent_customer_address.Clear();//////
            agent_prop_type.ResetText();
            agent_prop_livarea.Clear();
            agent_prop_bedrooms.ResetText();
            agent_prop_bathrooms.ResetText();
            agent_prop_yearbuilt.Clear();
            agent_prop_location.Clear();
            agent_prop_price.Clear();//////////
            existingcustfn.Clear();
            existingcustln.Clear();////////
            isexistingcust.Checked = false;//////////
            EXCust_datagrid.Rows.Clear();/////
        }

        //////////////////////////      Property TAB //////////////////////////////////////////////////////




        private void Searchproperty_Click(object sender, EventArgs e)
        {
            int searchparamcount = 0;

            string query = "Select P.*,  A.Agent_fname , A.Agent_lname, C.Customer_ID, C.Cust_fname, C.Cust_lname ";
            query += "from Property P inner join Agent A on A.Agent_ID = P.Agent_ID ";
            query += "inner join Registration R on R.Property_ID = P.Property_ID inner join Customer C on C.Customer_ID = R.Customer_ID where ";

            if (!String.IsNullOrEmpty(Agent_sproperty_ID.Text))
            {
                query += "Property_ID =" + "\"" + Agent_sproperty_ID.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Agent_sproperty_type.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Prop_type =" + "\"" + Agent_sproperty_type.Text.ToString() + "\"";
                searchparamcount++;
            }                       
            if (!String.IsNullOrEmpty(Agent_sproperty_livarea.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Prop_area =" + "\"" + Agent_sproperty_livarea.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Agent_sproperty_be.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Prop_Br =" + "\"" + Agent_sproperty_be.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Agent_sproperty_ba.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Prop_Ba =" + "\"" + Agent_sproperty_ba.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Agent_sproperty_year.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Prop_Yr =" + "\"" + Agent_sproperty_year.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Agent_sproperty_loc.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Prop_Loc =" + "\"" + Agent_sproperty_loc.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Agent_sproperty_price.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Prop_price =" + "\"" + Agent_sproperty_price.ToString() + "\"";
                searchparamcount++;
            }           


            if (searchparamcount == 0)
            {
            query = "Select P.*,T.Transaction_ID ,A.Agent_fname , A.Agent_lname, C.Customer_ID, C.Cust_fname, C.Cust_lname ";
            query += "from Property P inner join Agent A on A.Agent_ID = P.Agent_ID ";
            query += "inner join Registration R on R.Property_ID = P.Property_ID ";
            query += "inner join Customer C on C.Customer_ID = R.Customer_ID ";
            query += "left join Transaction T on T.Property_ID = P.Property_ID";
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

                propsearchdatagrid.Rows.Clear();

                while (reader.Read())
                {
                    this.propsearchdatagrid.Rows.Add(
                         reader["Property_ID"].ToString(),
                         reader["Prop_type"].ToString(),
                         reader["Prop_area"].ToString(),
                         reader["Prop_Br"].ToString(),
                         reader["Prop_Ba"].ToString(),
                         reader["Prop_Yr"].ToString(),
                         reader["Prop_Loc"].ToString(),
                         reader["Prop_price"].ToString(),
                         reader["Transaction_ID"].ToString(),
                         reader["Customer_ID"].ToString(),
                         reader["Cust_fname"].ToString(),
                         reader["Cust_lname"].ToString(),
                         reader["Agent_ID"].ToString(),
                         reader["Agent_fname"].ToString(),
                         reader["Agent_lname"].ToString()                         
                         );
                }
            }
            catch
            {
                MessageBox.Show("Insert less parameters for more results");
            }
            connection.Close();   
        }

        private void propsearchdatagridcell(object sender, DataGridViewCellEventArgs e)
        {
            /////////////////just to clean the form
            Agent_sproperty_agent.Clear();
            Agent_sproperty_ID.Clear();
            Agent_sproperty_custIDFN.Clear();
            Agent_sproperty_type.ResetText();
            Agent_sproperty_livarea.Clear();
            Agent_sproperty_be.ResetText();
            Agent_sproperty_ba.ResetText();
            Agent_sproperty_year.Clear();
            Agent_sproperty_loc.Clear();
            Agent_sproperty_price.Clear();
            issold.Checked = false;
            datesold.Clear();
            /////////////// end of clean form
            ///start populating here
            try
            {
                if (e.RowIndex >= 0)
                {
                    Agent_sproperty_ID.Text = propsearchdatagrid.Rows[e.RowIndex].Cells["PropertyID"].Value.ToString();

                    string custidfn = "ID: " + propsearchdatagrid.Rows[e.RowIndex].Cells["outputcustomerID"].Value.ToString() + " Name: "
                                                    + propsearchdatagrid.Rows[e.RowIndex].Cells["CustomerFN"].Value.ToString() + " "
                                                    + propsearchdatagrid.Rows[e.RowIndex].Cells["CustomerLN"].Value.ToString();

                    Agent_sproperty_custIDFN.Text = custidfn;

                    Agent_sproperty_type.Text = propsearchdatagrid.Rows[e.RowIndex].Cells["PropertyType"].Value.ToString();
                    Agent_sproperty_livarea.Text = propsearchdatagrid.Rows[e.RowIndex].Cells["PropertyArea"].Value.ToString();
                    Agent_sproperty_be.Text = propsearchdatagrid.Rows[e.RowIndex].Cells["PropertyBedrooms"].Value.ToString();
                    Agent_sproperty_ba.Text = propsearchdatagrid.Rows[e.RowIndex].Cells["PropertyBathrooms"].Value.ToString();
                    Agent_sproperty_year.Text = propsearchdatagrid.Rows[e.RowIndex].Cells["PropertyYear"].Value.ToString();
                    Agent_sproperty_loc.Text = propsearchdatagrid.Rows[e.RowIndex].Cells["PropertyLocation"].Value.ToString();
                    Agent_sproperty_price.Text = propsearchdatagrid.Rows[e.RowIndex].Cells["PropertyPrice"].Value.ToString();

                    string agentidfirstlastname = "ID: " + propsearchdatagrid.Rows[e.RowIndex].Cells["outputAgentID"].Value.ToString() + " Name: "
                                                    + propsearchdatagrid.Rows[e.RowIndex].Cells["AgentFN"].Value.ToString() + " "
                                                    + propsearchdatagrid.Rows[e.RowIndex].Cells["AgentLN"].Value.ToString();
                    Agent_sproperty_agent.Text = agentidfirstlastname;
            // end of populating 

                    //start filling sold status and date

                    if (String.IsNullOrEmpty(propsearchdatagrid.Rows[e.RowIndex].Cells["TransactionID"].Value.ToString()))
                    {
                        issold.Checked = false;
                        datesold.Clear();
                    }
                    else
                    {
                        issold.Checked = true;

                        string query = "select Trans_date from Transaction where Transaction_ID = \"" + propsearchdatagrid.Rows[e.RowIndex].Cells["TransactionID"].Value.ToString() + "\"";

                        try
                        {
                            connection.Open();
                            MySqlCommand cmd = new MySqlCommand(query, connection);
                            MySqlDataAdapter mcmd = new MySqlDataAdapter();
                            MySqlDataReader reader;
                            mcmd.SelectCommand = cmd;

                            reader = cmd.ExecuteReader();

                            if (reader.Read())
                            {
                                datesold.Text = ((DateTime)reader["Trans_date"]).ToString("d");
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Error Occured!");
                        }
                        connection.Close();  
                    }
                }
            }
            catch
            {
               
            }
            //end of filling sold status and date
        }

        private void ResetProperty_Click(object sender, EventArgs e)
        {
            Agent_sproperty_agent.Clear();
            Agent_sproperty_ID.Clear();
            Agent_sproperty_custIDFN.Clear();
            Agent_sproperty_type.ResetText();
            Agent_sproperty_livarea.Clear();
            Agent_sproperty_be.ResetText();
            Agent_sproperty_ba.ResetText();
            Agent_sproperty_year.Clear();
            Agent_sproperty_loc.Clear();
            Agent_sproperty_price.Clear();
            issold.Checked = false;
            datesold.Clear();
            propsearchdatagrid.Rows.Clear();
        }

        private void Editproperty_Click(object sender, EventArgs e)
        {

            if (connection != null)
            {
                //validation here
                //-------------------------------
                if ((Agent_sproperty_ID.Text.Trim() == "") ||
                    (Agent_sproperty_type.Text.Trim() == "") ||
                    (Agent_sproperty_livarea.Text.Trim() == "") ||
                    (Agent_sproperty_be.Text.Trim() == "") ||
                    (Agent_sproperty_ba.Text.Trim() == "") ||
                    (Agent_sproperty_year.Text.Trim() == "") ||
                    (Agent_sproperty_loc.Text.Trim() == "") ||
                    (Agent_sproperty_price.Text.Trim() == ""))
                {
                    MessageBox.Show("All fields must be filled");
                    return;
                }
                //validation completed

                //when validation is passed continue here:

                connection.Open();
                string nameOfProcedure = "edit_property";
                MySqlCommand cmd = new MySqlCommand(nameOfProcedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("propertyid", Agent_sproperty_ID.Text.Trim());
                cmd.Parameters["propertyid"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("proptype", Agent_sproperty_type.Text.Trim());
                cmd.Parameters["proptype"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("proplivarea", Agent_sproperty_livarea.Text.Trim());
                cmd.Parameters["proplivarea"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("propbedrooms", Agent_sproperty_be.Text.Trim());
                cmd.Parameters["propbedrooms"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("propbathrooms", Agent_sproperty_ba.Text.Trim());
                cmd.Parameters["propbathrooms"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("propyear", Agent_sproperty_year.Text.Trim());
                cmd.Parameters["propyear"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("proploc", Agent_sproperty_loc.Text.Trim());
                cmd.Parameters["proploc"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("propprice", Agent_sproperty_price.Text.Trim());
                cmd.Parameters["propprice"].Direction = ParameterDirection.Input;

                // output the number of records changed

                MySqlParameter retId = new MySqlParameter("rowcount", MySqlDbType.Int16);
                retId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retId);

                cmd.ExecuteNonQuery();

                connection.Close();

                propid = 0;

                propid = int.Parse(cmd.Parameters["rowcount"].Value.ToString());
                if (propid > 0)
                {
                    MessageBox.Show(propid + " Property Record(s) updated");
                }
                else
                {
                    MessageBox.Show("no records updated");
                }
                ResetProperty_Click(sender, e);
            }
        }

        /// <summary>
        /// CUstomer TAB  ///////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void SearchCustomer_Click(object sender, EventArgs e)
        {
            int searchparamcount = 0;
            string query = "select * from Customer where ";

            if (!String.IsNullOrEmpty(Search_custID.Text))
            {
                query += "Customer_ID =" + "\"" + Search_custID.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Search_custFN.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Cust_fname =" + "\"" + Search_custFN.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Search_custLN.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Cust_lname =" + "\"" + Search_custLN.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Search_custPhone.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Cust_phone =" + "\"" + Search_custPhone.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Search_custGovid.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Cust_govid =" + "\"" + Search_custGovid.Text.ToString() + "\"";
                searchparamcount++;
            }
            if (!String.IsNullOrEmpty(Search_custAddress.Text))
            {
                if (searchparamcount > 0)
                {
                    query += " and ";
                }
                query += "Cust_address =" + "\"" + Search_custAddress.Text.ToString() + "\"";
                searchparamcount++;
            }

            if (searchparamcount == 0)
            {
                query = "select * from Customer";               
            }            
            
            try 
            { 
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataAdapter mcmd = new MySqlDataAdapter();
            MySqlDataReader reader;
            mcmd.SelectCommand = cmd;

            reader = cmd.ExecuteReader();

            Search_Cust_Datagrid.Rows.Clear();

                while (reader.Read())
                {
                    this.Search_Cust_Datagrid.Rows.Add(
                         reader["Customer_ID"].ToString(),
                         reader["Cust_fname"].ToString(),
                         reader["Cust_lname"].ToString(),
                         reader["Cust_phone"].ToString(),
                         reader["Cust_govid"].ToString(),
                         reader["Cust_address"].ToString()
                         );          
                } 
            }
            catch
            {
                MessageBox.Show("Insert less parameters for more results");
            }

            connection.Close();    
        }

        private void Searchcustdatagridclickcell(object sender, DataGridViewCellEventArgs e)
        {
            Search_custID.Clear();

            try
            {
                if (e.RowIndex >= 0)
                {
                    Search_custID.Text = Search_Cust_Datagrid.Rows[e.RowIndex].Cells["CustsearchID"].Value.ToString();
                    Search_custFN.Text = Search_Cust_Datagrid.Rows[e.RowIndex].Cells["CustsearchFN"].Value.ToString();
                    Search_custLN.Text = Search_Cust_Datagrid.Rows[e.RowIndex].Cells["CustsearchLN"].Value.ToString();
                    Search_custPhone.Text = Search_Cust_Datagrid.Rows[e.RowIndex].Cells["Custsearchphone"].Value.ToString();
                    Search_custGovid.Text = Search_Cust_Datagrid.Rows[e.RowIndex].Cells["Custsearchgovid"].Value.ToString();
                    Search_custAddress.Text = Search_Cust_Datagrid.Rows[e.RowIndex].Cells["Custsearchaddress"].Value.ToString();                    
                }

                string query = "Select P.*, R.Reg_ID , R.Reg_date from Property P ";
                query += "inner join Registration R on R.Property_ID = P.Property_ID ";
                query += "inner join Customer C on C.Customer_ID = R.Customer_ID ";
                query += "where C.Customer_ID = \"" + Search_custID.Text.ToString() + "\"";

                try 
                { 
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter mcmd = new MySqlDataAdapter();
                    MySqlDataReader reader;
                    mcmd.SelectCommand = cmd;


                    reader = cmd.ExecuteReader();
                    
                    ListViewItem item;
                    string[] data = new string[6];

                    Customer_owned_properties.Items.Clear();
                    while (reader.Read())
                    {
                        data[0] = reader["Property_ID"].ToString().Trim();
                        data[1] = reader["Prop_type"].ToString().Trim();
                        data[2] = reader["Prop_area"].ToString().Trim();
                        data[3] = reader["Prop_Loc"].ToString().Trim();
                        data[4] = reader["Prop_price"].ToString().Trim();
                        data[5] = ((DateTime)reader["Reg_date"]).ToString("d");
                                         
                        item = new ListViewItem(data);
                        Customer_owned_properties.Items.Add(item);
                    }                     
                }
                catch
                {
                    MessageBox.Show("Insert less parameters for more results");
                }
                connection.Close();    
            }
            catch
            {

            }            
        }

        private void EditCustomerbutton_Click(object sender, EventArgs e)
        {
            if (connection != null)
            {
                //validation here
                //-------------------------------
                if ((Search_custID.Text.Trim() == "") ||
                    (Search_custFN.Text.Trim() == "") ||
                    (Search_custLN.Text.Trim() == "") ||
                    (Search_custPhone.Text.Trim() == "") ||
                    (Search_custGovid.Text.Trim() == "") ||
                    (Search_custAddress.Text.Trim() == ""))
                {
                    MessageBox.Show("All fields must be filled");
                    return;
                }
                //validation completed

                //when validation is passed continue here:

                connection.Open();
                string nameOfProcedure = "edit_customer";
                MySqlCommand cmd = new MySqlCommand(nameOfProcedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("customerid", Search_custID.Text.Trim());
                cmd.Parameters["customerid"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("customerfn", Search_custFN.Text.Trim());
                cmd.Parameters["customerfn"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("customerln", Search_custLN.Text.Trim());
                cmd.Parameters["customerln"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("customerphone", Search_custPhone.Text.Trim());
                cmd.Parameters["customerphone"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("customergovid", Search_custGovid.Text.Trim());
                cmd.Parameters["customergovid"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("customeraddress", Search_custAddress.Text.Trim());
                cmd.Parameters["customeraddress"].Direction = ParameterDirection.Input;


                MySqlParameter retId = new MySqlParameter("rowcount", MySqlDbType.Int16);
                retId.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(retId);

                cmd.ExecuteNonQuery();

                connection.Close();

                customeredited = 0;
                customeredited = int.Parse(cmd.Parameters["rowcount"].Value.ToString());
                if (customeredited > 0)
                {
                    MessageBox.Show(customeredited + " Customer Record(s) updated");
                }
                else
                {
                    MessageBox.Show("no records updated");
                }
                Resetcust_Click(sender, e);
            }
        }

        private void Resetcust_Click(object sender, EventArgs e)
        {
            Search_custID.Clear();
            Search_custFN.Clear();
            Search_custLN.Clear();
            Search_custPhone.Clear();
            Search_custGovid.Clear();
            Search_custAddress.Clear();
            Customer_owned_properties.Items.Clear();
            Search_Cust_Datagrid.Rows.Clear();

        }      
    }
}
        
    