using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : MonoBehaviour {

    private int productionRate = 0;
    private ResourceType producedResource = ResourceType.Null;
    private int acceptPrice = -1;
    private int stockProducedProduct = 0;

    // --------------------------------------------------------------------------------------------------------
    // Accessors
    // --------------------------------------------------------------------------------------------------------
    public int GetProductionRate()
    {
        return productionRate;
    }
    public void SetProductionRate(int value)
    {
        productionRate = value;
    }

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

    public int GetAcceptPrice()
    {
        return acceptPrice;
    }
    public void SetAcceptPrice(int value)
    {
        acceptPrice = value;
    }

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
}
