﻿using Gemba.IObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.ImageObjects
{
    class ImageElemObject : IImageElem
    {
        public int ImageElemId { get ; set ; }
        public int ImageElemType { get ; set ; }
        public int XPoint { get ; set ; }
        public int YPoint { get ; set ; }
        public float ImageElemFlipPort { get ; set ; }
        public string ImageElemValue { get ; set ; }
        public int ImageElemPortsCount { get ; set ; }

        // Инфа о подстоединенных портах и связях

        public List<int> PortsInId { get; set; }
        public List<int> PortsOutId { get; set; }

    }
}
