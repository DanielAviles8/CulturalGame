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
    
    [SerializeField] private Light _enemyEyes;
    
    [SerializeField] private float _crouchTimer;
    [SerializeField] private float _chaseTimer;

    [SerializeField] private float _distanceToCountExit = 10;
    [SerializeField] private float _timeTillExit = 3;
    [SerializeField] private float _exitTimer = 0;

    private bool isPlayerDetected;
    private bool isPlayerSus;

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
        _enemyEyes.color = Color.yellow;
    }
    public void UpdatingWanderingState()
    {
        if(!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            _targetPos = GetRandomPointInCircle();
            _agent.SetDestination(_targetPos);
        }
        if (ChaseTrigger.Chase == true)
        {
            isPlayerSus = true;
        }
    }
    public void ExitingWanderingState()
    {

    }
    public void EnteringSusState()
    {

    }
    public void UpdatingSusState()
    {
        if (CameraController._isCrouched)
        {
            _crouchTimer += Time.deltaTime;
            if (_crouchTimer > _chaseTimer)
            {
                _agent.SetDestination(_player.transform.position);
                _enemyEyes.color = Color.red;
                isPlayerDetected = true;
            }
        }
        else
        {
            _agent.SetDestination(_player.transform.position);
            _enemyEyes.color = Color.red;
            _crouchTimer = 0;
            isPlayerDetected = true;
        }

    }
    public void ExitingSusState()
    {
        isPlayerSus = false;
    }
    public void EnteringChasingState()
    {

    }
    public void UpdatingChasingState()
    {
        if(_player != null)
        {
            _agent.SetDestination(_player.transform.position);
        }

        if(ChaseTrigger.Chase == false || Vector2.Distance(_player.transform.position, _agent.transform.position) > _distanceToCountExit)          
        {
            _exitTimer += Time.deltaTime;
            if (_exitTimer > _timeTillExit)
            {
                isPlayerDetected = false;
            }
        }
    }
    public void ExitingChasingState()
    {
         _exitTimer = 0;
        isPlayerDetected = false;
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
        var susState = new SusState(this);
        var chasingState = new ChasingState(this);
        stateMachine.AddTransition(wanderingState, susState, new FuncPredicate(() => isPlayerSus == true));
        stateMachine.AddTransition(susState, chasingState, new FuncPredicate(() => isPlayerDetected == true));
        stateMachine.AddTransition(chasingState, wanderingState, new FuncPredicate(() => isPlayerDetected == false));
        stateMachine.SetState(wanderingState);
    }
}
