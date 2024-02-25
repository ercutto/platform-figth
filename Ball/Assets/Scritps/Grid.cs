using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace hyperCasual
{
    public class Grid : MonoBehaviour
    {
        public enum PieceType
        {
            EMPTY,
            NORLMAL,
            BUBBLE,
            COUNT,
        };
        [System.Serializable]
        public struct PiecePrefab
        {
            public PieceType type;
            public GameObject prefab;
        };
        public int xDim;
        public int yDim;
        public float fillTime;
        public PiecePrefab[] piecePrefabs;
        public GameObject backgroundPrefab;
        private Dictionary<PieceType, GameObject> piecesPrefabDict;
        private GamePiece[,] pieces;
        private bool inverse = false;

        private GamePiece pressedPiece;
        private GamePiece enteredPiece;
        // Start is called before the first frame update
        public void Start()
        {
            piecesPrefabDict = new Dictionary<PieceType, GameObject>();
            for (int i = 0; i < piecePrefabs.Length; i++)
            {
                if (!piecesPrefabDict.ContainsKey(piecePrefabs[i].type))
                {
                    piecesPrefabDict.Add(piecePrefabs[i].type, piecePrefabs[i].prefab);
                }
            }
            for (int x = 0; x < xDim; x++)
            {
                for (int y = 0; y < yDim; y++)
                {
                    GameObject background = (GameObject)Instantiate(backgroundPrefab, GetWorldPosition(x, y), Quaternion.identity);
                    background.transform.parent = transform;
                }
            }

            pieces = new GamePiece[xDim, yDim];
            for (int x = 0; x < xDim; x++)
            {
                for (int y = 0; y < yDim; y++)
                {
                    SpawnNewPiece(x, y, PieceType.EMPTY);
                }
            }

            Destroy(pieces[1, 4].gameObject);
            SpawnNewPiece(1, 4, PieceType.BUBBLE);

            Destroy(pieces[2, 4].gameObject);
            SpawnNewPiece(2, 4, PieceType.BUBBLE);

            Destroy(pieces[3, 4].gameObject);
            SpawnNewPiece(3, 4, PieceType.BUBBLE);

            Destroy(pieces[5, 4].gameObject);
            SpawnNewPiece(5, 4, PieceType.BUBBLE);

            Destroy(pieces[6, 4].gameObject);
            SpawnNewPiece(6, 4, PieceType.BUBBLE);

            Destroy(pieces[7, 4].gameObject);
            SpawnNewPiece(7, 4, PieceType.BUBBLE);

            Destroy(pieces[4, 0].gameObject);
            SpawnNewPiece(4, 0, PieceType.BUBBLE);

            StartCoroutine(nameof(Fill));
        }


        // Update is called once per frame
        void Update()
        {

        }
        public IEnumerator Fill()
        {
            bool needsRefill = true;
            while(needsRefill){
                yield return new WaitForSeconds(fillTime);

                while (FillStep())
                {
                    inverse = !inverse;
                    yield return new WaitForSeconds(fillTime);
                }

                needsRefill = ClearAllValidMatches();
            }
        }
        public bool FillStep()
        {
            bool movedPiece = false;
            for (int y = yDim - 2; y >= 0; y--)
            {
                for (int loopX = 0; loopX < xDim; loopX++)
                {
                    int x = loopX;
                    if (inverse)
                    {
                        x = xDim - 1 - loopX;
                    }

                    GamePiece piece = pieces[x, y];
                    if (piece.IsMovable())
                    {
                        GamePiece pieceBelow = pieces[x, y + 1];
                        if (pieceBelow.Type == PieceType.EMPTY)
                        {
                            piece.MovableComponent.Move(x, y + 1, fillTime);
                            pieces[x, y + 1] = piece;
                            SpawnNewPiece(x, y, PieceType.EMPTY);
                            movedPiece = true;
                        }
                        else
                        {
                            for (int diag = -1; diag <= 1; diag++)
                            {
                                if (diag != 0)
                                {
                                    int diagX = x + diag;
                                    if (inverse)
                                    {
                                        diagX = x - diag;
                                    }

                                    if (diagX >= 0 && diagX < xDim)
                                    {
                                        GamePiece diagonalPiece = pieces[diagX, y + 1];

                                        if (diagonalPiece.Type == PieceType.EMPTY)
                                        {
                                            bool hasPieceAbove = true;

                                            for (int aboveY = y; aboveY >= 0; aboveY--)
                                            {
                                                GamePiece pieceAbove = pieces[diagX, aboveY];
                                                if (pieceAbove.IsMovable())
                                                {
                                                    break;
                                                }
                                                else if (!pieceAbove.IsMovable() && pieceAbove.Type != PieceType.EMPTY)
                                                {
                                                    hasPieceAbove = false;
                                                    break;
                                                }
                                            }

                                            if (!hasPieceAbove)
                                            {
                                                Destroy(diagonalPiece.gameObject);
                                                piece.MovableComponent.Move(diagX, y + 1, fillTime);
                                                pieces[diagX, y + 1] = piece;
                                                SpawnNewPiece(x, y, PieceType.EMPTY);
                                                movedPiece = true;
                                                break;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            for (int x = 0; x < xDim; x++)
            {
                GamePiece pieceBelow = pieces[x, 0];
                if (pieceBelow.Type == PieceType.EMPTY)
                {
                    GameObject newPiece = (GameObject)Instantiate(piecesPrefabDict[PieceType.NORLMAL], GetWorldPosition(x, -1), Quaternion.identity);
                    newPiece.transform.parent = transform;
                    pieces[x, 0] = newPiece.GetComponent<GamePiece>();
                    pieces[x, 0].Init(x, -1, this, PieceType.NORLMAL);
                    pieces[x, 0].MovableComponent.Move(x, 0, fillTime);
                    pieces[x, 0].ColorComponent.SetColor((ColorPiece.ColorType)Random.Range(0, pieces[x, 0].ColorComponent.NumColor));
                    movedPiece = true;
                }
            }
            return movedPiece;
        }
        public GamePiece SpawnNewPiece(int x, int y, PieceType type)
        {

            GameObject newPiece = (GameObject)Instantiate(piecesPrefabDict[type], GetWorldPosition(x, y), Quaternion.identity);
            newPiece.transform.parent = transform;
            pieces[x, y] = newPiece.GetComponent<GamePiece>();
            pieces[x, y].Init(x, y, this, type);
            return pieces[x, y];

        }
        public Vector2 GetWorldPosition(int x, int y)
        {
            return new Vector2(transform.position.x - xDim / 2.0f + x, transform.position.y + yDim / 2.0f - y);
        }

        public bool IsAdjacent(GamePiece piece1, GamePiece piece2)
        {
            return (piece1.X == piece2.X && (int)Mathf.Abs(piece1.Y - piece2.Y) == 1) || (piece1.Y == piece2.Y && (int)Mathf.Abs(piece1.X - piece2.X) == 1);
        }
        public void SwapPieces(GamePiece piece1, GamePiece piece2)
        {
            if (piece1.IsMovable() && piece2.IsMovable())
            {
                pieces[piece1.X, piece1.Y] = piece2;
                pieces[piece2.X, piece2.Y] = piece1;

                if (GetMatch(piece1,piece2.X, piece2.Y)!=null||GetMatch(piece2,piece1.X,piece1.Y) !=null) {
                    int piece1X = piece1.X;
                    int piece1Y = piece1.Y;

                    piece1.MovableComponent.Move(piece2.X, piece2.Y, fillTime);
                    piece2.MovableComponent.Move(piece1X, piece1Y, fillTime);

                    ClearAllValidMatches();

                    StartCoroutine(Fill());
                }
                else
                {
                    pieces[piece1.X,piece1.Y] = piece1;
                    pieces[piece2.X, piece2.Y]= piece2;
                }

                

            }
        }
        public void PressPiece(GamePiece piece)
        {
            pressedPiece = piece;
        }
        public void EnterPiece(GamePiece piece)
        {
            enteredPiece = piece;
        }
        public void ReleasePiece()
        {
            if (IsAdjacent(pressedPiece, enteredPiece))
            {
                SwapPieces(pressedPiece, enteredPiece);
            }
        }
        public List<GamePiece> GetMatch(GamePiece piece, int newX, int newY)
        {
            if (piece.IsColored())
            {
                ColorPiece.ColorType color = piece.ColorComponent.Color;
                List<GamePiece> horizontalPieces = new List<GamePiece>();
                List<GamePiece> verticalPieces = new List<GamePiece>();
                List<GamePiece> matchingPiece = new List<GamePiece>();

                //horizontal Check
                horizontalPieces.Add(piece);

                for (int dir = 0; dir <= 1; dir++)
                {
                    for (int xOffset = 1; xOffset < xDim; xOffset++)
                    {
                        int x;
                        if (dir == 0)
                        {//going to Left
                            x = newX - xOffset;
                        }
                        else
                        {//going to Right
                            x = newX + xOffset;
                        }

                        if (x < 0 || x >= xDim)
                        {
                            break;
                        }

                        if (pieces[x, newY].IsColored() && pieces[x, newY].ColorComponent.Color == color)
                        {
                            horizontalPieces.Add(pieces[x, newY]);
                        }
                        else
                        {
                            break;
                        }

                    }
                }//Dogru 47 53

                if (horizontalPieces.Count >= 3)
                {

                    for (int i = 0; i < horizontalPieces.Count; i++)
                    {
                        matchingPiece.Add(horizontalPieces[i]);
                    }
                }
                //Dogru 48 10
                

                //LDirection matching list
                if (horizontalPieces.Count >= 3)
                {
                    for(int i = 0;i < horizontalPieces.Count; i++)
                    {
                        for (int dir = 0; dir <=1 ; dir++)
                        {
                            for(int yOffset = 1; yOffset < yDim; yOffset++)
                            {
                                int y;
                                if(dir == 0)
                                {
                                    y=newY - yOffset;
                                }
                                else
                                {
                                    y = newY + yOffset;
                                }

                                if(y< 0 || y >= yDim)
                                {
                                    break;
                                }

                                if (pieces[horizontalPieces[i].X, y].IsColored() && pieces[horizontalPieces[i].X, y].ColorComponent.Color == color)
                                {
                                    verticalPieces.Add(pieces[horizontalPieces[i].X, y]);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }

                        if(verticalPieces.Count < 2)
                        {
                            verticalPieces.Clear();
                        }
                        else
                        {
                            for (int j = 0; j < verticalPieces.Count; j++)
                            {
                                matchingPiece.Add(verticalPieces[j]);
                            }

                            break;
                        }
                    }
                }


                if (matchingPiece.Count >= 3)
                {
                    return matchingPiece;
                }

                horizontalPieces.Clear();
                verticalPieces.Clear();
                verticalPieces.Add(piece);

                for (int dir = 0; dir <= 1; dir++)
                {
                    for (int yOffset = 1; yOffset < xDim; yOffset++)
                    {
                        int y;
                        if (dir == 0)
                        {//going to Left
                            y = newY - yOffset;
                        }
                        else
                        {//going to Right
                            y = newY + yOffset;
                        }

                        if (y < 0 || y >= yDim)
                        {
                            break;
                        }

                        if (pieces[newX,y].IsColored() && pieces[newX, y].ColorComponent.Color == color)
                        {
                            verticalPieces.Add(pieces[newX, y]);
                        }
                        else
                        {
                            break;
                        }

                    }
                }

                if (verticalPieces.Count >= 3)
                {

                    for (int i = 0; i < verticalPieces.Count; i++)
                    {
                        matchingPiece.Add(verticalPieces[i]);
                    }
                }
                //horizontal L
                if (verticalPieces.Count >= 3)
                {
                    for (int i = 0; i < verticalPieces.Count; i++)
                    {
                        for (int dir = 0; dir <= 1; dir++)
                        {
                            for (int xOffset = 1; xOffset < xDim; xOffset++)
                            {
                                int x;
                                if (dir == 0)
                                {
                                    x = newX - xOffset;
                                }
                                else
                                {
                                    x = newX + xOffset;
                                }

                                if (x < 0 || x >= xDim)
                                {
                                    break;
                                }

                                if (pieces[x,verticalPieces[i].Y].IsColored() && pieces[x,verticalPieces[i].Y].ColorComponent.Color == color)
                                {
                                    verticalPieces.Add(pieces[x,verticalPieces[i].Y]);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }

                        if (horizontalPieces.Count < 2)
                        {
                            horizontalPieces.Clear();
                        }
                        else
                        {
                            for (int j = 0; j < horizontalPieces.Count; j++)
                            {
                                matchingPiece.Add(horizontalPieces[j]);
                            }

                            break;
                        }
                    }
                }

                if (matchingPiece.Count >= 3)
                {
                    return matchingPiece;
                }
            }

            return null;
        }

        public bool ClearAllValidMatches()
        {
            bool needsRefill=false;
            for (int y = 0; y < yDim; y++)
            {
                for (int x = 0; x < xDim; x++)
                {
                    if (pieces[x, y].IsClearable())
                    {
                        List<GamePiece> match = GetMatch(pieces[x, y], x, y);

                        if(match != null) {
                            for (int i = 0; i < match.Count; i++)
                            {
                                if (ClearPiece(match[i].X, match[i].Y)){
                                    needsRefill= true;
                                }
                            }   
                        }
                    }
                }
            }

            return needsRefill;
        }
        public bool ClearPiece(int x, int y)
        {
            if (pieces[x, y].IsClearable() && !pieces[x,y].ClearComponent.IsBeenCleared)
            {
                pieces[x, y].ClearComponent.Clear();
                SpawnNewPiece(x, y, PieceType.EMPTY);
                ClearObstacles(x,y);
                return true;
            }

            return false;
        }
        public void ClearObstacles(int x, int y)
        {
            for(int adjacentX=x-1;adjacentX <= x + 1; adjacentX++)
            {
                if(adjacentX !=x &&adjacentX>=0&&adjacentX<xDim)
                {
                    if (pieces[adjacentX,y].Type==PieceType.BUBBLE&& pieces[adjacentX, y].IsClearable())
                    {
                        pieces[adjacentX, y].ClearComponent.Clear();
                        SpawnNewPiece(adjacentX,y, PieceType.EMPTY);
                    }
                }
            }

            for (int adjacentY = y-1; adjacentY <=y+1; adjacentY++)
            {
                if (adjacentY != y && adjacentY >= 0 && adjacentY < yDim)
                {
                    if (pieces[x, adjacentY].Type == PieceType.BUBBLE && pieces[x, adjacentY].IsClearable())
                    {
                        pieces[x,adjacentY].ClearComponent.Clear();
                        SpawnNewPiece(x,y, PieceType.EMPTY);
                    }
                }
            }
        }
    }
}

