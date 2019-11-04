using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class CodeManager
    {
        public int Id { get; set; }
        public string Prefix { get; set; }
        public string DateFormat { get; set; }
        public int NumberOfZeroInNumber { get; set; }
        public int Index { get; set; }
        public string CodeDefine { get; set; }
        public int Element { get; set; }
    }
}
