using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpBarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private Slider expSlider;

    private CharacterStat characterStat;

    private void OnEnable()
    {
        CharacterStat.OnExpUpdate += UpdateUI;
        characterStat = BattleManager.Instance.GetPlayableCharacter().Stat;
        UpdateUI();
    }

    private void OnDisable()
    {
        CharacterStat.OnExpUpdate -= UpdateUI;
    }

    public void UpdateUI()
    {
        levelText.text = $"Lv {characterStat.Level}";
        expText.text = $"{characterStat.CurrentExp} / {characterStat.MaxExp}";
        expSlider.value = characterStat.CurrentExp / (float)characterStat.MaxExp;
    }
}
