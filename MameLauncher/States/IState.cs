using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MameLauncher.States
{
    public interface IState
    {
        void UpdateState();
        void Init();
        void Init(string Value);
    }
}
