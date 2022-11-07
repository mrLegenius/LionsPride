using System.Threading;
using Cysharp.Threading.Tasks;

namespace Lev.BehaviorTree
{
public abstract class BehaviorTreeNode
{
    public abstract UniTask<bool> Execute(CancellationToken ct);
}
}