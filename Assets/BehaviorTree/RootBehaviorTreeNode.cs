using System.Threading;
using Cysharp.Threading.Tasks;

namespace Lev.BehaviorTree
{
public class RootBehaviorTreeNode : BehaviorTreeNode
{
    private readonly BehaviorTreeNode _next;
    private CancellationTokenSource _cts;
    public RootBehaviorTreeNode(BehaviorTreeNode next)
    {
        _next = next;
    }

    public void StartBehaviourTree()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        _cts = new CancellationTokenSource();
        Execute(_cts.Token).Forget();
    }

    public void StopBehaviourTree()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }
    
    public override async UniTask<bool> Execute(CancellationToken ct)
    {
        while (true)
        {
            await _next.Execute(ct);

            if (ct.IsCancellationRequested) return false;
        }
    }
}
}
