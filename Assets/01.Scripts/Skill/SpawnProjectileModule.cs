using UnityEngine;

[CreateAssetMenu(menuName = "Game/SkillModule/SpawnProjectile")]
public class SpawnProjectileModule : SkillModule
{
    [Header("투사체 프리팹 & 데이터")]
    public GameObject projectilePrefab;
    public ProjectileData projectileData;

    private ActiveSkillLevelInfo curSkillData;

    public override void Execute(ActiveSkillRuntime runtime)
    {
        if (!projectilePrefab || !projectileData) return;
        curSkillData = runtime.Data.levelInfos[runtime.Level-1];
        int half = curSkillData.projectileCount / 2;
        for (int i = 0; i < curSkillData.projectileCount; i++)
        {
            float angleOffset = (i - half) * curSkillData.angleBetweenProjectiles;
            Quaternion rotation = runtime.Owner.rotation * Quaternion.Euler(0, 0, angleOffset);

            Projectile projObj = ObjectPoolManager.Instance.GetPool<Projectile>(runtime.Data.skillName).GetObject();

            projObj.transform.position = runtime.Owner.position;
            projObj.transform.rotation = rotation;

            float damage = StatCalculator.CalculateModifiedDamage(
                SkillManager.Instance.GetAllPassiveStat(), curSkillData.baseDamage);
            if (projObj)
            {
                projObj.Setup(projectileData, runtime.Owner,runtime.Target, damage);
            }
        }
    }
}