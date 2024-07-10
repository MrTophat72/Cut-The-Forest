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


    [Tooltip("-1 is infinite")]
    public int InventorySpace = -1;

    protected InventoryEntry m_Inventory = new InventoryEntry();
    public InventoryEntry Inventory => m_Inventory;

    protected int m_CurrentAmount = 0;

    public int AddItem(int resourceId, int amount) 
    {
        int maxInventorySpace = InventorySpace == -1 ? Int32.MaxValue : InventorySpace;
        if (m_CurrentAmount == maxInventorySpace)
            return amount;

        int addedAmount = Mathf.Min(maxInventorySpace - m_CurrentAmount, amount);
        m_Inventory.Resources[resourceId] += addedAmount;
        m_CurrentAmount += addedAmount;
        return amount - addedAmount;
    }

    public int GetItem(int resourceId, int requestAmount)
    {
        


            int amount = Mathf.Min(requestAmount, m_Inventory.Resources[resourceId]);
            m_Inventory.Resources[resourceId] -= amount;

  //          if (m_Inventory[found].Count == 0)
  //          {//no more of that resources, so we remove it
  //              m_Inventory.RemoveAt(found);
  //          }

            m_CurrentAmount -= amount;

            return amount;

    }






    public virtual string GetName()
    {
        return gameObject.name;
    }

    public virtual string GetData()
    {
        return "";
    }

    public void GetContent()
    {
        //m_Inventory;
    }
}
