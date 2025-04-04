using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 해당 컴포넌트를 가지고 있는 개체가 주변에 해당하는 레이어를 찾아내기 위함
/// </summary>
public class Scanner : MonoBehaviour 
{
    public Transform nearstObject;
    public List<RaycastHit2D> hitList;
    public LayerMask layerMask;
    public float range;

    void Update()
    {
        hitList = Physics2D.CircleCastAll(transform.position,range,Vector2.zero,0,layerMask).ToList();

        nearstObject = FindNearstObject();
    }

    /// <summary>
    /// 설정한 레이어의 근처에 있는 오브젝트를 찾는 함수
    /// </summary>
    public Transform FindNearstObject()
    {
        Transform result = null;
        float diff = 9999;
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
