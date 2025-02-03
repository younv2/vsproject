using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]private MonsterData baseData;
    private MonsterStat stat;


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position  = Vector3.MoveTowards(transform.position, GameObject.FindWithTag("Player").gameObject.transform.position, baseData.MoveSpeed * Time.deltaTime);
    }
}
