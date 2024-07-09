using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainHub : MonoBehaviour
{
    // Start is called before the first frame update

    private int m_Wood;
    private int m_Rock;
    private int m_Gold;
    public int wood
    {
        get { return m_Wood; }
        set
        {
            if (value < 0)
            {
                Debug.LogError("You can't set a negative Wood value");
            }
            else
            {
                m_Wood = value;
            }
        }
    }
    public int rock
    {
        get { return m_Rock; }
        set
        {
            if (value < 0)
            {
                Debug.LogError("You can't set a negative Rock value");
            }
            else
            {
                m_Rock = value;
            }
        }
    }
    public int gold
    {
        get { return m_Gold; }
        set
        {
            if (value < 0)
            {
                Debug.LogError("You can't set a negative Gold value");
            }
            else
            {
                m_Gold = value;
            }
        }
    }
    [SerializeField] TextMeshProUGUI woodText;
    [SerializeField] TextMeshProUGUI rockText;
    [SerializeField] TextMeshProUGUI goldText;
    public void StartUpBase(int woodResource, int rockResource, int goldResource)
    {
        m_Wood = woodResource;
        m_Rock = rockResource;
        m_Gold = goldResource;
    }
    // Update is called once per frame
    void Update()
    {

        woodText.text = "Wood: " + m_Wood;
        rockText.text = "Rock: " + m_Rock;
        goldText.text = "Gold: " + m_Gold;

    }
}
