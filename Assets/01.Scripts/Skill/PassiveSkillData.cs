using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/PassiveSkillData")]
public class PassiveSkillData : SkillDataBase
{
    [Header("패시브 효과 설정")]
    public List<PassiveSkillLevelInfo> levelInfos;

    public void ApplyPassiveEffect(PlayableCharacter character)
    {
    }
    public void RemovePassiveEffect(PlayableCharacter character)
    {
    }
}