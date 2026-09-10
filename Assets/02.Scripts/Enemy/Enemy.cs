using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _damage;
    [SerializeField] private Item[] _itemPrefabs;
    private float _itemSpawnProbability;

    private Animator _animator;

    // ToDo: 적이 공격 당할 때 재생시켜주는 피격 사운드
    private AudioSource _damagedAudioSource;

    // - 죽을 때 생성할 이펙트 프리팹
    [SerializeField] private GameObject _deathEffectPrefab;

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

            ScoreManager scoreManager = GameObject.FindAnyObjectByType<ScoreManager>();
            scoreManager.AddScore(100);

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
        if (_itemPrefabs == null || _itemPrefabs.Length == 0) return;

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
            item.PlayItemAnimation();
        }
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }
}