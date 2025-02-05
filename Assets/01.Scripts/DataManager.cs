using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DataManager : MonoSingleton<DataManager>
{
    AddressablesLoader loader = new AddressablesLoader();
    private List<SkillData> skillDataList = new List<SkillData>();
    public List<SkillData> SkillDataList { get { return skillDataList; } }

    public List<AssetReference> skillAddressList;

    private bool isSkillDataLoaded = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public IEnumerator LoadAllData()
    {
        StartCoroutine(LoadSkillData());

        yield return new WaitUntil(() => isSkillDataLoaded);
    }

    public IEnumerator LoadSkillData()
    {
        bool loadDone = false;
        loader.LoadAssetListAsync<SkillData>(skillAddressList, (callback) =>
        {
            skillDataList = callback;
            loadDone = true;
        });
        while (!loadDone)
        {
            yield return null;
        }
        isSkillDataLoaded = true;
        Debug.Log("DataManager: 스킬 데이터 로드 완료");
    }

    

}
