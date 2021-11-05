using Gemba.IObjects;
using Gemba.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.ImageJunctionsObjects
{
    class ImageJunctionObject : IImageJunction
    {
        public int ImageJunctId { get; set; }
        public int ImageJunctType { get; set; }
        public int ImageJunctFromElemId { get; set; }
        public int ImageJunctToElemId { get; set; }
        public int ImageJunctFromPortId { get; set; }
        public int ImageJunctToPortId { get; set; }

        Junction Junction1;
        public ImageJunctionObject(ref List<Junction> Junctions)
        {
            Junction1 = new Junction();
            Junctions.Add(Junction1);

        }
        
        public void SetParamsToJunction( ref List<Elem> Elems, ref List<Junction> ConstJunctions)
        {
            Junction1.FromElem = ImageJunctFromElemId;
            Junction1.FromPort = ImageJunctFromPortId;
            Junction1.JuncId = ImageJunctId;
            Junction1.ToElem = ImageJunctToElemId;
            Junction1.ToPort = ImageJunctToPortId;

            if (Elems[Junction1.FromElem].Operation == 3)
            {
                ConstJunctions.Add(Junction1); // Эмитируем метод, в котором проверяется левый элемент, если это константа, то добавляем в массив
            }
            
        }
    }
}
