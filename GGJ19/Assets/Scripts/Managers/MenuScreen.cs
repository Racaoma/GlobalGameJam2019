using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScreen : MonoBehaviour
{
    public string SceneName;
    public Animator CreditsScreen;
    public const string TriggerIn = "In";
    public const string TriggerOut = "Out";
    private bool wasClicked=false;
    public Button PlayButton;
    // Start is called before the first frame update

    private void Awake() {
        wasClicked=false;
    }

    public void StartGame() {
        if (!wasClicked)
        {
            wasClicked = true;
            PlayButton.interactable = !wasClicked;
            ScenesFader.Instance.ChangeScene(SceneName);
        }
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
