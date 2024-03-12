using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramWEB.Define.DB
{
    public class Proc
    {
        public string name { set; get; } = null;
        public string []param { set; get; } = null;
        public string Name
        {
            set
            {

            }
            get
            {
                string str = name;
                if (param != null && param.Length > 0)
                {
                    for (int i = 0; i < param.Length - 1; i++)
                    {
                        str += " " + param[0] + ",";
                    }
                    str += " " + param[param.Length - 1];
                }
                return name;
            }
        }
        public Proc() { }
        public Proc(string name, string[] param)
        {
            this.name = name;   
            this.param = param;
        }
    }
}