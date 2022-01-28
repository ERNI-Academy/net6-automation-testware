using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWare.Samples.API.Resources.Sine;

public record SineResponse(string operation, string expression, string result, string error);
