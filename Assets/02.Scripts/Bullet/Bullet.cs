using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 목적 : 총알을 위로 움직이고 싶다.

    public float MoveSpeed;

    private void Update()
    {
        // Vector2 direction = new Vector2(1, 0);
        Vector2 direction = Vector2.up;

        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }

    // 충돌 관련 이벤트 (Enter -> Stay -> Exit)

    // 충돌이 시작되면 호출되는 이벤트 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌 했다!");

        // 나 죽고!
        Destroy(this.gameObject);

        // 충돌한 친구가 Enemy일때만 죽이자!
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 너 죽자!
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Debug.Log("충돌 중이다!");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Debug.Log("충돌 끝!");
    }
}