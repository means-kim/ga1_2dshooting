using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표 : 스페이스 바를 누를 때마다 총알을 생성해서 발사하고 싶다.
    // 필요 속성
    // - 총알 프리팹
    public GameObject BulletPrefab;
    // - 생성 위치(총구)
    public Transform RightFirePoint;
    public Transform LeftFirePoint;

    public float FireCooldown = 0.5f;
    private float LastFireCooldown = 0f;
    
    private bool isNumber1KeyPressed = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isNumber1KeyPressed = !isNumber1KeyPressed;
        }

        if (!isNumber1KeyPressed)
        {
            SpaceBulletFire();
        }
        else if (isNumber1KeyPressed)
        {
            AutoBulletFire();
        }
    }

    private void BulletFire()
    {
        // 2. 총알 프리팹을 생성한다.
        // Instantiate는 프리팹을 복사해서 (MonoBehaviour를 상속받는)게임 오브젝트를 만들고 씬에 넣어주는 기능

        if (Time.time >= LastFireCooldown + FireCooldown)
        {
            LastFireCooldown = Time.time;
                
            GameObject rightBullet = Instantiate(BulletPrefab);
            GameObject leftBullet = Instantiate(BulletPrefab);
            
            rightBullet.transform.position = RightFirePoint.position;    // 생성한 총알의 위치를 나(플레이어)의 위치로
            leftBullet.transform.position =  LeftFirePoint.position;
        }
    }

    private void SpaceBulletFire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BulletFire();
        }
    }
    
    private void AutoBulletFire()
    {
        BulletFire();
    }
}
