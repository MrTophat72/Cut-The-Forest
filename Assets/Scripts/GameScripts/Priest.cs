using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : ResourceGathering
{
    [SerializeField] List<float> MiningSpeed;


    protected override float GatheringSpeed()
    {
        if (MiningSpeed.Count > resourceType)
        {
            return MiningSpeed[resourceType];
        }
        else
        {
            Debug.LogWarning("The list doesn't contain this item");
            return 0;
        }



    }
}
