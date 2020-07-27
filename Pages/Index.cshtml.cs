using System.Collections.Generic;
using UrlShortener.Context;
using UrlShortener.ExtensionClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace UrlShortener.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        
        public List<ShortenedUrl> TemporaryList;

        public void OnGetAsync()
        {
            var tempDb = HttpContext.Session.GetObject<TempDb>("TempDb");
            if (tempDb == null)
            {
                tempDb = new TempDb();
                tempDb.urlList = new List<ShortenedUrl>();
                HttpContext.Session.SetObject("TempDb", tempDb);
            }

            TemporaryList = tempDb.urlList;
        }
    }
}
