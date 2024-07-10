using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiningObjects : Building
{

    [SerializeField] int startAmount;
    [SerializeField] int Resource;


    private void LateUpdate()
    {
        if (startAmount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
