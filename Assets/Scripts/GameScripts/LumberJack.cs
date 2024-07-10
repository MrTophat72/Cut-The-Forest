using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberJack : Unit
{
    // Start is called before the first frame update
    public int MaxAmountTransported = 1;
    private int resourceType;
    private int tempType;
    private Building m_CurrentMineTarget;
    private Building.InventoryEntry m_Transporting = new Building.InventoryEntry();

    private void Start()
    {
        resourceType = 0;
    }
    // We override the GoTo function to remove the current transport target, as any go to order will cancel the transport
    public override void GoTo(Vector3 position)
    {
        base.GoTo(position);
        m_CurrentMineTarget = null;
    }

    protected override void BuildingInRange()
    {

        tempType = m_Target.StringtoInt(m_Target.GetName());
        if (tempType >= -1)
        {
            resourceType = tempType;
        }
        if (m_Target == MainHub.Instance)
        {

            //we arrive at the base, unload!
            if (m_Transporting.Resources[resourceType] > 0)
            {
                m_Target.AddItem(resourceType, 1);
            }

            //we go back to the building we came from
            GoTo(m_CurrentMineTarget);
            m_Transporting.Resources[resourceType] = 0;
        }
        else
        {
            if (m_Target.Inventory.Resources[resourceType] > MaxAmountTransported)
            {
                m_Transporting.Resources[resourceType] = m_Target.Inventory.Resources[resourceType];
                m_Transporting.Resources[resourceType] = m_Target.GetItem(m_Transporting.Resources[resourceType], MaxAmountTransported);
                m_CurrentMineTarget = m_Target;
                GoTo(MainHub.Instance);
            }
        }
    }

    //Override all the UI function to give a new name and display what it is currently transporting
    public override string GetName()
    {
        return "Transporter";
    }

    public override string GetData()
    {
        return $"Can transport up to {MaxAmountTransported}";
    }
}
