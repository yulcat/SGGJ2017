using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EffectSpawner : SGSingleton<EffectSpawner>
{
    Dictionary<string, List<GameObject>> spawned = new Dictionary<string, List<GameObject>>();
    Dictionary<string, GameObject> originals = new Dictionary<string, GameObject>();

    GameObject GetEffectInstance(string key)
    {
        if (spawned.ContainsKey(key))
        {
            try
            {
                return spawned[key].First(e => !e.activeSelf);
            }
            catch
            {
                var instance = InstantiateEffect(key);
                spawned[key].Add(instance);
                return instance;
            }
        }
        var firstInstance = InstantiateEffect(key);
        spawned.Add(key, new List<GameObject>());
        spawned[key].Add(firstInstance);
        return firstInstance;
    }

    GameObject InstantiateEffect(string key)
    {
        var instance = Instantiate<GameObject>(GetOriginal(key));
        instance.transform.SetParent(transform);
        return instance;
    }

    public static GameObject GetEffect(string key)
    {
        return Instance.GetEffectInstance(key);
    }

    public static void SetEffect(string key, Vector2 position)
    {
        var effect = GetEffect(key);
        effect.transform.position = new Vector3(position.x, position.y, effect.transform.position.z);
        effect.SetActive(true);
    }

    GameObject GetOriginal(string key)
    {
        if (originals.ContainsKey(key)) return originals[key];
        var original = Resources.Load<GameObject>("Effects/" + key);
        originals.Add(key, original);
        return originals[key];
    }
}
