using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using DG.Tweening;


public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;
    public Collectibles collectibles;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void CollectCollectible(CollectibleName name, Vector3 pos)
    {
        Vector3 worldToScreen = Camera.main.WorldToScreenPoint(pos);

    }

    public int GetCollectibleCount(CollectibleName name)
    {
        return collectibles[name].CollectibleCount;
    }

    public aCollectible getaCollectible(CollectibleName name)
    {
        return collectibles[name];
    }
}

[System.Serializable]
public class aCollectible
{
    public string CollectibleSaveName;
    public Sprite CollectibleIcon;
    [HideInInspector]
    public int CollectibleCount
    {
        get
        {
            return PlayerPrefs.GetInt(CollectibleSaveName, 0);
        }
        set
        {

            PlayerPrefs.SetInt(CollectibleSaveName, value);
            PlayerPrefs.Save();
        }
    }

}

[System.Serializable]
public class Collectibles : SerializableDictionaryBase<CollectibleName, aCollectible> { }

public enum CollectibleName
{
    Coin,
    Star,
    Diamond,
}