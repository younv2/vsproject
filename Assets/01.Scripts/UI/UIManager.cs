using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UIManager : MonoSingleton<UIManager>
{
    public AssetLabelReference addressablePopupLabel;
    private List<BasePopup> popupList = new List<BasePopup>();
    private AddressablesLoader loader = new AddressablesLoader();
    public SkillUpPopup skillUpPopup;
    public GameResultPopup gameResultPopup;

    private async void Start()
    {
        await Initialize();
    }

    public async Task Initialize()
    {
        var task = loader.LoadAssetListAsync<GameObject>(addressablePopupLabel);
        await task;
        if(task.Result != null)
        {
            foreach (var data in task.Result)
            {
                var temp = Instantiate(data.GetComponent<BasePopup>(), this.transform);
                popupList.Add(temp);
                temp.Initialize();
            }
        }

        skillUpPopup = (SkillUpPopup)popupList.Find(x=>x.GetType() == typeof(SkillUpPopup));
        gameResultPopup = (GameResultPopup)popupList.Find(x=>x.GetType() == typeof(GameResultPopup));

    }
}
