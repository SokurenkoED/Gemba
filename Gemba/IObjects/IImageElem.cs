using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.IObjects
{
    interface IImageElem
    {
        public int ImageElemId { get; set; }
        public int ImageElemType { get; set; }
        public int XPoint { get; set; }
        public int YPoint { get; set; }
        public float ImageElemFlipPort { get; set; }
        public string ImageElemValue { get; set; }

        // Что еще возможно стоит добавить

        public int ImageElemPortsCount { get; set; }

    }
}
