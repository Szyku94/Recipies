using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebScrapper
{
    public class KwestiaSmaku:Scrapper
    {
        public KwestiaSmaku() : base("https://www.kwestiasmaku.com", "/przepisy/posilki") { }

        protected override IEnumerable<string> GetMainPageLinks()
        {
            foreach (var recipie in GetRecipieLinks(MainPage))
            {
                yield return recipie;
            }
            for (var i = 1; ; i++)
            {
                var links = GetRecipieLinks(MainPage + $"?page={i}");
                if (!links.Any())
                {
                    yield break;
                }
                foreach (var recipie in links)
                {
                    yield return recipie;
                }
            }
        }

        protected override Recipie GetRecipie(string url)
        {
            var html = GetHtml(url);
            var recipie = new Recipie
            {
                Name = html.CssSelect("h1.page-header.przepis").First().InnerText,
                Instructions = html.CssSelect(".field-name-field-przygotowanie li").Select(instruction => Regex.Replace(instruction.InnerText, @"[\n\r\t]", "")),
                Ingredients = html.CssSelect(".group-skladniki li").Select(ingredient => Regex.Replace(ingredient.InnerText, @"[\n\r\t]", "")),
                Link = url
            };
            return recipie;
        }

        private IEnumerable<string> GetRecipieLinks(string url)
        {
            var html = GetHtml(url);
            var container = html.CssSelect(".views-bootstrap-grid-plugin-style");
            var links = container.CssSelect(".col > a");
            return links.Select(link => link.Attributes)
                .Where(attributes => attributes.Contains("href"))
                .Select(attributes => attributes["href"].Value)
                .Distinct()
                .Select(link => RootUrl + link)
                .ToList();
        }
    }
}
