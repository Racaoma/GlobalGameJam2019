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
    private PlayerAttackController attackController;

    //Enemies
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public Enemy enemy1Ref;
    public Enemy enemy2Ref;
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
        attackController = playerCharacter.GetComponent<PlayerAttackController>();
        attackController.canAttack = false;
        attackController.canShoot = false;
    }

    private void startSwordTraining()
    {
        GameObject obj = Instantiate(enemy1Prefab, positionSpawnEnemy1);
        enemy1Ref = obj.GetComponent<Enemy>();
        attackController.canAttack = true;
    }

    private void startNerfTraining()
    {
        GameObject obj = Instantiate(enemy2Prefab, positionSpawnEnemy2);
        enemy2Ref = obj.GetComponent<Enemy>();
        attackController.canAttack = false;
        attackController.canShoot = true;
    }

    private void startAmmoTraining()
    {
        attackController.giveBulletsEnabled = false;
        attackController.canShoot = false;
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
                if (enemy1Ref.currentState == enemyState.KnockedDown) startNerfTraining();
            }
            else if (currentPhase == tutorialPhase.Nerf)
            {
                if (enemy2Ref.currentState == enemyState.KnockedDown) startAmmoTraining();
            }
            else if (currentPhase == tutorialPhase.Ammo)
            {
                if(attackController.Bullets == attackController.getMaxBullets())
                {
                    Destroy(enemy1Ref);
                    Destroy(enemy2Ref);
                    LevelFlow.Instance.currentGameState = gameState.Interwave;
                    LevelFlow.Instance.setNextGameState(gameState.Wave1);
                }
            }
        }
    }
}
