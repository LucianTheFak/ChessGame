using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }

    [SerializeField] private BoardLayout startingBoardLayout;
    [SerializeField] private Board board;

    private PieceCreator pieceCreator;

    private Camera mainCamera;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
        pieceCreator = GetComponent<PieceCreator>();

        CreatePiecesFromLayout(startingBoardLayout);
    }

    private void CreatePiecesFromLayout(BoardLayout layout)
    {
        for(int i = 0; i < layout.GetPiecesCount(); i++)
        {
            Vector2Int squarePosition = layout.GetSquarePosition(i);
            PieceInfo.TeamColour team = layout.GetTeamColourOnSquare(i);
            PieceInfo.PieceType pieceType = layout.GetPieceTypeOnSquare(i);

            SpawnPiece(squarePosition, team, pieceType);
        }
    }

    private void SpawnPiece(Vector2Int squarePosition, PieceInfo.TeamColour team, PieceInfo.PieceType pieceType)
    {
        Piece newPiece = pieceCreator.CreatePiece(pieceType, team).GetComponent<Piece>();
        newPiece.SetData(squarePosition, team, board);
    }
}
