//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.Features;
//using Microsoft.Net.Http.Headers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;

//namespace dotnetnepal.Core
//{
//    /// <summary>
//    /// Represents a common helper
//    /// </summary>
//    public partial class WebHelper : IWebHelper
//    {
//        #region Fields 

//        private const string NullIpAddress = "::1";

//        private readonly IHttpContextAccessor _httpContextAccessor;
        
//        #endregion

//        #region Constructor

//        public WebHelper(IHttpContextAccessor httpContextAccessor, HostingConfig hostingConfig, IApplicationLifetime applicationLifetime)
//        {
//            this._httpContextAccessor = httpContextAccessor;
//        }

//        #endregion

//        #region Utilities

//        /// <summary>
//        /// Check whether current HTTP request is available
//        /// </summary>
//        /// <returns>True if available; otherwise false</returns>
//        protected virtual bool IsRequestAvailable()
//        {
//            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
//                return false;

//            try
//            {
//                if (_httpContextAccessor.HttpContext.Request == null)
//                    return false;
//            }
//            catch (Exception)
//            {
//                return false;
//            }

//            return true;
//        }

//        protected virtual bool IsIpAddressSet(IPAddress address)
//        {
//            return address != null && address.ToString() != NullIpAddress;
//        }


//        #endregion

//        #region Methods

//        /// <summary>
//        /// Get context IP address
//        /// </summary>
//        /// <returns>URL referrer</returns>
//        public virtual string GetCurrentIpAddress()
//        {
//            if (!IsRequestAvailable())
//                return string.Empty;

//            var result = string.Empty;
//            try
//            {
//                //first try to get IP address from the forwarded header
//                if (_httpContextAccessor.HttpContext.Request.Headers != null)
//                {
//                    //the X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a client
//                    //connecting to a web server through an HTTP proxy or load balancer
//                    var forwardedHttpHeaderKey = "X-FORWARDED-FOR";
//                    if (!string.IsNullOrEmpty(_hostingConfig.ForwardedHttpHeader))
//                    {
//                        //but in some cases server use other HTTP header
//                        //in these cases an administrator can specify a custom Forwarded HTTP header (e.g. CF-Connecting-IP, X-FORWARDED-PROTO, etc)
//                        forwardedHttpHeaderKey = _hostingConfig.ForwardedHttpHeader;
//                    }

//                    var forwardedHeader = _httpContextAccessor.HttpContext.Request.Headers[forwardedHttpHeaderKey];
//                    if (!StringValues.IsNullOrEmpty(forwardedHeader))
//                        result = forwardedHeader.FirstOrDefault();
//                }

//                //if this header not exists try get connection remote IP address
//                if (string.IsNullOrEmpty(result) && _httpContextAccessor.HttpContext.Connection.RemoteIpAddress != null)
//                    result = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
//            }
//            catch { return string.Empty; }

//            //some of the validation
//            if (result != null && result.Equals("::1", StringComparison.OrdinalIgnoreCase))
//                result = "127.0.0.1";

//            //"TryParse" doesn't support IPv4 with port number
//            if (IPAddress.TryParse(result ?? string.Empty, out IPAddress ip))
//                //IP address is valid 
//                result = ip.ToString();
//            else if (!string.IsNullOrEmpty(result))
//                //remove port
//                result = result.Split(':').FirstOrDefault();

//            return result;
//        }


//        public virtual string GetThisPageUrl(bool includeQueryString)
//        {
//            bool useSsl = IsCurrentConnectionSecured();
//            return GetThisPageUrl(includeQueryString, useSsl);
//        }

//        public virtual string GetThisPageUrl(bool includeQueryString, bool useSsl)
//        {
//            if (!IsRequestAvailable())
//                return string.Empty;

//            //get the host considering using SSL
//            var url = GetStoreHost(useSsl).TrimEnd('/');

//            //get full URL with or without query string
//            url += includeQueryString ? GetRawUrl(_httpContextAccessor.HttpContext.Request)
//                : $"{_httpContextAccessor.HttpContext.Request.PathBase}{_httpContextAccessor.HttpContext.Request.Path}";

//            return url.ToLowerInvariant();
//        }

//        public bool HasUserAgent(HttpRequest request)
//        {
//            return !String.IsNullOrEmpty(request.Headers["User-Agent"]);
//        }

//        public virtual string ModifyQueryString(string url, string queryStringModification, string anchor)
//        {
//            if (url == null)
//                url = string.Empty;
//            url = url.ToLowerInvariant();

//            if (queryStringModification == null)
//                queryStringModification = string.Empty;
//            queryStringModification = queryStringModification.ToLowerInvariant();

//            if (anchor == null)
//                anchor = string.Empty;
//            anchor = anchor.ToLowerInvariant();


//            string str = string.Empty;
//            string str2 = string.Empty;
//            if (url.Contains("#"))
//            {
//                str2 = url.Substring(url.IndexOf("#") + 1);
//                url = url.Substring(0, url.IndexOf("#"));
//            }
//            if (url.Contains("?"))
//            {
//                str = url.Substring(url.IndexOf("?") + 1);
//                url = url.Substring(0, url.IndexOf("?"));
//            }
//            if (!string.IsNullOrEmpty(queryStringModification))
//            {
//                if (!string.IsNullOrEmpty(str))
//                {
//                    var dictionary = new Dictionary<string, string>();
//                    foreach (string str3 in str.Split(new[] { '&' }))
//                    {
//                        if (!string.IsNullOrEmpty(str3))
//                        {
//                            string[] strArray = str3.Split(new[] { '=' });
//                            if (strArray.Length == 2)
//                            {
//                                if (!dictionary.ContainsKey(strArray[0]))
//                                {
//                                    //do not add value if it already exists
//                                    //two the same query parameters? theoretically it's not possible.
//                                    //but MVC has some ugly implementation for checkboxes and we can have two values
//                                    //find more info here: http://www.mindstorminteractive.com/topics/jquery-fix-asp-net-mvc-checkbox-truefalse-value/
//                                    //we do this validation just to ensure that the first one is not overridden
//                                    dictionary[strArray[0]] = strArray[1];
//                                }
//                            }
//                            else
//                            {
//                                dictionary[str3] = null;
//                            }
//                        }
//                    }
//                    foreach (string str4 in queryStringModification.Split(new[] { '&' }))
//                    {
//                        if (!string.IsNullOrEmpty(str4))
//                        {
//                            string[] strArray2 = str4.Split(new[] { '=' });
//                            if (strArray2.Length == 2)
//                            {
//                                dictionary[strArray2[0]] = strArray2[1];
//                            }
//                            else
//                            {
//                                dictionary[str4] = null;
//                            }
//                        }
//                    }
//                    var builder = new StringBuilder();
//                    foreach (string str5 in dictionary.Keys)
//                    {
//                        if (builder.Length > 0)
//                        {
//                            builder.Append("&");
//                        }
//                        builder.Append(str5);
//                        if (dictionary[str5] != null)
//                        {
//                            builder.Append("=");
//                            builder.Append(dictionary[str5]);
//                        }
//                    }
//                    str = builder.ToString();
//                }
//                else
//                {
//                    str = queryStringModification;
//                }
//            }
//            if (!string.IsNullOrEmpty(anchor))
//            {
//                str2 = anchor;
//            }
//            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)) + (string.IsNullOrEmpty(str2) ? "" : ("#" + str2))).ToLowerInvariant();
//        }

//        public virtual string RemoveQueryString(string url, string queryString)
//        {
//            if (url == null)
//                url = string.Empty;
//            url = url.ToLowerInvariant();

//            if (queryString == null)
//                queryString = string.Empty;
//            queryString = queryString.ToLowerInvariant();


//            string str = string.Empty;
//            if (url.Contains("?"))
//            {
//                str = url.Substring(url.IndexOf("?") + 1);
//                url = url.Substring(0, url.IndexOf("?"));
//            }
//            if (!string.IsNullOrEmpty(queryString))
//            {
//                if (!string.IsNullOrEmpty(str))
//                {
//                    var dictionary = new Dictionary<string, string>();
//                    foreach (string str3 in str.Split(new[] { '&' }))
//                    {
//                        if (!string.IsNullOrEmpty(str3))
//                        {
//                            string[] strArray = str3.Split(new[] { '=' });
//                            if (strArray.Length == 2)
//                            {
//                                dictionary[strArray[0]] = strArray[1];
//                            }
//                            else
//                            {
//                                dictionary[str3] = null;
//                            }
//                        }
//                    }
//                    dictionary.Remove(queryString);

//                    var builder = new StringBuilder();
//                    foreach (string str5 in dictionary.Keys)
//                    {
//                        if (builder.Length > 0)
//                        {
//                            builder.Append("&");
//                        }
//                        builder.Append(str5);
//                        if (dictionary[str5] != null)
//                        {
//                            builder.Append("=");
//                            builder.Append(dictionary[str5]);
//                        }
//                    }
//                    str = builder.ToString();
//                }
//            }
//            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)));
//        }

//        public virtual T QueryString<T>(string name)
//        {
//            if (!IsRequestAvailable())
//                return default(T);

//            if (StringValues.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Query[name]))
//                return default(T);

//            return CommonHelper.To<T>(_httpContextAccessor.HttpContext.Request.Query[name].ToString());
//        }

  
//        public virtual bool IsRequestBeingRedirected
//        {
//            get
//            {
//                var response = _httpContextAccessor.HttpContext.Response;
//                //ASP.NET 4 style - return response.IsRequestBeingRedirected;
//                int[] redirectionStatusCodes = { 301, 302 };
//                return redirectionStatusCodes.Contains(response.StatusCode);
//            }
//        }


//        public virtual bool IsPostBeingDone
//        {
//            get
//            {
//                if (_httpContextAccessor.HttpContext.Items["grand.IsPOSTBeingDone"] == null)
//                    return false;

//                return Convert.ToBoolean(_httpContextAccessor.HttpContext.Items["grand.IsPOSTBeingDone"]);
//            }
//            set
//            {
//                _httpContextAccessor.HttpContext.Items["grand.IsPOSTBeingDone"] = value;
//            }
//        }

//        public virtual bool IsLocalRequest(HttpRequest req)
//        {
//            //source: https://stackoverflow.com/a/41242493/7860424
//            var connection = req.HttpContext.Connection;
//            if (IsIpAddressSet(connection.RemoteIpAddress))
//            {
//                //We have a remote address set up
//                return IsIpAddressSet(connection.LocalIpAddress)
//                    //Is local is same as remote, then we are local
//                    ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress)
//                    //else we are remote if the remote IP address is not a loopback address
//                    : IPAddress.IsLoopback(connection.RemoteIpAddress);
//            }

//            return true;
//        }

//        public virtual string GetRawUrl(HttpRequest request)
//        {
//            //first try to get the raw target from request feature
//            //note: value has not been UrlDecoded
//            var rawUrl = request.HttpContext.Features.Get<IHttpRequestFeature>()?.RawTarget;

//            //or compose raw URL manually
//            if (string.IsNullOrEmpty(rawUrl))
//                rawUrl = $"{request.PathBase}{request.Path}{request.QueryString}";

//            return rawUrl;
//        }
//        #endregion
//    }





//}
