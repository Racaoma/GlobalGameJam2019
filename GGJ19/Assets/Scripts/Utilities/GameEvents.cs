using UnityEngine;
using System;

public static class GameEvents
{
    public static class GameState
    {
        public static Action StartGame;
        public static Action StartBoss;
        public static Action WaveWon;
        public static Action WaveLose;
        public static Action StartLevel;
        public static Action Victory;
        public static Action Tutorial;
    }

    public static class PlayerAction
    {
        public static Action SwordAttack;
        public static Action NerfShoot;
        public static Action TookDamage;
        public static Action Jump;
    }

    public static class EnemyAction
    {
        public static Action SwordHit;
        public static Action NerfHit;
        public static Action LazerShoot;
    }

    public static class Menu
    {
        public static Action buttonSelect;
    }
}
