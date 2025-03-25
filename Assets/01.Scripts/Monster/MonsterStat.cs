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

    public void InitStat(MonsterData data)
    {
        speed = data.MoveSpeed;
        maxHp = data.MaxHp * DataManager.Instance.TimeBasedBattleScalers.GetCurrentMonsterPowerMultiple();
        attackPower = data.AttackPower * DataManager.Instance.TimeBasedBattleScalers.GetCurrentMonsterPowerMultiple();
        currentHp = MaxHp;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (CurrentHp < 0) 
            currentHp = 0;

        if (IsDead())
        {
            OnDeath();
        }
    }

    public bool IsDead() => CurrentHp <= 0;

    private void OnDeath() => Debug.Log("몬스터 사망!");

    public float GetCurrentHPPercent() => currentHp / maxHp;
}
