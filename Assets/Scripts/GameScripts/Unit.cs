using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Unit : MonoBehaviour
{
    public float speed = 1f;
    protected NavMeshAgent m_Agent;
    [SerializeField] protected Building m_Target;
    // Start is called before the first frame update
    protected void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = speed;
        m_Agent.acceleration = 999;
        m_Agent.angularSpeed = 999;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (m_Target != null)
        {
            float distance = Vector3.Distance(m_Target.transform.position,transform.position);
            if (distance < 0.7f)
            {
                m_Agent.isStopped = true;
                BuildingInRange();
            } else if(distance < 1f && m_Target == MainHub.Instance)
            {
                m_Agent.isStopped = true;
                BuildingInRange();
            }


        }
    }


    public virtual void GoTo(Building target)
    {
        m_Target = target;
        if (m_Target != null)
        {
            m_Agent.SetDestination(m_Target.transform.position);
            m_Agent.isStopped = false;
        }
    }

    public virtual void GoTo(Vector3 position)
    {
        //we don't have a target anymore if we order to go to a random point.
        m_Target = null;
        m_Agent.SetDestination(position);
        m_Agent.isStopped = false;
        
    }

    
    protected abstract void BuildingInRange();

    public virtual string GetName()
    {
        return "Unit";
    }

    public virtual string GetData()
    {
        return "";
    }

    public virtual void GetContent(ref List<Building.InventoryEntry> content)
    {

    }
}
