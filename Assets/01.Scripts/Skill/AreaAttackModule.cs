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

    private bool isOwnerFliped = false;
    [Header("이미지")]
    public bool isPersistentVisual = false;
    public GameObject vfxPrefab;
    public float oneShotDuration = 0.5f;

    private GameObject vfxInstance;
    private Transform vfxOriginParent;

    public override void Execute(ActiveSkillRuntime runtime)
    {
        if (runtime == null || runtime.Owner == null)
            return;
        isOwnerFliped = runtime.Owner.GetComponent<PlayableCharacter>()?.IsFlip ?? false;
        runtime.Owner.GetComponent<PlayableCharacter>()?.StartCoroutine(AttackCoroutine(runtime));
    }

    private IEnumerator AttackCoroutine(ActiveSkillRuntime runtime)
    {
        yield return new WaitForSeconds(startupDelay);
        int flip = (isOwnerFliped ? -1 : 1);
        Vector2 attackCenter = (Vector2)runtime.Owner.position + centerOffset * flip;

        float finalDamage = StatCalculator.CalculateModifiedDamage(
            SkillManager.Instance.GetAllPassiveStat(), runtime.Data.levelInfos[runtime.Level - 1].baseDamage);

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

        // vfx 처리
        if (vfxPrefab != null)
        {
            if (vfxInstance == null)
            {
                vfxInstance = ObjectPoolManager.Instance.GetPool<GameObject>(vfxPrefab.name).GetObject();
                if (isPersistentVisual)
                {
                    vfxOriginParent = vfxInstance.transform.parent;
                    vfxInstance.transform.SetParent(runtime.Owner.transform, false);
                    runtime.Owner.GetComponent<PersistentVfxController>().OnOwnerDestroyed += () =>
                    {
                        if (vfxOriginParent != null&&runtime.Owner.transform!=null)
                        {
                            vfxInstance.transform.SetParent(vfxOriginParent, false);
                        }
                        ObjectPoolManager.Instance.GetPool<GameObject>(vfxPrefab.name).ReleaseObject(vfxInstance);
                        vfxInstance = null;
                    };
                }
                vfxInstance.transform.position = runtime.Owner.position + (Vector3)centerOffset * flip;
                vfxInstance.transform.rotation = runtime.Owner.rotation;
            }
            //vfx 사이즈 조절
            if (areaType == AttackAreaType.Circle)
            {
                float diameter = circleRadius * 2f;
                vfxInstance.transform.localScale = new Vector3(diameter, diameter, 1f);
            }
            else if (areaType == AttackAreaType.Rectangle)
            {
                vfxInstance.transform.localScale = new Vector3(rectangleSize.x, rectangleSize.y, 1f);
            }
            if (!isPersistentVisual)
            {
                yield return new WaitForSeconds(oneShotDuration);
                ObjectPoolManager.Instance.GetPool<GameObject>(vfxPrefab.name).ReleaseObject(vfxInstance);
                vfxInstance = null;
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