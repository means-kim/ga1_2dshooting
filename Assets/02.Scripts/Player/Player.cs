using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health = 100;

    public void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}