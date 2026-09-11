using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    [SerializeField] protected float _moveSpeed;

    [SerializeField] protected float _damage;

    // [SerializeField] private Item[] _itemPrefabs;
    [SerializeField] private int _itemSpawnProbability = 30;
    private int _itemRandomSpawnProbability;

    private Animator _animator;

    // ToDo: 적이 공격 당할 때 재생시켜주는 피격 사운드
    private AudioSource _damagedAudioSource;

    // - 죽을 때 생성할 이펙트 프리팹
    [SerializeField] private GameObject _deathEffectPrefab;

    [SerializeField] private ItemSpawnDataTableSO _itemSpawnDataTable;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _damagedAudioSource = GetComponent<AudioSource>();
    }

    protected void Update()
    {
        Move();
    }

    public void TakeDamage(float damage)
    {
        _damagedAudioSource.Play();
        _health -= damage;

        _animator.SetTrigger("Hit");

        if (_health <= 0)
        {
            ItemDrop();

            SpawnDeathEffect();

            // 싱글톤 패턴
            // 1. 전역적으로 누구를 뜻하는지 안다.    => 적역으로 접근 가능
            // 2. 그 누구가 한명 인것을 안다.        => 인스턴스(생성된 객체)가 하나임을 보장한다.

            ScoreManager.Instance.AddScore(100);

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
        // Destroy(gameObject);
    }

    private void ItemDrop()
    {
        // Todo: Scriptable Object를 사용해서 구현

        _itemRandomSpawnProbability = Random.Range(0, 100);

        if (_itemRandomSpawnProbability < _itemSpawnProbability)
        {
            // 1. 전체 가중치를 더한다.
            int totalWeight = 0;
            foreach (ItemSpawnData data in _itemSpawnDataTable.Datas)
            {
                totalWeight += data.Weight;
            }

            // 2. 전체 가중치에서 랜덤 가중치를 뽑는다.
            int randomWeight = Random.Range(0, totalWeight);

            // 3. 가중치를 누적하면서 구간 찾기
            int cumulativeWeight = 0;
            foreach (ItemSpawnData data in _itemSpawnDataTable.Datas)
            {
                cumulativeWeight += data.Weight;
                if (cumulativeWeight > randomWeight)
                {
                    GameObject item = Instantiate(data.itemPrefab);
                    item.transform.position = transform.position;
                    Item itemComponent = item.GetComponent<Item>();
                    itemComponent.PlayItemAnimation();
                    break;
                }
            }
        }

        // if (_itemPrefabs == null || _itemPrefabs.Length == 0) return;
        //
        // _itemSpawnProbability = Random.Range(0.0f, 100.0f);
        //
        // if (_itemSpawnProbability >= 30.0f)
        // {
        //     return;
        // }
        // else if (_itemSpawnProbability < 30.0f)
        // {
        //     Debug.Log("아이템 생성");
        //     int itemIndex = Random.Range(0, _itemPrefabs.Length);
        //
        //     Item item = Instantiate(_itemPrefabs[itemIndex]);
        //     item.transform.position = transform.position;
        //     item.PlayItemAnimation();
        // }
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }
}