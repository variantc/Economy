using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : MonoBehaviour {

    private int productionRate = -1;
    private ResourceType producedResource = ResourceType.Null;
    private float acceptPrice = -1;
    private int stockProducedProduct = 0;

    // --------------------------------------------------------------------------------------------------------
    // Accessors
    // --------------------------------------------------------------------------------------------------------
    // Rate at which resource is produced
    public int GetProductionRate()
    {
        return productionRate;
    }
    public void SetProductionRate(int value)
    {
        productionRate = value;
    }

    // Type of resource produced
    public ResourceType GetProducedResource()
    {
        if (producedResource == ResourceType.Null)
            Debug.LogError("Producer.GetProducedResource::ResourceType not set, returning ResourceType.Null");

        return producedResource;
    }
    public void SetProducedResource(ResourceType value)
    {
        producedResource = value;
    }

    // Accepted price
    public float GetAcceptPrice()
    {
        return acceptPrice;
    }
    public void SetAcceptPrice(float value)
    {
        acceptPrice = value;
    }

    // Stock of resource available
    public int GetStockProducedProduct()
    {
        return stockProducedProduct;
    }
    public void SetStockProducedProduct(int value)
    {
        stockProducedProduct = value;
    }

    // --------------------------------------------------------------------------------------------------------
    // Setup the instance
    // --------------------------------------------------------------------------------------------------------
    public void SetUpProducer (ResourceType type, int rate)
    {
        producedResource = type;
        productionRate = rate;
    }

    // (Re)calculate the price this producer accepts
    void UpdateAcceptPrice()
    {
        // Check that production rate has been set
        if (this.productionRate < 0)
            Debug.LogError("Production rate not set for: " + this.transform.name + ".  Has it been set up?");

        acceptPrice = 10.0f * stockProducedProduct / productionRate;
    }

    // take an amount of the resource out of stock
    public int SellResource(int amount, ResourceType requested)
    {
        // Check resource type hase been set
        if (this.producedResource == ResourceType.Null)
            Debug.LogError("ResourceType not set for: " + this.transform.name);

        // check here that resource types match
        if (requested == this.producedResource)
        {
            if (amount >= stockProducedProduct)
                stockProducedProduct -= amount;
            else
            {
                amount = stockProducedProduct;
                stockProducedProduct = 0;
            }
        }
        else
            amount = 0;

        return amount;
    }

    // update the stock of the producer
    public void UpdateStock ()
    {
        stockProducedProduct += productionRate;
        UpdateAcceptPrice();
    }
}
