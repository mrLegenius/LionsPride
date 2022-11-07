using System;
using System.Collections.Generic;
using Lev.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

namespace Lev
{
public class Lioness : MonoBehaviour
{
    [SerializeField] private float _areaRadius;
    [SerializeField] private float _sleepTime;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    
    private RootBehaviorTreeNode _behaviorTree;

    private void Awake()
    {
        _behaviorTree = CreateBehaviorTree();
    }

    private void Start()
    {
        _behaviorTree.StartBehaviourTree();
    }

    private void OnDestroy()
    {
        _behaviorTree.StopBehaviourTree();
    }

    private RootBehaviorTreeNode CreateBehaviorTree() =>
        new(new SequenceBehaviorTreeNode(
            new List<BehaviorTreeNode>
            {
                new RoamingBehaviorTreeNode(transform.position, _areaRadius, _navMeshAgent),
                new WaitBehaviorTreeNode(_sleepTime)
            }));
}
}
