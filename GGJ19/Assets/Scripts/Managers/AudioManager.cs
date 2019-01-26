using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("BGM")]
    public AudioClip BGM_Intro;
    public AudioClip BGM_InGame;
    public AudioClip BGM_Menu;

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
}
