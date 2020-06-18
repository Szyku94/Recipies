using HtmlAgilityPack;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebScrapper
{
    public abstract class Scrapper
    {
        public readonly string RootUrl;
        public readonly string MainPage;
        protected ScrapingBrowser _browser = new ScrapingBrowser();

        protected Scrapper(string rootUrl, string mainPage)
        {
            RootUrl = rootUrl;
            MainPage = $"{RootUrl}{mainPage}";
        }

        public IEnumerable<Recipie> Scrap()
        {
            var mainPageLinks = GetMainPageLinks();
            return mainPageLinks.Select(GetRecipie);
        }

        protected HtmlNode GetHtml(string url)
        {
            WebPage webpage = _browser.NavigateToPage(new Uri(url));
            return webpage.Html;
        }

        protected abstract IEnumerable<string> GetMainPageLinks();
        protected abstract Recipie GetRecipie(string url);
    }
}
