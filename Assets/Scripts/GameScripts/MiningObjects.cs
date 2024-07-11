using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MiningObjects : Building
{

    [SerializeField] int startAmount;
    public string Resource;
    [SerializeField] int currentResource;
    [SerializeField] InventoryEntry testingInventory;

    private void Start()
    {
        isAlive = true;
        Resource = gameObject.name.Split("(")[0].ToLower();
        currentResource = StringtoInt(Resource);
        AddItem(currentResource, startAmount);
        testingInventory = GetContent();
    }
    private void Update()
    {
        if (m_Inventory.Resources[currentResource] <= 0)
        {
            isAlive = false;
            Destroy(gameObject);
        }
    }

    public override string GetData()
    {
        return Resource;
    }
}
