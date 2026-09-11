using UnityEngine;

public class ItemPool : MonoBehaviour
{
    private static ItemPool _instance;
    public static ItemPool Instance => _instance;

    [Header("아이템 프리팹들")]
    [SerializeField] private Item[] _itemPrefabs;

    [Header("아이템 풀 사이즈")]
    [SerializeField] private int _poolSize;

    private Item[,] _pool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        _pool = new Item[_itemPrefabs.Length, _poolSize];

        for (int i = 0; i < _itemPrefabs.Length; i++)
        {
            Item itemPrefab = _itemPrefabs[i];

            for (int j = 0; j < _poolSize; j++)
            {
                Item item = Instantiate(itemPrefab, gameObject.transform);
                item.gameObject.SetActive(false);
                _pool[i, j] = item;
            }
        }
    }

    public Item GetItem(ItemType itemType)
    {
        for (int i = 0; i < _itemPrefabs.Length; i++)
        {
            if (itemType == null)
            {
                continue;
            }

            for (int j = 0; j < _poolSize; j++)
            {
                Item item = _pool[i, j];

                if (item.gameObject.activeSelf == false)
                {
                    item.gameObject.SetActive(true);
                    return item;
                }
            }
        }

        return null;
    }
}