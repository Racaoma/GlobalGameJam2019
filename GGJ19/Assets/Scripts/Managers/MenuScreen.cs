using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{
    public string SceneName;
    public Animator CreditsScreen;
    public const string TriggerIn = "In";
    public const string TriggerOut = "Out";
    // Start is called before the first frame update

    public void StartGame() {
        ScenesFader.Instance.ChangeScene(SceneName);
    }

    public void CreditScreenIn() {
        CreditsScreen.SetTrigger(TriggerIn);
    }

    public void CreditScreenOut() {
        CreditsScreen.SetTrigger(TriggerOut);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
