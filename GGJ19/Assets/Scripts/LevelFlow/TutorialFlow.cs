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
    public GameObject buttonsInfo;
    private PlayerAttackController attackController;
    private SpriteRenderer spriteRendererRef;

    //Enemies
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    private Enemy enemy1Ref;
    private Enemy enemy2Ref;
    public Vector3 positionSpawnEnemy1;
    public Vector3 positionSpawnEnemy2;

    //Controls
    private bool tutorialStarted = false;
    private tutorialPhase currentPhase;

    //Methods
    public void startTutorial()
    {
        buttonsInfo.SetActive(true);
        buttonsInfo.transform.GetChild(0).gameObject.SetActive(true);
        currentPhase = tutorialPhase.Movement;
        tutorialStarted = true;
        attackController = playerCharacter.GetComponent<PlayerAttackController>();
        attackController.canAttack = false;
        attackController.canShoot = false;
    }

    private void startSwordTraining()
    {
        buttonsInfo.transform.GetChild(0).gameObject.SetActive(false);
        buttonsInfo.transform.GetChild(1).gameObject.SetActive(true);
        spriteRendererRef = buttonsInfo.transform.GetChild(1).GetComponent<SpriteRenderer>();
        Debug.Log(spriteRendererRef);
        currentPhase = tutorialPhase.Sword;
        GameObject obj = Instantiate(enemy1Prefab, positionSpawnEnemy1, Quaternion.identity);
        enemy1Ref = obj.GetComponent<Enemy>();
        attackController.canAttack = true;
    }

    private void startNerfTraining()
    {
        buttonsInfo.transform.GetChild(1).gameObject.SetActive(false);
        buttonsInfo.transform.GetChild(2).gameObject.SetActive(true);
        spriteRendererRef = buttonsInfo.transform.GetChild(2).GetComponent<SpriteRenderer>();
        currentPhase = tutorialPhase.Nerf;
        GameObject obj = Instantiate(enemy2Prefab, positionSpawnEnemy2, Quaternion.identity);
        enemy2Ref = obj.GetComponent<Enemy>();
        attackController.canAttack = false;
        attackController.canShoot = true;
    }

    private void startAmmoTraining()
    {
        buttonsInfo.SetActive(false);
        buttonsInfo.transform.GetChild(2).gameObject.SetActive(false);
        currentPhase = tutorialPhase.Ammo;
        attackController.giveBulletsEnabled = false;
        attackController.canShoot = false;
    }

    private void endTutorial()
    {
        buttonsInfo.SetActive(false);
        if (enemy1Ref != null) enemy1Ref.killEnemy();
        if (enemy2Ref != null) enemy2Ref.killEnemy();
        LevelFlow.Instance.currentGameState = gameState.Interwave;
        LevelFlow.Instance.setNextGameState(gameState.Wave1);
        tutorialStarted = false;
        attackController.giveBulletsEnabled = true;
        attackController.canShoot = true;
        attackController.canAttack = true;
    }

    private void Update()
    {
        if(tutorialStarted)
        {
            if(currentPhase == tutorialPhase.Movement)
            {
                if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
                {
                    startSwordTraining();
                }
                else if (Input.GetButtonDown("Fire2")) endTutorial();
            }
            else if (currentPhase == tutorialPhase.Sword)
            {
                if (playerCharacter.transform.localScale.x < 0f) spriteRendererRef.flipX = true;
                else spriteRendererRef.flipX = false;
                if (enemy1Ref.currentState == enemyState.KnockedDown) startNerfTraining();
            }
            else if (currentPhase == tutorialPhase.Nerf)
            {
                if (playerCharacter.transform.localScale.x < 0f) spriteRendererRef.flipX = true;
                else spriteRendererRef.flipX = false;
                if (enemy2Ref.currentState == enemyState.KnockedDown) startAmmoTraining();
            }
            else if (currentPhase == tutorialPhase.Ammo)
            {
                if(attackController.Bullets == attackController.getMaxBullets())
                {
                    endTutorial();
                }
            }
        }
    }
}
