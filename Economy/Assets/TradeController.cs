using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeController : MonoBehaviour {

    public Consumer ConsumerPrefab;
    public Producer ProducerPrefab;

    public List<Consumer> consumerList;
    public List<Producer> producerList;

    // Use this for initialization
    void Start ()
    {
        Consumer consumer = Instantiate(ConsumerPrefab, this.transform.position + new Vector3(0f, 0f, 0f), Quaternion.identity);
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
        Producer producer = Instantiate(ProducerPrefab, this.transform.position + spawnPos, Quaternion.identity);
        producer.transform.SetParent(this.transform);
        producer.SetUpProducer(ResourceType.Wood, 10);
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

        // Display the marker information
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
    }

    void UpdateTurn()
    {
        // Update the stock (and price) of each producer
        foreach (Producer p in producerList)
        {
            p.UpdateStock();
        }
        // Update the markets of the consumers
        foreach (Consumer c in consumerList)
        {
        }
    }
}
