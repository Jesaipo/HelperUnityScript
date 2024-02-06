using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class DisplayableKeyValuePair<TKey, TValue>
{
    public DisplayableKeyValuePair()
    {
    }

    public DisplayableKeyValuePair(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }

    [field: SerializeField] public TKey Key { set; get; }
    [field: SerializeField] public TValue Value { set; get; }
}

public class WeightedRandomHelper : MonoBehaviour
{
    static public T SelectOne<T>(ref List<DisplayableKeyValuePair<T, int>> list)
    {
        List<KeyValuePair<T, int>> newList = new List<KeyValuePair<T, int>>();

        foreach (var item in list)
        {
            newList.Add(new KeyValuePair<T, int>(item.Key, item.Value));
        }

        return SelectOne<T>(ref newList, false);
    }

    static public T SelectOne<T>(ref List<KeyValuePair<T, int>> list, bool modify = false)
    {
        if(list.Count ==0)
        {
            Debug.LogError("Cannot random choose from empty list");
            return default(T);
        }

        int totalCount = list.Sum(x => x.Value);

        int random = MyRandom.GetSeededRandomIntRange(0, totalCount);

        for (int i= 0; i < list.Count; i++)
        {
            KeyValuePair<T, int> keyValue = list[i];
            random -= keyValue.Value;
            if(random < 0)
            {
                if(modify)
                {
                    list[i] = new KeyValuePair<T, int>(keyValue.Key, keyValue.Value - 1);
                }
                return keyValue.Key;
            }
        }

        return list.Last().Key;
    }
}
