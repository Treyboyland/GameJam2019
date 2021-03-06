﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameEvents
{
    public class Player
    {
        [Serializable]
        public class CollectedCoin : UnityEvent<int> { }

        [Serializable]
        public class UpdateScore : UnityEvent<int> { }

        [Serializable]
        public class CollectedJumpPowerUp : UnityEvent { }

        [Serializable]
        public class PlayerJumped : UnityEvent { }


    }

    public class GameScore
    {
        [Serializable]
        public class SetScore : UnityEvent<int> { }
    }

    public class SystemEvents
    {
        [Serializable]
        public class GameComplete : UnityEvent { }

        [Serializable]
        public class GoToSecretRoom : UnityEvent { }

        [Serializable]
        public class MuteBackgroundAudio : UnityEvent { }

        [Serializable]
        public class PlayAlienTheme : UnityEvent { }
    }
}
