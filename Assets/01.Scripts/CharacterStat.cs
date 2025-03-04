using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat
{
    private int level;
    private int currentExp;
    private int maxExp;
    private float maxHp;
    private float currentHp;
    private float moveSpeed;
    private float drainItemRange;
    private float luck;

    public static Action OnExpUpdate;
    public int Level { get { return level; } }
    public int CurrentExp { get { return currentExp; } }
    public int MaxExp { get { return maxExp; } }
    public float MoveSpeed { get { return moveSpeed; } }
    public float Luck { get { return luck; } }

    public float DrainItemRange { get {  return drainItemRange; } }
    /// <summary>
    /// 캐릭터 초기 설정
    /// </summary>
    public void Init()
    {
        level = 1;
        currentExp = 0;
        maxExp = DataManager.Instance.GetExpByLevel(level);
        maxHp = 50;
        currentHp = 50;
        moveSpeed = 3f;
        drainItemRange = 1f;
        luck = 0f;
    }
    /// <summary>
    /// 체력 감소
    /// </summary>
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp < 0) currentHp = 0;

        Debug.Log($"[MonsterStat] 체력 감소: {damage}, 남은 체력: {currentHp}");

        // 사망 체크
        if (IsDead())
        {
            OnDeath();
        }
    }
    /// <summary>
    /// 캐릭터가 죽었는지 확인
    /// </summary>
    public bool IsDead()
    {
        return currentHp <= 0;
    }

    /// <summary>
    /// 사망 이벤트
    /// </summary>
    private void OnDeath()
    {
        Debug.Log("플레이어 사망!");
    }
    /// <summary>
    /// 현재 체력 퍼센트
    /// </summary>
    /// <returns></returns>
    public float GetCurrentHPPercent()
    {
        return currentHp / maxHp;
    }
    /// <summary>
    /// 캐릭터 레벨 업
    /// </summary>
    public void LevelUp()
    {
        level++;
        currentExp -= maxExp;
        maxExp = DataManager.Instance.GetExpByLevel(level);
        UIManager.Instance.skillUpPopup.Show();
    }
    /// <summary>
    /// 캐릭터 경험치 추가
    /// </summary>
    public void AddExp(int exp)
    {
        currentExp += exp;
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
        OnExpUpdate?.Invoke();
    }
}
