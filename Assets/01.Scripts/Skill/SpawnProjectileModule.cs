using UnityEngine;

[CreateAssetMenu(menuName = "Game/SkillModule/SpawnProjectile")]
public class SpawnProjectileModule : SkillModule
{
    [Header("투사체 프리팹 & 데이터")]
    public GameObject projectilePrefab;   // Projectile 컴포넌트가 달린 프리팹
    public ProjectileData projectileData;

    [Header("기본 속성")]
    public float baseDamage = 10f;
    public int projectileCount = 1;
    public float angleBetweenProjectiles = 10f;

    public override void Execute(SkillRuntime runtime)
    {
        if (!projectilePrefab || !projectileData) return;

        int half = projectileCount / 2;
        for (int i = 0; i < projectileCount; i++)
        {
            float angleOffset = (i - half) * angleBetweenProjectiles;
            Quaternion rotation = runtime.Owner.rotation * Quaternion.Euler(0, angleOffset, 0);

            Projectile projObj = ObjectPoolManager.Instance.GetPool<Projectile>(runtime.Data.skillName).GetObject();
            if (projObj)
            {
                projObj.Setup(projectileData, runtime.Owner,runtime.Target, baseDamage);
            }
        }
    }
}