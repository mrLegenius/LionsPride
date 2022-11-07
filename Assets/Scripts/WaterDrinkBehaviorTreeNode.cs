using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Lev.BehaviorTree;

namespace Lev
{
public class WaterDrinkBehaviorTreeNode : BehaviorTreeNode
{
    private readonly Func<bool> _isFull;
    private readonly Action _drink;

    public WaterDrinkBehaviorTreeNode(Func<bool> isFull, Action drink)
    {
        _isFull = isFull;
        _drink = drink;
    }
    protected override async UniTask<bool> ExecuteOverride(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested && !_isFull())
        {
            _drink();
            await UniTask.Yield();
        }

        return !ct.IsCancellationRequested;
    }
}
}
