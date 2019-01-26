using UnityEngine;
using System;

public static class GameEvents
{
    public static class GameState
    {
        public static Action OnStartGame;
        public static Action WaveWon;
    }

    public static class PlayerActions
    {
        public static Action SwordAttack;
        public static Action NerfShoot;
        public static Action BodyFall;
        public static Action TookDamage;
    }

    public static class Enemies
    {
        public static Action SwordHit;
        public static Action NerfHit;
        public static Action LazerShoot;
    }

    public static class UI
    {
        public static Action buttonSelect;
    }
}
