using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour {

    public Node nodePrefab;

    public List<Node> nodeList;
    
    public void SpawnNode ()
    {
        ResourceType randomResource = (ResourceType)Random.Range(0,4);
        Vector3 spawnPos = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f),0f);
        nodeList.Add(Instantiate(nodePrefab));
        nodeList[nodeList.ToArray().Length - 1].SetupNode(ResourceType.Null, randomResource, 0, 1, spawnPos);
    }

    public void ProcessNodes()
    {
        foreach (Node n in nodeList)
        {
            n.Process();
        }
    }
}
