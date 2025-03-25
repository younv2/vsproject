using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DataManager : MonoSingleton<DataManager>
{
    AddressablesLoader loader = new AddressablesLoader();
    [Header("어드레서블 라벨")]
    public AssetLabelReference skillAddressableLabel;
    public AssetLabelReference monsterAddressableLabel;

    private List<SkillDataBase> skillDataList;
    private List<MonsterData> monsterDataList;
    
    private Dictionary<int, int> characterExpTable = new Dictionary<int, int>();
    private TimeBasedBattleScaler timeBasedBattleScalers = new TimeBasedBattleScaler();

    private bool isSkillDataLoaded = false;
    private bool isMonsterDataLoaded = false;

    public List<SkillDataBase> SkillDataList { get { return skillDataList; } }
    public TimeBasedBattleScaler TimeBasedBattleScalers { get { return timeBasedBattleScalers; } }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public IEnumerator LoadAllData()
    {
        StartCoroutine(LoadSkillData());
        StartCoroutine(LoadMonsterData());
        LoadExpTable();
        LoadTimeBasedBattleScaler();

        yield return new WaitUntil(() => isSkillDataLoaded);
        yield return new WaitUntil(() => isMonsterDataLoaded);
    }

    public IEnumerator LoadSkillData()
    {
        bool loadDone = false;
        loader.LoadAssetListAsync<SkillDataBase>(skillAddressableLabel, (skillDataList) =>
        {
            this.skillDataList = skillDataList;
            loadDone = true;
        });
        while (!loadDone)
        {
            yield return null;
        }
        isMonsterDataLoaded = true;
        Debug.Log("DataManager: 스킬 데이터 로드 완료");
    }

    public IEnumerator LoadMonsterData()
    {
        bool loadDone = false;
        loader.LoadAssetListAsync<MonsterData>(monsterAddressableLabel, (monsterDataList) =>
        {
            this.monsterDataList = monsterDataList;
            loadDone = true;
        });
        while (!loadDone)
        {
            yield return null;
        }
        isSkillDataLoaded = true;
        Debug.Log("DataManager: 스킬 데이터 로드 완료");
    }

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

    public void LoadTimeBasedBattleScaler()
    {
        List<Dictionary<string, object>> datas = CSVReader.Read(Global.TIME_BASED_BATTLE_SCALER_TABLE_PATH);

        timeBasedBattleScalers.DataSetting(datas);
    }

    #region Getter
    public int GetExpByLevel(int level) => characterExpTable[level];
    public SkillDataBase GetSkillData(string skillName) => skillDataList.Find(x => x.skillName.Equals(skillName));
    public SkillDataBase GetSkillData(int skillId) => skillDataList.Find(x => x.skillId.Equals(skillId));
    public MonsterData GetMonsterData(string monsterName) => monsterDataList.Find(x => x.MonsterName.Equals(monsterName));
    public MonsterData GetMonsterData(int monsterId) => monsterDataList.Find(x => x.MonsterId.Equals(monsterId));
    #endregion
}
