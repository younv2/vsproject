using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using System.Collections;

public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{
    private Dictionary<string,object> pools = new Dictionary<string, object>();
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
        bool loadDone = false;
        // Addressables 로드
        loader.LoadAssetListAsync<GameObject>(addressList, (loadedList) =>
        {
            foreach (var go in loadedList)
            {
                // 순서대로 검사, 성공하면 true 반환 → 다음 if-else 검사 안 함
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
        
        // 로딩 종료 대기
        while (!loadDone)
        {
            yield return null;
        }
        // 반복되는 TryGetComponent 패턴을 헬퍼 메서드로 정리
        
        Debug.Log("ObjectPoolManager: 풀 초기화 완료");
    }
    /// <summary>
    /// 제네릭 헬퍼 메서드:
    /// GameObject에서 T 컴포넌트를 찾으면 풀에 등록 후 true 반환
    /// </summary>
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
