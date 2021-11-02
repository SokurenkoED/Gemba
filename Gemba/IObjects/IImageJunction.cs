using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.IObjects
{
    interface IImageJunction
    {
        public int ImageJunctId { get; set; }
        public int ImageJunctType { get; set; }
        public int ImageJunctFromElemId { get; set; }
        public int ImageJunctToElemId { get; set; }
        public int ImageJunctFromPortId { get; set; }
        public int ImageJunctToPortId { get; set; }
    }
}
