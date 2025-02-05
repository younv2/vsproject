using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using System.Collections;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    private Dictionary<string,Pool> pools;
    public Dictionary<string, Pool> Pools { get { return pools; } }

    private AddressablesLoader loader = new AddressablesLoader();
    [SerializeField]private List<AssetReference> addressList;
    private bool isInitialized = false;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    // Ǯ �ʱ�ȭ (�ڷ�ƾ)
    public IEnumerator InitPools()
    {
        pools = new Dictionary<string, Pool>();
        isInitialized = false;

        // Addressables �ε�
        bool loadDone = false;
        loader.LoadAssetListAsync<GameObject>(addressList, (loadedList) =>
        {
            foreach (var go in loadedList)
            {
                AddPool(go);
            }
            loadDone = true;
        });

        // �ε� ���� ���
        while (!loadDone)
        {
            yield return null;
        }

        isInitialized = true;
        Debug.Log("ObjectPoolManager: Ǯ �ʱ�ȭ �Ϸ�");
    }
    public void AddPool(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab.name))
        {
            pools.Add(prefab.name, new Pool(prefab));
        }
    }

}
