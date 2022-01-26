using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWare.Samples.API.Resources.Integrate
{
    public record IntegrateResponse(string operation, string expression, string result);
}
