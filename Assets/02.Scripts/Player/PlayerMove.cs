using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMove : MonoBehaviour
{   
    // 목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.
    
    // 필요 필드:
    public float Speed;
    
    // 매 프레임마다 실행된다.
    // 초당 프레임 실행 횟수 : 별다른 설정이 없을 경우 가능한 많이 진행
    private void Update()
    {
        // 1. 키보드 입력을 받는다.
        float h = Input.GetAxis("Horizontal");  // 키보드 왼/오른쪽 입력 상태에 따라 -1f ~ 0 ~ 1f
        float v = Input.GetAxis("Vertical");    // 키보드 위/아래 입력 상태에 따라 -1f ~ 0 ~ 1f
        
        Debug.Log($"h: {h}, v: {v}");
        
        // if (Input.GetKey(KeyCode.LeftArrow))
        // {
        //     Debug.Log("왼쪽 방향키를 누르는 중");
        //     
        // 2. 키보드 입력에 따라 방향을 구한다.
        Vector2 direction = new Vector2(h, v); // 왼쪽 방향
        
        //     // 게임에는 벡터라는 타입이 있다. 벡터는(크기와 방향을 의미한다)
        //     Vector2 direction = new Vector2(-1, 0); // 왼쪽 방향
        //     // Vector2 direction = Vector2.left;
        //
        //     // 3. 방향과 속력에 따라 이동한다.
        //     // 속도 = 방향 * 속력                         
        //     // 매직 넘버란 : 보는 사람에 따라 의미가 달라질 수 있는 것
        //     // 헷갈리는 숫자. 여기서는 0.05f 가 매직넘버에 해당
        //     transform.Translate(direction * Speed * Time.deltaTime);
        //     // deltaTime: 이전 프레임으로부터 지금 프레임까지 시간이 얼마나 지났는지 m/s로 반환 (1000분의 1초)
        // }
        
        // 새로운 위치 = 현재 위치 + (방향 * 속력 * 시간)
        transform.position += (Vector3)direction * Speed * Time.deltaTime;
    }
}
