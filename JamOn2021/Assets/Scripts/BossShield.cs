using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    [SerializeField] GameObject rayPrefab;
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] Transform[] shieldPositions;

    List<int> usedIndexes = new List<int>();

    private void Start()
    {
        Spawn(3);
    }

    public void Spawn(int numEscudos)
    {
        double ang = 0;

        for (int i = 0; i < numEscudos; ++i)
        {
            int index;

            do
            {
                index = Random.Range(0, shieldPositions.Length);
            }
            while (usedIndexes.Contains(index));
            usedIndexes.Add(index);

            Instantiate(shieldPrefab, shieldPositions[index].position,Quaternion.identity);
        }

        usedIndexes.Clear();
    }

}
