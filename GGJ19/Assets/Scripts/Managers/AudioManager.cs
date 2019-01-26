using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
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
    public AudioClip SFX_BodyFall;
    public AudioClip SFX_ChildDamaged;
    public AudioClip SFX_ChildJump;
    public AudioClip SFX_LazerShoot;
    public AudioClip SFX_VictoryFanfare;

    [Header("Menus SFX")]
    public AudioClip SFX_MenuSelect;

    //Control Variables
    private AudioClip nextBGM;
    public float audioFadeOutFactor = 0.6f;

    //Start
    private void Start()
    {
        GameEvents.SFX.StartBGM += playIntroBGM;
        GameEvents.SFX.WaveWon += playSFX;
        GameEvents.SFX.SwordAttack += playSFX;
        GameEvents.SFX.NerfShoot += playSFX;
        GameEvents.SFX.BodyFall += playSFX;
        GameEvents.SFX.TookDamage += playSFX;
        GameEvents.SFX.SwordHit += playSFX;
        GameEvents.SFX.NerfHit += playSFX;
        GameEvents.SFX.LazerShoot += playSFX;
        GameEvents.SFX.buttonSelect += playSFX;
    }

    private void OnDestroy()
    {
        GameEvents.SFX.StartBGM -= playIntroBGM;
        GameEvents.SFX.WaveWon -= playSFX;
        GameEvents.SFX.SwordAttack -= playSFX;
        GameEvents.SFX.NerfShoot -= playSFX;
        GameEvents.SFX.BodyFall -= playSFX;
        GameEvents.SFX.TookDamage -= playSFX;
        GameEvents.SFX.SwordHit -= playSFX;
        GameEvents.SFX.NerfHit -= playSFX;
        GameEvents.SFX.LazerShoot -= playSFX;
        GameEvents.SFX.buttonSelect -= playSFX;
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
