using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fakturisanje.Models;

namespace Fakturisanje.viewModel
{
    public class Stavke1
    {
        public IEnumerable<Stavke> Stavken { get; set; }
        public Stavke  Stavke { get; set; }
        public List<Stavke> Stavken1 { get; set; }

    }
}