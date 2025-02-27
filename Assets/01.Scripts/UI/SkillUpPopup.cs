using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SkillUpPopup : BasePopup
{
    [SerializeField]private List<SkillUpElement> elementList;

    public void Setting()
    {
        foreach(var data in elementList)
        {
            data.Setting(DataManager.Instance.GetSkillData("ThrowRock"),this);
        }
    }
    public override void Show()
    {
        base.Show();
        Setting();
        BattleManager.Instance.Pause(true);
    }

    public override void Close()
    {
        base.Close();
        BattleManager.Instance.Pause(false);
    }
}
