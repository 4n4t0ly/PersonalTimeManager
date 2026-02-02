using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Core
{
    internal class PrimitiveTask
    {
        protected string _name = "";
        protected string _description = "";
        protected byte _priority = 5; //1 - lowest, 10 - highest
        protected byte _dificulty = 3; //1 - lowest, 5 - highest
        protected bool _isItDone = false;
    }
}
