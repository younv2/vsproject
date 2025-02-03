using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    private List<Pool> pools;
    public List<Pool> Pools { get { return pools; } }
    protected override void Awake()
    {
        pools = new List<Pool>();
        
        AddPool(Resources.Load("Prefab/Character") as GameObject);
        var temp = Addressables.LoadAssetAsync<GameObject>("Prefab/Monster/Slime");
        temp.WaitForCompletion();
        pools[0].GetObject();
        AddPool(temp.Result);
    }

    public void AddPool(GameObject go)
    {
        pools.Add(new Pool(go));
    }
}
