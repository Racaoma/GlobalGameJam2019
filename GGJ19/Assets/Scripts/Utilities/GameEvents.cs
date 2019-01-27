using UnityEngine;
using System;

public static class GameEvents
{
    public static class SFX
    {
        public static Action<AudioClip> StartBGM;
        public static Action<AudioClip> WaveWon;
        public static Action<AudioClip> SwordAttack;
        public static Action<AudioClip> NerfShoot;
        public static Action<AudioClip> BodyFall;
        public static Action<AudioClip> TookDamage;
        public static Action<AudioClip> SwordHit;
        public static Action<AudioClip> NerfHit;
        public static Action<AudioClip> LazerShoot;
        public static Action<AudioClip> buttonSelect;
    }
}
