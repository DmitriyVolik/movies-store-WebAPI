using BLL.Extensions;

namespace BLL.Services;

public static class TreeManager
{
    public static List<T> GetParents<T>(ITree<T> node, List<T> parentNodes = null) where T : class
    {
        while (true)
        {
            parentNodes ??= new List<T>();
            if (node?.Parent?.Data == null) return parentNodes;
            parentNodes.Add(node.Parent.Data);
            node = node.Parent;
        }
    }
}

