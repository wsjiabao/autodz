using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTP.TAlib
{
    public interface IKLine
    {
        float OPEN { get; }
        float CLOSE { get; }
        float HIGH { get; }
        float LOW { get; }
    }
}
