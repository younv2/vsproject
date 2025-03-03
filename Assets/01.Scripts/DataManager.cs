using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DataManager : MonoSingleton<DataManager>
{
    AddressablesLoader loader = new AddressablesLoader();
    private List<SkillData> skillDataList = new List<SkillData>();
    public List<SkillData> SkillDataList { get { return skillDataList; } }

    public List<AssetReference> skillAddressList;

    private Dictionary<int,int> characterExpTable = new Dictionary<int,int>();
    private bool isSkillDataLoaded = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    /// <summary>
    /// 게임에 필요한 전체 데이터 로드
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadAllData()
    {
        StartCoroutine(LoadSkillData());
        LoadExpTable();

        yield return new WaitUntil(() => isSkillDataLoaded);
    }
    /// <summary>
    /// 스킬 데이터 로드
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// 경험치 테이블 로드
    /// </summary>
    public void LoadExpTable()
    {
        string jsonString;
        if (!File.Exists(Global.EXP_TABLE_PATH))
        {
            Debug.Log("데이터 파일이 존재하지 않음");
        }
        using (FileStream fs = new FileStream(Global.EXP_TABLE_PATH, FileMode.Open, FileAccess.Read))
        {
            using (StreamReader sr = new StreamReader(fs, new System.Text.UTF8Encoding(false)))
            {
                jsonString = sr.ReadToEnd();
            }
        }
        characterExpTable = JsonConvert.DeserializeObject<Dictionary<int, int>>(jsonString);
        if(characterExpTable is not null)
        {
            Debug.Log("DataManager: 경험치 테이블 데이터 로드 완료");
        }

    }
    public int GetExpByLevel(int level)
    {
        return characterExpTable[level];
    }
    public SkillData GetSkillData(string  skillName)
    {
        return skillDataList.Find(x => x.skillName.Equals(skillName));
    }


}
