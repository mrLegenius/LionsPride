using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Lev.BehaviorTree
{
public class SequenceBehaviorTreeNode : BehaviorTreeNode
{
    private readonly List<BehaviorTreeNode> _childNodes;

    public SequenceBehaviorTreeNode(List<BehaviorTreeNode> nodes)
    {
        _childNodes = nodes;
    }
    
    protected override async UniTask<bool> ExecuteOverride(CancellationToken ct)
    {
        foreach (var child in _childNodes)
        {
            await child.Execute(ct);

            if (ct.IsCancellationRequested) return false;
        }
        
        return true;
    }
}
}