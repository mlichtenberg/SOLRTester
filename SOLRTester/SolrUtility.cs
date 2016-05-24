using Microsoft.Practices.ServiceLocation;
using SolrNet;
using System.Collections.Generic;
using System.Linq;

namespace SOLRTester
{
    public class SolrUtility
    {
        private string _solrUrl = string.Empty;
        private ISolrOperations<Dictionary<string, object>> _solr = null;
        
        public SolrUtility(string solrUrl)
        {
            _solrUrl = solrUrl;
            Startup.InitContainer();
            Startup.Init<Dictionary<string, object>>(_solrUrl);
            _solr = ServiceLocator.Current.GetInstance<ISolrOperations<Dictionary<string, object>>>();
        }

        public void Index(List<Dictionary<string, object>> documents)
        {
            _solr.AddRange(documents);
            _solr.Commit();
        }

        public void Delete(string query)
        {
            ISolrQuery solrQuery = new SolrQuery(query);
            ResponseHeader response = _solr.Delete(solrQuery);
            _solr.Commit();            
        }

        public SolrQueryResults<Dictionary<string, object>> Query(string query)
        {
            SolrQueryResults<Dictionary<string, object>> results = _solr.Query(query);
            return results;
        }
    }
}
