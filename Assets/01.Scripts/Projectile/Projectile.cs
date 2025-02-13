using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    private ProjectileData data;
    private Transform owner;
    private Transform target;
    private float damage;
    private Vector3 movDir;
    private float timeAlive = 0f;
    private int hitCount = 0;

    public void Setup(ProjectileData data, Transform owner,Transform target, float damage)
    {
        this.data = data;
        this.owner = owner;
        this.damage = damage;
        this.target = target;
        movDir = Vector3.Normalize(target.position - owner.position);
        
        timeAlive = 0f;
        // onSpawn 이펙트 실행
        ExecuteModules(data.onSpawnModules, null);
    }

    void FixedUpdate()
    {
        // 이동
        transform.Translate(movDir * data.speed * Time.deltaTime);

        // 수명 체크
        timeAlive += Time.deltaTime;
        if (timeAlive >= data.lifeTime)
        {
            Expire();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
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
            ExecuteModules(data.onHitModules, other.transform);
        }
    }

    private void Expire()
    {
        // onExpire 이펙트
        ExecuteModules(data.onExpireModules, null);
        ObjectPoolManager.Instance.GetPool<Projectile>(name.Replace("(Clone)","")).ReleaseObject(this);
    }

    private void ExecuteModules(System.Collections.Generic.List<SkillModule> modules, Transform collisionTarget)
    {
        if (modules == null) return;
        var context = new ProjectileContext(this, collisionTarget);
        foreach (var mod in modules)
        {
            mod.Execute(context);
        }
    }
}