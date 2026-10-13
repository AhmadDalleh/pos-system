using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Pos.SharedKernel
{
    public static class SharedKernelAssembly
    {
        public static readonly Assembly Assembly = typeof(SharedKernelAssembly).Assembly;
    }
}
