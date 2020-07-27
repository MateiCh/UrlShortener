using UrlShortener.Context;
using UrlShortener.ExtensionClasses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Controllers
{
    public class ShortenerController : ControllerBase
    {
        [HttpGet, Route("/{shortId}")]
        public IActionResult GetRedirect(string shortId)
        {
            var tempDb = HttpContext.Session.GetObject<TempDb>("TempDb");
            if (tempDb != null)
            {
                var actualLink = tempDb.urlList.FirstOrDefault(x => x.ShortUrl.Equals(shortId));
                if (actualLink != null)
                    return RedirectPermanent(actualLink.Url);
            }
            return Redirect("/");
        }

        [HttpGet, Route("api/ShortenLink")]
        public async Task<IActionResult> OnGetShortenLinkAsync(string originalUrl)
        {
            ShortenedUrl newShortUrl = new ShortenedUrl();
            originalUrl = originalUrl.ToLower();
            if (!originalUrl.StartsWith("https://") && !originalUrl.StartsWith("http://"))
                originalUrl = "https://" + originalUrl;
            newShortUrl.Url = originalUrl;
            newShortUrl.ShortUrl = GetRandomString();

            var tempList = HttpContext.Session.GetObject<TempDb>("TempDb");
            tempList.urlList.Add(newShortUrl);
            HttpContext.Session.SetObject("TempDb", tempList);

            return new JsonResult(newShortUrl)
            {
                ContentType = "text/json",
                StatusCode = 200,
            };
        }

        private string GetRandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
                stringChars[i] = chars[random.Next(chars.Length)];

            return new string(stringChars);
        }
    }
}

