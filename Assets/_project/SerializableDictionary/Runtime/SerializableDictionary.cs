using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<tk, tv>
{
    public List<KeyValue<tk, tv>> values = new List<KeyValue<tk, tv>>();

    public void Add(tk key, tv value)
    {
        KeyValue<tk, tv> pair = new KeyValue<tk, tv> { Key = key, Value = value };
        values.Add(pair);
    }

    public bool TryGetValue(tk key, out tv value)
    {
        foreach (var pair in values)
        {
            if (EqualityComparer<tk>.Default.Equals(pair.Key, key))
            {
                value = pair.Value;
                return true;
            }
        }

        value = default(tv);
        return false;
    }
}
