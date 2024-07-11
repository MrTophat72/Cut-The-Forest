using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{


    [System.Serializable]
    public class InventoryEntry
    {
        public int[] Resources = new int[3];
    }
    public bool isAlive = true;

    [Tooltip("-1 is infinite")]
    public int InventorySpace = -1;

    protected InventoryEntry m_Inventory = new InventoryEntry();
    public InventoryEntry Inventory => m_Inventory;

   // protected int m_CurrentAmount = 0;

    public int AddItem(int resourceId, int amount) 
    {
        m_Inventory.Resources[resourceId] += amount;
        return m_Inventory.Resources[resourceId];
    }

    public int AddItem(string resourceId, int amount)
    {
        return AddItem(StringtoInt(resourceId),amount);
    }
        public int GetItem(int resourceId, int requestAmount)
    {
        if(requestAmount > m_Inventory.Resources[resourceId])
        {
            return -1;
        }
            m_Inventory.Resources[resourceId] -= requestAmount;
            return m_Inventory.Resources[resourceId];
    }
    public int GetItem(string resourceId, int requestAmount)
    {
        return GetItem(StringtoInt(resourceId), requestAmount);
    }

    public int StringtoInt(string str)
    {

        if (str.ToLower().Equals("wood") || str.ToLower().Equals("tree"))
        {
            return 0;
        }
        else if (str.ToLower().Equals("rock"))
        {
            return 1;
        }
        else if (str.ToLower().Equals("gold"))
        {
            return 2;
        }
        else if (str.ToLower().Equals("mainhub"))
        {
            return -1;
        } else { 

            Debug.LogWarning("ERROR: There is no such String!"+str);
            return -2;
        }

    }


    public virtual string GetName()
    {
        return gameObject.name;
    }

    public virtual string GetData()
    {
        return "";
    }

    public InventoryEntry GetContent()
    {
        return m_Inventory;
    }
}
