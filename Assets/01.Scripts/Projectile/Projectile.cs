using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    private ProjectileData data;
    private Transform owner;
    private float damage;

    private float timeAlive = 0f;
    private int hitCount = 0;

    public void Setup(ProjectileData data, Transform owner, float damage)
    {
        this.data = data;
        this.owner = owner;
        this.damage = damage;

        // onSpawn 이펙트 실행
        ExecuteEffects(data.onSpawnEffects, null);
    }

    void Update()
    {
        // 이동
        transform.Translate(Vector3.forward * data.speed * Time.deltaTime);

        // 수명 체크
        timeAlive += Time.deltaTime;
        if (timeAlive >= data.lifeTime)
        {
            Expire();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Monster>();
        if (enemy != null)
        {
            // 즉시 적에게 데미지
            enemy.TakeDamage(damage);

            // 관통 횟수
            hitCount++;
            if (hitCount > data.penetrateCount)
            {
                Expire();
            }

            // onHit 이펙트
            ExecuteEffects(data.onHitEffects, other.transform);
        }
    }

    private void Expire()
    {
        // onExpire 이펙트
        ExecuteEffects(data.onExpireEffects, null);
        Destroy(gameObject);
    }

    private void ExecuteEffects(System.Collections.Generic.List<SkillEffect> effects, Transform collisionTarget)
    {
        if (effects == null) return;
        var context = new ProjectileContext(this, collisionTarget);
        foreach (var eff in effects)
        {
            eff.Execute(context);
        }
    }
}