using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserControll : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Camera CameraObject;
    [SerializeField] GameObject PauseText;
    public GameObject Marker;
    public bool isGameActive;

    [SerializeField] Unit m_Selected = null;
    // Update is called once per frame

    private void Start()
    {
        Marker.SetActive(false);
        isGameActive = true;
    }
    void Update()
    {
        if (isGameActive)
        {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            CameraObject.transform.position = CameraObject.transform.position + new Vector3(move.x, 0, move.y) * Time.deltaTime * speed;

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(" Mouse down!");
                HandleSelection();
            }
            else if (m_Selected != null && Input.GetMouseButtonDown(1))
            {//right click give order to the unit
                Debug.Log(" Unit Selected!");
                HandleAction();
            }

            MarkerHandling();

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            PauseScreen();
        }
    }

    private void HandleSelection()
    {
        var ray = CameraObject.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //the collider could be children of the unit, so we make sure to check in the parent
            var unit = hit.collider.GetComponentInParent<Unit>();
            m_Selected = unit;
           


            //check if the hit object have a IUIInfoContent to display in the UI
            //if there is none, this will be null, so this will hid the panel if it was displayed
            //var uiInfo = hit.collider.GetComponentInParent<UIMainScene.IUIInfoContent>();
           // UIMainScene.Instance.SetNewInfoContent(uiInfo);
        }
    }

    private void HandleAction()
    {
        var ray = CameraObject.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var building = hit.collider.GetComponentInParent<Building>();

            if (building != null)
            {
                m_Selected.GoTo(building);
            }
            else
            {
                m_Selected.GoTo(hit.point);
            }
        }
    }
    public void PauseScreen()
    {
        
        PauseText.SetActive(isGameActive);
        isGameActive = !isGameActive;
    }
    void MarkerHandling()
    {
        if (m_Selected == null && Marker.activeInHierarchy)
        {
            Marker.SetActive(false);
            Marker.transform.SetParent(null);
        }
        else if (m_Selected != null && Marker.transform.parent != m_Selected.transform)
        {
            Marker.SetActive(true);
            Marker.transform.SetParent(m_Selected.transform, false);
            Marker.transform.localPosition = Vector3.zero;
        }
    }
    public void GoToMainMenu()
    {
        MainManager.Instance.SaveFiles();
        SceneManager.LoadScene(0);
    }
}
