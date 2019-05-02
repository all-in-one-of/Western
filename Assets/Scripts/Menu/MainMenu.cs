using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MenuPosition;
    public GameObject MenuFinalPosition;
    public GameObject Selecter;
    public Transform SelecterPlayPosition;
    public Transform SelecterQuitPosition;
    public Sprite SelecterSelected;
    private bool SelecterOnPlay;

    // Start is called before the first frame update
    void Start()
    {
        SelecterOnPlay = true;
        StartCoroutine (MenuStarting());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical_1")>0)
        {
            Selecter.transform.position = SelecterPlayPosition.position;
            SelecterOnPlay = true;
        }
        else if (Input.GetAxis("Vertical_1") < 0)
        {
            Selecter.transform.position = SelecterQuitPosition.position;
            SelecterOnPlay = false;
        }

        if (Input.GetButtonDown("A_Button_1"))
        {
            print("lul");
            StartCoroutine(ButtonPress());
        }
    }

    IEnumerator MenuStarting()
    {
        for (int i = 1010; i > MenuFinalPosition.transform.position.y; i=i-4)
        {
            MenuPosition.transform.position = new Vector3(Screen.width/2,i, 0);
            yield return null;
            
        }
    }

    IEnumerator ButtonPress()
    {
            print("lol");
            Selecter.gameObject.GetComponent<Image>().sprite = SelecterSelected;
            yield return new WaitForSeconds(0.5f);
            if (SelecterOnPlay == true)
            {
                SceneManager.LoadScene("Eve");
            }
            else
            {
                Application.Quit();
            }
 
    }
}
