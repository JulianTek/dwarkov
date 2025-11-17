using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    [SerializeField]
    protected float hearingRadius = 5f;
    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D heardEvent = Physics2D.OverlapCircle(transform.position, hearingRadius, LayerMask.GetMask("Sound"));
        if (heardEvent != null)
        {
            if (heardEvent.gameObject.GetComponent<SoundEmitter>())
            {
                OnHeardEvent(heardEvent.transform);
            }
        }
    }

    protected virtual void OnHeardEvent(Transform transform) { }
}
