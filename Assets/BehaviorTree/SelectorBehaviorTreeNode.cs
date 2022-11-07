using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Lev.BehaviorTree
{
public class SelectorBehaviorTreeNode : BehaviorTreeNode
{
    private readonly List<BehaviorTreeNode> _childNodes;

    public SelectorBehaviorTreeNode(List<BehaviorTreeNode> nodes)
    {
        _childNodes = nodes;
    }

    public override async UniTask<bool> Execute(CancellationToken ct)
    {
        foreach (var child in _childNodes)
        {
            bool success = await child.Execute(ct);
            if (success) return true;
        }

        return false;
    }
}
}