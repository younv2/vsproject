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
    // 풀 초기화 (코루틴)
    public IEnumerator InitPools()
    {
        pools = new Dictionary<string, Pool>();
        isInitialized = false;

        // Addressables 로드
        bool loadDone = false;
        loader.LoadAssetListAsync<GameObject>(addressList, (loadedList) =>
        {
            foreach (var go in loadedList)
            {
                AddPool(go);
            }
            loadDone = true;
        });

        // 로딩 종료 대기
        while (!loadDone)
        {
            yield return null;
        }

        isInitialized = true;
        Debug.Log("ObjectPoolManager: 풀 초기화 완료");
    }
    public void AddPool(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab.name))
        {
            pools.Add(prefab.name, new Pool(prefab));
        }
    }

}
