using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    [SerializeField] protected float _moveSpeed;

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
}