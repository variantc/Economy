using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : MonoBehaviour {

    private int productionRate = -1;
    private ResourceType producedResource = ResourceType.Null;
    private float acceptPrice = -1;
    private int producerStock = 0;
    private float wealth = 0;
    private int maxStock = 100;

    // --------------------------------------------------------------------------------------------------------
    // Accessors
    // --------------------------------------------------------------------------------------------------------
    // Rate at which resource is produced
    public int GetProductionRate() {
        if (productionRate < 0)
        {
            Debug.LogError("Producer.GetProductionRate: ProductionRate not set, returning -1");
            productionRate = 0;
        }
        return productionRate;
    }
    public void SetProductionRate(int value) {
        if (value < 0)
            Debug.LogError("Producer.SetProductionRate: Invalid production rate as argument, assigning 0");
        else
            productionRate = value;
    }

    // Type of resource produced
    public ResourceType GetProducedResource()
    {
        if (producedResource == ResourceType.Null)
            Debug.LogError("Producer.GetProducedResource::ResourceType not set, returning ResourceType.Null");

        return producedResource;
    }
    public void SetProducedResource(ResourceType value) { producedResource = value; }

    // Accepted price
    public float GetAcceptPrice() {
        if (productionRate < 0)
            Debug.LogError("Producer.GetAcceptPrice: acceptPrice not set, returning -1");

        return acceptPrice;
    }
    public void SetAcceptPrice(float value)
    {
        if (value < 0)
            Debug.LogError("Producer.SetProductionRate: Invalid production rate as argument, assigning 0");
        else
            acceptPrice = value;
    }

    // Stock of resource available - Do I want a function to increase/decrease stock?
    public int GetStockProducedProduct() { return producerStock; }
    public void SetStockProducedProduct(int value) {
        if (value < 0)
        {
            Debug.LogError("Producer.SetProductionRate: Invalid stock target as argument, assigning 0");
            producerStock = 0;
        }
        else
            producerStock = value;
    }

    // Current wealth
    public float GetWealth() { return wealth; }
    public void SetWealth(float value) { wealth = value; }

    // Max stock
    public int GetMaxStock() { return maxStock; }
    public void GetMaxStock(int value) { maxStock = value; }

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

        acceptPrice = 10.0f * productionRate / producerStock;
    }

    // take an amount of the resource out of stock - returns that amount
    public int SellResource(int amount, ResourceType requested, out float cost)
    {
        // Check resource type hase been set
        if (this.producedResource == ResourceType.Null)
            Debug.LogError("ResourceType not set for: " + this.transform.name);

        // check here that resource types match
        if (requested == this.producedResource)
        {
            if (amount >= producerStock)
                producerStock -= amount;
            else
            {
                amount = producerStock;
                producerStock = 0;
            }
        }
        else
            amount = 0;

        // TODO: How to 'take' money from consumer?
        cost = amount * acceptPrice;
        wealth += cost;
        
        return amount;
    }

    // If sale fails, refund amount
    public void Refund(float refundAmount)
    {
        wealth -= refundAmount;
    }

    // update the stock of the producer
    public void UpdateStock ()
    {
        producerStock += productionRate;
        UpdateAcceptPrice();
    }
}
