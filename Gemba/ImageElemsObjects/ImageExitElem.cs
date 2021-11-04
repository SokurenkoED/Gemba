using Gemba.ImageObjects;
using Gemba.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.ImageElemsObjects
{
    class ImageExitElem : ImageElemObject
    {
        private Exit ExitElem1;
        public ImageExitElem(ref List<Elem> Elems)
        {
            ExitElem1 = new Exit();
            Elems.Add(ExitElem1);
        }
        public void SetParamsToElem()
        {
            ExitElem1.Operation = 5;
            ExitElem1.ElemId = ImageElemId;
            ExitElem1.ElemValue = ImageElemValue;
        }
    }
}
