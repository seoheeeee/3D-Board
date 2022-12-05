using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{



    #region Old Dice
    //[SerializeField] Rigidbody rb;
    //[SerializeField] DiceSide[] diceSides;

    //[SerializeField] bool hasLanded;
    //[SerializeField] bool thrown;
    //Vector3 initPostion;
    //public int diceValue;

    //private void Start()
    //{
    //    initPostion = transform.position;

    //    rb.useGravity = false;

    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        RollDice();
    //    }
    //    if(rb.IsSleeping() && !hasLanded && thrown)
    //    {
    //        hasLanded = true;
    //        rb.useGravity = false;
    //    }
    //    else if(rb.IsSleeping() && !hasLanded && thrown)
    //    {
    //        RollAgain();
    //    }
    //}

    //void RollDice()
    //{
    //    if(!thrown && !hasLanded)
    //    {
    //        thrown = true;
    //        rb.useGravity = true;
    //        //transform.rotation = Quaternion.identity;
    //        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    //        rb.AddForce(new Vector3(0, 70, 0), ForceMode.Impulse);

    //    }
    //    else if(thrown && hasLanded)
    //    {
    //        DiceReset();
    //    }
    //}
    //void DiceReset()
    //{
    //    transform.position = initPostion;
    //    thrown = false;
    //    hasLanded = false;
    //    rb.useGravity = false;
    //}
    //void RollAgain()
    //{
    //    DiceReset();
    //    thrown = true;
    //    rb.useGravity = true;
    //    //rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    //    rb.AddForce(new Vector3(0, 70, 0),ForceMode.Impulse);

    //}

    //void SideValueCheck()
    //{
    //    diceValue = 0;

    //    foreach (DiceSide item in diceSides)
    //    {
    //        if (item.OnGround)
    //        {
    //            diceValue = item.sideValue;
    //            Debug.Log("Dice Value " + diceValue);
    //        }
    //    }
    //}
    #endregion
}
