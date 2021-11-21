﻿using Gemba.IObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gemba.Objects
{
    class Variable : IVariable
    {
        public int VarId { get; set; }
        public int VarType { get; set; }
        public string VarValue { get; set; }

        public double SolvVar { get; set; }
    }
}
