using System.Threading;
using Cysharp.Threading.Tasks;

namespace Lev.BehaviorTree
{
public class WaitBehaviorTreeNode : BehaviorTreeNode
{
    private readonly float _secondsToWait;
    public WaitBehaviorTreeNode(float seconds)
    {
        _secondsToWait = seconds;
    }
    
    protected override async UniTask<bool> ExecuteOverride(CancellationToken ct)
    {
        await UniTask.Delay((int)(_secondsToWait * 1000), cancellationToken: ct);
        return !ct.IsCancellationRequested;
    }
}
}