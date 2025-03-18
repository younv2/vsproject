using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpElement : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI lvText;
    public TextMeshProUGUI descriptionText;
    private SkillDataBase skillData;
    public void Setting(SkillDataBase data,SkillUpPopup popup)
    {
        skillData = data;
        nameText.text = skillData.name;
        lvText.text = skillData.name;
        descriptionText.text = skillData.name;
        Button button = this.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            SkillManager.Instance.LearnSkill(skillData);

            popup.Close();
        });
    }
}
