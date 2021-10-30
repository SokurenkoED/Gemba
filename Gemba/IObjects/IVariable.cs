using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.IObjects
{
    interface IVariable
    {
        public int VarId { get; set; }

        public int VarType { get; set; }

        public string VarValue { get; set; }

        public float SolvVar { get; set; }
    }
}
