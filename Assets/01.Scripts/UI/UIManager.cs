using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UIManager : MonoSingleton<UIManager>
{
    public AssetLabelReference addressablePopupLabel;
    private List<BasePopup> popupList = new List<BasePopup>();
    private AddressablesLoader loader = new AddressablesLoader();
    public SkillUpPopup skillUpPopup;
    public GameResultPopup gameResultPopup;
    private void Start()
    {
        StartCoroutine(Initialize());
    }
    /// <summary>
    /// UIManager 초기 설정
    /// </summary>
    /// <returns></returns>
    IEnumerator Initialize()
    {
        bool isLoadDone = false;
        loader.LoadAssetListAsync<GameObject>(addressablePopupLabel, (callback) =>
        {
            foreach (var data in callback)
            {
                var temp = Instantiate(data.GetComponent<BasePopup>(), this.transform);
                popupList.Add(temp);
                temp.Initialize();
            }
            isLoadDone = true;
        });
        yield return new WaitUntil(() => isLoadDone);
        skillUpPopup = (SkillUpPopup)popupList.Find(x=>x.GetType() == typeof(SkillUpPopup));
        gameResultPopup = (GameResultPopup)popupList.Find(x=>x.GetType() == typeof(GameResultPopup));
        yield return null;
    }
}
