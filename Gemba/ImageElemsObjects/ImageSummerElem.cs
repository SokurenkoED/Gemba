using Gemba.ImageObjects;
using Gemba.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.ImageElemsObjects
{
    class ImageSummerElem : ImageElemObject
    {
        private Summer SumElem;
        public ImageSummerElem( ref List<Elem> Elems)
        {
            SumElem = new Summer();
            Elems.Add(SumElem);
        }
        public void SetParamsToElem()
        {
            SumElem.Operation = 1;
            SumElem.ElemId = ImageElemId;
        }
    }
}
