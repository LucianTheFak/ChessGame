using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private float squareSize;
    [SerializeField] private Transform bottomLeftSquareTransform;
    internal Vector3 SetPosition(Vector2Int position)
    {
        return bottomLeftSquareTransform.position + new Vector3(position.x * squareSize, 0, position.y * squareSize);
    }
}
