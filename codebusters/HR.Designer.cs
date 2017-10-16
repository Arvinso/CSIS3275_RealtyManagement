namespace codebusters
{
    partial class HR
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.hragentgovid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.hragentemployed = new System.Windows.Forms.CheckBox();
            this.hragentaddress = new System.Windows.Forms.TextBox();
            this.hragentphone = new System.Windows.Forms.TextBox();
            this.hragentfn = new System.Windows.Forms.TextBox();
            this.Agentsearch = new System.Windows.Forms.Button();
            this.Agentedit = new System.Windows.Forms.Button();
            this.Agentadd = new System.Windows.Forms.Button();
            this.HRdatagrid = new System.Windows.Forms.DataGridView();
            this.AgentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgentFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgentLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgentPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Agentgovid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgentAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Employed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateEmployed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.hragentln = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.loggedinHR = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.hragentpw = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.HRagentID = new System.Windows.Forms.TextBox();
            this.HRreset = new System.Windows.Forms.Button();
            this.showall = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.HRdatagrid)).BeginInit();
            this.SuspendLayout();
            // 
            // hragentgovid
            // 
            this.hragentgovid.Location = new System.Drawing.Point(245, 275);
            this.hragentgovid.Name = "hragentgovid";
            this.hragentgovid.Size = new System.Drawing.Size(176, 22);
            this.hragentgovid.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(94, 278);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 46;
            this.label5.Text = "Government ID";
            // 
            // hragentemployed
            // 
            this.hragentemployed.AutoSize = true;
            this.hragentemployed.Location = new System.Drawing.Point(97, 380);
            this.hragentemployed.Name = "hragentemployed";
            this.hragentemployed.Size = new System.Drawing.Size(92, 21);
            this.hragentemployed.TabIndex = 45;
            this.hragentemployed.Text = "Employed";
            this.hragentemployed.UseVisualStyleBackColor = true;
            // 
            // hragentaddress
            // 
            this.hragentaddress.Location = new System.Drawing.Point(245, 340);
            this.hragentaddress.Name = "hragentaddress";
            this.hragentaddress.Size = new System.Drawing.Size(176, 22);
            this.hragentaddress.TabIndex = 44;
            // 
            // hragentphone
            // 
            this.hragentphone.Location = new System.Drawing.Point(245, 219);
            this.hragentphone.Name = "hragentphone";
            this.hragentphone.Size = new System.Drawing.Size(176, 22);
            this.hragentphone.TabIndex = 43;
            // 
            // hragentfn
            // 
            this.hragentfn.Location = new System.Drawing.Point(245, 127);
            this.hragentfn.Name = "hragentfn";
            this.hragentfn.Size = new System.Drawing.Size(176, 22);
            this.hragentfn.TabIndex = 42;
            // 
            // Agentsearch
            // 
            this.Agentsearch.Location = new System.Drawing.Point(512, 107);
            this.Agentsearch.Name = "Agentsearch";
            this.Agentsearch.Size = new System.Drawing.Size(183, 63);
            this.Agentsearch.TabIndex = 41;
            this.Agentsearch.Text = "Search";
            this.Agentsearch.UseVisualStyleBackColor = true;
            this.Agentsearch.Click += new System.EventHandler(this.Agentsearch_Click);
            // 
            // Agentedit
            // 
            this.Agentedit.Location = new System.Drawing.Point(280, 478);
            this.Agentedit.Name = "Agentedit";
            this.Agentedit.Size = new System.Drawing.Size(141, 61);
            this.Agentedit.TabIndex = 40;
            this.Agentedit.Text = "Edit Agent";
            this.Agentedit.UseVisualStyleBackColor = true;
            this.Agentedit.Click += new System.EventHandler(this.Agentedit_Click);
            // 
            // Agentadd
            // 
            this.Agentadd.Location = new System.Drawing.Point(97, 478);
            this.Agentadd.Name = "Agentadd";
            this.Agentadd.Size = new System.Drawing.Size(143, 61);
            this.Agentadd.TabIndex = 36;
            this.Agentadd.Text = "Add New Agent";
            this.Agentadd.UseVisualStyleBackColor = true;
            this.Agentadd.Click += new System.EventHandler(this.Agentadd_Click);
            // 
            // HRdatagrid
            // 
            this.HRdatagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HRdatagrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AgentID,
            this.AgentFirstName,
            this.AgentLastName,
            this.AgentPhone,
            this.Agentgovid,
            this.AgentAddress,
            this.Employed,
            this.PW,
            this.DateEmployed});
            this.HRdatagrid.Location = new System.Drawing.Point(512, 200);
            this.HRdatagrid.Name = "HRdatagrid";
            this.HRdatagrid.RowTemplate.Height = 24;
            this.HRdatagrid.Size = new System.Drawing.Size(1259, 397);
            this.HRdatagrid.TabIndex = 39;
            this.HRdatagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.HRdatagridcell);
            // 
            // AgentID
            // 
            this.AgentID.HeaderText = "AgentID";
            this.AgentID.Name = "AgentID";
            this.AgentID.Width = 80;
            // 
            // AgentFirstName
            // 
            this.AgentFirstName.HeaderText = "Agent First Name";
            this.AgentFirstName.Name = "AgentFirstName";
            this.AgentFirstName.Width = 130;
            // 
            // AgentLastName
            // 
            this.AgentLastName.HeaderText = "Agent Last Name";
            this.AgentLastName.Name = "AgentLastName";
            this.AgentLastName.Width = 130;
            // 
            // AgentPhone
            // 
            this.AgentPhone.HeaderText = "Agent Phone Number";
            this.AgentPhone.Name = "AgentPhone";
            this.AgentPhone.Width = 150;
            // 
            // Agentgovid
            // 
            this.Agentgovid.HeaderText = "Agent Government ID";
            this.Agentgovid.Name = "Agentgovid";
            this.Agentgovid.Width = 150;
            // 
            // AgentAddress
            // 
            this.AgentAddress.HeaderText = "Agent Address";
            this.AgentAddress.Name = "AgentAddress";
            this.AgentAddress.Width = 150;
            // 
            // Employed
            // 
            this.Employed.HeaderText = "Employed";
            this.Employed.Name = "Employed";
            // 
            // PW
            // 
            this.PW.HeaderText = "PIN";
            this.PW.Name = "PW";
            this.PW.Visible = false;
            // 
            // DateEmployed
            // 
            this.DateEmployed.HeaderText = "Date Employed";
            this.DateEmployed.Name = "DateEmployed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 343);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 38;
            this.label3.Text = "Adress";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(94, 222);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 37;
            this.label2.Text = "Phone Number";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 17);
            this.label1.TabIndex = 35;
            this.label1.Text = "Agent First Name";
            // 
            // hragentln
            // 
            this.hragentln.Location = new System.Drawing.Point(245, 168);
            this.hragentln.Name = "hragentln";
            this.hragentln.Size = new System.Drawing.Size(176, 22);
            this.hragentln.TabIndex = 49;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(94, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 17);
            this.label4.TabIndex = 48;
            this.label4.Text = "Agent Last Name";
            // 
            // loggedinHR
            // 
            this.loggedinHR.AutoSize = true;
            this.loggedinHR.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loggedinHR.Location = new System.Drawing.Point(689, 36);
            this.loggedinHR.Name = "loggedinHR";
            this.loggedinHR.Size = new System.Drawing.Size(201, 32);
            this.loggedinHR.TabIndex = 121;
            this.loggedinHR.Text = "_____   ______";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(485, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(148, 32);
            this.label6.TabIndex = 120;
            this.label6.Text = "Welcome :";
            // 
            // hragentpw
            // 
            this.hragentpw.Location = new System.Drawing.Point(245, 425);
            this.hragentpw.Name = "hragentpw";
            this.hragentpw.Size = new System.Drawing.Size(176, 22);
            this.hragentpw.TabIndex = 123;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(94, 425);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 17);
            this.label7.TabIndex = 122;
            this.label7.Text = "Agent PIN";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(94, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 17);
            this.label8.TabIndex = 127;
            this.label8.Text = "Agent ID";
            // 
            // HRagentID
            // 
            this.HRagentID.Location = new System.Drawing.Point(245, 83);
            this.HRagentID.Name = "HRagentID";
            this.HRagentID.ReadOnly = true;
            this.HRagentID.Size = new System.Drawing.Size(176, 22);
            this.HRagentID.TabIndex = 128;
            // 
            // HRreset
            // 
            this.HRreset.Location = new System.Drawing.Point(1250, 105);
            this.HRreset.Name = "HRreset";
            this.HRreset.Size = new System.Drawing.Size(74, 61);
            this.HRreset.TabIndex = 129;
            this.HRreset.Text = "Reset";
            this.HRreset.UseVisualStyleBackColor = true;
            this.HRreset.Click += new System.EventHandler(this.HRreset_Click);
            // 
            // showall
            // 
            this.showall.AutoSize = true;
            this.showall.Location = new System.Drawing.Point(701, 145);
            this.showall.Name = "showall";
            this.showall.Size = new System.Drawing.Size(83, 21);
            this.showall.TabIndex = 130;
            this.showall.Text = "Show All";
            this.showall.UseVisualStyleBackColor = true;
            // 
            // HR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1783, 609);
            this.Controls.Add(this.showall);
            this.Controls.Add(this.HRreset);
            this.Controls.Add(this.HRagentID);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.hragentpw);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.loggedinHR);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.hragentln);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.hragentgovid);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.hragentemployed);
            this.Controls.Add(this.hragentaddress);
            this.Controls.Add(this.hragentphone);
            this.Controls.Add(this.hragentfn);
            this.Controls.Add(this.Agentsearch);
            this.Controls.Add(this.Agentedit);
            this.Controls.Add(this.Agentadd);
            this.Controls.Add(this.HRdatagrid);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "HR";
            this.Text = "HR";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.exitform);
            this.Load += new System.EventHandler(this.HR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.HRdatagrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox hragentgovid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox hragentemployed;
        private System.Windows.Forms.TextBox hragentaddress;
        private System.Windows.Forms.TextBox hragentphone;
        private System.Windows.Forms.TextBox hragentfn;
        private System.Windows.Forms.Button Agentsearch;
        private System.Windows.Forms.Button Agentedit;
        private System.Windows.Forms.Button Agentadd;
        private System.Windows.Forms.DataGridView HRdatagrid;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox hragentln;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label loggedinHR;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox hragentpw;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox HRagentID;
        private System.Windows.Forms.Button HRreset;
        private System.Windows.Forms.CheckBox showall;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgentFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgentLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgentPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Agentgovid;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgentAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn Employed;
        private System.Windows.Forms.DataGridViewTextBoxColumn PW;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateEmployed;
    }
}