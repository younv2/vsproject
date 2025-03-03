using System;
using UnityEngine;

public class MonsterStat
{
    private float speed;
    private float maxHp;
    private float attackPower;
    private float currentHp;

    public float Speed { get { return speed; } }
    public float MaxHp { get { return maxHp; } }
    public float AttackPower { get { return attackPower; } }
    public float CurrentHp { get { return currentHp; } }
    /// <summary>
    /// 몬스터의 스탯 정보 초기화
    /// </summary>
    /// <param name="data"></param>
    public void InitStat(MonsterData data)
    {
        speed = data.MoveSpeed;
        maxHp = data.MaxHp;
        attackPower = data.AttackPower;
        currentHp = MaxHp;
    }
    /// <summary>
    /// 체력 감소
    /// </summary>
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (CurrentHp < 0) currentHp = 0;

        Debug.Log($"[MonsterStat] 체력 감소: {damage}, 남은 체력: {CurrentHp}");

        // 사망 체크
        if (IsDead())
        {
            OnDeath();
        }
    }
    /// <summary>
    /// 몬스터가 죽었는지 확인
    /// </summary>
    public bool IsDead()
    {
        return CurrentHp <= 0;
    }

    /// <summary>
    /// 사망 이벤트
    /// </summary>
    private void OnDeath()
    {
        Debug.Log("몬스터 사망!");
    }
    /// <summary>
    /// 현재 체력 퍼센트 확인
    /// </summary>
    /// <returns></returns>
    public float GetCurrentHPPercent()
    {
        return currentHp / maxHp;
    }
}
