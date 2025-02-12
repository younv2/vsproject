using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 목적이 무엇인가
/// 해당 컴포넌트를 가지고 있는 개체가 주변에 해당하는 레이어를 찾아내기 위함
/// 참조 - 골드메탈 유튜브 - 뱀서라이크 원거리 공격 구현하기
/// </summary>
public class Scanner : MonoBehaviour 
{
    public Transform nearstObject;
    public List<RaycastHit2D> hitList;
    public LayerMask layerMask;
    public float range;

    // Update is called once per frame
    void Update()
    {
        hitList = Physics2D.CircleCastAll(transform.position,range,Vector2.zero,0,layerMask).ToList();

        nearstObject = FindNearstObject();
    }

    public Transform FindNearstObject()
    {
        Transform result = null;
        float diff = 100;
        foreach(var hit in hitList)
        {
            float curDiff = Vector2.Distance(transform.position, hit.point);
            if (diff > curDiff)
            {
                diff = curDiff;
                result = hit.transform;
            }
        }

        return result;
    }
}
