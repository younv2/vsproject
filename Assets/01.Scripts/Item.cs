using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public virtual void Use()
    {
        Remove();
    }

    public void Remove()
    {
        BattleManager.Instance.itemDic.Remove(gameObject.GetInstanceID());
        ObjectPoolManager.Instance.GetPool<Item>(name).ReleaseObject(this);
    }
}
