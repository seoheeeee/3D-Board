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
    public Dictionary<Direction, Node> node;

    private void Start()
    {
        foreach (Node item in nextNode)
        {
            node.Add(item.direction, item);
        }
    }

}
