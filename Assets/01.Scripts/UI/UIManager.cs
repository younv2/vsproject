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
    [SerializeField]private SkillUpPopup skillUpPopup;
    private void Start()
    {
        StartCoroutine(Initialize());
    }
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
        skillUpPopup = popupList.OfType<SkillUpPopup>().FirstOrDefault();
        yield return null;
    }


}
