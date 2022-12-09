using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Straight,
    Fork,
    Left,
    Right,
}

public class Node : MonoBehaviour
{
    public Direction direction;
    public List<Node> nextNode;
}
