using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] piecePrefabs;

    public GameObject CreatePiece(PieceInfo.PieceType pieceType, PieceInfo.TeamColour team)
    {
        GameObject prefab = null;
        switch (pieceType)
        {
            case PieceInfo.PieceType.Pawn:
                if(team == PieceInfo.TeamColour.White) prefab = piecePrefabs[0];
                else prefab = piecePrefabs[1];
                break;
            case PieceInfo.PieceType.Knight:
                if (team == PieceInfo.TeamColour.White) prefab = piecePrefabs[2];
                else prefab = piecePrefabs[3];
                break;
            case PieceInfo.PieceType.Bishop:
                if (team == PieceInfo.TeamColour.White) prefab = piecePrefabs[4];
                else prefab = piecePrefabs[5];
                break;
            case PieceInfo.PieceType.Rook:
                if (team == PieceInfo.TeamColour.White) prefab = piecePrefabs[6];
                else prefab = piecePrefabs[7];
                break;
            case PieceInfo.PieceType.Queen:
                if (team == PieceInfo.TeamColour.White) prefab = piecePrefabs[8];
                else prefab = piecePrefabs[9];
                break;
            case PieceInfo.PieceType.King:
                if (team == PieceInfo.TeamColour.White) prefab = piecePrefabs[10];
                else prefab = piecePrefabs[11];
                break;
            case PieceInfo.PieceType.None:
                return null;
        }

        if (prefab)
        {
            GameObject newPiece = Instantiate(prefab);
            return newPiece;
        }

        return null;
    }
}
