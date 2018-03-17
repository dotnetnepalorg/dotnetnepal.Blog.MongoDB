namespace dotnetnepal.Core
{
    public partial interface IWebHelper
    {
 
        string GetUrlReferrer();

        string GetCurrentIpAddress();

        string GetThisPageUrl(bool includeQueryString);


        string GetThisPageUrl(bool includeQueryString, bool useSsl);
        

        string ModifyQueryString(string url, string queryStringModification, string anchor);

  
        string RemoveQueryString(string url, string queryString);


        T QueryString<T>(string name);

        bool IsRequestBeingRedirected { get; }

        bool IsPostBeingDone { get; set; }

 
    }





}
