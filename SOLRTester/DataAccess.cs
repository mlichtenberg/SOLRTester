using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SOLRTester
{
    public class DataAccess
    {
        public List<Dictionary<string, object>> GetData()
        {
            // Mapping data to a schema: https://github.com/mausch/SolrNet/blob/master/Documentation/Mapping.md
            List<Dictionary<string, object>> documents = new List<Dictionary<string, object>>();
            /*
            Dictionary<string, object> document = new Dictionary<string, object> {
                    {"id", "test.item" },
                    {"field1", 1},
                    {"field2", "something else"},
                    {"field3", new DateTime(2010, 5, 5, 12, 23, 34)},
                    {"field4", new[] {1,2,3}},
                };
            documents.Add(document);
            */

            List<string> headers = new List<string>();
            string[] dataLines = File.ReadAllLines(@"..\\..\\Data\\proceedingsofzoo19221zool.txt");
            IEnumerable<string> files = Directory.EnumerateFiles(@"..\\..\\Data\proceedingsofzoo19221zool");
            List<string> fileList = files.ToList();
            fileList.Sort();

            bool readHeaders = true;
            foreach(string data in dataLines)
            {
                if (readHeaders)
                {
                    string[] headerLabels = data.Split('\t');
                    foreach (string label in headerLabels)
                    {
                        headers.Add(label);
                    }
                    readHeaders = false;
                }
                else
                {
                    Dictionary<string, object> document = new Dictionary<string, object>();
                    string[] values = data.Split('\t');
                    int index = 0;
                    foreach(string value in values)
                    {
                        string[] allValues = value.Split('|');
                        document.Add(headers[index], allValues);
                        index++;
                    }

                    document.Add("PageText", File.ReadAllText(fileList[documents.Count]));

                    documents.Add(document);
                }
            }

            return documents;
        }
    }
}
