using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LumberJack : Unit
{
    // Start is called before the first frame update
    public int MaxAmountTransported = 1;

    private Building m_CurrentTransportTarget;
    private Building.InventoryEntry m_Transporting = new Building.InventoryEntry();

    // We override the GoTo function to remove the current transport target, as any go to order will cancel the transport
    public override void GoTo(Vector3 position)
    {
        base.GoTo(position);
        m_CurrentTransportTarget = null;
    }

    protected override void BuildingInRange()
    {
        if (m_Target == MainHub.Instance)
        {
            //we arrive at the base, unload!
            if (m_Transporting.Resources[0] > 0)
                m_Target.AddItem(0, 1);

            //we go back to the building we came from
            GoTo(m_CurrentTransportTarget);
            m_Transporting.Resources[1] = 0;
        }
        else
        {
            if (m_Target.Inventory.Resources[1] > 0)
            {
                m_Transporting.Resources[1] = m_Target.Inventory.Resources[1];
                m_Transporting.Resources[1] = m_Target.GetItem(m_Transporting.Resources[1], MaxAmountTransported);
                m_CurrentTransportTarget = m_Target;
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
