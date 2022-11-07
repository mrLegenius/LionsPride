using System.Collections.Generic;
using Lev.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

namespace Lev
{
public class Lioness : MonoBehaviour
{
    [Header("Thirst")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _thirstLevel = 0.3f;
    [SerializeField] private float _thirstGrowthRate = 0.01f;
    
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _thirstThreshold = 0.4f;
    [SerializeField] private float _drinkRate = 0.01f;
    [SerializeField] private GameObject _waterSource;
    
    [Header("Rest")]
    [SerializeField] private float _restAreaRadius;
    [SerializeField] private float _restTime;
    
    [Header("Dependencies")]
    [SerializeField] private NavMeshAgent _navMeshAgent;
    
    private RootBehaviorTreeNode _behaviorTree;

    private bool _isDrinking;

    private float ThirstLevel
    {
        get => _thirstLevel;
        set => _thirstLevel = Mathf.Clamp01(value);
    }

    private void Awake()
    {
        _behaviorTree = CreateBehaviorTree();
    }

    private void Start()
    {
        _behaviorTree.StartBehaviourTree();
    }

    private void Update()
    {
        if (!_isDrinking)
            ThirstLevel += _thirstGrowthRate;
    }

    private void OnDestroy()
    {
        _behaviorTree.StopBehaviourTree();
    }

    private RootBehaviorTreeNode CreateBehaviorTree() =>
        new(new SelectorBehaviorTreeNode(
            new List<BehaviorTreeNode>
            {
                new SequenceBehaviorTreeNode( 
                    new List<BehaviorTreeNode>
                    {
                        new MoveToTargetBehaviorTreeNode(_waterSource, _navMeshAgent),
                        new WaterDrinkBehaviorTreeNode(() => ThirstLevel <= 0.1f, () => ThirstLevel -= _drinkRate)
                            .OnEntered(() => _isDrinking = true)
                            .OnExited(_ => _isDrinking = false),
                    })
                    .AddCondition(() => ThirstLevel > _thirstThreshold),
                new SequenceBehaviorTreeNode(
                    new List<BehaviorTreeNode>
                    {
                        new RoamingBehaviorTreeNode(transform.position, _restAreaRadius, _navMeshAgent),
                        new WaitBehaviorTreeNode(_restTime),
                    }),
            }));
}
}
