using Gemba.IObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.Objects
{
    class Action : IAction
    {
        public int ActionId { get; set; }
        public int Out1 { get ; set ; }
        public int Operation { get ; set ; }
        public List<int> In { get; set; } = new List<int>();
    }
}
