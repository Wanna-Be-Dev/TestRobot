using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Finish : MonoBehaviour
{
    public UnityEvent OnFinishReached;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            OnFinishReached.Invoke();

    }
}
