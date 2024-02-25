using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int xDim;
    public int yDim;
    public GameObject piecePrefab;
    public GameObject emptyPrefab;
    public GameObject pieceBackground;
    private GameObject piece;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < xDim; x++)
        {
            for(int y = 0; y < yDim; y++)
            {
               piece= (GameObject)Instantiate(pieceBackground, GetWorldPosition(x, y), Quaternion.identity);
            }   
        }
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
               piece = (GameObject)Instantiate(emptyPrefab, GetWorldPosition(x, y), Quaternion.identity);
            }
        }
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                if(piece.GetComponent<PieceBool>().empty)
                {
                    piece = (GameObject)Instantiate(piecePrefab, GetWorldPosition(x, y), Quaternion.identity);
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(transform.position.x - xDim / 2.0f + x, transform.position.y + yDim / 2.0f - y);
    }
  
}
