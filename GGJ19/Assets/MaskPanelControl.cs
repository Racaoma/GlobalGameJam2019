using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPanelControl : MonoBehaviour
{
    private Animator animator;
    void OnEnable()
    {
        GameEvents.GameState.StartLevel += StartPanel;
        GameEvents.GameState.WaveLose += ClosePanel;
    }

    void OnDisable()
    {
        GameEvents.GameState.StartLevel -= StartPanel;
        GameEvents.GameState.WaveLose -= ClosePanel;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void StartPanel()
    {
        animator.SetTrigger("start");
        animator.SetBool("opened", true);
    }

    private void ClosePanel()
    {
        animator.SetBool("opened", false);
    }
}
