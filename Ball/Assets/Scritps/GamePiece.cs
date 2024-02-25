using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace hyperCasual
{


    public class GamePiece : MonoBehaviour
    {
        private int x;
        private int y;
        public int X { get { return x; }
            set { if (IsMovable()) { x = value; } } }
        public int Y { get { return y; }
            set { if (IsMovable()) { y = value; } }
        }
        private Grid.PieceType type;
        public Grid.PieceType Type { get { return type; } }
        private Grid grid;
        public Grid GridRef { get { return grid; } }
        private MovablePiece movableComponent;
        public MovablePiece MovableComponent { get {  return movableComponent; } }
        private ColorPiece colorComponent;
        public ColorPiece ColorComponent { get { return colorComponent;} }

        private ClearablePiece clearComponent;
        public ClearablePiece ClearComponent { get { return clearComponent;} }

        private void Awake()
        {
            movableComponent = gameObject.GetComponent<MovablePiece>();
            colorComponent = gameObject.GetComponent<ColorPiece>();
            clearComponent = gameObject.GetComponent<ClearablePiece>(); 
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnMouseEnter()
        {
            grid.EnterPiece(this);
        }
        private void OnMouseDown()
        {
            grid.PressPiece(this);
        }
        private void OnMouseUp()
        {
            grid.ReleasePiece();
        }
        public void Init(int _x, int _y, Grid _grid, Grid.PieceType _type)
        {
            x = _x;
            y = _y;
            grid = _grid;
            type = _type;
        }
        public bool IsMovable()
        {
            return movableComponent!=null;
        }
        public bool IsColored()
        {
            return colorComponent!=null;
        }
        public bool IsClearable()
        {
            return clearComponent!=null;
        }
    }
}
