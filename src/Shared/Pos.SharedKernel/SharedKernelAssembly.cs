using System.Reflection;

namespace Pos.SharedKernel;

public static class SharedKernelAssembly
{
    public static readonly Assembly Assembly = typeof(SharedKernelAssembly).Assembly;
}
