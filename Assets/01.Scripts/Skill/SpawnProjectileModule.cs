using UnityEngine;

[CreateAssetMenu(menuName = "Game/SkillModule/SpawnProjectile")]
public class SpawnProjectileModule : SkillModule
{
    [Header("투사체 프리팹 & 데이터")]
    public GameObject projectilePrefab;   // Projectile 컴포넌트가 달린 프리팹
    public ProjectileData projectileData;

    public override void Execute(SkillRuntime runtime)
    {
        if (!projectilePrefab || !projectileData) return;
        SkillLevelInfo curSkillData = runtime.Data.levelInfos[runtime.Level];
        int half = curSkillData.projectileCount / 2;
        for (int i = 0; i < curSkillData.projectileCount; i++)
        {
            float angleOffset = (i - half) * curSkillData.angleBetweenProjectiles;
            Quaternion rotation = runtime.Owner.rotation * Quaternion.Euler(0, angleOffset, 0);

            Projectile projObj = ObjectPoolManager.Instance.GetPool<Projectile>(runtime.Data.skillName).GetObject();
            projObj.transform.position = runtime.Owner.position;
            projObj.transform.rotation = rotation;
            if (projObj)
            {
                projObj.Setup(projectileData, runtime.Owner,runtime.Target, curSkillData.baseDamage);
            }
        }
    }
}