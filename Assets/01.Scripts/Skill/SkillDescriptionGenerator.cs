using System.Text;
using UnityEngine;

public static class SkillDescriptionGenerator
{
    /// <summary>
    /// 주어진 스킬 데이터와 현재 레벨에 맞는 Active, Passive 설명 생성
    /// </summary>
    public static string GetDescription(SkillDataBase skillData, int level)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"[스킬명] {skillData.skillName}");
        sb.AppendLine($"[레벨] {level}");

        if (skillData is ActiveSkillData activeSkillData && activeSkillData.levelInfos.Count > 0)
        {
            int idx = Mathf.Clamp(level-1, 0, activeSkillData.levelInfos.Count-1);
            ActiveSkillLevelInfo activeInfo = activeSkillData.levelInfos[idx];
            sb.AppendLine("--- 액티브 효과 ---");
            sb.AppendLine($"쿨타임: {activeInfo.cooldown}초");
            sb.AppendLine($"기본 데미지: {activeInfo.baseDamage}");
            sb.AppendLine($"발사체 개수: {activeInfo.projectileCount}");
            sb.AppendLine($"발사 각도: {activeInfo.angleBetweenProjectiles}°");
        }

        if (skillData is PassiveSkillData passiveSkillData && passiveSkillData.levelInfos.Count > 0)
        {
            int idx = Mathf.Clamp(level - 1, 0, passiveSkillData.levelInfos.Count-1);
            PassiveSkillLevelInfo passiveInfo = passiveSkillData.levelInfos[idx];
            bool hasPassiveEffect = false;
            StringBuilder passiveSb = new StringBuilder();
            passiveSb.AppendLine("--- 패시브 효과 ---");

            if (!Mathf.Approximately(passiveInfo.attackBonus, 0))
            {
                passiveSb.AppendLine($"공격력 보너스: {passiveInfo.attackBonus}%");
                hasPassiveEffect = true;
            }
            if (!Mathf.Approximately(passiveInfo.hpBonus, 0))
            {
                passiveSb.AppendLine($"HP 보너스: {passiveInfo.hpBonus}%");
                hasPassiveEffect = true;
            }
            if (!Mathf.Approximately(passiveInfo.dropRangeBonus, 0))
            {
                passiveSb.AppendLine($"드랍 범위 보너스: {passiveInfo.dropRangeBonus}%");
                hasPassiveEffect = true;
            }
            if (!Mathf.Approximately(passiveInfo.expBonus, 0))
            {
                passiveSb.AppendLine($"경험치 보너스: {passiveInfo.expBonus}%");
                hasPassiveEffect = true;
            }
            if (!Mathf.Approximately(passiveInfo.luckBonus, 0))
            {
                passiveSb.AppendLine($"럭 보너스: {passiveInfo.luckBonus}%");
                hasPassiveEffect = true;
            }
            if (hasPassiveEffect)
            {
                sb.Append(passiveSb.ToString());
            }
        }


        return sb.ToString();
    }
}