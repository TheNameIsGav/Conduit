using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).GetComponent<Text>().text = "Power: " + Current.instance.power;
        transform.GetChild(1).GetComponent<Text>().text = "Integrity: " + Current.instance.strength;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
