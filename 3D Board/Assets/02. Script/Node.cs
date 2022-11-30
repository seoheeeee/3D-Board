using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    public enum Direction
    {
        Left,
        Right
    }
    public Direction direction;
    public List<Node> nextNode;

}
