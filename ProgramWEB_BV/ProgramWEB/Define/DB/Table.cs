using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramWEB.Define.DB
{
    public class Table
    {
        public string name { set; get; }
        public string[]thuocTinhs { set; get; }
        public string[]tenTiengViet { set; get; }
        public bool[]required { set; get; }
        public Table() {
            name = "";
            thuocTinhs = new string[] {};
            tenTiengViet = new string[] {};
        }
        public Table(string name, string[]thuocTinhs, string[]tenTiengViet)
        {
            this.name = name;
            this.thuocTinhs = thuocTinhs;
            this.tenTiengViet = tenTiengViet;
        }
        public Table(string name, string[] thuocTinhs, string[] tenTiengViet, bool[] required)
        {
            this.name = name;
            this.thuocTinhs = thuocTinhs;
            this.tenTiengViet = tenTiengViet;
            this.required = required;
        }
    }
}