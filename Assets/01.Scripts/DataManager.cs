using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

    public List<SkillDataBase> SkillDataList { get { return skillDataList; } }
    public TimeBasedBattleScaler TimeBasedBattleScalers { get { return timeBasedBattleScalers; } }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public Task LoadAllData()
    {
        Task skillTask = LoadSkillDataAsync();
        Task monsterTask = LoadMonsterDataAsync();
        Task expTask = LoadExpTableAsync();
        Task scalerTask = LoadTimeBasedBattleScalerAsync();

        return Task.WhenAll(skillTask, monsterTask, expTask, scalerTask);
    }

    public async Task LoadSkillDataAsync()
    {
        var task = loader.LoadAssetListAsync<SkillDataBase>(skillAddressableLabel);
        await task;
        if(task.IsCompleted &&task.Result!= null)
        {
            skillDataList = task.Result as List<SkillDataBase>;
            Debug.Log("DataManager: 스킬 데이터 로드 완료");
        }
    }

    public async Task LoadMonsterDataAsync()
    {
        var task = loader.LoadAssetListAsync<MonsterData>(monsterAddressableLabel);
        await task;
        if (task.IsCompleted && task.Result != null)
        {
            this.monsterDataList = task.Result as List<MonsterData>;
            Debug.Log("DataManager: 몬스터 데이터 로드 완료");
        }
    }

    public async Task LoadExpTableAsync()
    {
        if (!File.Exists(Global.EXP_TABLE_PATH))
        {
            Debug.Log("데이터 파일이 존재하지 않음");
            return;
        }

        string jsonString;
        using (FileStream fs = new FileStream(Global.EXP_TABLE_PATH, FileMode.Open, FileAccess.Read, FileShare.Read))
        using (StreamReader sr = new StreamReader(fs, new System.Text.UTF8Encoding(false)))
        {
            jsonString = await sr.ReadToEndAsync();
        }
        characterExpTable = JsonConvert.DeserializeObject<Dictionary<int, int>>(jsonString);
        if (characterExpTable != null)
        {
            Debug.Log("DataManager: 경험치 테이블 데이터 로드 완료");
        }
    }

    public async Task LoadTimeBasedBattleScalerAsync()
    {
        List<Dictionary<string, object>> datas = await Task.Run(() => CSVReader.Read(Global.TIME_BASED_BATTLE_SCALER_TABLE_PATH));
        
        timeBasedBattleScalers.DataSetting(datas);
        
        Debug.Log("DataManager: 시간 기반 배틀 스케일러 로드 완료");
    }

    #region Getter
    public int GetExpByLevel(int level) => characterExpTable[level];
    public SkillDataBase GetSkillData(string skillName) => skillDataList.Find(x => x.skillName.Equals(skillName));
    public SkillDataBase GetSkillData(int skillId) => skillDataList.Find(x => x.skillId.Equals(skillId));
    public MonsterData GetMonsterData(string monsterName) => monsterDataList.Find(x => x.MonsterName.Equals(monsterName));
    public MonsterData GetMonsterData(int monsterId) => monsterDataList.Find(x => x.MonsterId.Equals(monsterId));
    #endregion
}
