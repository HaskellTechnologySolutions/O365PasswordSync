using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace O365PWSync_SyncServiceConfig
{
	public partial class ConsentBrowserForm : Form
	{
		public ConsentBrowserForm(string url)
		{
			InitializeComponent();
			this.DialogResult = DialogResult.Abort;
			webBrowserConsent.Navigate(url);
			webBrowserConsent.Navigated += WebBrowserConsent_Navigated;
		}

		private void WebBrowserConsent_Navigated(object sender, WebBrowserNavigatedEventArgs e)
		{
			string newUrl = webBrowserConsent.Url.ToString();
			textBoxURL.Text = newUrl;

			if (newUrl.Contains("admin_consent=True"))
			{
				this.DialogResult = DialogResult.Yes;
				this.Close();
			}
			else if (newUrl.Contains("error=access_denied"))
			{
				this.DialogResult = DialogResult.No;
				this.Close();
			}
		}
	}
}
