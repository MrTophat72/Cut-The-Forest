using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainHub : Building
{
    // Start is called before the first frame update

    private int m_Wood;
    private int m_Rock;
    private int m_Gold;
   
    [SerializeField] TextMeshProUGUI woodText;
    [SerializeField] TextMeshProUGUI rockText;
    [SerializeField] TextMeshProUGUI goldText;

    public static MainHub Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public void StartUpBase(int woodResource, int rockResource, int goldResource)
    {
        m_Wood = AddItem(0, woodResource);
        m_Rock = AddItem(1, rockResource);
        m_Gold = AddItem(2, goldResource);
    }
    // Update is called once per frame
    void Update()
    {

        woodText.text = "Wood: " + m_Wood;
        rockText.text = "Rock: " + m_Rock;
        goldText.text = "Gold: " + m_Gold;

    }
}
