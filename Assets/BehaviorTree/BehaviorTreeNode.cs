using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Lev.BehaviorTree
{
public abstract class BehaviorTreeNode
{
    private readonly List<Func<bool>> _conditions = new();

    private Action _onEntered;
    private Action<bool> _onExited;
    public BehaviorTreeNode AddCondition(Func<bool> condition)
    {
        _conditions.Add(condition);
        return this;
    }

    public BehaviorTreeNode OnEntered(Action callback)
    {
        _onEntered += callback;
        return this;
    }
    
    public BehaviorTreeNode OnExited(Action<bool> callback)
    {
        _onExited += callback;
        return this;
    }
    
    protected abstract UniTask<bool> ExecuteOverride(CancellationToken ct);

    public async UniTask<bool> Execute(CancellationToken ct)
    {
        foreach (Func<bool> condition in _conditions)
        {
            if (!condition()) 
                return false;
        }

        _onEntered?.Invoke();
        bool success = await ExecuteOverride(ct);
        _onExited?.Invoke(success);
        return success;
    }
    
    
}
}