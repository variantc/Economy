using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JobType { Null, Farm, Chop, Smith }

public class Job : MonoBehaviour {

    public Body ownerBody;

    public ResourceType consumedResource;
    public ResourceType producedResource;

    public int productionCount;
    public int consumptionCount;

    public JobType jobType = JobType.Null;

    private void Start()
    {
        //Debug.Log("Job::Start");
        ownerBody = this.transform.parent.gameObject.GetComponent<Body>();
    }

    public JobType SetJobType (JobType type)
    {
        //Debug.Log("Job::SetJobType");
        if (this.jobType != JobType.Null)
        {
            Debug.LogError("Job already set");
            return type;
        }

        switch (type)
        {
            case JobType.Farm:
                this.jobType = type;
                productionCount = 5;
                producedResource = ResourceType.Food;
                consumptionCount = 0;
                consumedResource = ResourceType.Null;
                break;
            case JobType.Chop:
                this.jobType = type;
                productionCount = 5;
                producedResource = ResourceType.Wood;
                consumptionCount = 0;
                consumedResource = ResourceType.Null;
                break;
            case JobType.Smith:
                this.jobType = type;
                productionCount = 5;
                producedResource = ResourceType.Tool;
                consumptionCount = 3;
                consumedResource = ResourceType.Wood;
                break;
            default:
                break;
        }

        return this.jobType;

    }

    public void Produce ()
    {
        Debug.Log("Job.Produce");

        // No job set
        if (this.jobType == JobType.Null)
        {
            Debug.LogError("Job not yet set");
            return;
        }

        // We don't have the required resources
        if (ownerBody.resourceDictionary[this.consumedResource] < consumptionCount)
        {
            Debug.LogError("Unable to consume required amount of resources");
            return;
        }


        // test if we have the required count of consumed resources
        if ((consumedResource != ResourceType.Null) || (consumptionCount == 0))
        {
            Debug.Log(this.jobType.ToString());
            Debug.Log(this.producedResource.ToString());
            Debug.Log(this.productionCount.ToString());

        }


        // Consume resources
        ownerBody.resourceDictionary[this.consumedResource] -= consumptionCount;
        // Produce resources
        ownerBody.resourceDictionary[this.producedResource] += productionCount;

        Debug.Log(ownerBody.resourceDictionary[this.producedResource]);
        return;

        // Consume consumedResources and produce producedResources
    }

}
