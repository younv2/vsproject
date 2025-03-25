using UnityEngine;

public class AttackRangeVisualizer : MonoBehaviour
{
    // 공격 모듈(ScriptableObject)을 Inspector에 할당
    public AreaAttackModule attackModule;

    private void OnDrawGizmos()
    {
        if (attackModule == null) return;

        AttackRangeInfo info = attackModule.GetAttackRangeInfo(transform);

        Gizmos.color = Color.red;
        if (info.areaType == AttackAreaType.Circle)
        {
            Gizmos.DrawWireSphere(info.center, info.circleRadius);
        }
        else if (info.areaType == AttackAreaType.Rectangle)
        {
            Vector3 center = info.center;
            Vector3 size = new Vector3(info.rectangleSize.x, info.rectangleSize.y, 0);
            Gizmos.DrawWireCube(center, size);
        }
    }
}
