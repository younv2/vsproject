using UnityEngine;

public class PlayableCharacter : MonoBehaviour, IPoolable
{
    private CharacterStat stat = new CharacterStat();
    private Scanner scanner;
    public CharacterStat Stat { get { return stat; } }
    void Start()
    {
        scanner = transform.GetComponent<Scanner>();
    }
    void OnEnable()
    {
        Stat.Init();
    }
    void Update()
    {

    }
    public Transform GetNearstTarget()
    {
        return scanner.nearstObject.transform;
    }
}
