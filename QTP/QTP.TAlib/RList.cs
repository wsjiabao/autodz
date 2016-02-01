using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTP.TAlib
{
    public class RList<T> : List<T>
    {
        public new T this[int index]
        {
            get 
            {
                return base[this.Count - index - 1];
            }
        }
    }
}
