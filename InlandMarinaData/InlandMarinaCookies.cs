using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlandMarinaData
{
    public class InlandMarinaCookies
    {
        private const string SlipsKey = "myslips";
        private const string Delimiter = "-";

        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }
        public InlandMarinaCookies(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
        }
        public InlandMarinaCookies(IResponseCookies cookies)
        {
            responseCookies = cookies;
        }

        public void SetMySlipsIds(List<Slip> myslips)
        {
            List<int> ids = myslips.Select(s => s.ID).ToList();
            string idsString = String.Join(Delimiter, ids);
            CookieOptions options = new CookieOptions { Expires = DateTime.Now.AddDays(30) };
            RemoveMySlipsIds();     // delete old cookie first
            responseCookies.Append(SlipsKey, idsString, options);
        }

        public string[] GetMySlipsIds()
        {
            string cookie = requestCookies[SlipsKey];
            if (string.IsNullOrEmpty(cookie))
                return new string[] { };   // empty string array
            else
                return cookie.Split(Delimiter);
        }

        public void RemoveMySlipsIds()
        {
            responseCookies.Delete(SlipsKey);
        }
    }
}

