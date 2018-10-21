using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour {

    // Do we have resource-specific traders? - at the moment just try to go to the nearest node?

    public NodeController nodeController;

    public ResourceType carriedResource = ResourceType.Null;
    public void SetCarriedResource (ResourceType resource)
    {
        carriedResource = resource;
    }

    public float travelSpeed = 0.5f;
    public int traderStock = 0;
    public int traderStockMax = 10;

    public Node destinationNode = null;
    //public void SetDestinationNode(Node destination)
    //{
    //    destinationNode = destination;
    //}

    // Do I want to put this elsewhere? - maybe in TraderController?
    private void Start()
    {
        nodeController = FindObjectOfType<NodeController>();
    }

    // use SELLING bool as toggle to switch between collecting and dropping off carriedResource
    // false = collecting ; true = dropping off
    public bool SELLING = false;
    public bool ARRIVED = false;

    public void DetermineDestination ()
    {
        //// if have no resource, choose a random resource and go to the closest node which does not have an empty stock
        //if (SELLING == false)
        //{
        //    // distance to node variable, arbitrarily large
        //    float dist = 100f;
        //    ARRIVED = false;

        //    foreach (Node n in nodeController.nodeList)
        //    {
        //        float nodeDist = (this.transform.position - n.transform.position).magnitude;

        //        if (nodeDist < dist)
        //        {
        //            destinationNode = n;
        //            dist = nodeDist;
        //        }
        //    }

        //    if (destinationNode == null)
        //    {
        //        Debug.LogError("Trader.DetermineDestination :: algorithm failed; destinationNode not found");
        //    }
        //}
        //else
        //{
        float dist = 100f;
        ARRIVED = false;

        foreach (Node n in nodeController.nodeList)
        {
            if (n.inputResource == carriedResource)
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
            Debug.LogError("Trader.DetermineDestination :: no node available accepting ResourceType." + carriedResource);
            FindLargestStock();
            //SetRandomResource();
        }
        //}
    }

    public void MoveTowardsNode ()
    {
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
            // if there are no resources available to pick up here, pick another randomly
            if (destinationNode.outputStock == 0)
            {
                SetRandomResource();
                ARRIVED = false;
                SELLING = false;
            }

            // deliver and take from destinationNode
            if (carriedResource == destinationNode.inputResource)
            {
                int acceptedAmount = destinationNode.InputResourceToNode(carriedResource, this.traderStock);
                this.traderStock -= acceptedAmount;
            }
            else
            {
                Debug.LogError("Trader.MoveTowardsNode :: Some shit has gone wrong in this function to do with carried and destination input resources");
            }

            carriedResource = destinationNode.outputResource;
            traderStock += destinationNode.OutputResourceFromNode(carriedResource, traderStockMax - traderStock);
            SELLING = true;
            Debug.Log(carriedResource + ", units: " + traderStock);
        }
    }

    public void ReportDestination ()
    {
        Debug.Log(this.transform.name + " heading to: " + destinationNode.transform.position);
    }

    public void SetRandomResource ()
    {
        ResourceType resourceType = (ResourceType)Random.Range(0, 4);
        this.SetCarriedResource(resourceType);
    }

    public void FindLargestStock()
    {
        int highestStock = 0;
        foreach(Node n in nodeController.nodeList)
        {
            if (n.outputStock > highestStock)
                destinationNode = n;
        }
    }
}