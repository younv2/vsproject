using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<PlayableCharacter>(out var data))
        {
            data.Stat.AddExp(10);
            ObjectPoolManager.Instance.GetPool<Item>(name.Replace("(Clone)", "")).ReleaseObject(this);
        }
    }
}
