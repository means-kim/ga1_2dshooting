using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMove : MonoBehaviour
{   
    // 목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.
    
    // 필요 필드:
    public float Speed;
    public float MaxSpeed = 10f;
    public float MinSpeed = 1f;

    public float MinX = -2.8f;
    public float MaxX = 2.8f;
    public float MinY = -4.7f;
    public float MaxY = 0f;
    
    // 매 프레임마다 실행된다.
    // 초당 프레임 실행 횟수 : 별다른 설정이 없을 경우 가능한 많이 진행
    private void Update()
    {
        // 1. 키보드 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal");  // 키보드 왼/오른쪽 입력 상태에 따라 -1f ~ 0 ~ 1f
        float v = Input.GetAxisRaw("Vertical");    // 키보드 위/아래 입력 상태에 따라 -1f ~ 0 ~ 1f
        
        Debug.Log($"h: {h}, v: {v}");
 
        // 2. 키보드 입력에 따라 방향을 구한다.
        Vector2 direction = new Vector2(h, v); // 왼쪽 방향
        
        // 3. 방향과 속력에 따라 이동한다.
        Vector2 normalizedSpeed = (direction * Speed).normalized; // 벡터의 길이를 1로 만들어주는 것 (즉, 방향만 유지한다.)
        // 새로운 위치 = 현재 위치 + (방향 * 속력 * 시간)
        transform.Translate(normalizedSpeed * Speed * Time.deltaTime);
        
        // 4. 플레이어 이동 영역을 제한한다. (실습과제 1)
        // float posX = Mathf.Clamp(transform.position.x, MinX, MaxX); // X 축 화면 고정
        Vector2 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, MinY, MaxY);
        transform.position = new Vector2(pos.x, pos.y);
        
        // 5. 화면 반대편에서 나오게 하기   (실습과제 2)
        if (pos.x > MaxX)
        {
            pos.x = MinX;
        }
        else if (pos.x < MinX)
        {
            pos.x = MaxX;
        }
        transform.position = pos;
        
        // 6. 키보드 E키를 누르면 속도 업, Q키를 누르면 속도 다운
        if (Input.GetKey(KeyCode.E))
        {
            if (Speed < MaxSpeed)
            {
                Speed += 0.1f;
            }
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            if (Speed > MinSpeed)
            {
                Speed -= 0.1f;
            }
        }
    }
}
