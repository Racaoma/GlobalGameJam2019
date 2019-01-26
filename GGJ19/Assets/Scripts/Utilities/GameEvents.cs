using UnityEngine;
using System;

public static class GameEvents
{
    public static class GameState
    {
        public static Action OnStartGame;
        public static Action OnEndGame;
    }

    public static class PlayerActions
    {
        public static Action SwordAttack;
        public static Action NerfShoot;
    }
}
