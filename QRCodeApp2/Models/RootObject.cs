using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRCodeApp2.Models
{
    public class RootObject
    {
        public string Type { get; set; }
        public List<Symbol> Symbol { get; set; }
    }

    public class Symbol
    {
        public int Seq { get; set; }
        public string Data { get; set; }
        public object Error { get; set; }
    }
}
