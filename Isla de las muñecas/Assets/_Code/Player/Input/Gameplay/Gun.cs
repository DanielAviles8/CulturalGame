using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private ParticleSystem _shootParticle;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private ParticleSystem _impactParticle;
    [SerializeField] private TrailRenderer _bulletTrail;
    [SerializeField] private float _shootDelay = 0.5f;
    [SerializeField] private LayerMask _layerMask;

    private float _lastShootTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot()
    {
        if (_lastShootTime + _shootDelay < Time.time)
        {
            _shootParticle.Play();
            Vector3 direction = GetDirection();

            if (Physics.Raycast(_bulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, _layerMask))
            {
                TrailRenderer trail = Instantiate(_bulletTrail, _bulletSpawnPoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit));
                _lastShootTime = Time.time;
            }
        }
    }
    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;
        return direction;
    }
    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while(time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = hit.point;
        Instantiate(_impactParticle, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(trail.gameObject, trail.time);
    }
}
