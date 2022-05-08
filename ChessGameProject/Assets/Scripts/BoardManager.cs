using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; private set; }

    public Piece[,] pieces { get; set; }
    private Piece selectedPiece;
    public bool isWhiteTurn = true;

    private const float tileSize = 1.0f;
    private const float tileOffset = 0.5f;

    private int selectedTileX = -1;
    private int selectedTileY = -1;

    public List<GameObject> chessPiecesPrefabs;
    private List<GameObject> activeChessPieces;

    private Camera mainCamera;
    [SerializeField]
    private LayerMask boardLayer;

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

        SpawnInitialChessPieces();
    }

    void Update()
    {
        CheckSelection();
        DrawBoard();

        if (Input.GetMouseButtonDown(0))
        {
            if(selectedTileX >= 0 && selectedTileY >= 0)
            {
                if (!selectedPiece)
                {
                    SelectChessPiece(selectedTileX, selectedTileY);
                }
                else
                {
                    MoveChessPiece(selectedTileX, selectedTileY);
                }
            }
        }
    }

    private void SelectChessPiece(int x, int y)
    {
        if (!pieces[x, y]) return;

        if (pieces[x, y].isWhite != isWhiteTurn) return;

        selectedPiece = pieces[x, y];
    }

    private void MoveChessPiece(int x, int y)
    {
        if(selectedPiece.isLegalMove(x, y))
        {
            pieces[selectedPiece.positionX, selectedPiece.positionY] = null;
            selectedPiece.transform.position = GetTileCenter(x, y);
            selectedPiece.SetPosition(x, y);
            pieces[x, y] = selectedPiece;
            isWhiteTurn = !isWhiteTurn;
        }

        selectedPiece = null;
    }

    private void CheckSelection()
    {
        if (!mainCamera) return;

        RaycastHit hit;
        if(Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, boardLayer))
        {
            selectedTileX = (int)hit.point.x;
            selectedTileY = (int)hit.point.z;
        }
        else
        {
            selectedTileX = -1;
            selectedTileY = -1;
        }
    }

    private void SpawnChessPiece(int index, int x, int y)
    {
        GameObject go = Instantiate(chessPiecesPrefabs[index], GetTileCenter(x, y), Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        pieces[x, y] = go.GetComponent<Piece>();
        pieces[x, y].SetPosition(x, y);
        activeChessPieces.Add(go);
    }

    private void SpawnInitialChessPieces()
    {
        activeChessPieces = new List<GameObject>();
        pieces = new Piece[8, 8];

        //Kings
        SpawnChessPiece(0, 4, 0);
        SpawnChessPiece(6, 4, 7);

        //Queens
        SpawnChessPiece(1, 3, 0);
        SpawnChessPiece(7, 3, 7);

        //Rooks
        SpawnChessPiece(2, 0, 0);
        SpawnChessPiece(2, 7, 0);

        SpawnChessPiece(8, 0, 7);
        SpawnChessPiece(8, 7, 7);

        //Bishops
        SpawnChessPiece(3, 2, 0);
        SpawnChessPiece(3, 5, 0);

        SpawnChessPiece(9, 2, 7);
        SpawnChessPiece(9, 5, 7);

        //Knights
        SpawnChessPiece(4, 1, 0);
        SpawnChessPiece(4, 6, 0);

        SpawnChessPiece(10, 1, 7);
        SpawnChessPiece(10, 6, 7);

        //Pawns
        for (int i = 0; i < 8; i++) SpawnChessPiece(5, i, 1);
        for (int i = 0; i < 8; i++) SpawnChessPiece(11, i, 6);
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (tileSize * x) + tileOffset;
        origin.z += (tileSize * y) + tileOffset;

        return origin;
    }

    private void DrawBoard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }

        if(selectedTileX >= 0 && selectedTileY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectedTileY + Vector3.right * selectedTileX, 
                Vector3.forward * (selectedTileY + 1) + Vector3.right * (selectedTileX + 1));

            Debug.DrawLine(
                Vector3.forward * (selectedTileY + 1) + Vector3.right * selectedTileX,
                Vector3.forward * selectedTileY + Vector3.right * (selectedTileX + 1));
        }
    }
}
