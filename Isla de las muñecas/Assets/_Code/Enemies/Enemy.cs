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

    private float _crouchTimer = 0;
    private float _chaseTimer = 2;

    [SerializeField] private float _distanceToCountExit = 3;
    [SerializeField] private float _timeTillExit = 3;
    [SerializeField] private float _exitTimer = 0;

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
    public void ExitingWanderingState()
    {

    }
    public void EnteringChasingState()
    {
        if (CameraController._isCrouched)
        {
            _crouchTimer += Time.deltaTime;
            if (_crouchTimer > _chaseTimer)
            {
                _agent.SetDestination(_player.transform.position);
            }
        }
        else
        {
            _agent.SetDestination(_player.transform.position);
            _crouchTimer = 0;
        }
    }
    public void UpdatingChasingState()
    {
        if(_player != null)
        {
            _agent.SetDestination(_player.transform.position);
        }

        if (Vector2.Distance(_player.transform.position, _agent.transform.position) > _distanceToCountExit)
        {
            _exitTimer += Time.deltaTime;
            if (_exitTimer > _timeTillExit)
            {
                ChaseTrigger.Chase = false;
            }
        }
        else
        {
            _exitTimer = 0;
        }
    }
    public void ExitingChasingState()
    {
        ChaseTrigger.Chase = false;
    }
    public void DoDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            _enemyCollider.enabled = false;
            Death = true;
            gameObject.SetActive(false);
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
