using DG.Tweening;
using System;
using UnityEngine;

public class PlayableCharacter : MonoBehaviour, IPoolable
{
    [SerializeField]private Scanner monsterScanner;
    [SerializeField]private Scanner expItemScanner;
    private CharacterStat stat = new CharacterStat();
    private HPBarUI hPBarUI;

    public CharacterStat Stat { get { return stat; } }


    public void ManualFixedUpdate()
    {
        DrainExp();
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
    /// 캐릭터 근처 몬스터 반환
    /// </summary>
    public Transform GetNearstTarget()
    {
        if (monsterScanner.nearstObject == null)
            return null;
        return monsterScanner.nearstObject.transform;
    }
    /// <summary>
    /// 경험치 아이템 흡수
    /// </summary>
    public void DrainExp()
    {
        expItemScanner.range = StatCalculator.CalculateModifiedDrop(SkillManager.Instance.GetAllPassiveStat(),Stat.DrainItemRange);
        if (expItemScanner.hitList != null)
        {
            foreach (var item in expItemScanner.hitList)
            {
                item.transform.Translate(Vector3.Normalize(transform.position - item.transform.position) * Time.deltaTime * 5f);
            }
        }
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
