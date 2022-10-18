using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    public List<GameObject> enemys;
    private int maxEnemys = 30;
    public int EnemysSpawned { get; set; }

    private void Start()
    {
        EnemysSpawned = 0;
    }

    private void Update()
    {
        if (EnemysSpawned < maxEnemys) StartCoroutine(SpawnEnemys(Random.Range(0, enemys.Count)));
    }

    IEnumerator SpawnEnemys(int enemyNum)
    {
        EnemysSpawned++;

        yield return new WaitForSeconds(Random.Range(.1f, 3));
        Vector3 p = new Vector3(Random.Range(-42, 43), -2.2f, 10);
        Instantiate(enemys[enemyNum], p, Quaternion.identity);
    }
}
