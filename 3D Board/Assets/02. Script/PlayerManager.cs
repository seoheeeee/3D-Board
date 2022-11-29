using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text numTxt;
    [SerializeField]
    int num;

    public int Num { 
        
        get => num;

        set 
        {
            num = value;
            numTxt.text = value.ToString();
        }
    }

}
