namespace O365PWSync_SyncServiceConfig
{
	partial class ConsentBrowserForm
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
			this.webBrowserConsent = new System.Windows.Forms.WebBrowser();
			this.textBoxURL = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// webBrowserConsent
			// 
			this.webBrowserConsent.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.webBrowserConsent.Location = new System.Drawing.Point(0, 20);
			this.webBrowserConsent.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowserConsent.Name = "webBrowserConsent";
			this.webBrowserConsent.ScriptErrorsSuppressed = true;
			this.webBrowserConsent.Size = new System.Drawing.Size(800, 430);
			this.webBrowserConsent.TabIndex = 0;
			// 
			// textBoxURL
			// 
			this.textBoxURL.Dock = System.Windows.Forms.DockStyle.Top;
			this.textBoxURL.Enabled = false;
			this.textBoxURL.Location = new System.Drawing.Point(0, 0);
			this.textBoxURL.Name = "textBoxURL";
			this.textBoxURL.Size = new System.Drawing.Size(800, 20);
			this.textBoxURL.TabIndex = 1;
			// 
			// ConsentBrowserForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.textBoxURL);
			this.Controls.Add(this.webBrowserConsent);
			this.Name = "ConsentBrowserForm";
			this.Text = "O365 Delegation Sign-In";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.WebBrowser webBrowserConsent;
		private System.Windows.Forms.TextBox textBoxURL;
	}
}