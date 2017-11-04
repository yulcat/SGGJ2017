using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CreateAssetMenu()]
public class TileLoader : ScriptableObject
{
    public GameObject floor;
    public GameObject wall;
    public GameObject inCorner;
    public GameObject outCorner;
    public GameObject spike;
    public GameObject tar;

    static TileLoader Set
    {
        get
        {
            if (_set == null)
            {
                _set = AssetDatabase.LoadAssetAtPath<TileLoader>("Assets/Tiles.asset");
            }
            return _set;
        }
    }
    static TileLoader _set;

    [MenuItem("Tools/Tileset Loader")]
    static void LoadImage()
    {
        var path = EditorUtility.OpenFilePanel("Open Tileset Image", Application.dataPath, "png");
        if (string.IsNullOrEmpty(path)) return;
        var tex = new Texture2D(2, 2);
        tex.LoadImage(System.IO.File.ReadAllBytes(path));
        var pixels = tex.GetPixels32();
        var floors = new List<Vector2Int>();
        var spikes = new List<Vector2Int>();
        var tars = new List<Vector2Int>();
        for (int x = 0; x < tex.width; x++)
        {
            for (var y = 0; y < tex.height; y++)
            {
                var pixel = pixels[x + y * tex.width];
                if (IsSimilarColor(pixel, Color.white)) floors.Add(new Vector2Int(x, y));
                if (IsSimilarColor(pixel, Color.red)) spikes.Add(new Vector2Int(x, y));
                if (IsSimilarColor(pixel, Color.blue)) tars.Add(new Vector2Int(x, y));
            }
        }
        Generate(floors, spikes, tars);
    }

    static bool IsSimilarColor(Color32 one, Color other)
    {
        return Vector4.Magnitude(one - other) < 0.2f;
    }

    static void Generate(List<Vector2Int> floors, List<Vector2Int> spikes, List<Vector2Int> tars)
    {
        float xmax = floors.Max(f => f.x);
        float ymax = floors.Max(f => f.y);
        float xmin = floors.Min(f => f.x);
        float ymin = floors.Min(f => f.y);
        float xc = (xmax + xmin) / 2;
        float yc = (ymax + ymin) / 2;
        var parent = new GameObject("Tiles");
        var outlines = new Dictionary<Vector2Int, List<Vector2Int>>();
        foreach (var floor in floors)
        {
            var go = Instantiate<GameObject>(Set.floor);
            go.transform.position = new Vector2(floor.x - xc, floor.y - yc);
            go.transform.SetParent(parent.transform);
            foreach (var dirc in directions8())
            {
                var target = floor + dirc;
                if (floors.Contains(target) || spikes.Contains(target) || tars.Contains(target)) continue;
                AddListInDictionary(target, floor, outlines);
            }
        }
        foreach (var spike in spikes)
        {
            var go = Instantiate<GameObject>(Set.spike);
            go.transform.position = new Vector2(spike.x - xc, spike.y - yc);
            go.transform.SetParent(parent.transform);
        }
        foreach (var tar in tars)
        {
            var go = Instantiate<GameObject>(Set.tar);
            go.transform.position = new Vector2(tar.x - xc, tar.y - yc);
            go.transform.SetParent(parent.transform);
        }
        foreach (var kvp in outlines)
        {
            var directions = directions4.Where(d => kvp.Value.Contains(d + kvp.Key)).ToArray();
            GameObject original = Set.wall;
            int dirc = 0;
            switch (directions.Length)
            {
                case 1:
                    original = Set.wall;
                    dirc = directions4.TakeWhile(d => d != directions[0]).Count();
                    break;
                case 2:
                    original = Set.outCorner;
                    if (directions.Contains(Vector2Int.up) && directions.Contains(Vector2Int.left))
                        dirc = 0;
                    else if (directions.Contains(Vector2Int.left) && directions.Contains(Vector2Int.down))
                        dirc = 1;
                    else if (directions.Contains(Vector2Int.down) && directions.Contains(Vector2Int.right))
                        dirc = 2;
                    else if (directions.Contains(Vector2Int.right) && directions.Contains(Vector2Int.up))
                        dirc = 3;
                    else continue;
                    break;
                case 3:
                    continue;
                case 0:
                    original = Set.inCorner;
                    if (kvp.Value.Contains(new Vector2Int(1, -1) + kvp.Key))
                        dirc = 2;
                    else if (kvp.Value.Contains(new Vector2Int(-1, -1) + kvp.Key))
                        dirc = 1;
                    else if (kvp.Value.Contains(new Vector2Int(-1, 1) + kvp.Key))
                        dirc = 0;
                    else if (kvp.Value.Contains(new Vector2Int(1, 1) + kvp.Key))
                        dirc = 3;
                    else continue;
                    break;
                case 4:
                    continue;
            }
            var go = Instantiate<GameObject>(original);
            go.transform.position = new Vector2(kvp.Key.x - xc, kvp.Key.y - yc);
            go.transform.eulerAngles = new Vector3(0, 0, 90 * dirc);
            go.transform.SetParent(parent.transform);
        }
    }

    static void AddListInDictionary(Vector2Int target, Vector2Int neighbor, Dictionary<Vector2Int, List<Vector2Int>> dic)
    {
        if (!dic.ContainsKey(target))
        {
            dic.Add(target, new List<Vector2Int>());
            dic[target].Add(neighbor);
        }
        dic[target].Add(neighbor);
    }

    static IEnumerable<Vector2Int> directions8()
    {
        yield return new Vector2Int(-1, -1);
        yield return new Vector2Int(-1, 0);
        yield return new Vector2Int(-1, 1);
        yield return new Vector2Int(0, -1);
        yield return new Vector2Int(0, 1);
        yield return new Vector2Int(1, -1);
        yield return new Vector2Int(1, 0);
        yield return new Vector2Int(1, 1);
    }
    static Vector2Int[] directions4 = {
        Vector2Int.up,
        Vector2Int.left,
        Vector2Int.down,
        Vector2Int.right,
    };
}
