using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Serialization
{
    public interface ITraceResultSerializer
    {
        void Serialize(TraceResultDto traceResult, string filename);
    }
}
