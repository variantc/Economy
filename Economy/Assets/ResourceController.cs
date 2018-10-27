using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    public NodeController nodeController;
    public Resource resourcePrefab;
    public List<Resource> resourceList;
    float spawnRange = 3f;


    private void Start()
    {
        nodeController = FindObjectOfType<NodeController>();
    }

    // At the moment this is called upon pressing the ProcessNodes button
    public void UpdateResources()
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

    public void SpawnResourceFromNode(Node n)
    {
        // Instantiate resource and set type to output type, also set as child of resourceController transform
        Resource resource = Instantiate(resourcePrefab, n.transform.position, Quaternion.identity);
        resource.transform.SetParent(this.transform);
        resource.resourceType = n.outputResource;
        resourceList.Add(resource);
    }
}
