using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using static UnityEngine.AddressableAssets.Addressables;

public class AddressablesLoader
{
    /// <summary>
    /// 에셋 로드
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public void LoadAssetAsync<T>(AssetReference list)
    {
        T result = default(T);

        Addressables.LoadAssetAsync<T>(list).Completed += (op) =>
        {
            Debug.Log(op.ToString());
        };
    }
    /// <summary>
    /// 에셋 리스트 로드
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="callback"></param>
    public void LoadAssetListAsync<T>(List<AssetReference> list, Action<List<T>> callback = null)
    {
        List<T> result  = new List<T>();

        Addressables.LoadAssetsAsync<T>(list, asset =>
        {
            result.Add(asset);
            callback?.Invoke(result);
        }, MergeMode.Union).Completed += HandleCompletion;
    }
    /// <summary>
    /// 어드레서블 주소를 통한 에셋 리스트 로드
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="callback"></param>
    public void LoadAssetListAsync<T>(string address, Action<List<T>> callback = null)
    {
        List<T> result = new List<T>();
        Addressables.LoadResourceLocationsAsync(address).Completed += (list) =>
        {
            if (list.Status == AsyncOperationStatus.Succeeded && list.Result != null)
            {
                IList<IResourceLocation> locations = list.Result;

                Addressables.LoadAssetsAsync<T>(locations, asset =>
                {
                    result.Add(asset);
                    callback?.Invoke(result);
                }).Completed += HandleCompletion;
            }
            else
            {
                Debug.LogError($"❌ LoadResourceLocationsAsync 실패 또는 결과 없음: {address}");
            }
        };
    }
    /// <summary>
    /// 라벨 정보를 통해 에셋 리스트 로드
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="label"></param>
    /// <param name="callback"></param>
    public void LoadAssetListAsync<T>(AssetLabelReference label, Action<List<T>> callback = null)
    {
        List<T> result = new List<T>();
        Addressables.LoadResourceLocationsAsync(label).Completed += (list) =>
        {
            if (list.Status == AsyncOperationStatus.Succeeded && list.Result != null)
            {
                IList<IResourceLocation> locations = list.Result;

                Addressables.LoadAssetsAsync<T>(locations, asset =>
                {
                    result.Add(asset);
                    callback?.Invoke(result);
                }).Completed += HandleCompletion;
            }
            else
            {
                Debug.LogError($"❌ LoadResourceLocationsAsync 실패 또는 결과 없음: {label}");
            }
        };

    }
    private void HandleCompletion<T>(AsyncOperationHandle<IList<T>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
        }
        else
        {
            Debug.LogError("Failed to load assets.");
        }
    }
}
