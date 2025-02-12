using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public float speed = 5f;
    public float lifeTime = 3f;
    public int penetrateCount = 0; // 0이면 관통X

    [Header("이벤트별 이펙트")]
    public List<SkillEffect> onSpawnEffects;
    public List<SkillEffect> onHitEffects;
    public List<SkillEffect> onExpireEffects;
}