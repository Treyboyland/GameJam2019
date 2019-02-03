using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    bool loading = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && !loading)
        {
            loading = true;
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}
