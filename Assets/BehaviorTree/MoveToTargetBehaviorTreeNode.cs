using System.Threading;
using Cysharp.Threading.Tasks;
using Lev.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

namespace Lev.BehaviorTree
{
public class MoveToTargetBehaviorTreeNode : BehaviorTreeNode
{
    private readonly GameObject _target;
    private readonly NavMeshAgent _navMeshAgent;
    
    public MoveToTargetBehaviorTreeNode(GameObject target, NavMeshAgent navMeshAgent)
    {
        _target = target;
        _navMeshAgent = navMeshAgent;
    }
    protected override async UniTask<bool> ExecuteOverride(CancellationToken ct)
    {
        _navMeshAgent.SetDestination(_target.transform.position);
        await UniTask.WaitUntil(IsDestinationReached, cancellationToken: ct);
        return !ct.IsCancellationRequested;
    }
    
    private bool IsDestinationReached() => 
        _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;
}
}
