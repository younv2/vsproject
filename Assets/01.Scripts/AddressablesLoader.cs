using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public void LoadAssetListAsync<T>(List<AssetReference> list, Action<List<T>> callback = null)
    {
        List<T> result  = new List<T>();

        Addressables.LoadAssetsAsync<T>(list, asset =>
        {
            result.Add(asset);
            callback?.Invoke(result);
        }, MergeMode.Union).Completed += (handle) => {
            HandleCompletion(handle);
            callback?.Invoke(result);
        };
    }
    /// <summary>
    /// 어드레서블 주소를 통한 에셋 리스트 로드
    /// </summary>
    public async void LoadAssetListAsync<T>(string address, Action<List<T>> callback = null)
    {
        List<T> result = new List<T>();
        LoadResourceLocationsAsync(address).Completed += (handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded && handle.Result != null)
            {
                IList<IResourceLocation> list = handle.Result;

                LoadAssetsAsync<T>(list, asset =>
                {
                    result.Add(asset);
                }).Completed += (handle) => {
                    HandleCompletion(handle);
                    callback?.Invoke(result);
                };
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
    public async Task<IList<T>> LoadAssetListAsync<T>(AssetLabelReference label)
    {
        List<T> result;
        AsyncOperationHandle<IList<IResourceLocation>> handle = LoadResourceLocationsAsync(label);
        await handle.Task;
        if (handle.Status != AsyncOperationStatus.Succeeded || handle.Result == null)
        {
            Debug.LogError($"❌ LoadResourceLocationsAsync 실패 또는 결과 없음: {label}");
            return null;
        }

        IList<IResourceLocation> list = handle.Result;
        result = new List<T>(list.Count);
        AsyncOperationHandle<IList<T>> assetHandle = LoadAssetsAsync<T>(list, asset =>
        {
            result.Add(asset);
        });
        await assetHandle.Task;
        HandleCompletion(assetHandle);
        return assetHandle.Task.Result;
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
