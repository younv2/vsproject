using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpElement : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    private SkillData skillData;
    public void Setting(SkillData data,SkillUpPopup popup)
    {
        skillData = data;
        nameText.text = skillData.name;
        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            SkillManager.Instance.LearnSkill(skillData);

            popup.Close();
        });
    }
}
