using System;
using UnityEngine;

public class MonsterStat
{
    public float Speed { get; private set; }
    public float MaxHp { get; private set; }
    public float AttackPower { get; private set; }
    public float CurrentHp { get; private set; }
    /// <summary>
    /// 몬스터의 스탯 정보 초기화
    /// </summary>
    /// <param name="data"></param>
    public void InitStat(MonsterData data)
    {
        Speed = data.MoveSpeed;
        MaxHp = data.MaxHp;
        AttackPower = data.AttackPower;
        CurrentHp = MaxHp;
    }
    /// <summary>
    /// 체력 감소
    /// </summary>
    public void TakeDamage(float damage)
    {
        CurrentHp -= damage;
        if (CurrentHp < 0) CurrentHp = 0;

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
}
