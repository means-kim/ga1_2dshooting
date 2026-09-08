using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField] private Transform _bombFirePoint;
    [SerializeField] private float _bombCooldown = 10f;
    [SerializeField] private GameObject _bombPrefab;
    private float _currentTime = 0f;
    private float _bombDuration = 3f;

    private void Start()
    {
        _currentTime = _bombCooldown;
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.B))
        {
            BombFire();
        }
    }

    private void BombFire()
    {
        if (_currentTime < _bombCooldown) return;

        Debug.Log($"폭탄 생성");
        GameObject bomb = Instantiate(_bombPrefab);

        bomb.transform.position = _bombFirePoint.position;

        Destroy(bomb, _bombDuration);

        _currentTime = 0;
    }
}