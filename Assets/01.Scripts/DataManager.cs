using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DataManager : MonoSingleton<DataManager>
{
    AddressablesLoader loader = new AddressablesLoader();

    private List<SkillDataBase> skillDataList = new List<SkillDataBase>();
    public AssetLabelReference skillAddressableLabel;
    private Dictionary<int, int> characterExpTable = new Dictionary<int, int>();
    private TimeBasedBattleScaler timeBasedBattleScalers = new TimeBasedBattleScaler();

    public List<SkillDataBase> SkillDataList { get { return skillDataList; } }
    public TimeBasedBattleScaler TimeBasedBattleScalers { get { return timeBasedBattleScalers; } }
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
        LoadTimeBasedBattleScaler();

        yield return new WaitUntil(() => isSkillDataLoaded);
    }
    /// <summary>
    /// 스킬 데이터 로드
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadSkillData()
    {
        bool loadDone = false;
        loader.LoadAssetListAsync<SkillDataBase>(skillAddressableLabel, (callback) =>
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
        if (characterExpTable is not null)
        {
            Debug.Log("DataManager: 경험치 테이블 데이터 로드 완료");
        }
    }
    /// <summary>
    /// 시간별 몬스터 스케일 데이터 로드
    /// </summary>
    public void LoadTimeBasedBattleScaler()
    {
        var datas = CSVReader.Read(Global.TIME_BASED_BATTLE_SCALER_TABLE_PATH);

        timeBasedBattleScalers.DataSetting(datas);
    }

    #region Getter
    public int GetExpByLevel(int level)
    {
        return characterExpTable[level];
    }
    public SkillDataBase GetSkillData(string skillName)
    {
        return skillDataList.Find(x => x.skillName.Equals(skillName));
    }
    public SkillDataBase GetRandomSkillData()
    {
        return skillDataList[Random.Range(0, skillDataList.Count)];
    }
    public List<SkillDataBase> GetAllSkillData()
    {
        return skillDataList;
    }
    #endregion
}
