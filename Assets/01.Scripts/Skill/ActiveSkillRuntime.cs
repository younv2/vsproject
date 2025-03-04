using UnityEngine;

public class ActiveSkillRuntime
{
    public ActiveSkillData Data { get; private set; }
    public Transform Owner { get; private set; }
    public Transform Target { get; private set; }
    public int Level { get; private set; } = 1;

    private float cooldownTimer;

    public ActiveSkillRuntime(ActiveSkillData data, Transform owner)
    {
        Data = data;
        Owner = owner;
        // 초기 쿨타임 설정
        cooldownTimer = Data.GetCooldown(Level);
    }

    public void Update(float deltaTime)
    {
        cooldownTimer -= deltaTime;
        Target = BattleManager.Instance.GetPlayableCharacter().GetNearstTarget();
        if (cooldownTimer <= 0f)
        {
            Data.Activate(this);
            cooldownTimer = Data.GetCooldown(Level);
        }
    }

    public void LevelUp()
    {
        Level++;
        // 레벨 최대치 제한 등은 필요시 추가
        if (Level > Data.levelInfos.Count)
            Level = Data.levelInfos.Count;
    }
}

public class PassiveSkillRuntime
{
    public PassiveSkillData Data { get; private set; }
    public Transform Owner { get; private set; }
    public int Level { get; private set; } = 1;

    public PassiveSkillRuntime(PassiveSkillData data, Transform owner)
    {
        Data = data;
        Owner = owner;
    }

    public void LevelUp()
    {
        Level++;
        // 레벨 최대치 제한 등은 필요시 추가
        if (Level > Data.levelInfos.Count)
            Level = Data.levelInfos.Count;
    }
}
