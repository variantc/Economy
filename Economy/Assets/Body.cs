using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

    public Job jobPrefab;

    public Dictionary<ResourceType, int> resourceDictionary = new Dictionary<ResourceType, int>();
    public List<Job> jobList;

    private void Start()
    {
        //Debug.Log("Body::Start");
        InitialiseDictionary();
        AddJob(JobType.Chop);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach (KeyValuePair<ResourceType, int> kvp in resourceDictionary)
            {
                Debug.Log("Current " + kvp.Key + ": " + kvp.Value);
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            foreach (Job j in jobList)
            {
                Debug.Log("Current jobs: " + j.jobType);
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ProduceJobs();
        }
    }

    void InitialiseDictionary()
    {
        ResourceType[] resourceTypeArray = (ResourceType[])Enum.GetValues(typeof(ResourceType));
        foreach (ResourceType resource in resourceTypeArray)
        {
            resourceDictionary.Add(resource, 0);
        }
    }

    public void ProduceJobs ()
    {
        Debug.Log("Body.ProduceJobs");
        foreach (Job j in jobList)
        {
            j.Produce();
        }
    }

    public void AddJob (JobType type)
    {
        //Debug.Log("Body::AddJob");
        Job job = Instantiate(jobPrefab,this.transform);
        job.SetJobType(type);
        jobList.Add(job);
    }

}
