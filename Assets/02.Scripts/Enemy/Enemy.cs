using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 100;
    public float MoveSpeed;

    public GameObject EnemyPrefabs;

    public Transform LeftSpawnPoint;
    public Transform CenterSpawnPoint;
    public Transform RightSpawnPoint;

    private void Update()
    {
        Vector2 direction = Vector2.down;

        transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }

    // private void EnemySpawn()
    // {
    //     GameObject LeftEnemy = Instantiate(EnemyPrefabs);
    //     GameObject CenterEnemy = Instantiate(EnemyPrefabs);
    //     GameObject RightEnemy = Instantiate(EnemyPrefabs);
    //
    //     LeftEnemy.transform.position = LeftSpawnPoint.position;
    //     CenterEnemy.transform.position = CenterSpawnPoint.position;
    //     RightEnemy.transform.position = RightSpawnPoint.position;
    // }
}