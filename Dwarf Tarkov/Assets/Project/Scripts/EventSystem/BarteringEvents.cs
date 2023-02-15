using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarteringEvents : MonoBehaviour
{
    public delegate bool HasEnoughCreditsEvent(int credits);
    public delegate void CreditsEvent(int credits);

    public HasEnoughCreditsEvent OnCheckIfPlayerHasEnoughCredits;

    public CreditsEvent OnPlayerBuysItem;
}
