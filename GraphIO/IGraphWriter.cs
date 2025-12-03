using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphIO
{
    public interface IGraphWriter
    {
        /// <summary>Записать граф в текстовый поток.</summary>
        void Write(Graph graph, System.IO.TextWriter writer);
    }
}
