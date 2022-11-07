using System.Threading;
using Cysharp.Threading.Tasks;
using Lev.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class RoamingBehaviorTreeNode : BehaviorTreeNode
{
    private readonly float _areaRadius;
    private readonly Vector3 _areaCenter;
    private readonly NavMeshAgent _navMeshAgent;

    public RoamingBehaviorTreeNode(Vector3 areaCenter, float areaRadius, NavMeshAgent navMeshAgent)
    {
        _areaRadius = areaRadius;
        _areaCenter = areaCenter;
        _navMeshAgent = navMeshAgent;
    }

    protected override async UniTask<bool> ExecuteOverride(CancellationToken ct)
    {
        _navMeshAgent.SetDestination(GetRandomLocation());
        await UniTask.WaitUntil(IsDestinationReached, cancellationToken: ct);
        return !ct.IsCancellationRequested;
    }

    private bool IsDestinationReached() => 
        _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance;

    private Vector3 GetRandomLocation()
    {
        var randomPoint = Random.insideUnitCircle * _areaRadius;
        return _areaCenter + new Vector3(randomPoint.x, 0.0f, randomPoint.y);
    }
}
