using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Board/Layout")]
public class BoardLayout : ScriptableObject
{
    [Serializable]
    private class BoardSetup
    {
        public Vector2Int position;
        public PieceInfo.PieceType pieceType;
        public PieceInfo.TeamColour teamColour;
    }

    [SerializeField]
    private BoardSetup[] boardSquares;

    public int GetPiecesCount()
    {
        return boardSquares.Length;
    }

    public Vector2Int GetSquarePosition(int index)
    {
        if(boardSquares.Length <= index)
        {
            return new Vector2Int(-1, -1); 
        }

        return new Vector2Int(boardSquares[index].position.x - 1, boardSquares[index].position.y - 1);
    }

    public PieceInfo.PieceType GetPieceTypeOnSquare(int index)
    {
        if (boardSquares.Length <= index)
        {
            return PieceInfo.PieceType.None;
        }

        return boardSquares[index].pieceType;
    }

    public PieceInfo.TeamColour GetTeamColourOnSquare(int index)
    {
        if (boardSquares.Length <= index)
        {
            return PieceInfo.TeamColour.Black;
        }

        return boardSquares[index].teamColour;
    }
}
