using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Context
{
    public class ShortenedUrl
    {
        public string Url { get; set; }
        public string ShortUrl { get; set; }
    }
}
