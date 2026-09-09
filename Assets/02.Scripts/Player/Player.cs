using CartoonFX;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 캡슐화
    // - 데이터 은닉
    // - 메서드를 통한 상태 변경
    [SerializeField] private float _health = 100;
    [SerializeField] private GameObject _deathEffectPrefab;

    private AudioSource _playerHitAudioSource;

    public float Health => _health; // 람다식 문법을 활용한 읽기 전용 프로퍼티

    // public float Health
    // {
    //     get { return _health; }
    // }

    // 잘 설계된 클래스는
    // - 필드 (인스턴스 변수)
    // - 필드에 잘못된 값이 할당되지 않게 막고, 정상적으로 동작하는 메서드

    // getter/setter : 특정 데이터를 get/set 해주는 메서드
    // public float GetHealth()
    // {
    //     return _health;
    // }

    private void Awake()
    {
        _playerHitAudioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _playerHitAudioSource.Play();
        if (_health <= 0)
        {
            SpawnDeathEffect();

            Destroy(gameObject);
        }
    }

    public void Heal(float heal)
    {
        _health += heal;
    }

    private void SpawnDeathEffect()
    {
        if (_deathEffectPrefab == null) return;

        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }
}