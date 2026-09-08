using UnityEngine;

public class HomingEnemy : Enemy
{
    // 캐싱 : 자주 쓸법한 데이터(객체)를 가까운 곳에 저장해두고 쓰는 것
    private GameObject _player;
    private const float RotationOffset = 90.0f;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        if (_player == null)
        {
            Debug.Log("플레이어 태그를 가진 플레이어를 찾지 못했습니다.");
            return;
        }
    }

    protected override void Move()
    {
        if (_player == null) return;

        // 1. 방향을 구한다. (상대방 위치 - 내 위치)
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle + RotationOffset);

        // 2, 방향과 속도에 맞게 이동한다.
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }
}