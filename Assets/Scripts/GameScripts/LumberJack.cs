using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LumberJack : ResourceGathering
{
    [SerializeField] List<int> MiningSpeed;
    [SerializeField] float timer;
    [SerializeField] float start;
    protected override void GatheringSpeed()
    {
        timer = MiningSpeed[resourceType];
        start = timer;
        float solvingTimeIssue = Time.deltaTime;
    while(timer > 0)
        {
            timer =timer - Time.deltaTime;

        }

    }

 

}
