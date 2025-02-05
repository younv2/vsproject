using UnityEngine;


public enum SKILLID
{
    THROWROCK = 0,
    WIPE = 1,
    MAX =100
}
public class Skill : MonoBehaviour
{
    [SerializeField]private SkillData skilldata;
    public SkillData SkillData { get { return skilldata; } }
    

}
