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
    public float MaxHp { get {  return StatCalculator.CalculateModifiedHP(SkillManager.Instance.GetAllPassiveStat(),maxHp); } }
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
        OnExpUpdate?.Invoke();
    }
    /// <summary>
    /// 체력 감소
    /// </summary>
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp < 0) currentHp = 0;

        // 사망 체크
        if (IsDead())
        {
            OnDeath();
        }
    }
    public bool IsDead() => currentHp <= 0;
    private void OnDeath() => Debug.Log("플레이어 사망!");
    public void LevelUp()
    {
        level++;
        currentExp -= maxExp;
        maxExp = DataManager.Instance.GetExpByLevel(level);
        UIManager.Instance.skillUpPopup.Show();
    }
    public void AddExp(int exp)
    {
        currentExp += StatCalculator.CalculateModifiedExp(SkillManager.Instance.GetAllPassiveStat(),exp);
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
        OnExpUpdate?.Invoke();
    }

    public float GetCurrentHPPercent() => currentHp / MaxHp;

}
