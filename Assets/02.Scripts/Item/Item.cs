using UnityEngine;

public abstract class Item : MonoBehaviour
{
    // protected Vector2 _direction;
    [SerializeField] protected float _pickupCooldown = 3.0f;
    [SerializeField] protected float _moveSpeed = 1.0f;
    [SerializeField] private float _curveAmount = 0.3f;
    [SerializeField] private GameObject _pickupItemEffect;

    private float _currentTime = 0f;
    protected GameObject _player;
    private Vector2 _p0;
    private Vector2 _p1;
    private Vector2 _p2;
    private float _t;
    private bool _isBezierStarted;
    private Animator _animator;
    // private AudioSource _pickupAudioSource;

    [SerializeField] private ItemType _type;
    public ItemType Type => _type;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        // _pickupAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (_player == null) return;

        _currentTime += Time.deltaTime;

        if (_currentTime >= _pickupCooldown)
        {
            // Vector2 direction = _player.transform.position - transform.position;
            // direction.Normalize();
            //
            // transform.Translate(direction * _moveSpeed * Time.deltaTime);
            BezierMove();
        }
    }

    protected abstract void Pickup();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
            // _pickupAudioSource.Play();
            SpawnPickupEffect();
            Destroy(gameObject);
        }
    }

    private void BezierMove()
    {
        if (!_isBezierStarted)
        {
            _p0 = transform.position;
            _p2 = _player.transform.position;
            _p1 = (_p0 + _p2) / 2.0f + Vector2.right * _curveAmount;
            _isBezierStarted = true;
        }

        _p2 = _player.transform.position;

        _t += Time.deltaTime * _moveSpeed;
        _t = Mathf.Clamp01(_t);

        Vector2 start = Vector2.Lerp(_p0, _p1, _t);
        Vector2 end = Vector2.Lerp(_p1, _p2, _t);

        Vector2 position = Vector2.Lerp(start, end, _t);

        transform.position = position;
    }

    private void SpawnPickupEffect()
    {
        Instantiate(_pickupItemEffect, transform.position, Quaternion.identity);
    }

    public void PlayItemAnimation()
    {
        _animator.SetTrigger("IsCreate");
    }
}