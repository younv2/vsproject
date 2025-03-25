using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpElement : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    private SkillDataBase skillData;

    public void Setting(SkillDataBase data,SkillUpPopup popup)
    {
        skillData = data;
        descriptionText.text = SkillManager.Instance.GetNextLevelDescription(data);
        Button button = this.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            SkillManager.Instance.LearnSkill(skillData);

            popup.Close();
        });
    }
}
