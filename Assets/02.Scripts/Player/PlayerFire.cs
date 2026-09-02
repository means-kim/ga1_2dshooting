using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFire : MonoBehaviour
{
    // 목표 : 스페이스 바를 누를 때마다 총알을 생성해서 발사하고 싶다.
    // 필요 속성
    // - 총알 프리팹
    public GameObject MainBulletPrefab;
    public GameObject SideBulletPrefab;
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
        if (Time.time >= LastFireCooldown + FireCooldown)
        {
            LastFireCooldown = Time.time;
                
            GameObject rightBullet = Instantiate(MainBulletPrefab);
            GameObject leftBullet = Instantiate(MainBulletPrefab);
            
            GameObject rightSideBullet = Instantiate(SideBulletPrefab);
            GameObject leftSideBullet = Instantiate(SideBulletPrefab);
            
            rightBullet.transform.position = RightFirePoint.position;
            leftBullet.transform.position =  LeftFirePoint.position;
            
            rightSideBullet.transform.position = RightFirePoint.position;
            leftSideBullet.transform.position =  LeftFirePoint.position;
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
