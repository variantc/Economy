using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour {

    public Node nodePrefab;

    public List<Node> nodeList;

    float spawnRange = 3f;
    
    // Spawns a node in a random location within spawnRange
    public void SpawnNode (string nodeString)
    {
        ResourceType outputResource;
        ResourceType inputResource;

        switch (nodeString)
        {
            case "Wood":
                outputResource = ResourceType.Wood;
                inputResource = ResourceType.Null;
                break;
            case "Tool":
                outputResource = ResourceType.Tool;
                inputResource = ResourceType.Wood;
                break;
            case "Food":
                outputResource = ResourceType.Food;
                inputResource = ResourceType.Tool;
                break;
            default:
                outputResource = ResourceType.Null;
                inputResource = ResourceType.Null;
                break;
        }
        // default 1:1 processing - Update?
        int outputAmount = 1;
        int inputAmount = 1;

        // Implies Sink
        if (outputResource == ResourceType.Null)
            outputAmount = 0;
        // Implies Sources
        if (inputResource == ResourceType.Null)
            inputAmount = 0;

        //ResourceType resourceType = (ResourceType)Random.Range(0,4);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(-spawnRange, spawnRange),0f);
        Node newNode = Instantiate(nodePrefab);
        nodeList.Add(newNode);
        newNode.SetupNode(inputResource, outputResource, inputAmount, outputAmount, spawnPos);
        newNode.transform.SetParent(this.transform);
        newNode.transform.name = "Node:" + newNode.transform.position.x + "," + newNode.transform.position.y ;
    }

    // Tells each node to perform the processing
    public void ProcessNodes()
    {
        foreach (Node n in nodeList)
        {
            n.Process();
        }
    }
}
