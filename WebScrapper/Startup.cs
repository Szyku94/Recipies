using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace WebScrapper
{
    class Startup
    {
        static void Main(string[] args)
        {
            var scrapper = new KwestiaSmaku();
            var x = scrapper.Scrap().First();
        }
    }
}
