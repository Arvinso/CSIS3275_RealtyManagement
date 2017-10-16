namespace codebusters
{
    partial class Login
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
            this.label1 = new System.Windows.Forms.Label();
            this.usn_type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pw = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.clearbutton = new System.Windows.Forms.Button();
            this.usn_select = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "User";
            // 
            // usn_type
            // 
            this.usn_type.FormattingEnabled = true;
            this.usn_type.Items.AddRange(new object[] {
            "HR",
            "Agent",
            "Manager"});
            this.usn_type.Location = new System.Drawing.Point(130, 27);
            this.usn_type.Name = "usn_type";
            this.usn_type.Size = new System.Drawing.Size(259, 24);
            this.usn_type.TabIndex = 1;
            this.usn_type.SelectionChangeCommitted += new System.EventHandler(this.Search_Member);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Username";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "PIN";
            // 
            // pw
            // 
            this.pw.Location = new System.Drawing.Point(130, 133);
            this.pw.Name = "pw";
            this.pw.Size = new System.Drawing.Size(259, 22);
            this.pw.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(176, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 43);
            this.button1.TabIndex = 6;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Login_click);
            // 
            // clearbutton
            // 
            this.clearbutton.Location = new System.Drawing.Point(33, 177);
            this.clearbutton.Name = "clearbutton";
            this.clearbutton.Size = new System.Drawing.Size(75, 43);
            this.clearbutton.TabIndex = 7;
            this.clearbutton.Text = "Clear";
            this.clearbutton.UseVisualStyleBackColor = true;
            this.clearbutton.Click += new System.EventHandler(this.clearbutton_Click);
            // 
            // usn_select
            // 
            this.usn_select.FormattingEnabled = true;
            this.usn_select.Location = new System.Drawing.Point(130, 83);
            this.usn_select.Name = "usn_select";
            this.usn_select.Size = new System.Drawing.Size(259, 24);
            this.usn_select.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::codebusters.Properties.Resources.logo_codebusters;
            this.pictureBox1.Location = new System.Drawing.Point(431, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(205, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 240);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.usn_select);
            this.Controls.Add(this.clearbutton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pw);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.usn_type);
            this.Controls.Add(this.label1);
            this.Name = "Login";
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox usn_type;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pw;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button clearbutton;
        private System.Windows.Forms.ComboBox usn_select;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}