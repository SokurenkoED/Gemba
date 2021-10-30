using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.IObjects
{
    interface IJunction
    {
        public int JuncId { get; set; }

        public int FromElem { get; set; }

        public int FromPort { get; set; }

        public int ToElem { get; set; }

        public int ToPort { get; set; }


    }
}
