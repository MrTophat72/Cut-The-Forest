using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] List<GameObject> Obstacles;
    [SerializeField] List<GameObject> Workers;
    [SerializeField] GameObject ResourceText;
    private int choice;
    private bool testing;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("MainManager") != null) {
            testing = false;
        if (MainManager.Instance.xLocation.Count<1)
        {
            Debug.Log("New Game!");
            StartNew(0);
            SpawnOneGold();
            GameObject.Find("MainHub").GetComponent<MainHub>().StartUpBase(0,0,0);
            SpawnOneWorker(0);
        } else
        {
            Debug.Log("Load Game!");
            LoadGame();
            GameObject.Find("MainHub").GetComponent<MainHub>().
                StartUpBase(MainManager.Instance.Resources[0], MainManager.Instance.Resources[1],
                MainManager.Instance.Resources[2]);
        }
        } else
        {
            testing = true;
            Debug.Log("New Game!");
            StartNew(0);
            SpawnOneGold();
            GameObject.Find("MainHub").GetComponent<MainHub>().StartUpBase(0, 0, 0);
            SpawnOneWorker(0);
        }
    }


    private int StartNew(int min)
    {
        if (min < 200)
        {
            float x = Random.Range(-5f, 5f);
            float z = Random.Range(-5f, 5f);
            if (Mathf.Abs(x) < 1.5 && Mathf.Abs(z) < 1.5)
            {
                
                return StartNew(min);
            }
            else
            {
                SpawnObstacle(new Vector3(x, 0f, z));
                return StartNew(min + 1);
            }
        }
        return 1;
    }

    private void SpawnOneGold()
    {
        float x = Random.Range(-5f, 5f);
        float z = Random.Range(-5f, 5f);
        if (Mathf.Abs(x) < 4 && Mathf.Abs(z) < 4)
        {

            SpawnOneGold();
        }
        else
        {
            Instantiate(Obstacles[2], new Vector3(x,0,z), transform.rotation);
        }
    }
    private void LoadGame()
    {
        List<int> type = MainManager.Instance.type;
        List<float> xLoc = MainManager.Instance.xLocation;
        List<float> zLoc = MainManager.Instance.zLocation;
        for (int i = 0; i<type.Count;i++)
        {
            Instantiate(Obstacles[type[i]], new Vector3(xLoc[i], 0f, zLoc[i]), transform.rotation); 
        }
    }

    // 0.1% chance for gold 
    // 10 % chance for Stone
    // 90 % chance for Tree
    private void SpawnObstacle(Vector3 Location)
    {

        int chance = Random.Range(0, 1000);
        if(chance < 900)
        {
            //Debug.Log("Spawn a Tree");
            choice = 0;
            
            
        } else if (chance < 999){
            choice = 1;
           // MainManager.Instance.Locations.Add(new float[3] { 1f, Location.x, Location.z });
        } else
        {
            choice = 2;
           // MainManager.Instance.Locations.Add(new float[3] { 2f, Location.x, Location.z });
        }
        if (testing == false)
        {
            MainManager.Instance.xLocation.Add(Location.x);
            MainManager.Instance.zLocation.Add(Location.z);
            MainManager.Instance.type.Add(choice);
        }
        Instantiate(Obstacles[choice], Location, transform.rotation);

    }

    public void SpawnOneWorker(int type)
    {
        Instantiate(Workers[type], MainHub.Instance.transform.position + new Vector3(1, 0, 0), transform.rotation);
    }

}
