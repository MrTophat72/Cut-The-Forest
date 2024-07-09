using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserControll : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject CameraObject;
    [SerializeField] GameObject PauseText;
    public bool isGameActive;
    // Update is called once per frame

    private void Start()
    {
        isGameActive = true;
    }
    void Update()
    {
        if (isGameActive)
        {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            CameraObject.transform.position = CameraObject.transform.position + new Vector3(move.x, 0, move.y) * Time.deltaTime * speed;
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            PauseScreen();
        }
    }

    public void PauseScreen()
    {
        
        PauseText.SetActive(isGameActive);
        isGameActive = !isGameActive;
    }

    public void GoToMainMenu()
    {
        MainManager.Instance.SaveFiles();
        SceneManager.LoadScene(0);
    }
}
