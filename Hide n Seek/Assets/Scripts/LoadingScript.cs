using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    public Text LoadingText;
    string dots = "";
    int noOfDots = 1;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadSceneAsync(0);
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("loadingTextUpdate", 1.0f, 1.0f);
    }

    void loadingTextUpdate()
    {
        if(noOfDots == 3)
        {
            noOfDots = 0;
            dots = "";
        }
        for(int i = 0; i <noOfDots; i++)
        {
            dots += ".";
        }
        LoadingText.text = "Loading" + dots;
        noOfDots++;
    }

}
