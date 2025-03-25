using Mono.Cecil;
using System.Collections.Generic;
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

    /// <summary>
    /// 투사체 생성
    /// </summary>
    /// <param name="data">투사체 정보</param>
    /// <param name="owner">투사체를 생성할 위치</param>
    /// <param name="target">투사체 이동할 목표</param>
    /// <param name="damage">투사체 데미지</param>
    public void Setup(ProjectileData data, Transform owner, Transform target, float damage)
    {
        this.data = data;
        this.owner = owner;
        this.damage = damage;
        this.target = target;

        BattleManager.Instance.projectileList.Add(this);

        movDir = this.target == null ? Vector3.zero : Vector3.Normalize(this.target.position - this.owner.position);

        timeAlive = 0f;
        // onSpawn 이펙트 실행
        ExecuteModules(data.onSpawnModules, null);
    }

    public void ManualFixedUpdate()
    {
        transform.Translate(movDir * data.speed * Time.deltaTime, Space.Self);
        
        Scan();

        timeAlive += Time.deltaTime;
        if (timeAlive >= data.lifeTime)
        {
            Expire();
        }
    }

    private void Scan()
    {
        Collider2D hit = null;
        hit = Physics2D.OverlapCircle(this.transform.position, 0.5f,1<<LayerMask.NameToLayer("Enemy"));
        if (hit == null) 
            return;

        var enemy = BattleManager.Instance.GetMonsterFromInstanceId(hit.gameObject.GetInstanceID());
        if (enemy == null) 
            return;

        enemy.TakeDamage(damage);

        hitCount++;
        if (hitCount > data.penetrateCount)
        {
            Expire();
        }

        ExecuteModules(data.onHitModules, hit.transform);
    }

    private void Expire()
    {
        ExecuteModules(data.onExpireModules, null);
        Remove();
    }

    public void Remove()
    {
        BattleManager.Instance.projectileList.Remove(this);
        ObjectPoolManager.Instance.GetPool<Projectile>(name).ReleaseObject(this);
    }

    private void ExecuteModules(List<SkillModule> modules, Transform collisionTarget)
    {
        if (modules == null) 
            return;

        var context = new ProjectileContext(this, collisionTarget);
        foreach (var mod in modules)
        {
            mod.Execute(context);
        }
    }

    public Vector3 GetMovDir() => movDir;
}