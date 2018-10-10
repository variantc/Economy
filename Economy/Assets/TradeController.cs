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
        Consumer consumer = Instantiate(ConsumerPrefab, this.transform.position + new Vector3(1.5f, 3f, 0f), Quaternion.identity);
        consumer.transform.SetParent(this.transform);
        consumer.SetUpConsumer(ResourceType.Wood, 5);
        consumerList.Add(consumer);

        Producer producer = Instantiate(ProducerPrefab, this.transform.position + new Vector3(-3f, -2f, 0f), Quaternion.identity);
        producer.transform.SetParent(this.transform);
        producer.SetUpProducer(ResourceType.Wood, 10);
        producerList.Add(producer);

        producer = Instantiate(ProducerPrefab, this.transform.position + new Vector3(3f, -0.5f, 0f), Quaternion.identity);
        producer.transform.SetParent(this.transform);
        producer.SetUpProducer(ResourceType.Wood, 10);
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
                    Debug.Log("Matched resource type");
                    c.AddProducerToMarket(p);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            GenerateConsumerMarkets();
        if (Input.GetKeyDown(KeyCode.D))
        {
            foreach (Consumer c in consumerList)
                c.DisplayMarket();
        }
    }
}
