using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    private Dictionary<string,Pool> pools;
    public Dictionary<string, Pool> Pools { get { return pools; } }

    private AddressablesLoader loader = new AddressablesLoader();
    [SerializeField]private List<AssetReference> addressList;
    protected override void Awake()
    {
        Init();
    }
    public void Init()
    {
        pools = new Dictionary<string, Pool>();

        loader.LoadAssetListAsync<GameObject>(addressList, (callback) =>
        {
            List<GameObject> list = callback; 

            foreach (var data in list)
            {
                AddPool(data);
            }
        });
    }
    public void AddPool(GameObject go)
    {
        pools.Add(go.name,new Pool(go));
    }

}
