using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manufacturer : MonoBehaviour {
    // This class needs to convert one resource type to another
    // Thus should have a Consumer (input) and Producer (output)

    public Consumer consumerPrefab;
    public Producer producerPrefab;

    Consumer input;
    Producer output;

    private void Start()
    {
        input = Instantiate(consumerPrefab, this.transform);
        output = Instantiate(producerPrefab, this.transform);
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
