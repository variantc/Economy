using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderController : MonoBehaviour {

    public Trader traderPrefab;
    public List<Trader> traderList;
    float spawnRange = 3f;

    public void UpdateTraders ()
    {
        foreach (Trader t in traderList)
        {
            t.DetermineDestination();
            t.ReportDestination();
            t.MoveTowardsNode();
        }
    }

    public void SpawnTrader ()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0f);
        Trader newTrader = Instantiate(traderPrefab,spawnPos,Quaternion.identity);
        traderList.Add(newTrader);
        newTrader.transform.SetParent(this.transform);
        newTrader.transform.name = "Trader " + (traderList.ToArray().Length - 1);
        // setting each trader to have a random resource type which it head towards first
        newTrader.SetRandomResource();
    }
}
