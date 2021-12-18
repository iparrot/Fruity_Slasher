using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameplayManager
{
    public static void Lose()
    {
        // Show Replay button or fade out into Main menu screen
        // for now, restart scene
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
