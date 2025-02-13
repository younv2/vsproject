using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    SkillManager skillManagerInstance;
    TimeManager timeManagerInstance;
    MonsterSpawnManager monsterSpawnManagerInstance;
    PlayableCharacter playableCharacter;
    //List<Projetile> projetileList; 

    void Start()
    {
        skillManagerInstance = SkillManager.Instance;
        timeManagerInstance = TimeManager.Instance;
        monsterSpawnManagerInstance = MonsterSpawnManager.Instance;
        playableCharacter = ObjectPoolManager.Instance.GetPool<PlayableCharacter>(Global.CHARACTER).GetObject();
    }

    void Update()
    {
        skillManagerInstance.ManualUpdate();
        timeManagerInstance.ManualUpdate();
    }
    private void FixedUpdate()
    {
        foreach(var data in monsterSpawnManagerInstance.MonsterList)
        {
            data.ManualFixedUpdate();
        }

        //TODO - Physics Settings의 Simulation Monde 추후 Script로 수정해서 직접 Physics 관리 할 것 
       /* foreach(var data in projetileList)
        {
            projetileList.Update();
        }*/
    }
    public PlayableCharacter GetPlayableCharacter()
    {
        return playableCharacter;
    }
}
