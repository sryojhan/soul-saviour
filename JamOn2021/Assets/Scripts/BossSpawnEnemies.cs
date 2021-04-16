using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnEnemies : MonoBehaviour
{

    [SerializeField] GameObject[] spawnPoints = { null };

    [SerializeField] GameObject[] enemies;

    [SerializeField] ContactFilter2D isBoss;

    [SerializeField] BossBattle battle;

    private Vector2[] positions;
    private Collider2D[] colliders;

    List<int> usedIndexes = new List<int>();

    private void Awake()
    {
        if (spawnPoints[0] != null)
        {
            positions = new Vector2[spawnPoints.Length];
            colliders = new Collider2D[spawnPoints.Length];

            for (int i = 0; i < spawnPoints.Length; ++i)
            {
                positions[i] = spawnPoints[i].GetComponent<Transform>().position;
                colliders[i] = spawnPoints[i].GetComponent<Collider2D>();
            }
        }
    }


    public void Spawn(int numEnemies)
    {
        for (int i = 0; i < numEnemies; ++i)
        {
            int indexEnemy = Random.Range(0, enemies.Length);
            int indexPosition;

            Collider2D[] overlapsWithBoss = { null };

            do
            {
                indexPosition = Random.Range(0, positions.Length);
                colliders[indexPosition].OverlapCollider(isBoss, overlapsWithBoss);
            }
            while (overlapsWithBoss[0] != null || usedIndexes.Contains(indexPosition));

            usedIndexes.Add(indexPosition);

            Instantiate(enemies[indexEnemy], new Vector3(positions[indexPosition].x, positions[indexPosition].y, 0), Quaternion.identity);
        }
        StartCoroutine(delayedStopAttack());
    }
    IEnumerator delayedStopAttack()
    {
        yield return new WaitForSeconds(3);
        usedIndexes.Clear();
        battle.StopAttack();
        StopCoroutine(delayedStopAttack());
    }

}
