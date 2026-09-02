using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMove : MonoBehaviour
{   
    // 목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고 싶다.
    
    // 필요 필드:
    public float Speed;

    public float MinX = -2.4f;
    public float MaxX = 2.4f;
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
        transform.Translate(normalizedSpeed * Time.deltaTime);
        
        // 4. 플레이어 이동 영역을 제한한다.
        float posX = Mathf.Clamp(transform.position.x, MinX, MaxX);
        float posY = Mathf.Clamp(transform.position.y, MinY, MaxY);
        
        transform.position = new Vector2(posX, posY);
    }
}
