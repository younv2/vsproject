using UnityEngine;


public enum SKILLID
{
    THROWROCK = 0,
    WIPE = 1
}
public class SkillRuntime
{
    public SkillData Data { get; private set; }
    public Transform Owner { get; private set; }
    public Transform Target { get; private set; }
    public int Level { get; private set; } = 1;

    private float cooldownTimer;

    public SkillRuntime(SkillData data, Transform owner)
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
            // 스킬 발동
            Data.Activate(this);

            // 발동 후 쿨타임 갱신
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
