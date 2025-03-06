using System.Collections;
using UnityEngine;
public enum AttackAreaType
{
    Circle,
    Rectangle
}
[CreateAssetMenu(menuName = "Game/SkillModule/AreaAttackModule")]
public class AreaAttackModule : SkillModule
{
    [Header("공격 딜레이")]
    public float startupDelay = 0.3f;
    public float afterDelay = 0.2f; 

    [Header("공격 영역 설정")]
    public AttackAreaType areaType = AttackAreaType.Circle;
    public float circleRadius = 5f;         
    public Vector2 rectangleSize = new Vector2(5f, 3f);
    public Vector2 centerOffset;            

    public override void Execute(ActiveSkillRuntime runtime)
    {
        if (runtime == null || runtime.Owner == null)
            return;

        runtime.Owner.GetComponent<PlayableCharacter>()?.StartCoroutine(AttackCoroutine(runtime));
    }

    private IEnumerator AttackCoroutine(ActiveSkillRuntime runtime)
    {
        yield return new WaitForSeconds(startupDelay);

        Vector2 attackCenter = (Vector2)runtime.Owner.position + centerOffset;

        float finalDamage = StatCalculator.CalculateModifiedDamage(
            SkillManager.Instance.GetAllPassiveStat(),runtime.Data.levelInfos[runtime.Level - 1].baseDamage);

        Collider2D[] hits = null;
        if (areaType == AttackAreaType.Circle)
        {
            hits = Physics2D.OverlapCircleAll(attackCenter, circleRadius);
        }
        else if (areaType == AttackAreaType.Rectangle)
        {
            hits = Physics2D.OverlapBoxAll(attackCenter, rectangleSize, 0f);
        }

        if (hits != null)
        {
            foreach (var hit in hits)
            {
                Monster enemy = hit.GetComponent<Monster>();
                if (enemy != null)
                {
                    enemy.TakeDamage(finalDamage);
                }
            }
        }
        yield return new WaitForSeconds(afterDelay);
    }

    public AttackRangeInfo GetAttackRangeInfo(Transform owner)
    {
        AttackRangeInfo info = new AttackRangeInfo();
        info.areaType = areaType;
        info.center = (Vector2)owner.position + centerOffset;
        info.circleRadius = circleRadius;
        info.rectangleSize = rectangleSize;
        return info;
    }
}
/// <summary>
/// 공격 범위 정보를 담은 간단한 데이터 구조체
/// </summary>
public struct AttackRangeInfo
{
    public AttackAreaType areaType;
    public Vector2 center;
    public float circleRadius;
    public Vector2 rectangleSize;
}