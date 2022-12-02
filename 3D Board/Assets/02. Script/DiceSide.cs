using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    private bool onGround;
    public int sideValue;

    public bool OnGround 
    { 
        get => onGround; 
        private set => onGround = value; 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
            OnGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
            OnGround = false;
    }

}
