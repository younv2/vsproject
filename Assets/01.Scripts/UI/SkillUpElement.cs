using TMPro;
using UnityEngine;

public class SkillUpElement : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    private SkillData skillData;
    public void Setting(SkillData data)
    {
        skillData = data;
        nameText.text = skillData.name;
    }
}
