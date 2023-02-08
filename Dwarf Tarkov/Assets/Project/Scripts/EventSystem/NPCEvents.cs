using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEvents
{
    public delegate void NPCNameEvent(string name);

    public NPCNameEvent OnStartDialogue;
}
