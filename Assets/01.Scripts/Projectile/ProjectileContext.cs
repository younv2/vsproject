using UnityEngine;
public class ProjectileContext
{
    public Projectile Projectile { get; private set; }
    public Transform CollisionTarget { get; private set; }

    // 편의 프로퍼티
    public Vector3 Position => Projectile.transform.position;
    public Transform Owner => Projectile != null ? Projectile.transform : null;

    public ProjectileContext(Projectile proj, Transform collisionTarget)
    {
        Projectile = proj;
        CollisionTarget = collisionTarget;
    }
}