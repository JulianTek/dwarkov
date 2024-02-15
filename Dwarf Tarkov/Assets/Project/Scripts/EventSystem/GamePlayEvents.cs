using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEvents
{
    public delegate void GameplayEvent();

    public GameplayEvent OnPlayerResumesGame;

    public GameplayEvent OnPlayerExtracted;
}
