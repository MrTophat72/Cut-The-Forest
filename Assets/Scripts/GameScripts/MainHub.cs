using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainHub : Building
{
    // Start is called before the first frame update

    [SerializeField] int m_Wood;
    [SerializeField] int m_Rock;
    [SerializeField] int m_Gold;
   
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
        AddItem("wood", woodResource);
        AddItem("rock", rockResource);
        AddItem("gold", goldResource);
    }
    // Update is called once per frame
    void Update()
    {
        woodText.text = "Wood: " + m_Inventory.Resources[0];
        rockText.text = "Rock: " + m_Inventory.Resources[1];
        goldText.text = "Gold: " + m_Inventory.Resources[2];
    }

    
   
}
