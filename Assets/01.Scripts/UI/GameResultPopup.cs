using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class GameResultPopup : BasePopup
{
    public Button restartBtn;
    public TextMeshProUGUI resultTxt;
    public TextMeshProUGUI timeTxt;
    /// <summary>
    /// 요소의 세부 설정 및 갯수 세팅
    /// </summary>
    public void Setting(bool iswin)
    {
        restartBtn.onClick.AddListener(() =>
        {
            BattleManager.Instance.GemeReset();
            base.Close();
            BattleManager.Instance.Pause(false);
        });
        resultTxt.text = iswin? Global.WIN : Global.LOSE;
        timeTxt.text = Formatter.TimeFormat(TimeManager.Instance.GameTime);
        if (UIManager.Instance.skillUpPopup.isOn)
            UIManager.Instance.skillUpPopup.Close();

    }
    public void Show(bool isWin)
    {
        base.Show();
        Setting(isWin);
        BattleManager.Instance.Pause(true);
    }

    public override void Close()
    {
        base.Close();
        Application.Quit();
        BattleManager.Instance.Pause(false);
    }
}
