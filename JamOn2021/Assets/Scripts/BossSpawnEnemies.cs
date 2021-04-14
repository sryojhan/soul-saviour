using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnEnemies : MonoBehaviour
{

    [SerializeField] GameObject[] spawnPoints;

    [SerializeField] GameObject[] enemies;

    [SerializeField] ContactFilter2D isBoss;

    private Vector2[] positions;
    private Collider2D[] colliders;

    List<int> usedIndexed = new List<int>();

    private void Start()
    {
        positions = new Vector2[spawnPoints.Length];
        colliders = new Collider2D[spawnPoints.Length];

        for (int i = 0; i < spawnPoints.Length; ++i)
        {
            positions[i] = spawnPoints[i].GetComponent<Transform>().position;
            colliders[i] = spawnPoints[i].GetComponent<Collider2D>();
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
            while (overlapsWithBoss[0] != null && usedIndexed.Contains(indexPosition));
            
            usedIndexed.Add(indexPosition);

            // Instantiate(enemies[indexEnemy], spawnPositions[indexPosition]);
        }
    }
}
