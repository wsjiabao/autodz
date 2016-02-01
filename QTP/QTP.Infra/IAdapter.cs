using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTP.Infra
{
    public interface IMdAdapter
    {
        void Run();

        int Subscribe(string symbol, int exchange);
        void Close();
    }
}
