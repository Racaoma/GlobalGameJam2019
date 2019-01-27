using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tutorialPhase
{
    Movement,
    Sword,
    Nerf,
    Ammo
}

public class TutorialFlow : Singleton<TutorialFlow>
{
    //Player
    public GameObject playerCharacter;

    //Enemies
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy1Ref;
    public GameObject enemy2Ref;
    public Transform positionSpawnEnemy1;
    public Transform positionSpawnEnemy2;

    //Controls
    private bool tutorialStarted = false;
    private tutorialPhase currentPhase;

    //Methods
    public void startTutorial()
    {
        currentPhase = tutorialPhase.Movement;
        tutorialStarted = true;
    }

    private void startSwordTraining()
    {
        enemy1Ref = Instantiate(enemy1Prefab, positionSpawnEnemy1);
    }

    private void startNerfTraining()
    {
        enemy2Ref = Instantiate(enemy2Prefab, positionSpawnEnemy2);
    }

    private void startAmmoTraining()
    {

    }

    private void Update()
    {
        if(tutorialStarted)
        {
            if(currentPhase == tutorialPhase.Movement)
            {
                if (Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") != 0)
                {
                    startSwordTraining();
                    currentPhase = tutorialPhase.Sword;
                }
            }
            else if (currentPhase == tutorialPhase.Sword)
            {
                //TODO
            }
            else if (currentPhase == tutorialPhase.Nerf)
            {
                //TODO
            }
            else if (currentPhase == tutorialPhase.Ammo)
            {
                Destroy(enemy1Ref);
                Destroy(enemy2Ref);
            }
        }
    }
}
