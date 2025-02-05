using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Object/SkillData")]
public class SkillData : ScriptableObject
{
    [System.Serializable]
    public class SkillLevelData
    {
        public int level;
        public float AttackPower;
    }

    [SerializeField]private int skillId;
    public int SkillId { get { return skillId; } }
    [SerializeField]private string skillName;
    public string SkillName { get {  return skillName; } }

    [SerializeField] private SkillLevelData[] skillLevelDatas;
    public SkillLevelData[] SkillLevelDatas { get { return skillLevelDatas; } }

}
