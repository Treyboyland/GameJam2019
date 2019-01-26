using System.Collections;
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
    }
}
