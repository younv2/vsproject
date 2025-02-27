using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UIManager : MonoSingleton<UIManager>
{
    public AssetLabelReference addressablePopupLabel;
    private List<BasePopup> popupList = new List<BasePopup>();
    private AddressablesLoader loader = new AddressablesLoader();
    private SkillUpPopup skillUpPopup;
    private void Start()
    {
        loader.LoadAssetListAsync<GameObject>(addressablePopupLabel, (callback) =>
        {
            foreach(var data in callback)
            {
                var temp = Instantiate(data.GetComponent<BasePopup>(), this.transform);
                popupList.Add(temp);
                temp.Initialize();
            }
        });
        
    }

}
