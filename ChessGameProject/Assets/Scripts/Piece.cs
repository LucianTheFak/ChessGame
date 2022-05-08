using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public Board board { protected get; set; }
    public Vector2Int occupiedSquare{ get; set; }
    public bool hasMoved { get; private set; }
    public PieceInfo.TeamColour team { get; set; }
    public List<Vector2Int> availableMoves;

    public abstract List<Vector2Int> SelectAvailableMoves();

    private void Awake()
    {
        availableMoves = new List<Vector2Int>();
    }

    public bool isFromSameTeam(Piece piece)
    {
        return team == piece.team;
    }

    public bool CanMoveTo(Vector2Int position)
    {
        return availableMoves.Contains(position);
    }

    public virtual void MovePiece(Vector2Int position)
    {

    }

    protected void TryToAddMove(Vector2Int position)
    {
        availableMoves.Add(position);
    }

    public void SetData(Vector2Int position, PieceInfo.TeamColour team, Board board)
    {
        this.team = team;
        occupiedSquare = position;
        this.board = board;
        transform.position = board.SetPosition(position);
    }
}
