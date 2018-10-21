using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public ResourceType inputResource = ResourceType.Null;
    public ResourceType outputResource = ResourceType.Null;

    // number of input resources required and output resources produced
    public int inputNum = 0;
    public int outputNum = 0;

    NodeController nodeController;

    // stock of input/output resources
    public int inputStock = 0;
    public int outputStock = 0;
    public int inputMaxStock = 10;
    public int outputMaxStock = 10;

    public bool NODE_SET = false;
    public bool INPUT_MET = false;

    private void Start()
    {
        nodeController = FindObjectOfType<NodeController>();
    }

    // must be called to set up this node
    public void SetupNode (ResourceType input, ResourceType output, int inNum, int outNum, Vector3 pos)
    {
        inputResource = input;
        outputResource = output;
        inputNum = inNum;
        outputNum = outNum;
        NODE_SET = true;

        this.transform.position = pos;
    }

    public void Process()
    {
        if (NODE_SET == false)
        {
            Debug.LogError("Node.Process :: Node not yet setup");
            return;
        }
        if (inputStock < inputNum)
        {
            Debug.Log("Node.Process :: Not enough input resources this turn");
            return;
        }

        // if we get here, we have enough input resources to perform the process - check
        if (inputNum < inputStock)
        {
            Debug.LogError("Node.Process :: Trying to decrease inputStock below zero");
            return;
        }

        // Reduce the input resources stock and produce the output resource
        inputStock -= inputNum;
        outputStock += outputNum;

        // Finally limit the stock to output resource max:
        if (outputStock > outputMaxStock)
        {
            Debug.Log("Node.Process :: outputStock exceeded: " + (outputStock - outputMaxStock) + " units wasted");
            outputStock = outputMaxStock;
        }
    }

    public int OutputResourceFromNode (ResourceType requestedResource, int requestedAmount)
    {
        int outputAmount = 0;
        // check requested resource matches this supply
        if (requestedResource != outputResource)
            Debug.LogError("Node.OutputResource :: Incorrect input resource");

        if (outputStock <= 0)
            Debug.Log("Node.OutputResource :: Not enough resources in stock to satisfy request");

        if (outputStock < requestedAmount)
        {
            outputAmount = outputStock;
            outputStock = 0;
            Debug.Log("Node.InputResource :: Not enough resources to fully satisfy output request: Only " + outputAmount + " units outputted");
        }

        if (outputStock >= requestedAmount)
        {
            outputStock -= outputAmount;
            outputAmount = requestedAmount;
        }

        return outputAmount;
    }

    public int InputResourceToNode (ResourceType suppliedResource, int suppliedAmount)
    {
        // Returns the amount of the supplied resource that the node accepts
        int acceptedAmount = 0;
        // check supplied resource matches this input
        if (suppliedResource != inputResource)
        {
            Debug.LogError("Node.InputResource :: Incorrect input resource");
            // acceptedAmount should stay 0
        }
        if (suppliedAmount > inputMaxStock - inputStock)
        {
            acceptedAmount = (inputMaxStock - inputStock);
            Debug.LogError("Node.InputResource :: too many supplied resources, inputMaxStock would be exceeded: Only " + acceptedAmount + " units accepted");
        }
        else
            acceptedAmount = suppliedAmount;

        // increase the input stock
        inputStock += suppliedAmount;

        // return the accepted amount for the caller of the function to use
        return acceptedAmount;
    }

    //void Process ()
    //{
    //    if (NODE_SET == false)
    //    {
    //        Debug.LogError("Node.Process :: Node not yet setup");
    //        outNum = 0;
    //        return ResourceType.Null;
    //    }
    //    if (input != inputResource)
    //    {
    //        Debug.LogError("Node.Process :: Incorrect input resource");
    //        outNum = 0;
    //        return ResourceType.Null;
    //    }
    //    if (inNum < inputNum)
    //    {
    //        Debug.LogError("Node.Process :: Not enough input resources to perform process");
    //        outNum = 0;
    //        return ResourceType.Null;
    //    }
    //    outNum = outputNum;
    //    return outputResource;
    //}
}
