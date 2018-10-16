using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manufacturer : MonoBehaviour {
    // This class needs to convert one resource type to another
    // Thus should have a Consumer (input) and Producer (output)

    TradeController tradeController;

    Consumer input;
    Producer output;

    private void Start()
    {
        // tradeController must already be instantiated
        tradeController = FindObjectOfType<TradeController>();

        input = Instantiate(tradeController.consumerPrefab, this.transform);
        output = Instantiate(tradeController.producerPrefab, this.transform);
    }

    public void SetUpManufacturer(ResourceType rawMaterial, ResourceType producedMaterial, int rawRequired, int producedAmount)
    {
        input.SetUpConsumer(rawMaterial, rawRequired);
        output.SetUpProducer(producedMaterial, producedAmount);
    }


    void Process ()
    {

    }
}
