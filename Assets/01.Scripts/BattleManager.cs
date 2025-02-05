using UnityEngine;

public class BattleManager : MonoSingleton<BattleManager>
{
    SkillManager skillManagerInstance;
    TimeManager timeManagerInstance;

    //List<Projetile> projetileList;

    void Start()
    {
        skillManagerInstance = SkillManager.Instance;
        timeManagerInstance = TimeManager.Instance;

        ObjectPoolManager.Instance.Pools[Global.CHARACTER].GetObject();
    }

    void Update()
    {
        skillManagerInstance.Update();
        timeManagerInstance.Update();
    }
    private void FixedUpdate()
    {
        //TODO - Physics Settings의 Simulation Monde 추후 Script로 수정해서 직접 Physics 관리 할 것 
       /* foreach(var data in projetileList)
        {
            projetileList.Update();
        }*/
    }
}
