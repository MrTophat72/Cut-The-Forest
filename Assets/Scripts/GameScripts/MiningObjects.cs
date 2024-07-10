using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiningObjects : Building
{

    [SerializeField] int startAmount;
    public string Resource { get; [SerializeField] set; };

    private void Start()
    {
        Resource = gameObject.name;
    }
    private void LateUpdate()
    {
        if (startAmount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override string GetData()
    {
        return Resource;
    }
}
