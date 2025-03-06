using UnityEngine;

public class AttackRangeVisualizer : MonoBehaviour
{
    // 공격 모듈(ScriptableObject)을 Inspector에 할당
    public AreaAttackModule attackModule;

    private void OnDrawGizmos()
    {
        if (attackModule == null) return;

        // 현재 오브젝트(예: 스킬을 사용하는 캐릭터)의 Transform을 기준으로 범위 정보 가져오기
        AttackRangeInfo info = attackModule.GetAttackRangeInfo(transform);

        Gizmos.color = Color.red;
        if (info.areaType == AttackAreaType.Circle)
        {
            // 원형 범위 시각화
            Gizmos.DrawWireSphere(info.center, info.circleRadius);
        }
        else if (info.areaType == AttackAreaType.Rectangle)
        {
            // 직사각형 범위 시각화
            Vector3 center = info.center;
            Vector3 size = new Vector3(info.rectangleSize.x, info.rectangleSize.y, 0);
            Gizmos.DrawWireCube(center, size);
        }
    }
}
