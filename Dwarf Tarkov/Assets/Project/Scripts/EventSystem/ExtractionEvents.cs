using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionEvents
{
    public delegate void ExtractionTimerStartEvent(int duration);
    public delegate void ExtractionTimerEvent();
    public delegate void ExtractionValueEvent(int inventoryValue);
    public delegate void ExtractionEvent();

    public ExtractionTimerStartEvent OnStartExtractionTimer;
    public ExtractionTimerEvent OnExtractionTimerFinished;
    public ExtractionTimerEvent OnStopExtractionTimer;

    public ExtractionEvent OnGetInventoryValue;
    public ExtractionValueEvent OnSetInventoryValue;

}
