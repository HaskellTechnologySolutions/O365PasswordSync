namespace O365PWSync_SyncServiceConfig
{
	partial class Form1
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
			this.textBoxDatabaseFilename = new System.Windows.Forms.TextBox();
			this.textBoxO365Tenant = new System.Windows.Forms.TextBox();
			this.textBoxO365Username = new System.Windows.Forms.TextBox();
			this.textBoxO365Password = new System.Windows.Forms.TextBox();
			this.textBoxClientID = new System.Windows.Forms.TextBox();
			this.textBoxClientSecret = new System.Windows.Forms.TextBox();
			this.groupBoxDatabase = new System.Windows.Forms.GroupBox();
			this.labelDatabaseFilename = new System.Windows.Forms.Label();
			this.groupBoxClientApp = new System.Windows.Forms.GroupBox();
			this.labelClientID = new System.Windows.Forms.Label();
			this.labelClientSecret = new System.Windows.Forms.Label();
			this.groupBoxO365Settings = new System.Windows.Forms.GroupBox();
			this.buttonConsentO365Delegation = new System.Windows.Forms.Button();
			this.labelO365Tenant = new System.Windows.Forms.Label();
			this.labelO365Username = new System.Windows.Forms.Label();
			this.labelO365Password = new System.Windows.Forms.Label();
			this.groupBoxADSettings = new System.Windows.Forms.GroupBox();
			this.labelADDomain = new System.Windows.Forms.Label();
			this.textBoxADDomain = new System.Windows.Forms.TextBox();
			this.labelADUsername = new System.Windows.Forms.Label();
			this.textBoxADUsername = new System.Windows.Forms.TextBox();
			this.labelADPassword = new System.Windows.Forms.Label();
			this.textBoxADPassword = new System.Windows.Forms.TextBox();
			this.labelADGroupName = new System.Windows.Forms.Label();
			this.textBoxADGroupName = new System.Windows.Forms.TextBox();
			this.buttonLoad = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.groupBoxDatabase.SuspendLayout();
			this.groupBoxClientApp.SuspendLayout();
			this.groupBoxO365Settings.SuspendLayout();
			this.groupBoxADSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxDatabaseFilename
			// 
			this.textBoxDatabaseFilename.Location = new System.Drawing.Point(154, 19);
			this.textBoxDatabaseFilename.Name = "textBoxDatabaseFilename";
			this.textBoxDatabaseFilename.Size = new System.Drawing.Size(500, 20);
			this.textBoxDatabaseFilename.TabIndex = 0;
			// 
			// textBoxO365Tenant
			// 
			this.textBoxO365Tenant.Location = new System.Drawing.Point(154, 19);
			this.textBoxO365Tenant.Name = "textBoxO365Tenant";
			this.textBoxO365Tenant.Size = new System.Drawing.Size(500, 20);
			this.textBoxO365Tenant.TabIndex = 3;
			// 
			// textBoxO365Username
			// 
			this.textBoxO365Username.Location = new System.Drawing.Point(154, 45);
			this.textBoxO365Username.Name = "textBoxO365Username";
			this.textBoxO365Username.Size = new System.Drawing.Size(500, 20);
			this.textBoxO365Username.TabIndex = 4;
			// 
			// textBoxO365Password
			// 
			this.textBoxO365Password.Location = new System.Drawing.Point(154, 71);
			this.textBoxO365Password.Name = "textBoxO365Password";
			this.textBoxO365Password.PasswordChar = '*';
			this.textBoxO365Password.Size = new System.Drawing.Size(500, 20);
			this.textBoxO365Password.TabIndex = 5;
			this.textBoxO365Password.UseSystemPasswordChar = true;
			// 
			// textBoxClientID
			// 
			this.textBoxClientID.Location = new System.Drawing.Point(154, 19);
			this.textBoxClientID.Name = "textBoxClientID";
			this.textBoxClientID.Size = new System.Drawing.Size(500, 20);
			this.textBoxClientID.TabIndex = 6;
			// 
			// textBoxClientSecret
			// 
			this.textBoxClientSecret.Location = new System.Drawing.Point(154, 45);
			this.textBoxClientSecret.Name = "textBoxClientSecret";
			this.textBoxClientSecret.PasswordChar = '*';
			this.textBoxClientSecret.Size = new System.Drawing.Size(500, 20);
			this.textBoxClientSecret.TabIndex = 7;
			this.textBoxClientSecret.UseSystemPasswordChar = true;
			// 
			// groupBoxDatabase
			// 
			this.groupBoxDatabase.Controls.Add(this.labelDatabaseFilename);
			this.groupBoxDatabase.Controls.Add(this.textBoxDatabaseFilename);
			this.groupBoxDatabase.Location = new System.Drawing.Point(12, 12);
			this.groupBoxDatabase.Name = "groupBoxDatabase";
			this.groupBoxDatabase.Size = new System.Drawing.Size(660, 50);
			this.groupBoxDatabase.TabIndex = 8;
			this.groupBoxDatabase.TabStop = false;
			this.groupBoxDatabase.Text = "Local Database";
			// 
			// labelDatabaseFilename
			// 
			this.labelDatabaseFilename.AutoSize = true;
			this.labelDatabaseFilename.Location = new System.Drawing.Point(50, 22);
			this.labelDatabaseFilename.Name = "labelDatabaseFilename";
			this.labelDatabaseFilename.Size = new System.Drawing.Size(98, 13);
			this.labelDatabaseFilename.TabIndex = 1;
			this.labelDatabaseFilename.Text = "Database Filename";
			// 
			// groupBoxClientApp
			// 
			this.groupBoxClientApp.Controls.Add(this.labelClientID);
			this.groupBoxClientApp.Controls.Add(this.textBoxClientID);
			this.groupBoxClientApp.Controls.Add(this.labelClientSecret);
			this.groupBoxClientApp.Controls.Add(this.textBoxClientSecret);
			this.groupBoxClientApp.Location = new System.Drawing.Point(12, 68);
			this.groupBoxClientApp.Name = "groupBoxClientApp";
			this.groupBoxClientApp.Size = new System.Drawing.Size(660, 75);
			this.groupBoxClientApp.TabIndex = 9;
			this.groupBoxClientApp.TabStop = false;
			this.groupBoxClientApp.Text = "O365 Client Application";
			// 
			// labelClientID
			// 
			this.labelClientID.AutoSize = true;
			this.labelClientID.Location = new System.Drawing.Point(46, 22);
			this.labelClientID.Name = "labelClientID";
			this.labelClientID.Size = new System.Drawing.Size(102, 13);
			this.labelClientID.TabIndex = 1;
			this.labelClientID.Text = "Client Application ID";
			// 
			// labelClientSecret
			// 
			this.labelClientSecret.AutoSize = true;
			this.labelClientSecret.Location = new System.Drawing.Point(26, 48);
			this.labelClientSecret.Name = "labelClientSecret";
			this.labelClientSecret.Size = new System.Drawing.Size(122, 13);
			this.labelClientSecret.TabIndex = 8;
			this.labelClientSecret.Text = "Client Application Secret";
			// 
			// groupBoxO365Settings
			// 
			this.groupBoxO365Settings.Controls.Add(this.buttonConsentO365Delegation);
			this.groupBoxO365Settings.Controls.Add(this.labelO365Tenant);
			this.groupBoxO365Settings.Controls.Add(this.textBoxO365Tenant);
			this.groupBoxO365Settings.Controls.Add(this.labelO365Username);
			this.groupBoxO365Settings.Controls.Add(this.textBoxO365Username);
			this.groupBoxO365Settings.Controls.Add(this.labelO365Password);
			this.groupBoxO365Settings.Controls.Add(this.textBoxO365Password);
			this.groupBoxO365Settings.Location = new System.Drawing.Point(12, 150);
			this.groupBoxO365Settings.Name = "groupBoxO365Settings";
			this.groupBoxO365Settings.Size = new System.Drawing.Size(660, 130);
			this.groupBoxO365Settings.TabIndex = 10;
			this.groupBoxO365Settings.TabStop = false;
			this.groupBoxO365Settings.Text = "O365 Settings";
			// 
			// buttonConsentO365Delegation
			// 
			this.buttonConsentO365Delegation.Location = new System.Drawing.Point(381, 97);
			this.buttonConsentO365Delegation.Name = "buttonConsentO365Delegation";
			this.buttonConsentO365Delegation.Size = new System.Drawing.Size(273, 23);
			this.buttonConsentO365Delegation.TabIndex = 12;
			this.buttonConsentO365Delegation.Text = "Delegate Permissions (Only needs to be done once)";
			this.buttonConsentO365Delegation.UseVisualStyleBackColor = true;
			this.buttonConsentO365Delegation.Click += new System.EventHandler(this.buttonConsentO365Delegation_Click);
			// 
			// labelO365Tenant
			// 
			this.labelO365Tenant.AutoSize = true;
			this.labelO365Tenant.Location = new System.Drawing.Point(76, 22);
			this.labelO365Tenant.Name = "labelO365Tenant";
			this.labelO365Tenant.Size = new System.Drawing.Size(72, 13);
			this.labelO365Tenant.TabIndex = 9;
			this.labelO365Tenant.Text = "Tenant Name";
			// 
			// labelO365Username
			// 
			this.labelO365Username.AutoSize = true;
			this.labelO365Username.Location = new System.Drawing.Point(11, 48);
			this.labelO365Username.Name = "labelO365Username";
			this.labelO365Username.Size = new System.Drawing.Size(137, 13);
			this.labelO365Username.TabIndex = 10;
			this.labelO365Username.Text = "Service Account Username";
			// 
			// labelO365Password
			// 
			this.labelO365Password.AutoSize = true;
			this.labelO365Password.Location = new System.Drawing.Point(13, 74);
			this.labelO365Password.Name = "labelO365Password";
			this.labelO365Password.Size = new System.Drawing.Size(135, 13);
			this.labelO365Password.TabIndex = 11;
			this.labelO365Password.Text = "Service Account Password";
			// 
			// groupBoxADSettings
			// 
			this.groupBoxADSettings.Controls.Add(this.labelADDomain);
			this.groupBoxADSettings.Controls.Add(this.textBoxADDomain);
			this.groupBoxADSettings.Controls.Add(this.labelADUsername);
			this.groupBoxADSettings.Controls.Add(this.textBoxADUsername);
			this.groupBoxADSettings.Controls.Add(this.labelADPassword);
			this.groupBoxADSettings.Controls.Add(this.textBoxADPassword);
			this.groupBoxADSettings.Controls.Add(this.labelADGroupName);
			this.groupBoxADSettings.Controls.Add(this.textBoxADGroupName);
			this.groupBoxADSettings.Location = new System.Drawing.Point(12, 286);
			this.groupBoxADSettings.Name = "groupBoxADSettings";
			this.groupBoxADSettings.Size = new System.Drawing.Size(660, 130);
			this.groupBoxADSettings.TabIndex = 10;
			this.groupBoxADSettings.TabStop = false;
			this.groupBoxADSettings.Text = "AD Settings";
			// 
			// labelADDomain
			// 
			this.labelADDomain.AutoSize = true;
			this.labelADDomain.Location = new System.Drawing.Point(105, 22);
			this.labelADDomain.Name = "labelADDomain";
			this.labelADDomain.Size = new System.Drawing.Size(43, 13);
			this.labelADDomain.TabIndex = 11;
			this.labelADDomain.Text = "Domain";
			// 
			// textBoxADDomain
			// 
			this.textBoxADDomain.Location = new System.Drawing.Point(154, 19);
			this.textBoxADDomain.Name = "textBoxADDomain";
			this.textBoxADDomain.Size = new System.Drawing.Size(500, 20);
			this.textBoxADDomain.TabIndex = 12;
			// 
			// labelADUsername
			// 
			this.labelADUsername.AutoSize = true;
			this.labelADUsername.Location = new System.Drawing.Point(93, 48);
			this.labelADUsername.Name = "labelADUsername";
			this.labelADUsername.Size = new System.Drawing.Size(55, 13);
			this.labelADUsername.TabIndex = 1;
			this.labelADUsername.Text = "Username";
			// 
			// textBoxADUsername
			// 
			this.textBoxADUsername.Location = new System.Drawing.Point(154, 45);
			this.textBoxADUsername.Name = "textBoxADUsername";
			this.textBoxADUsername.Size = new System.Drawing.Size(500, 20);
			this.textBoxADUsername.TabIndex = 6;
			// 
			// labelADPassword
			// 
			this.labelADPassword.AutoSize = true;
			this.labelADPassword.Location = new System.Drawing.Point(95, 74);
			this.labelADPassword.Name = "labelADPassword";
			this.labelADPassword.Size = new System.Drawing.Size(53, 13);
			this.labelADPassword.TabIndex = 8;
			this.labelADPassword.Text = "Password";
			// 
			// textBoxADPassword
			// 
			this.textBoxADPassword.Location = new System.Drawing.Point(154, 71);
			this.textBoxADPassword.Name = "textBoxADPassword";
			this.textBoxADPassword.PasswordChar = '*';
			this.textBoxADPassword.Size = new System.Drawing.Size(500, 20);
			this.textBoxADPassword.TabIndex = 7;
			this.textBoxADPassword.UseSystemPasswordChar = true;
			// 
			// labelADGroupName
			// 
			this.labelADGroupName.AutoSize = true;
			this.labelADGroupName.Location = new System.Drawing.Point(60, 100);
			this.labelADGroupName.Name = "labelADGroupName";
			this.labelADGroupName.Size = new System.Drawing.Size(88, 13);
			this.labelADGroupName.TabIndex = 10;
			this.labelADGroupName.Text = "Target AD Group";
			// 
			// textBoxADGroupName
			// 
			this.textBoxADGroupName.Location = new System.Drawing.Point(154, 97);
			this.textBoxADGroupName.Name = "textBoxADGroupName";
			this.textBoxADGroupName.Size = new System.Drawing.Size(500, 20);
			this.textBoxADGroupName.TabIndex = 9;
			// 
			// buttonLoad
			// 
			this.buttonLoad.Location = new System.Drawing.Point(516, 427);
			this.buttonLoad.Name = "buttonLoad";
			this.buttonLoad.Size = new System.Drawing.Size(75, 23);
			this.buttonLoad.TabIndex = 11;
			this.buttonLoad.Text = "Load Config";
			this.buttonLoad.UseVisualStyleBackColor = true;
			this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(597, 427);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 12;
			this.buttonSave.Text = "Save Config";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(684, 462);
			this.Controls.Add(this.groupBoxDatabase);
			this.Controls.Add(this.groupBoxClientApp);
			this.Controls.Add(this.groupBoxO365Settings);
			this.Controls.Add(this.groupBoxADSettings);
			this.Controls.Add(this.buttonLoad);
			this.Controls.Add(this.buttonSave);
			this.Name = "Form1";
			this.Text = "O365PWSync Configuration";
			this.groupBoxDatabase.ResumeLayout(false);
			this.groupBoxDatabase.PerformLayout();
			this.groupBoxClientApp.ResumeLayout(false);
			this.groupBoxClientApp.PerformLayout();
			this.groupBoxO365Settings.ResumeLayout(false);
			this.groupBoxO365Settings.PerformLayout();
			this.groupBoxADSettings.ResumeLayout(false);
			this.groupBoxADSettings.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxDatabaseFilename;
		private System.Windows.Forms.TextBox textBoxO365Tenant;
		private System.Windows.Forms.TextBox textBoxO365Username;
		private System.Windows.Forms.TextBox textBoxO365Password;
		private System.Windows.Forms.TextBox textBoxClientID;
		private System.Windows.Forms.TextBox textBoxClientSecret;
		private System.Windows.Forms.GroupBox groupBoxDatabase;
		private System.Windows.Forms.Label labelDatabaseFilename;
		private System.Windows.Forms.GroupBox groupBoxClientApp;
		private System.Windows.Forms.Label labelClientID;
		private System.Windows.Forms.Label labelClientSecret;
		private System.Windows.Forms.GroupBox groupBoxO365Settings;
		private System.Windows.Forms.Label labelO365Tenant;
		private System.Windows.Forms.Label labelO365Username;
		private System.Windows.Forms.Label labelO365Password;
		private System.Windows.Forms.GroupBox groupBoxADSettings;
		private System.Windows.Forms.Label labelADUsername;
		private System.Windows.Forms.TextBox textBoxADUsername;
		private System.Windows.Forms.Label labelADPassword;
		private System.Windows.Forms.TextBox textBoxADPassword;
		private System.Windows.Forms.Button buttonConsentO365Delegation;
		private System.Windows.Forms.Button buttonLoad;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Label labelADGroupName;
		private System.Windows.Forms.TextBox textBoxADGroupName;
		private System.Windows.Forms.Label labelADDomain;
		private System.Windows.Forms.TextBox textBoxADDomain;
	}
}

