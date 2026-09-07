using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _damage;
    [SerializeField] private Item[] _itemPrefabs;
    private float _itemSpawnProbability;


    protected void Update()
    {
        Move();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            ItemDrop();
            Destroy(gameObject);
        }
    }

    protected abstract void Move();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Player player = other.GetComponent<Player>();
        if (player == null)
        {
            Debug.Log("플레이어가 null 입니다.");
            return;
        }

        player.TakeDamage(_damage);
        Destroy(gameObject);
    }

    private void ItemDrop()
    {
        _itemSpawnProbability = Random.Range(0.0f, 100.0f);

        if (_itemSpawnProbability >= 30.0f)
        {
            return;
        }
        else if (_itemSpawnProbability < 30.0f)
        {
            Debug.Log("아이템 생성");
            int itemIndex = Random.Range(0, _itemPrefabs.Length);

            Item item = Instantiate(_itemPrefabs[itemIndex]);
            item.transform.position = transform.position;
        }
    }
}