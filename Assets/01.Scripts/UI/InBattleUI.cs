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
        if(TimeManager.Instance != null)
            TimeManager.Instance.OnTimeChanged -= UpdateTimeUI;
    }

    public void UpdateTimeUI(float time) => timeTxt.text = Formatter.TimeFormat(time);
}
