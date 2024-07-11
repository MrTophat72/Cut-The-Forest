using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class ResourceGathering : Unit
{
    // Start is called before the first frame update
    public int MaxAmountTransported = 1;
    protected int resourceType { get; private set; }
    private int tempType;
    private Building m_CurrentMineTarget;
    private Building.InventoryEntry m_Transporting = new Building.InventoryEntry();
    private string currentTargetString;
    private string tempString;
    private GameObject min;
    private GameObject[] obj;
    private int mineTargetInventory;
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
        tempString = m_Target.GetName().Split("(")[0];
        tempType = m_Target.StringtoInt(tempString);
        if (tempType > -1)
        {
            resourceType = tempType;
        }
        if (m_Target == MainHub.Instance)
        {
            //we arrive at the base, unload!
            UnloadAtBase();
        }
        else
        {
            if (m_Transporting.Resources[resourceType] < MaxAmountTransported)
            {
                ResourceCollection();
                //Debug.Log("In the if statement!");
                GoTo(MainHub.Instance);
            }


        }
    }

    private void UnloadAtBase()
    {
        if (m_Transporting.Resources[resourceType] > 0)
        {

            m_Target.AddItem(resourceType, 1);
        }
        if (mineTargetInventory == 0)
        {
            m_CurrentMineTarget = FindClosestTag().GetComponent<Building>();
            if (m_CurrentMineTarget == null)
            {
                m_CurrentMineTarget = MainHub.Instance;
            }

        }
        //we go back to the building we came from
        GoTo(m_CurrentMineTarget);
        m_Transporting.Resources[resourceType] = 0;
    }
    protected void ResourceCollection()
    {
        m_CurrentMineTarget = m_Target;
        currentTargetString = tempString;
        GatheringSpeed();
        m_Transporting.Resources[resourceType] = m_Target.Inventory.Resources[resourceType];
        mineTargetInventory = m_Target.GetItem(resourceType, MaxAmountTransported);
        m_Transporting.Resources[resourceType] = MaxAmountTransported;
    }

    protected GameObject FindClosestTag()
    {

        float distance = Vector3.Distance(m_Target.transform.position, transform.position);
        float min_dist = 100000;
        obj = GameObject.FindGameObjectsWithTag(currentTargetString);

        foreach (GameObject go in obj)
        {
            distance = Vector3.Distance(go.transform.position, transform.position);
            if (min_dist > distance)
            {
                min_dist = distance;
                min = go;
            }
        }

        return min;
    }
    //Override all the UI function to give a new name and display what it is currently transporting
    protected abstract void GatheringSpeed();

    public override string GetName()
    {
        return "Transporter";
    }

    public override string GetData()
    {
        return $"Can transport up to {MaxAmountTransported}";
    }


}