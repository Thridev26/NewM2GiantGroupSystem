using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace UI_Design
{
    public partial class MapPopupForm : Form
    {
        // Public properties that your log inquiry form can read after this closes
        public double SelectedLatitude { get; private set; }
        public double SelectedLongitude { get; private set; }
        public MapPopupForm()
        {
            InitializeComponent();
            InitializeMapBrowser();
        }

        private async void InitializeMapBrowser()
        {
            try
            {
                // Wait for the Chromium browser core engine to wake up safely
                await webView21.EnsureCoreWebView2Async(null);

                // Locate the map.html file we copied to the application directory
                string htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "map_A.html");
                
                // Navigate the browser control directly to your local HTML layout
                webView21.CoreWebView2.Navigate(new Uri(htmlPath).AbsoluteUri);

                // Listen for messages passed directly from the webpage's JavaScript channel
                webView21.WebMessageReceived += WebView21_WebMessageReceived;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load the map engine layout: {ex.Message}", "Browser Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WebView21_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            // Grab the raw JSON data string sent by the website's postMessage function
            string jsonString = e.WebMessageAsJson;

            try
            {
                // Use Newtonsoft.Json (installed automatically alongside WebView2) to parse the data coordinates
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

                SelectedLatitude = (double)data.latitude;
                SelectedLongitude = (double)data.longitude;

                   
            }
            catch
            {
                // Fallback catch block to protect against parsing blips
            }
        }

        private void webView21_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; // optional if you still want signal
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }


}

