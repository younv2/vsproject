using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ActiveSkillRuntime
{
    private ActiveSkillData data;
    private Transform owner;
    private Transform target;
    private int level = 1;


    public ActiveSkillData Data { get { return data; } }
    public Transform Owner { get { return owner; } }
    public Transform Target { get { return target; } }
    public int Level { get { return level; } }

    private float cooldownTimer;

    public ActiveSkillRuntime(ActiveSkillData data, Transform owner)
    {
        this.data = data;
        this.owner = owner;
        cooldownTimer = data.GetCooldown(level);
    }

    public void Update(float deltaTime)
    {
        cooldownTimer -= deltaTime;
        target = BattleManager.Instance.GetPlayableCharacter().GetNearstTarget();
        if (cooldownTimer <= 0f)
        {
            data.Activate(this);
            cooldownTimer = data.GetCooldown(Level);
        }
    }

    public void LevelUp()
    {
        level++;

        if (level > data.levelInfos.Count)
            level = data.levelInfos.Count;
    }
}

public class PassiveSkillRuntime
{
    private PassiveSkillData data;
    private Transform owner;
    private int level = 1;

    public PassiveSkillData Data { get { return data; } }
    public Transform Owner { get { return owner; } }
    public int Level { get { return level; } }

    public PassiveSkillRuntime(PassiveSkillData data, Transform owner)
    {
        this.data = data;
        this.owner = owner;
    }

    public void LevelUp()
    {
        level++;

        if (level > data.levelInfos.Count)
            level = data.levelInfos.Count;
    }
}
