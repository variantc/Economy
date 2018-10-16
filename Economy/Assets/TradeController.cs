using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeController : MonoBehaviour {

    public Consumer consumerPrefab;
    public Producer producerPrefab;

    public List<Consumer> consumerList;
    public List<Producer> producerList;

    // Use this for initialization 
    void Start ()
    {
        Consumer consumer = Instantiate(consumerPrefab, this.transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
        consumer.transform.SetParent(this.transform);
        consumer.SetUpConsumer(ResourceType.Wood, 5);
        consumerList.Add(consumer);

        // Spawn producers to form consumer's market
        // Temp: Hard coded locations
        SpawnProducer(new Vector3(-3f, -2f, 0f));
        SpawnProducer(new Vector3(3f, -0.5f, 0f));
        SpawnProducer(new Vector3(1f, 2.5f, 0f));

        // Fill the 'Market' for the consumer object, this means cycling through all producers and adding the consumers
        // to their markets
        GenerateConsumerMarkets();
    }
    
    // Spawn producer at spawnPos location
    // For now, have wood, with a spawn rate of '10'
    void SpawnProducer (Vector3 spawnPos)
    {
        Producer producer = Instantiate(producerPrefab, this.transform.position + spawnPos, Quaternion.identity);
        producer.transform.SetParent(this.transform);
        producer.SetUpProducer(ResourceType.Wood, 1);
        producer.transform.name = "Producer[" + producer.GetProducedResource() +
                                    ",(" + producer.transform.position.x + "," +
                                    producer.transform.position.y + ")]";
        producerList.Add(producer);
    }

    // cycles through all consumers which cycles through each producer, calculating a score and
    // adds to the consumer's market
    void GenerateConsumerMarkets()
    {
        foreach (Consumer c in consumerList)
        {
            // Zero each consumer's market
            c.ClearMarket();
            foreach (Producer p in producerList)
            {
                // test that a matching produced resource
                if (p.GetProducedResource() == c.GetConsumedResource())
                {
                    //Debug.Log("Matched resource type");
                    c.AddProducerToMarket(p);
                }
            }
        }
    }

    private void Update()
    {
        // Inputs
        
        // Display the market information
        if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (Consumer c in consumerList)
                c.DisplayMarket();
        }

        // Next Turn
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateTurn();
        }

        // Choose Producer
        if (Input.GetKeyDown(KeyCode.P))
        {
            PerformTransactions();
        }
    }

    private void PerformTransactions()
    {
        // Choose the producer for each consumer
        foreach (Consumer c in consumerList)
        {
            // Get chosen producer
            Producer buyFrom = c.ChooseProducer();
            // Calculate the amount to buy from the available money of consumer divided by price
            //int amountToBuy = (int)(c.GetWealth() / buyFrom.GetAcceptPrice());
            int amountToBuy = 1;

            // get the total cost (should be less than wealth of consumer) and the amount sold (should be <= amount
            // available from the producer
            float cost;
            int amountSold = c.ChooseProducer().SellResource(amountToBuy, c.GetConsumedResource(), out cost);

            if (cost > c.GetWealth())
            {
                Debug.LogError("Not enough money in consumer - Check TradeController.PerformTransactions");
                buyFrom.Refund(cost);
                return;
            }

            // Decrease wealth and increase stock of consumer
            c.SetWealth(c.GetWealth() - cost);
            c.SetStockConsumedProduct(c.GetStockConsumedProduct() + amountToBuy);
        }
    }

    void UpdateTurn()
    {
        // Update the stock (and price) of each producer
        foreach (Producer p in producerList)
        {
            p.UpdateStock();
        }
        // Regenerate the markets of the consumers
        GenerateConsumerMarkets();
    }
}
