using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _damage;

    protected void Update()
    {
        Move();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected abstract void Move();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
        if (player != null)
        {
            player.PlayerTakeDamage(_damage);

            Destroy(gameObject);
        }
        else if (player == null)
        {
            return;
        }
    }
}