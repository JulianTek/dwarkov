using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents
{
    public delegate void HealthbarEvent(float vallue);

    public HealthbarEvent OnUpdateHealthbar;
}
