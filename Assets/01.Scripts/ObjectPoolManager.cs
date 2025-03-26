using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using System.Collections;
using System.Threading.Tasks;

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

    public async Task InitPools()
    {
        var task = loader.LoadAssetListAsync<GameObject>(addressList);
        await task;
        if (task.IsCompleted && task.Result != null)
        {
            List<GameObject> goList = task.Result as List<GameObject>;

            foreach (var go in goList)
            {
                bool hasPoolingComponent = TryAddPool<Monster>(go) ||
                    TryAddPool<PlayableCharacter>(go) ||
                    TryAddPool<Projectile>(go) ||
                    TryAddPool<DamageTextUI>(go) ||
                    TryAddPool<HPBarUI>(go) ||
                    TryAddPool<Item>(go);

                if (!hasPoolingComponent) 
                { 
                    AddPool(go); 
                }   
            }
            Debug.Log("ObjectPoolManager: 풀 초기화 완료");
        } 
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
