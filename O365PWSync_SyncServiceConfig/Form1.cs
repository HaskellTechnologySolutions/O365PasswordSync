using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using O365PWSync_ConfigHandler;

namespace O365PWSync_SyncServiceConfig
{
	public partial class Form1 : Form
	{
		private string ConfigFilename;

		public Form1()
		{
			InitializeComponent();

			SetEnabled(false);
			buttonSave.Enabled = false;
			
		}

		private void SetEnabled(bool enabled)
		{
			textBoxDatabaseFilename.Enabled = enabled;
			textBoxClientID.Enabled = enabled;
			textBoxClientSecret.Enabled = enabled;
			textBoxO365Tenant.Enabled = enabled;
			textBoxO365Username.Enabled = enabled;
			textBoxO365Password.Enabled = enabled;
			textBoxADUsername.Enabled = enabled;
			textBoxADPassword.Enabled = enabled;
			buttonConsentO365Delegation.Enabled = enabled;
			textBoxADGroupName.Enabled = enabled;
			textBoxADDomain.Enabled = enabled;
		}

		private void buttonLoad_Click(object sender, EventArgs e)
		{
			string defaultSecureConfigFile = ConfigurationManager.AppSettings["defaultSecureConfigFile"];

			string defaultPath = Path.GetDirectoryName(defaultSecureConfigFile);
			string defaultFilename = Path.GetFileName(defaultSecureConfigFile);

			OpenFileDialog dlg = new OpenFileDialog();
			dlg.InitialDirectory = defaultPath;
			dlg.FileName = defaultFilename;
			dlg.Multiselect = false;
			dlg.CheckFileExists = false;
			dlg.CheckPathExists = true;

			if (dlg.ShowDialog() != DialogResult.OK) { return; }
			ConfigFilename = dlg.FileName;

			if (!File.Exists(ConfigFilename))
			{
				if (MessageBox.Show("File does not exist, create new?", "Create new file?", MessageBoxButtons.YesNo) != DialogResult.Yes) { return; }
				File.Create(ConfigFilename).Close();

				ConfigHandler.Initialize(ConfigFilename);

				textBoxDatabaseFilename.Text = ConfigurationManager.AppSettings["defaultDatabaseFilename"];

				SetEnabled(true);
				buttonSave.Enabled = true;
			}
			else
			{
				if (!ConfigHandler.Initialize(ConfigFilename)) { MessageBox.Show("Failed to initialize the Config Handler!"); return; }
				if (!ConfigHandler.LoadConfig()) { MessageBox.Show("Failed to load configuration from file!"); return; }

				textBoxDatabaseFilename.Text = ConfigHandler.DatabaseFilename;
				textBoxClientID.Text = ConfigHandler.ClientID;
				//textBoxClientSecret.Text = ConfigHandler.ClientSecret;
				textBoxClientSecret.BackColor = SystemColors.Control;
				textBoxO365Tenant.Text = ConfigHandler.O365TenantName;
				textBoxO365Username.Text = ConfigHandler.O365ServiceUsername;
				//textBoxO365Password.Text = ConfigHandler.O365ServicePassword;
				textBoxO365Password.BackColor = SystemColors.Control;
				textBoxADUsername.Text = ConfigHandler.ADServiceUsername;
				//textBoxADPassword.Text = ConfigHandler.ADServicePassword;
				textBoxADPassword.BackColor = SystemColors.Control;
				textBoxADGroupName.Text = ConfigHandler.ADGroupName;
				textBoxADDomain.Text = ConfigHandler.ADDomain;

				SetEnabled(true);
				buttonSave.Enabled = true;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SetEnabled(false);
			buttonSave.Enabled = false;
			buttonLoad.Enabled = false;

			ConfigHandler.DatabaseFilename = textBoxDatabaseFilename.Text;
			ConfigHandler.ClientID = textBoxClientID.Text;
			if (textBoxClientSecret.Text.Length > 0) { ConfigHandler.ClientSecret = textBoxClientSecret.Text; }
			ConfigHandler.O365TenantName = textBoxO365Tenant.Text;
			ConfigHandler.O365ServiceUsername = textBoxO365Username.Text;
			if (textBoxO365Password.Text.Length > 0) { ConfigHandler.O365ServicePassword = textBoxO365Password.Text; }
			ConfigHandler.ADServiceUsername = textBoxADUsername.Text;
			if (textBoxADPassword.Text.Length > 0) { ConfigHandler.ADServicePassword = textBoxADPassword.Text; }
			ConfigHandler.ADGroupName = textBoxADGroupName.Text;
			ConfigHandler.ADDomain = textBoxADDomain.Text;

			if (!ConfigHandler.SaveConfig()) { MessageBox.Show("Failed to save configuration to file!"); }
			else { MessageBox.Show("Configuration Saved! If this is the first time setup for the the O365 PW Sync tool, you will need to restart the computer. Otherwise, simply restart the O365PWSync service."); }

			buttonLoad.Enabled = true;
		}

		private void buttonConsentO365Delegation_Click(object sender, EventArgs e)
		{
			if (!(textBoxClientID.Text.Length > 0 && textBoxO365Tenant.Text.Length > 0)) { MessageBox.Show("Missing required fields, Tenant and Client ID"); return; }

			string consentURL = ConfigurationManager.AppSettings["consentURL"];
			consentURL = consentURL.Replace("{TenantName}", textBoxO365Tenant.Text).Replace("{ApplicationID}", textBoxClientID.Text);

			ConsentBrowserForm dialog = new ConsentBrowserForm(consentURL);
			if (dialog.ShowDialog() != DialogResult.Yes) { MessageBox.Show("O365 permissions delegation appears to have failed or not been completed!"); return; }
		}
	}
}
