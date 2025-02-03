using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ������ �����ΰ�
/// �ش� ������Ʈ�� ������ �ִ� ��ü�� �ֺ��� �ش��ϴ� ���̾ ã�Ƴ��� ����
/// ���� - ����Ż ��Ʃ�� - �켭����ũ ���Ÿ� ���� �����ϱ�
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
