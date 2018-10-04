using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType { Null, Food, Wood, Tool }

public class NewBehaviourScript : MonoBehaviour {


    ResourceType type = ResourceType.Null;

    public void SetResourceType (ResourceType type)
    {
        this.type = type;
    }

    public ResourceType GetResourceType ()
    {
        return this.type;
    }
}
