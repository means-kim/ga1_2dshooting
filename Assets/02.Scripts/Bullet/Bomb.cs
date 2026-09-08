using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (transform.position.y < 2)
        {
            Vector2 direction = Vector2.up;
            transform.Translate(direction * _moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }

    public void PlayBombAnimation()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Fire");
        }
    }
}