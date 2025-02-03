using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool : MonoBehaviour
{
    private ObjectPool<GameObject> pool;
    private const int maxSize = 100;
    private const int initSize = 20;

    private GameObject prefab;
    
    public Pool(GameObject prefab)
    {
        this.prefab = prefab;

        pool = new ObjectPool<GameObject>(CreateObject, ActivatePoolObject, DisablePoolObject, DestroyPoolObject, false, initSize, maxSize);

        List<GameObject> tempList = new List<GameObject>();
        for (int i = 0; i < initSize; i++)
        {
            tempList.Add(CreateObject());
        }
        for (int i = 0; i < tempList.Count; i++)
        {
            pool.Release(tempList[i]);
        }
    }

    private GameObject CreateObject() // 오브젝트 생성
    {
        return Instantiate(prefab, ObjectPoolManager.Instance.gameObject.transform);
    }

    private void ActivatePoolObject(GameObject obj) // 오브젝트 활성화
    {
        obj.SetActive(true);
    }

    private void DisablePoolObject(GameObject obj) // 오브젝트 비활성화
    {
        obj.SetActive(false);
    }

    private void DestroyPoolObject(GameObject obj) // 오브젝트 삭제
    {
        Destroy(obj);
    }

    public GameObject GetObject()
    {
        GameObject sel = null;

        if (pool.CountActive >= maxSize) // maxSize를 넘는다면 임시 객체 생성 및 반환
        {
            sel = CreateObject();
            sel.tag = "PoolOverObj";
        }
        else
        {
            sel = pool.Get();
        }

        return sel;
    }

    public void ReleaseObject(GameObject obj)
    {
        if (obj.CompareTag("PoolOverObj"))
        {
            Destroy(obj);
        }
        else
        {
            pool.Release(obj);
        }
    }
}