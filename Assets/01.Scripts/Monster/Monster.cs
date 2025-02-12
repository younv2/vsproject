using UnityEngine;

public class Monster : MonoBehaviour, IPoolable
{
    [SerializeField]private MonsterData baseData;
    private MonsterStat stat;

    // Update is called once per frame
    public void ManualFixedUpdate()
    {
        transform.position  = Vector3.MoveTowards(transform.position, GameObject.FindWithTag("Player").gameObject.transform.position, baseData.MoveSpeed * Time.deltaTime);
    }
}
