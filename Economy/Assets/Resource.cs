using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType { Null = 0, Food = 1, Wood = 2, Tool = 3 }

public class Resource : MonoBehaviour {
    public ResourceType resourceType;
    
    public ResourceController resourceController;

    public float travelSpeed = 0.25f;

    public Node destinationNode = null;

    public bool ARRIVED = false;

    private void Start()
    {
        resourceController = FindObjectOfType<ResourceController>();
    }

    public void DetermineDestination()
    {
        // if have no resource, choose a random resource and go to the closest node which does not have an empty stock
        // start with arbitrarily large distance to calculate closest
        float dist = 100f;
        ARRIVED = false;

        if (resourceController.nodeController.nodeList == null)
            Debug.LogError("what??");
        else
            Debug.LogError("huh?");

        if (resourceController.nodeController.nodeList.ToArray().Length <= 0)
        {
            Debug.LogError("Resource.DetermineDestination :: nodeList has no entries yet");
            return;
        }
        foreach (Node n in resourceController.nodeController.nodeList)
        {
            if (n.inputResource == resourceType)
            {
                float nodeDist = (this.transform.position - n.transform.position).magnitude;

                if (nodeDist < dist)
                {
                    destinationNode = n;
                    dist = nodeDist;
                }
            }
        }

        if (destinationNode == null)
        {
            Debug.LogError("Resource.DetermineDestination :: no node available accepting ResourceType." + resourceType);
            FindLargestStock();
            //SetRandomResource();
        }

        Debug.Log("Resourse.DetermineDestination :: Chosen destination node: " + destinationNode);
    }

    public void MoveTowardsNode()
    {
        Debug.Log(destinationNode);
        Vector3 dirVector = (destinationNode.transform.position - this.transform.position);
        // copy the direction vector and then normalise
        Vector3 dirVectorUnit = dirVector;
        dirVectorUnit.Normalize();

        // check trader doesn't overshoot the destinationNode
        if ((travelSpeed * dirVectorUnit).magnitude >= dirVector.magnitude)
            this.transform.position = destinationNode.transform.position;
        else
            this.transform.position += travelSpeed * dirVectorUnit;

        if (this.transform.position == destinationNode.transform.position)
            ARRIVED = true;

        if (ARRIVED == true)
        {
            Debug.Log(destinationNode.inputResource + " " + resourceType);

            // We need to check that the node 'wants' this resource
            // then destroy AND remove from resourceController's resourceList
            if (resourceType == destinationNode.inputResource)
            {
                int acceptedAmount = destinationNode.InputResourceToNode(resourceType, 1);
                resourceController.resourceList.Remove(this);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogError("Resource.MoveTowardsNode :: Something has gone wrong in this function to do with carried and destination input resources");
            }
        }
    }

    public void FindLargestStock()
    {
        int highestStock = 0;
        foreach (Node n in resourceController.nodeController.nodeList)
        {
            if (n.outputStock > highestStock)
                destinationNode = n;
        }
    }
}
