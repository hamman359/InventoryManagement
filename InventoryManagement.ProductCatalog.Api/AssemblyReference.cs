using System.Reflection;

namespace InventoryManagement.ProductCatalog.Api;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}