using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class RoundDB : ScriptableObject
{
    public List<GameObject> roundPrefabs;

    public Dictionary<int, GameObject> roundDict
        => roundPrefabs.Select((go, idx) => (idx, go)).ToDictionary(t => t.idx, t => t.go);
}
