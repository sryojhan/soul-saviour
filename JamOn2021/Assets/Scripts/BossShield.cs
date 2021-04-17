using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    [SerializeField] GameObject rayPrefab;
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] Transform[] shieldPositions;

    [SerializeField] BossBattle battle;

    List<int> usedIndexes = new List<int>();

    int numShields = 0;

    public void Spawn(int numEscudos)
    {
        numShields = numEscudos;
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

            GameObject shield = Instantiate(shieldPrefab, shieldPositions[index].position, Quaternion.identity);

            GameObject ray = Instantiate(rayPrefab, shieldPositions[index].position, Quaternion.identity);

            float xScale = (shield.transform.position - transform.position).magnitude;
            Vector3 dir = (shield.transform.position - transform.position).normalized;

            Vector3 changeScale = new Vector3(xScale - transform.localScale.x / 2, -0.9f, 0);
            ray.transform.localScale += changeScale;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            ray.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            ray.transform.position = transform.position + dir * (xScale / 2);

            shield.GetComponent<ShieldBehaviour>().setRay(ray);
            shield.GetComponent<ShieldBehaviour>().setBossShield(this);
        }

        usedIndexes.Clear();
    }

    public void DestroyShield()
    {
        numShields--;
        if (numShields <= 0)
        {
            battle.StopAttack();
            battle.StopShield();
        }
    }

}
