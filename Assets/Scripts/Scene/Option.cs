using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
