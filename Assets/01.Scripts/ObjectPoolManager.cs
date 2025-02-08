using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using System.Collections;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    private Dictionary<string,object> pools;
    public Dictionary<string, object> Pools { get { return pools; } }

    private AddressablesLoader loader = new AddressablesLoader();
    [SerializeField]private List<AssetReference> addressList;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    // Ǯ �ʱ�ȭ (�ڷ�ƾ)
    public IEnumerator InitPools()
    {
        pools = new Dictionary<string, object>();
        List<GameObject> list = new List<GameObject>();
        // Addressables �ε�
        bool loadDone = false;
        loader.LoadAssetListAsync<GameObject>(addressList, (loadedList) =>
        {
            list = loadedList;
            
            loadDone = true;
        });
        
        // �ε� ���� ���
        while (!loadDone)
        {

            yield return null;
        }
        foreach (var go in list)
        {
            // ��: Monster ������Ʈ�� �ִٸ� Pool<Monster>�� ���
            if (go.TryGetComponent<Monster>(out var monster))
            {
                AddPool(monster);
            }
            // ��: Character ������Ʈ�� �ִٸ� Pool<Character>�� ���
            else if (go.TryGetComponent<Character>(out var character))
            {
                AddPool(character);
            }
            else
            {
                // �� �� ������ GameObject ��ü�� Pool ����
                AddPool(go);
            }
        }
        Debug.Log("ObjectPoolManager: Ǯ �ʱ�ȭ �Ϸ�");
    }
    public void AddPool<T>(T prefab) where T: Object
    {
        if (!pools.ContainsKey(prefab.name))
        {
            pools.Add(prefab.name, new Pool<T>(prefab));
        }
    }
    // Ư�� Ǯ�� �������� �޼���
    public Pool<T> GetPool<T>(string prefabName) where T : Object
    {
        // pools���� object�� ����� Ǯ�� T Ÿ������ ĳ�����ؼ� ��ȯ
        if (pools.ContainsKey(prefabName))
        {
            return (Pool<T>)pools[prefabName];  // Ÿ���� T�� ĳ�����Ͽ� ��ȯ
        }

        return null;  // �ش� �̸��� Ǯ�� ���ٸ� null ��ȯ
    }
}
