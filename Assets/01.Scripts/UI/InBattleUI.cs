using TMPro;
using UnityEngine;

public class InBattleUI : MonoSingleton<InBattleUI>
{
    public TextMeshProUGUI timeTxt;
    private void OnEnable()
    {
        TimeManager.Instance.OnTimeChanged += UpdateTimeUI;
    }
    private void OnDisable()
    {
        TimeManager.Instance.OnTimeChanged -= UpdateTimeUI;
    }

    public void UpdateTimeUI()
    {
        timeTxt.text = Formatter.TimeFormat(TimeManager.Instance.GameTime);
    }
}
