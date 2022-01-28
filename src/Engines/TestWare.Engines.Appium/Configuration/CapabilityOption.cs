using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWare.Engines.Appium.Configuration;

internal class CapabilityOption<T>
{
    public string Name { get; set; }
    public T Value { get; set; }
}

