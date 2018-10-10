using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour {

    private int consumptionRate = 0;
    private ResourceType consumedResource = ResourceType.Null;
    private int offerPrice = -1;
    private int stockConsumedProduct = 0;
    

    // Personal dictionary for the consumer to store the market data of producers
    Dictionary<Producer, float> producerMarket;

    // --------------------------------------------------------------------------------------------------------
    // Accessors
    // --------------------------------------------------------------------------------------------------------
    public int GetConsumptionRate()
    {
        return consumptionRate;
    }
    public void SetConsumptionRate(int value)
    {
        consumptionRate = value;
    }

    public ResourceType GetConsumedResource()
    {
        if (consumedResource == ResourceType.Null)
            Debug.LogError("Consumer.GetConsumedResource::ResourceType not set, returning ResourceType.Null");

        return consumedResource;
    }
    public void SetProducedResource(ResourceType value)
    {
        consumedResource = value;
    }

    public int GetOfferPrice()
    {
        return offerPrice;
    }
    public void SetOfferPrice(int value)
    {
        offerPrice = value;
    }

    public int GetStockConsumedProduct()
    {
        return stockConsumedProduct;
    }
    public void SetStockConsumedProduct(int value)
    {
        stockConsumedProduct = value;
    }


    // --------------------------------------------------------------------------------------------------------
    // Setup the instance
    // --------------------------------------------------------------------------------------------------------
    public void SetUpConsumer(ResourceType type, int rate)
    {
        consumedResource = type;
        consumptionRate = rate;
        producerMarket = new Dictionary<Producer, float>();
    }

    public void AddProducerToMarket (Producer prod)
    {
        // calculate a score for the producer based on the distance and the price
        float score = (this.transform.position - prod.transform.position).magnitude * prod.GetAcceptPrice();
        Debug.Log(score);
        Debug.Log(prod.GetProducedResource());
        producerMarket.Add(prod, score);
    }

    public void DisplayMarket ()
    {
        foreach (KeyValuePair<Producer,float> kvp in producerMarket)
            Debug.Log(kvp.Key + ": Score: " + kvp.Value);
    }
}
