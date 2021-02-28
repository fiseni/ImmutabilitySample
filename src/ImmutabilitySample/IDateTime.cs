using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutabilitySample
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
