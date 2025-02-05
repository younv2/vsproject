public class SkillLevel
{
    private SKILLID skillId;
    public SKILLID SkillId { get { return skillId; } }
    private int level;
    public int Level { get { return level; } }


    public SkillLevel(SKILLID skillId)
    {
        this.skillId = skillId;
        this.level = 0;
    }
    public void LevelUp()
    {
        level++;
    }
}
