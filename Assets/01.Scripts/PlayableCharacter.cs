using System;
using UnityEngine;

public class PlayableCharacter : MonoBehaviour, IPoolable
{
    private Scanner scanner;
    private CharacterStat stat = new CharacterStat();
    public CharacterStat Stat { get { return stat; } }
    void Start()
    {
        scanner = transform.GetComponent<Scanner>();
    }
    void OnEnable()
    {
        Stat.Init();
    }
    /// <summary>
    /// 캐릭터 사망 처리
    /// </summary>
    public void OnDeath()
    {
        ObjectPoolManager.Instance.GetPool<PlayableCharacter>(name.Replace("(Clone)", "")).ReleaseObject(this);
    }
    public Transform GetNearstTarget()
    {
        return scanner.nearstObject.transform;
    }
    /// <summary>
    /// 캐릭터 데미지 처리
    /// </summary>
    internal void TakeDamage(float damage)
    {
        Debug.Log($"{damage}의 데미지를 입음");
        stat.TakeDamage(damage);
        if (stat.IsDead())
        {
            OnDeath();
        }
    }
}
