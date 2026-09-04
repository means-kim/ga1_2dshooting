using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected Vector2 _direction;
    [SerializeField] protected float _pickupColldown = 3.0f;
    [SerializeField] protected float _moveSpeed = 5.0f;

    private float _currentTime = 0f;
    protected GameObject _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (_player == null) return;

        _currentTime += Time.deltaTime;

        if (_currentTime >= _pickupColldown)
        {
            Vector2 direction = _player.transform.position - transform.position;
            direction.Normalize();

            transform.Translate(direction * _moveSpeed * Time.deltaTime);
        }
    }

    protected abstract void Pickup();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
            Destroy(gameObject);
        }
    }
}