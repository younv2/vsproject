using System;
using UnityEngine;

public class PlayableCharacter : MonoBehaviour, IPoolable
{
    private Scanner scanner;
    private CharacterStat stat = new CharacterStat();
    private HPBarUI hPBarUI;

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
        hPBarUI.Remove();
        ObjectPoolManager.Instance.GetPool<PlayableCharacter>(name.Replace("(Clone)", "")).ReleaseObject(this);
    }
    /// <summary>
    /// 캐릭터 사망 처리
    /// </summary>
    public Transform GetNearstTarget()
    {
        return scanner.nearstObject?.transform;
    }
    /// <summary>
    /// 캐릭터 데미지 처리
    /// </summary>
    internal void TakeDamage(float damage)
    {
        Debug.Log($"{damage}의 데미지를 입음");
        stat.TakeDamage(damage);
        var text = ObjectPoolManager.Instance.GetPool<DamageTextUI>("DamageText").GetObject();
        text.transform.position = transform.position + new Vector3(0, 1.5f, 0);
        text.Setup(damage);
        hPBarUI ??= ObjectPoolManager.Instance.GetPool<HPBarUI>("HPBar").GetObject();
        hPBarUI.Setup(this.transform, stat.GetCurrentHPPercent());
        if (stat.IsDead())
        {
            OnDeath();
        }
    }
}
