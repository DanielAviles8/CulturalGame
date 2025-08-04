using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TakeDamage : MonoBehaviour, IDamageable
{
    [SerializeField] private InputActionsHolder _inputActionsHolder;
    private GameInputActions _inputActions;

    public float _health = 100;
    [SerializeField] public static bool _invulnerable;
    [SerializeField] private float _timer;
    [SerializeField] public static bool Death;

    [SerializeField] private TextMeshProUGUI _healthText;

    [Header("EnemyDamageStats")]
    [SerializeField] private float _enemyDamage = 35f;
    // Start is called before the first frame update
    void Start()
    {
        Prepare();
        _invulnerable = false;
        Death = false;
    }
    private void OnDestroy()
    {
        _inputActions.Player.Heal.performed -= HealPlayer;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            IDamageable damageable = GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.DoDamage(_enemyDamage);
            }
        }
    }
    private void Prepare()
    {
        _inputActions = _inputActionsHolder._GameInputActions;
        _inputActions.Player.Heal.performed += HealPlayer;
    }
    // Update is called once per frame
    void Update()
    {
        if (_invulnerable)
        {
            _timer += Time.deltaTime;

            if (_timer >= 1f)
            {
                _invulnerable = false;
                _timer = 0f;
            }
        }

        if (_health >= 66)
        {
            _healthText.text = "";
        }
        else if( _health == 65)
        {
            _healthText.text = "Advertencia";
            _healthText.color = Color.yellow;
        }
        else if(_health == 30)
        {
            _healthText.text = "Peligro";
            _healthText.color = Color.red;
        }
    }
    public void DoDamage(float damage)
    {
        if (_invulnerable == false)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Debug.Log("Mori");
                Death = true;
            }
            _invulnerable = true;
            _timer = 0;
        }
    }
    public void HealPlayer(InputAction.CallbackContext ctx)
    {
        if (_health >= 66) return;
        if (PlayerMovement._healtPots > 0)
        {
            _health = _health + 35;
            PlayerMovement._healtPots--;
        }
    }
}