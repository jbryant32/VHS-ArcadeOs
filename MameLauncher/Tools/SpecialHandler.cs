using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MameLauncher.Tools
{
    public class SpecialHandler
    {
        static SpecialHandler _instance;
        public static SpecialHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SpecialHandler();
                    return _instance;
                }
                else
                {
                    return _instance;
                }
            }
        }
   
    }
}
