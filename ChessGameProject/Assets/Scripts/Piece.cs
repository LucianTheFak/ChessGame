using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public int positionX { set; get; }
    public int positionY { set; get; }
    public bool isWhite;

    public void SetPosition(int x, int y)
    {
        positionX = x;
        positionY = y;
    }

    public virtual bool isLegalMove(int x, int y)
    {
        return true;
    }
}
