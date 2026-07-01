using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;

    Transform ChooseSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }

    public GameObject SpawnEnemy()
    {
        Transform spawnPoint = ChooseSpawnPoint();

        int randomIndex = Random.Range(0, enemyPrefab.Length);

        GameObject enemyObject = Instantiate(enemyPrefab[randomIndex], spawnPoint.position, spawnPoint.rotation);

        return enemyObject;
    }
}
