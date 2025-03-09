using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Pool<T> where T : Object
{
    private ObjectPool<T> pool;
     
    private const int maxSize = 200;
    private const int initSize = 20;

    private T prefab;

    public Pool(T prefab)
    {
        this.prefab = prefab;

        pool = new ObjectPool<T>(CreateObject, ActivatePoolObject, DisablePoolObject, DestroyPoolObject, false, initSize, maxSize);

        List<T> tempList = new List<T>();
        for (int i = 0; i < initSize; i++)
        {
            tempList.Add(CreateObject());
        }
        for (int i = 0; i < tempList.Count; i++)
        {
            pool.Release(tempList[i]);
        }
    }
    /// <summary>
    /// 오브젝트 생성
    /// </summary>
    /// <returns></returns>
    private T CreateObject()
    {

        Transform parent = null;

        if (prefab is MonoBehaviour mono && mono.GetComponent<RectTransform>() != null)
        {
            parent = ObjectPoolManager.Instance.transform.Find("Canvas").transform;
        }
        else
        {
            // 2) 그 외(일반 오브젝트)면 ObjectPoolManager 아래로
            parent = ObjectPoolManager.Instance.transform;
        }

        T data = GameObject.Instantiate(prefab, parent);

        if (data is GameObject go)
        {
            go.name = prefab.name;
        }
        else if(data is MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.name = prefab.name;
        }
            
        return data;
    }
    /// <summary>
    /// 오브젝트 활성화
    /// </summary>
    /// <param name="obj"></param>
    private void ActivatePoolObject(T obj)
    {
        if (obj is MonoBehaviour mono)
        {
            mono.gameObject.SetActive(true);
        }
        if(obj is GameObject go)
        {
            go.SetActive(true);
        }
        
    }
    /// <summary>
    /// 오브젝트 비활성화
    /// </summary>
    /// <param name="obj"></param>
    private void DisablePoolObject(T obj)
    {
        if (obj is MonoBehaviour mono)
        {
            mono.gameObject.SetActive(false);
        }
        if (obj is GameObject go)
        {
            go.SetActive(false);
        }
    }
    /// <summary>
    /// 오브젝트 삭제
    /// </summary>
    /// <param name="obj"></param>
    private void DestroyPoolObject(T obj)
    {
        GameObject.Destroy(obj);
    }
    /// <summary>
    /// 오브젝트 가져오기
    /// </summary>
    /// <returns></returns>
    public T GetObject()
    {
        T sel = null;

        sel = pool.Get();

        return sel;
    }
    /// <summary>
    /// 오브젝트 제거
    /// </summary>
    /// <param name="obj"></param>
    public void ReleaseObject(T obj)
    {
        pool.Release(obj);
    }
}
public interface IPoolable
{

}