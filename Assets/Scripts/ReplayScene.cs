using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReplayScene
{
    Button replayButton;
    public ReplayScene(Button button)
    {
        replayButton = button;
        replayButton.onClick.AddListener(()=>Replay());
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
