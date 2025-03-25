using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using System.Collections;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    private Dictionary<string,object> pools = new Dictionary<string, object>();
    public Dictionary<string, object> Pools { get { return pools; } }

    private AddressablesLoader loader = new AddressablesLoader();
    [SerializeField]private AssetLabelReference addressList;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public IEnumerator InitPools()
    {
        bool loadDone = false;

        loader.LoadAssetListAsync<GameObject>(addressList, (loadedList) =>
        {
            foreach (var go in loadedList)
            {
                if (TryAddPool<Monster>(go)) { }
                else if (TryAddPool<PlayableCharacter>(go)) { }
                else if (TryAddPool<Projectile>(go)) { }
                else if (TryAddPool<DamageTextUI>(go)) { }
                else if (TryAddPool<HPBarUI>(go)) { }
                else if (TryAddPool<Item>(go)) { }
                else
                {
                    AddPool(go);
                }
            }
            loadDone = true;
        });
        
        while (!loadDone)
        {
            yield return null;
        }
        
        Debug.Log("ObjectPoolManager: 풀 초기화 완료");
    }

    private bool TryAddPool<T>(GameObject go) where T : Object
    {
        if (go.TryGetComponent<T>(out var component))
        {
            AddPool(component);
            return true;
        }
        return false;
    }

    public void AddPool<T>(T prefab) where T: Object
    {
        if (!pools.ContainsKey(prefab.name))
        {
            pools.Add(prefab.name, new Pool<T>(prefab));
        }
    }

    public Pool<T> GetPool<T>(string prefabName) where T : Object
    {

        if (pools.ContainsKey(prefabName))
        {
            return (Pool<T>)pools[prefabName]; 
        }

        return null; 
    }
}
