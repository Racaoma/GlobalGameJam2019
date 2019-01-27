using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonAwakePersistent<AudioManager>
{
    //AudioSource
    private AudioSource bgmAudioSource;

    [Header("BGM")]
    public AudioClip BGM_Menu;
    public AudioClip BGM_IntroInGame;
    public AudioClip BGM_InGame;
    public AudioClip BGM_IntroBoss;
    public AudioClip BGM_Boss;
    public AudioClip BGM_Defeat;
    public AudioClip BGM_Victory;
    public AudioClip BGM_Tutorial;

    [Header("SFX Audios")]
    public AudioClip SFX_SwordAttack;
    public AudioClip SFX_SwordHit;
    public AudioClip SFX_NerfShoot;
    public AudioClip SFX_NerfHit;
    public AudioClip SFX_ChildDamaged;
    public AudioClip SFX_ChildJump;
    public AudioClip SFX_LazerShoot;
    public AudioClip SFX_VictoryFanfare;

    [Header("Menus SFX")]
    public AudioClip SFX_ButtonSelect;

    //Control Variables
    private AudioClip nextIntroBGM;
    private AudioClip nextBGM;
    public float audioFadeOutFactor = 0.6f;

    //Events
    public void playBGM_Main()
    {
        Debug.Log("Play playBGM_Main");
        StartCoroutine(stopMusicWithFade());
        nextBGM = BGM_InGame;
    }
    public void playBGM_Boss()
    {
        Debug.Log("Play playBGM_Boss");
        StartCoroutine(stopMusicWithFade());
        nextIntroBGM = BGM_IntroBoss;
        nextBGM = BGM_Boss;
    }
    public void playBGM_Tutorial()
    {
        Debug.Log("Play playBGM_Tutorial");
        StartCoroutine(stopMusicWithFade());
        nextBGM = BGM_Tutorial;
    }
    public void playBGM_Victory()
    {
        Debug.Log("Play playBGM_Victory");
        StartCoroutine(stopMusicWithFade());
        nextBGM = BGM_Victory;
    }
    public void playBGM_Defeat()
    {
        Debug.Log("Play playBGM_Defeat");
        StartCoroutine(stopMusicWithFade());
        nextBGM = BGM_Defeat;
    }

    public void playSFX_WaveWon() { playSFX(SFX_VictoryFanfare); }
    public void playSFX_SwordAttack() { playSFX(SFX_SwordAttack); }
    public void playSFX_NerfShoot() { playSFX(SFX_NerfShoot); }
    public void playSFX_TookDamage() { playSFX(SFX_ChildDamaged); }
    public void playSFX_Jump() { playSFX(SFX_ChildJump); }
    public void playSFX_SwordHit() { playSFX(SFX_SwordHit); }
    public void playSFX_NerfHit() { playSFX(SFX_NerfHit); }
    public void playSFXButtonSelect() { AudioSource.PlayClipAtPoint(SFX_ButtonSelect, Camera.main.transform.position); }

    //Start
    private void Start()
    {
        //Get Audio Source
        bgmAudioSource = this.GetComponent<AudioSource>();

        //Setup Events
        GameEvents.GameState.StartGame += playBGM_Main;
        GameEvents.GameState.StartBoss += playBGM_Boss;
        GameEvents.GameState.WaveWon += playSFX_WaveWon;
        GameEvents.GameState.Victory += playBGM_Victory;
        GameEvents.GameState.Tutorial += playBGM_Tutorial;
        GameEvents.PlayerAction.SwordAttack += playSFX_SwordAttack;
        GameEvents.PlayerAction.NerfShoot += playSFX_NerfShoot;
        GameEvents.PlayerAction.TookDamage += playSFX_TookDamage;
        GameEvents.PlayerAction.Jump += playSFX_Jump;
        GameEvents.EnemyAction.SwordHit += playSFX_SwordHit;
        GameEvents.EnemyAction.NerfHit += playSFX_NerfHit;
    }

    private void OnDestroy()
    {
        GameEvents.GameState.StartGame -= playBGM_Main;
        GameEvents.GameState.StartBoss -= playBGM_Boss;
        GameEvents.GameState.WaveWon -= playSFX_WaveWon;
        GameEvents.GameState.Victory -= playBGM_Victory;
        GameEvents.GameState.Tutorial -= playBGM_Tutorial;
        GameEvents.PlayerAction.SwordAttack -= playSFX_SwordAttack;
        GameEvents.PlayerAction.NerfShoot -= playSFX_NerfShoot;
        GameEvents.PlayerAction.TookDamage -= playSFX_TookDamage;
        GameEvents.PlayerAction.Jump -= playSFX_Jump;
        GameEvents.EnemyAction.SwordHit -= playSFX_SwordHit;
        GameEvents.EnemyAction.NerfHit -= playSFX_NerfHit;
    }

    private void playIntroBGM(AudioClip bgm)
    {
        bgmAudioSource.volume = 1f;
        bgmAudioSource.clip = bgm;
        bgmAudioSource.loop = false;
        bgmAudioSource.Play();
        nextIntroBGM = null;
    }

    private void playBGM(AudioClip bgm)
    {
        bgmAudioSource.volume = 1f;
        bgmAudioSource.clip = nextBGM;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
        nextIntroBGM = null;
        nextBGM = null;
    }

    private void playSFX(AudioClip sfx)
    {
        if(sfx != null)
        {
            AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position);
        }
    }

    //Change Music with Fade
    private IEnumerator stopMusicWithFade()
    {
        while (bgmAudioSource.volume > 0f)
        {
            bgmAudioSource.volume -= audioFadeOutFactor * Time.deltaTime;
            yield return 0;
        }

        bgmAudioSource.volume = 0f;
        bgmAudioSource.Stop();
    }

    private void Update()
    {
        if(nextIntroBGM != null || nextBGM != null)
        {
            if(!bgmAudioSource.isPlaying)
            {
                if (nextIntroBGM != null) playIntroBGM(nextIntroBGM);
                else if (nextBGM != null) playBGM(nextBGM);
            }
        }
    }
}
