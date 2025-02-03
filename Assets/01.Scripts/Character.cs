using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour

{
    CharacterStat stat;
    List<Skill> skillList;
    Scanner scanner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        skillList = new List<Skill>
        {
            SkillManager.Instance.GetSkill("ThrowRock"),
            SkillManager.Instance.GetSkill("Wipe")
        };

        scanner = transform.GetComponent<Scanner>();
    }

    // Update is called once per frame
    void Update()
    {
        SkillManager.Instance.UseSkill(skillList[0]);
        SkillManager.Instance.UseSkill(skillList[1]);


    }

    public void AddSkill(int skillId)
    {
        skillList.Add(SkillManager.Instance.GetSkill("ThrowRock"));
    }
}
