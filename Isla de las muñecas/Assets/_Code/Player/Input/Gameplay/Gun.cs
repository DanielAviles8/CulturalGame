using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shootParticle;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private ParticleSystem _impactParticle;
    [SerializeField] private TrailRenderer _bulletTrail;
    [SerializeField] private float _shootDelay = 0.5f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private TextMeshProUGUI _bulletText;

    [Header("GunStats")]
    [SerializeField] public static float _bulletRemaining;
    public static float _magSize;
    public static float _bulletBackUps;
    public static float _reloadTime = 1.5f;
    private float _maxValueHit = 100f;
    [SerializeField] public static bool _reloading;


    private float _lastShootTime;
    // Start is called before the first frame update
    void Start()
    {
        _magSize = 12f;
        _bulletRemaining = 10;
        _reloading = false;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = GetDirection();
        Debug.DrawRay(_bulletSpawnPoint.position, direction * 1000, Color.red);
        if (_bulletRemaining > _magSize)
        {
            _bulletBackUps = _bulletRemaining - _magSize;
            _bulletRemaining = _magSize;
        }
        _bulletText.text = _bulletRemaining.ToString();
    }
    public void Shoot()
    {
        if (_lastShootTime + _shootDelay < Time.time && _bulletRemaining > 0 && !_reloading)
        {
            _bulletRemaining--;
            _shootParticle.Play();
            Vector3 direction = GetDirection();

            Ray ray = new Ray(_bulletSpawnPoint.position/* + transform.parent.position*/, direction);
            if (Physics.Raycast(ray, out RaycastHit hit, _maxValueHit, _layerMask))
            {
                Debug.Log(hit.collider.gameObject.name);

                IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.DoDamage(35);
                }

                StartCoroutine(SpawnTrail(_bulletSpawnPoint, hit.point/* + _bulletSpawnPoint.position*/));

                _lastShootTime = Time.time;
            }
            else
            {
                Debug.Log("NoHitteanding");

                var maxRay = ray.GetPoint(_maxValueHit);

                StartCoroutine(SpawnTrail(_bulletSpawnPoint, maxRay/* + _bulletSpawnPoint.position*/));

                _lastShootTime = Time.time;
            }
        }
    }
    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;
        return direction;
    }
    private IEnumerator SpawnTrail(Transform TrailOrigin, Vector3 hitTarget)
    {
        float time = 0;
        Vector3 startPosition = TrailOrigin.transform.position;
        TrailRenderer trail = Instantiate(_bulletTrail, _bulletSpawnPoint.position, Quaternion.identity);

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hitTarget, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }
        trail.transform.position = hitTarget;

        Destroy(trail.gameObject, trail.time);
        yield break;
    }
}