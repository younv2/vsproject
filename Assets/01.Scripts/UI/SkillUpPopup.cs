using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SkillUpPopup : BasePopup
{
    [SerializeField]private List<SkillUpElement> elementList;
    
    /// <summary>
    /// 요소의 세부 설정 및 갯수 세팅
    /// </summary>
    public void Setting()
    {
        float threshold = Random.Range(0,1f);
        int elementCount = BattleManager.Instance.GetPlayableCharacter().Stat.Luck >= threshold ? 4 : 3;
        var skillDataList  = new List<SkillDataBase>(DataManager.Instance.GetAllSkillData());
        skillDataList.Shuffle();
        // 리스트에 들어있는 SkillUpElement 중 원하는 개수만 활성화하고 나머지는 비활성화
        for (int i = 0; i < elementList.Count; i++)
        {
            if (i < elementCount)
            {
                
                elementList[i].gameObject.SetActive(true);
                elementList[i].Setting(skillDataList[i], this);
            }
            else
            {
                elementList[i].gameObject.SetActive(false);
            }
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
