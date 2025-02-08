using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    SkillManager skillManagerInstance;
    TimeManager timeManagerInstance;
    MonsterSpawnManager monsterSpawnManagerInstance;
    //List<Projetile> projetileList;

    void Start()
    {
        skillManagerInstance = SkillManager.Instance;
        timeManagerInstance = TimeManager.Instance;
        monsterSpawnManagerInstance = MonsterSpawnManager.Instance;
        ObjectPoolManager.Instance.GetPool<Character>(Global.CHARACTER).GetObject();
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

        //TODO - Physics Settings�� Simulation Monde ���� Script�� �����ؼ� ���� Physics ���� �� �� 
       /* foreach(var data in projetileList)
        {
            projetileList.Update();
        }*/
    }
}
