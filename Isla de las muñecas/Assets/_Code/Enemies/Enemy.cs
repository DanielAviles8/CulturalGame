using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public StateMachine stateMachine;
    public float Health;
    public static bool Death;

    public float RandomMovementRange = 5f;

    private Vector3 _targetPos;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _player;

    private bool _attack;
    private Collider _enemyCollider;
    // Start is called before the first frame update
    void Start()
    {
        _enemyCollider = GetComponent<Collider>();
        _player = GameObject.FindGameObjectWithTag("Player");
        SetUpStateMachine();
        Death = false;
    }
    // Update is called once per frame
    public void Update()
    {
        stateMachine.Update();
    }
    public void EnteringWanderingState()
    {
        _targetPos = GetRandomPointInCircle();
        _agent.SetDestination(_targetPos);
    }
    public void UpdatingWanderingState()
    {
        if(!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _targetPos = GetRandomPointInCircle();
            _agent.SetDestination(_targetPos);
        }
    }
    public void EnteringChasingState()
    {
        _agent.SetDestination(_player.transform.position);
    }
    public void UpdatingChasingState()
    {
        if(_player != null)
        {
            _agent.SetDestination(_player.transform.position);
        }
    }
    public void DoDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            _enemyCollider.enabled = false;
            Death = true;
        }
    }
    private Vector3 GetRandomPointInCircle()
    {
        return _agent.transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * RandomMovementRange;
    }
    private void SetUpStateMachine()
    {
        stateMachine = new StateMachine();
        var wanderingState = new WanderingState(this);
        var chasingState = new ChasingState(this);
        stateMachine.AddTransition(wanderingState, chasingState, new FuncPredicate(() => ChaseTrigger.Chase == true));
        //stateMachine.AddTransition(chasingState, wanderingState, new FuncPredicate(() => ChaseTrigger.Chase == false));
        stateMachine.SetState(wanderingState);
    }
}
