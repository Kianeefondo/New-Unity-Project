using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Starter : MonoBehaviour
{
    /**
     * Used in the start screen, will move the scene from the start screen to scene 1 which is the play screen.
     */
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
}
