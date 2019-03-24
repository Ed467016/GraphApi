using System;
using System.Configuration;
using System.Net.Http;
using System.Windows;

namespace GraphApi.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string GraphAPIUriKey = "GraphAPIUri";

        public MainWindow()
        {
            InitializeComponent();
            this.RequestTbox.Text = @"{
            projects {
                      projectName,
                      users{
                            firstName
                      }
                    }
                }";
        }

        private async void RequestBtn_Click(object sender, RoutedEventArgs e)
        {
            this.ProgressBar.IsIndeterminate = true;
            try
            {

                using (HttpClient httpClient = new HttpClient())
                {
                    string requestUri = ConfigurationManager.AppSettings[GraphAPIUriKey];
                    UriBuilder uriBuilder = new UriBuilder(requestUri);
                    var query = this.RequestTbox.Text;
                    uriBuilder.Query = $"query={query}";
                    var response = await httpClient.GetAsync(uriBuilder.Uri);
                    string stringResponse = await response.Content.ReadAsStringAsync();
                    this.ResponseTbox.Text = stringResponse;
                }
            }
            catch (Exception ex)
            {
                this.ResponseTbox.Text = ex.Message;
            }
            finally
            {
                this.ProgressBar.IsIndeterminate = false;
            }
        }
    }
}
