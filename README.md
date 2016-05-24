# SOLRTester
Simple C# WPF app that exercises the basic functionality of SOLR.NET.

To get started with this app, create a SOLR core named "solrtest".  Use the following command to create the core:

  bin/solr create_core -c solrtest

If using a Docker container to host SOLR, use this instead:

  docker exec -it <container_id> bin/solr/create_core -c solrtest

The four functions provided by this application are as follows:

  Add All Data = indexes the contents of the Data folder in your SOLR instance (do this first)
  Delete All Data = clears the contents of the SOLR index
  Submit Query = submits a query against your SOLR instance
  Delete = deletes the data specified in the "Query" text box from the SOLR index

