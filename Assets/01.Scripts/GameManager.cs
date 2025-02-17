using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera UICamera;

    private void Awake()
    {
        GameObject.Find("ObjectPoolManager").transform.Find("Canvas").GetComponent<Canvas>().worldCamera = UICamera;
    }
}
