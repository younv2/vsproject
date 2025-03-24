using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    /// <summary>
    /// 아이템 사용
    /// </summary>
    public virtual void Use()
    {
        Remove();
    }
    /// <summary>
    /// 아이템 삭제
    /// </summary>
    public void Remove()
    {
        BattleManager.Instance.itemDic.Remove(gameObject.GetInstanceID());
        ObjectPoolManager.Instance.GetPool<Item>(name).ReleaseObject(this);
    }
}
