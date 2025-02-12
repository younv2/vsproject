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
    // 풀 초기화 (코루틴) 
    // Todo - IPoolable로 묶어 간단하게 표현할 방법 찾아보기
    public IEnumerator InitPools()
    {
        pools = new Dictionary<string, object>();
        List<GameObject> list = new List<GameObject>();
        // Addressables 로드
        bool loadDone = false;
        loader.LoadAssetListAsync<GameObject>(addressList, (loadedList) =>
        {
            list = loadedList;
            
            loadDone = true;
        });
        
        // 로딩 종료 대기
        while (!loadDone)
        {

            yield return null;
        }
        foreach (var go in list)
        {
            // Monster 컴포넌트가 있다면 Pool<Monster>로 등록
            if (go.TryGetComponent<Monster>(out var monster))
            {
                AddPool(monster);
            }
            // Character 컴포넌트가 있다면 Pool<Character>로 등록
            else if (go.TryGetComponent<Character>(out var character))
            {
                AddPool(character);
            }
            // Skill 컴포넌트가 있다면 Pool<Projectile>로 등록
            else if (go.TryGetComponent<Projectile>(out var skill))
            {
                AddPool(skill);
            }
            else
            {
                // 둘 다 없으면 GameObject 자체로 Pool 생성
                AddPool(go);
            }
        }
        Debug.Log("ObjectPoolManager: 풀 초기화 완료");
    }
    public void AddPool<T>(T prefab) where T: Object
    {
        if (!pools.ContainsKey(prefab.name))
        {
            pools.Add(prefab.name, new Pool<T>(prefab));
        }
    }
    // 특정 풀을 가져오는 메서드
    public Pool<T> GetPool<T>(string prefabName) where T : Object
    {
        // pools에서 object로 저장된 풀을 T 타입으로 캐스팅해서 반환
        if (pools.ContainsKey(prefabName))
        {
            return (Pool<T>)pools[prefabName];  // 타입을 T로 캐스팅하여 반환
        }

        return null;  // 해당 이름의 풀이 없다면 null 반환
    }
}
