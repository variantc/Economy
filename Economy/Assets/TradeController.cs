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
        Consumer consumer = Instantiate(ConsumerPrefab, this.transform);
        consumer.SetUpConsumer(ResourceType.Wood, 5);
        consumerList.Add(consumer);

        Producer producer = Instantiate(ProducerPrefab, this.transform);
        producer.SetUpProducer(ResourceType.Wood, 10);
        producerList.Add(producer);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
