using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using SolrNet;
using System.Text;

namespace SOLRTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get data to index
                List<Dictionary<string, object>> documents = new DataAccess().GetData();
                
                SolrUtility utility = new SolrUtility(solrUrlTextBox.Text);
                utility.Index(documents);
                MessageBox.Show("Data added");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteAllButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SolrUtility utility = new SolrUtility(solrUrlTextBox.Text);
                utility.Delete("*:*");
                MessageBox.Show("Data deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void queryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SolrUtility utility = new SolrUtility(solrUrlTextBox.Text);
                SolrQueryResults<Dictionary<string, object>> results = utility.Query(queryTextBox.Text);
                StringBuilder sb = new StringBuilder();

                foreach(Dictionary<string, object> result in results)
                {
                    sb.Append("+++++START RESULT+++++\n\n");
                    foreach(string key in result.Keys)
                    {
                        if (result[key].GetType() == typeof(ArrayList))
                        {
                            foreach (var value in (ArrayList)result[key])
                            {
                                sb.AppendFormat("{0}: {1}\n", key, value.ToString());
                            }
                        }
                        else
                        {
                            sb.AppendFormat("{0}: {1}\n", key, result[key]);
                        }
                    }
                    sb.Append("\n+++++END RESULT+++++\n\n");
                }

                resultsTextBox.Text = sb.ToString();
                MessageBox.Show(string.Format("Query submitted. {0} results.", results.NumFound));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SolrUtility utility = new SolrUtility(solrUrlTextBox.Text);
                utility.Delete(queryTextBox.Text);
                MessageBox.Show("Data deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
