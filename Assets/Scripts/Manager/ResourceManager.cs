using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();

    private string _spritePath; //스프라이트 파일 경로

    public T Load<T>(string path) where T : Object
    {
        //스프라이트 중복 시 재활용
        if (typeof(T) == typeof(Sprite))
        {
            _spritePath = $"Sprites/{path}";

            if (_sprites.TryGetValue(_spritePath, out Sprite sprite))
                return sprite as T;

            Sprite sp = Resources.Load<Sprite>(_spritePath);
            _sprites.Add(_spritePath, sp);
            return sp as T;
        }

        //리소스가 Null인 경우 예외처리
        T resouce = Resources.Load<T>(path);

        if (resouce == null)
        {
            ConsoleLogger.LogError($"{path}를 불러오지 못했습니다.");
            return null;
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            ConsoleLogger.Log($"Failed to load prefab : {path}");
            return null;
        }

        return Instantiate(prefab, parent);
    }

    public GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        GameObject go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}