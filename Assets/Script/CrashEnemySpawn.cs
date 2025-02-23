using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashEnemySpawn : MonoBehaviour
{
    public GameObject CrashEnemy;
    public float xMin = -10f;
    public float xMax = 10f;
    public float spawnInterval = 0.2f;
    public float destroyTime = 5f;
    public float randomX;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Awake()
    {
        CrashEnemy = Resources.Load("CrashEnemy") as GameObject;
    }

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        randomX = Random.Range(-100, 100);
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            activeEnemies.RemoveAll(enemy => enemy == null);

            if (activeEnemies.Count < 10)
            {
                Vector3 spawnPos = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
                GameObject enemy = Instantiate(CrashEnemy, spawnPos, transform.rotation);
                activeEnemies.Add(enemy);
                Destroy(enemy, destroyTime);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
