using System;
using System.Collections.Generic;
using System.Text;

namespace WebScrapper
{
    public class Recipie
    {
        public string Name { get; set; }
        public IEnumerable<string> Instructions { get; set; }
        public IEnumerable<string> Ingredients { get; set; }
        public string Link { get; set; }
    }
}
