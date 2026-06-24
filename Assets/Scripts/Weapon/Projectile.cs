using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask damageMask;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifeTime = 4.0f;
    [SerializeField] private float explosionRadius = 0.0f;

    private float destroyTime;
    private bool isExplode;

    public void Initialize(int newDamage, float newLifeTime, float newExplosionRadius)
    {
        damage = newDamage;
        lifeTime = newLifeTime;
        explosionRadius = newExplosionRadius;

        // 현재 시간을 기준으로 제거 시간 계산.
        destroyTime = Time.time + lifeTime;
        isExplode = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isExplode == true)
        {
            return;
        }

        Vector3 hitPosition = Vector3.zero;
        if(collision.contactCount > 0)
        {
            hitPosition = collision.GetContact(0).point;
        }
        else
        {
            hitPosition = transform.position;
        }

        Explode(hitPosition, collision.collider);
    }

    void Explode(Vector3 hitPosition, Collider directHitCollider)
    {
        if(isExplode == true)
        {
            return;
        }

        isExplode = true;
        SpawnHitEffect(hitPosition);

        if(explosionRadius > 0.0f)
        {
            ApplyExplosionDamage(hitPosition);
        }
        else
        {
            ApplyDirectDamage(directHitCollider);
        }

        Destroy(gameObject);
    }

    void ApplyDirectDamage(Collider targetCollider)
    {
        if(targetCollider == null)
        {
            return;
        }

        EnemyHealth enemyHealth = targetCollider.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }

    void ApplyExplosionDamage(Vector3 center)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, explosionRadius, damageMask);

        for(int i=0; i<hitColliders.Length; ++i)
        {
            EnemyHealth enemyHealth = hitColliders[i].GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    void SpawnHitEffect(Vector3 position)
    {
        if(hitEffectPrefab == null)
        {
            return;
        }

        GameObject effectObject = Instantiate(hitEffectPrefab, position, Quaternion.identity);

        if(effectObject != null)
        {
            Destroy(effectObject, 2.0f);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(destroyTime <= 0.0f)
        {
            destroyTime = Time.time + lifeTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= destroyTime)
        {
            Explode(transform.position, null);
        }
    }
}
