using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool _instance;
    public static BulletPool Instance => _instance;

    // 오브젝트 풀링이란? : 
    // 오브젝트의 Pool(운덩이: 창고)을 만들어 두고,
    // 그 창고 안에 게임 오브젝트를 미리 필요한 만큼 만들어두고,
    // 필요할 때 마다 꺼내서 사용하고 필요가 없으면 반환하는 식으로 (활성화/비활성화)
    // 메모리 할당과(객체의 생성) 해제(파괴)를 최소화해서 성능 Up!

    // 필요 속성
    [Header("총알 프리팹들")]
    [SerializeField] private Bullet[] _bulletPrefabs;

    [Header("풀 사이즈")]
    [SerializeField] private int _poolSize;

    // 생성한 총알을 담아둘 풀
    private Bullet[,] _pool;

    private void Awake()
    {
        // 늦게 생성된 매니저는 삭제 (1개만 존재하게끔)
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        // 창고를 창고 크기 만큼 만든다. (창교 = Pool)
        _pool = new Bullet[_bulletPrefabs.Length, _poolSize];

        // 총알 프리팹 종류와 창고 크기 만큼 총알을 미리 만들어서 집어 넣는다.
        for (int i = 0; i < _bulletPrefabs.Length; i++)
        {
            Bullet bulletPrefab = _bulletPrefabs[i];

            for (int j = 0; j < _poolSize; j++)
            {
                Bullet bullet =
                    Instantiate(bulletPrefab, gameObject.transform); // 두번째 인자로 gameObject.transform 을 적으면 하위 요소로 들어간다.
                bullet.gameObject.SetActive(false); // 당장 사용할거 아니기에 오브젝트 비활성화
                _pool[i, j] = bullet;
            }
        }
    }

    public Bullet GetBullet(BulletType bulletType)
    {
        for (int i = 0; i < _bulletPrefabs.Length; i++) // 타입별로 순회 하면서
        {
            if (_pool[i, 0].Type != bulletType) // 첫번째 요소의 타입이 내가 원하는게 아니라면
            {
                continue;
            }

            for (int j = 0; j < _poolSize; j++) // 원하는 타입의 배열 순회
            {
                Bullet bullet = _pool[i, j];

                // 비활성화 되어있는 (즉, 누가 빌려가지 않은 총알 반환)
                if (bullet.gameObject.activeSelf == false)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.OnSpawn();
                    return bullet;
                }
            }
        }

        return null;
    }
}