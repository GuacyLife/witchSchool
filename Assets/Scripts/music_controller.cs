using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music_controller : MonoBehaviour
{
    public static music_controller instance;

    private void /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
}
