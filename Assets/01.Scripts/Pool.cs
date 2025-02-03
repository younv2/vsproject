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

    private GameObject CreateObject() // ������Ʈ ����
    {
        return Instantiate(prefab, ObjectPoolManager.Instance.gameObject.transform);
    }

    private void ActivatePoolObject(GameObject obj) // ������Ʈ Ȱ��ȭ
    {
        obj.SetActive(true);
    }

    private void DisablePoolObject(GameObject obj) // ������Ʈ ��Ȱ��ȭ
    {
        obj.SetActive(false);
    }

    private void DestroyPoolObject(GameObject obj) // ������Ʈ ����
    {
        Destroy(obj);
    }

    public GameObject GetObject()
    {
        GameObject sel = null;

        if (pool.CountActive >= maxSize) // maxSize�� �Ѵ´ٸ� �ӽ� ��ü ���� �� ��ȯ
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