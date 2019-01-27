using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    //AudioSource
    [SerializeField]
    private AudioSource bgmAudioSource;

    [Header("BGM")]
    public AudioClip BGM_Menu;
    public AudioClip BGM_IntroInGame;
    public AudioClip BGM_InGame;
    public AudioClip BGM_IntroBoss;
    public AudioClip BGM_Boss;

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
    private AudioClip nextBGM;
    public float audioFadeOutFactor = 0.6f;

    //Events
    public void playBGM_Main() { playIntroBGM(BGM_IntroInGame); }
    public void playBGM_Boss() { playIntroBGM(BGM_IntroBoss); }
    public void playSFX_WaveWon() { playSFX(SFX_VictoryFanfare); }
    public void playSFX_SwordAttack() { playSFX(SFX_SwordAttack); }
    public void playSFX_NerfShoot() { playSFX(SFX_NerfShoot); }
    public void playSFX_TookDamage() { playSFX(SFX_ChildDamaged); }
    public void playSFX_Jump() { playSFX(SFX_ChildJump); }
    public void playSFX_SwordHit() { playSFX(SFX_SwordHit); }
    public void playSFX_NerfHit() { playSFX(SFX_NerfHit); }
    public void playSFX_LazerShoot() { playSFX(SFX_LazerShoot); }
    public void playSFX_ButtonSelect() { playSFX(SFX_ButtonSelect); }

    //Start
    private void Start()
    {
        //Get Audio Source
        bgmAudioSource = this.GetComponent<AudioSource>();

        //Setup Events
        GameEvents.GameState.StartGame += playBGM_Main;
        GameEvents.GameState.StartBoss += playBGM_Boss;
        GameEvents.GameState.WaveWon += playSFX_WaveWon;
        GameEvents.PlayerAction.SwordAttack += playSFX_SwordAttack;
        GameEvents.PlayerAction.NerfShoot += playSFX_NerfShoot;
        GameEvents.PlayerAction.TookDamage += playSFX_TookDamage;
        GameEvents.PlayerAction.Jump += playSFX_Jump;
        GameEvents.EnemyAction.SwordHit += playSFX_SwordHit;
        GameEvents.EnemyAction.NerfHit += playSFX_NerfHit;
        GameEvents.EnemyAction.LazerShoot += playSFX_LazerShoot;
        GameEvents.Menu.buttonSelect += playSFX_ButtonSelect;
    }

    private void OnDestroy()
    {
        GameEvents.GameState.StartGame -= playBGM_Main;
        GameEvents.GameState.StartBoss -= playBGM_Boss;
        GameEvents.GameState.WaveWon -= playSFX_WaveWon;
        GameEvents.PlayerAction.SwordAttack -= playSFX_SwordAttack;
        GameEvents.PlayerAction.NerfShoot -= playSFX_NerfShoot;
        GameEvents.PlayerAction.TookDamage -= playSFX_TookDamage;
        GameEvents.PlayerAction.Jump -= playSFX_Jump;
        GameEvents.EnemyAction.SwordHit -= playSFX_SwordHit;
        GameEvents.EnemyAction.NerfHit -= playSFX_NerfHit;
        GameEvents.EnemyAction.LazerShoot -= playSFX_LazerShoot;
        GameEvents.Menu.buttonSelect -= playSFX_ButtonSelect;
    }

    private void playIntroBGM(AudioClip bgm)
    {
        if(bgmAudioSource.isPlaying) StartCoroutine(changeMusicWithFade(bgm));
        else if (bgm != null)
        {
            bgmAudioSource.clip = bgm;
            bgmAudioSource.loop = false;
            bgmAudioSource.Play();
        }
    }

    private void playBGM()
    {
        bgmAudioSource.clip = nextBGM;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
        nextBGM = null;
    }

    private void playSFX(AudioClip sfx)
    {
        if(sfx != null)
        {
            AudioSource.PlayClipAtPoint(sfx, Vector3.zero);
        }
    }

    //Change Music with Fade
    private IEnumerator changeMusicWithFade(AudioClip musicClip)
    {
        while (bgmAudioSource.volume > 0f)
        {
            bgmAudioSource.volume -= audioFadeOutFactor * Time.deltaTime;
            yield return 0;
        }

        //Change Music
        bgmAudioSource.Stop();
        playIntroBGM(musicClip);
    }

    private void Update()
    {
        if(nextBGM != null)
        {
            if (!bgmAudioSource.isPlaying) playBGM();
        }
    }
}
