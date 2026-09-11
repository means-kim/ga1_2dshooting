// 데이터 클래스: 순수하게 데이터(값)를 보관하고 전달하기 위한 목적으로 만든 클래스

using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject EnemyPrefab;
    public int Weight;
}