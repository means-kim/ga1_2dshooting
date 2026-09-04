using System;
using UnityEngine;
using Random = UnityEngine.Random;

// 역할 : 일정 시간마다 적을 생성해주고 싶다.
public class EnemySpawner : MonoBehaviour
{
    enum EnemyType
    {
        Normal,
        Aiming,
        Homing
    }

    // 필요 속성
    // - 타이머
    [SerializeField] private float _spawnInterval = 3f;
    private float _timer;
    private float _spawnProbability;

    // - 생설할 프리팹
    [SerializeField] private Enemy[] _enemyPrefab;
    // [SerializeField] private Enemy _aimingEnemyPrefab;
    // [SerializeField] private Enemy _homingEnemyPrefab;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0;

            _spawnInterval = UnityEngine.Random.Range(1f, 3f); // float 1 ~ 3
            // int randomInt = Random.Range(1, 3); // int : 1 ~ 2

            Spawn();
        }
    }

    private void Spawn()
    {
        _spawnProbability = UnityEngine.Random.Range(1f, 10f);

        if (_spawnProbability < 2f)
        {
            Enemy enemy = Instantiate(_enemyPrefab[(int)EnemyType.Homing]);
            enemy.transform.position = transform.position;
        }
        else if (_spawnProbability < 5f)
        {
            Enemy enemy = Instantiate(_enemyPrefab[(int)EnemyType.Aiming]);
            enemy.transform.position = transform.position;
        }
        else
        {
            Enemy enemy = Instantiate(_enemyPrefab[(int)EnemyType.Normal]);
            enemy.transform.position = transform.position;
        }
    }
}