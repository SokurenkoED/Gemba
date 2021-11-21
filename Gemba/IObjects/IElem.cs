using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.IObjects
{
    interface IElem
    {
        public int ElemId { get; set; }

        public int Operation { get; set; }

        public int TypeVariable { get; set; }
    }
}
