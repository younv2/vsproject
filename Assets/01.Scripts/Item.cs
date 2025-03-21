using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    /// <summary>
    /// 아이템 사용
    /// </summary>
    public void Use()
    {
        BattleManager.Instance.GetPlayableCharacter().Stat.AddExp(10);
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
