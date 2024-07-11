using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class ResourceGathering : Unit
{
    // Start is called before the first frame update
    public int MaxAmountTransported = 1;
    private int currentAmmount = 0;
    protected int resourceType { get; private set; }
    private int tempType;
    [SerializeField] Building m_CurrentMineTarget;
    [SerializeField] Building.InventoryEntry m_Transporting = new Building.InventoryEntry();
    protected string currentTargetString;
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
            
                if (currentAmmount < MaxAmountTransported)
                {
                    currentAmmount++;
                    StartCoroutine(ResourceCollection());
                } else
                {
                m_CurrentMineTarget = m_Target;
                GoTo(MainHub.Instance);
                }
            }
    }
    IEnumerator ResourceCollection()
    {
        doNotMove = true;
        m_CurrentMineTarget = m_Target;
        currentTargetString = tempString;
        yield return new WaitForSeconds(GatheringSpeed());
        m_Transporting.Resources[resourceType] = m_Target.Inventory.Resources[resourceType];
        mineTargetInventory = m_Target.GetItem(resourceType, MaxAmountTransported);
        m_Transporting.Resources[resourceType] = MaxAmountTransported;
        Debug.Log("Have waited");
        doNotMove = false;
        GoTo(MainHub.Instance);
    }

    private void UnloadAtBase()
    {
        if (currentAmmount > 0)
        {

            int temp = m_Target.StringtoInt(currentTargetString);
            m_Target.AddItem(temp, MaxAmountTransported);
            m_Transporting.Resources[temp] = 0;
            currentAmmount--;
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
        //m_Transporting.Resources[resourceType] = 0;
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
    protected abstract float GatheringSpeed();

    public override string GetName()
    {
        return "Transporter";
    }

    public override string GetData()
    {
        return $"Can transport up to {MaxAmountTransported}";
    }


}