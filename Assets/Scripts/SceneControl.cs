using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    public List<GameObject> enemys;
    private int maxEnemys = 30;
    [SerializeField] private int enemySpawned;

    private void Start()
    {
        enemySpawned = 0;
    }

    private void Update()
    {
        if (enemySpawned < maxEnemys) StartCoroutine(SpawnEnemys(Random.Range(0, enemys.Count)));
    }

    IEnumerator SpawnEnemys(int enemyNum)
    {
        enemySpawned++;

        yield return new WaitForSeconds(Random.Range(.1f, 3));
        Vector3 p = new Vector3(Random.Range(-42, 43), -2.2f, 10);
        Instantiate(enemys[enemyNum], p, Quaternion.identity);
    }

    public void EnemySpawned(int value)
    {
        enemySpawned -= value;
    }
}
