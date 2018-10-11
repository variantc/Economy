using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumer : MonoBehaviour {

    private int consumptionRate = 0;
    private ResourceType consumedResource = ResourceType.Null;
    private float offerPrice = -1;
    private int stockConsumedProduct = 0;
    

    // Personal dictionary for the consumer to store the market data of producers
    Dictionary<Producer, float> producerMarket;

    // --------------------------------------------------------------------------------------------------------
    // Accessors
    // --------------------------------------------------------------------------------------------------------
    // Rate at which the resource is consumed
    public int GetConsumptionRate()
    {
        return consumptionRate;
    }
    public void SetConsumptionRate(int value)
    {
        consumptionRate = value;
    }

    // Type of resource consumed
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

    // Price willing to pay
    public float GetOfferPrice()
    {
        return offerPrice;
    }
    public void SetOfferPrice(float value)
    {
        offerPrice = value;
    }

    // Stock of consumed resource
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

    // The 'Market' is the store of all relevant (producing the desired resource) producers
    public void AddProducerToMarket (Producer prod)
    {
        // Check producer prod supplies the correct resource
        if (prod.GetProducedResource() != this.consumedResource)
        {
            Debug.LogError("Trying to add producer of wrong type to market: " + prod.transform.name);
            return;
        }

        // calculate a score for the producer based on the distance and the price
        // FIXME: improve score based on average range and price ratios
        float score = (this.transform.position - prod.transform.position).magnitude * prod.GetAcceptPrice();
        producerMarket.Add(prod, score);
    }

    //public void CalculateMarketScores()
    //{
    //    foreach (KeyValuePair<Producer, float> kvp in producerMarket)
    //    {
    //        float score = (this.transform.position - kvp.Key.transform.position).magnitude * kvp.Key.GetAcceptPrice();
            
    //    }
    //}

    public void DisplayMarket ()
    {
        foreach (KeyValuePair<Producer,float> kvp in producerMarket)
            Debug.Log(kvp.Key + ": Score: " + kvp.Value);
    }
}
