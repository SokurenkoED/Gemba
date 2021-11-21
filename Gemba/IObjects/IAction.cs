using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.IObjects
{
    interface IAction
    {
        public int ActionId { get; set; }
        public int Out1 { get; set; }
        public int Operation { get; set; }
        public int VarTypeOut { get; set; }
        public List<int> VarTypeIn { get; set; }
        public List<int> In { get; set; }
    }
}
