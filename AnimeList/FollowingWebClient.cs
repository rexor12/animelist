using System;
using System.Net;

namespace AnimeList
{
    public class FollowingWebClient : WebClient
    {
        private CookieContainer _cookies = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                var req = request as HttpWebRequest;
                req.AllowAutoRedirect = true;
                req.CookieContainer = _cookies;
            }
            return request;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            var response = base.GetWebResponse(request);
            if (response is HttpWebResponse)
            {
                var res = response as HttpWebResponse;
                _cookies.Add(res.Cookies);
            }
            return response;
        }
    }
}