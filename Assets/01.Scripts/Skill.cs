using UnityEngine;

public class Skill : MonoBehaviour
{
    private SkillData skilldata;

    public Skill(string skillName)
    {
        skilldata = Resources.Load($"ScriptableObject/{skillName}") as SkillData;
    }
    public Skill(int skillId)
    {

        skilldata = Resources.Load($"ScriptableObject/{skillId}") as SkillData;
    }
    public SkillData GetSkillData()
    {
        return skilldata;
    }
}
