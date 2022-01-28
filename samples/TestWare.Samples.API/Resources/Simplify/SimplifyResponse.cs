using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWare.Samples.API.Resources.Simplify;

public record SimplifyResponse(string operation, string expression, string result);
