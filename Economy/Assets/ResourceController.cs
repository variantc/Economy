using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public NodeController nodeController;
    public Resource resourcePrefab;
    public List<Resource> resourceList;
    float spawnRange = 3f;

    // TEMPORARY! - for displaying the trader stock
    //public Text debugText;
    //private void FixedUpdate()
    //{
    //    if (resourceList.ToArray().Length > 0)
    //        debugText.text = "Trader Stock: " + resourceList[0].traderStock.ToString();
    //}

    // At the moment this is called upon pressing the ProcessNodes button
    public void UpdateTraders()
    {
        foreach (Resource r in resourceList)
        {
            r.DetermineDestination();
            r.MoveTowardsNode();
        }
    }

    public void SpawnResource()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange), 0f);
        Resource newResource = Instantiate(resourcePrefab, spawnPos, Quaternion.identity);
        resourceList.Add(resourcePrefab);
        newResource.transform.SetParent(this.transform);
        newResource.transform.name = "Resource " + newResource.resourceType;
    }
}
