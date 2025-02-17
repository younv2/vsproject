using UnityEngine;

[CreateAssetMenu(menuName = "Game/SkillModule/Knockback")]
public class KnockbackModule : SkillModule
{
    [Header("넉백 데이터")]
    public float knockbackPower;
    public override void Execute(ProjectileContext context)
    {
        context.CollisionTarget.transform.Translate(context.Projectile.GetMovDir() * knockbackPower);
    }
}
