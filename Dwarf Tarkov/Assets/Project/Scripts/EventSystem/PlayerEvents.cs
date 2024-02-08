using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents
{
    public delegate void PlayerEvent();

    public PlayerEvent OnPlayerDeath;
}
