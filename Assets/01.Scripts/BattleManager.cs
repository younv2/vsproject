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
        //TODO - Physics Settings�� Simulation Monde ���� Script�� �����ؼ� ���� Physics ���� �� �� 
       /* foreach(var data in projetileList)
        {
            projetileList.Update();
        }*/
    }
}
