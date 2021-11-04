using Gemba.ImageObjects;
using Gemba.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.ImageElemsObjects
{
    class ImageConstElem : ImageElemObject
    {
        private Const ConstElem1;
        public ImageConstElem(ref List<Elem> Elems)
        {
            ConstElem1 = new Const();
            Elems.Add(ConstElem1);
        }
        public void SetParamsToElem()
        {
            ConstElem1.Operation = 3;
            ConstElem1.ElemId = ImageElemId;
            ConstElem1.ElemValue = ImageElemValue;
        }
    }
}
