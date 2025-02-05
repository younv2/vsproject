using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DataManager : MonoSingleton<DataManager>
{
    AddressablesLoader addressablesLoader = new AddressablesLoader();
    private List<SkillData> skillDataList = new List<SkillData>();
    public List<SkillData> SkillDataList { get { return skillDataList; } }

    public List<AssetReference> skillAddressList;
    private void Start()
    {
        LoadAllSkillData();
    }

    public void LoadAllSkillData()
    {
        addressablesLoader.LoadAssetListAsync<SkillData>(skillAddressList, (callback) =>
        {
            skillDataList = callback;
            Debug.Log("스킬리스트 로드 완료");
        });
    }

    

}
