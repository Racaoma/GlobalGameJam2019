using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    public string SceneName;
    // Start is called before the first frame update
 
    public void StartGame() {
        ScenesFader.Instance.ChangeScene(SceneName);
    }

    public void CreditScreen() { }

    public void QuitGame() {
        Application.Quit();
    }
}
