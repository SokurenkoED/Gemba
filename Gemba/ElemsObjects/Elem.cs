using Gemba.IObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.Objects
{
    class Elem : IElem
    {
        public int ElemId { get; set; }

        public int Operation { get; set; }
    }
}
