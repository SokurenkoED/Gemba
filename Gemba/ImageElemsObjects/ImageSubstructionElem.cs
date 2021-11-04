using Gemba.ImageObjects;
using Gemba.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.ImageElemsObjects
{
    class ImageSubstructionElem : ImageElemObject
    {
        private Subtraction Substruction1;
        public ImageSubstructionElem(ref List<Elem> Elems)
        {
            Substruction1 = new Subtraction();
            Elems.Add(Substruction1);
        }
        public void SetParamsToElem()
        {
            Substruction1.Operation = 2;
            Substruction1.ElemId = ImageElemId;
        }
    }
}
