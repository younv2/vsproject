using System.Collections.Generic;
using UnityEngine;

public class CharacterStat
{
    private int level;
    private int currentExp;
    private int maxExp;
    private int maxHp;
    private int currentHp;
    private List<SkillLevel> skillLevel;
    public List<SkillLevel> SkillLevel {  get { return skillLevel; } }
}
