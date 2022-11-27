using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FoW_2 : MonoBehaviour
{
    public class Cell
    {
        public int x, y;
        public Vector3Int position;
        public bool hasCollider, seenCollider, visited;
        public int distance;

        public Cell(int x, int y, Vector3Int pos, bool hasColl)
        {
            this.x = x;
            this.y = y;
            this.position = pos;
            this.hasCollider = hasColl;
            this.visited = false;
        }

        public void SetVisited()
        {
            this.visited = true;
        }
    }

    Cell[,] graph = new Cell[32, 32];
    bool[,] toVisit = new bool[32, 32];
    int[,] distance = new int[32, 32];

    public Tilemap fogMap;
    public Tilemap collisionMap;
    public TileBase collisionTile;
    public TileBase fogTile;
    public TileBase borderTile;
    public GameObject player;

    GridLayout gridLayout;

    void Start()
    {
        gridLayout = this.GetComponent<GridLayout>();
    }


    void Update()
    {
        fogMap.ClearAllTiles();

        // Get Closest Position of Player in Tilemap
        Vector3Int playerCell = gridLayout.LocalToCell(player.transform.localPosition);
        //Debug.Log(playerCell);

        // Build Graph of Cells around Player
        Vector3Int currentCell;
        for (int i = 0; i < 32; i++)
        {
            for (int j = 0; j < 32; j++)
            {
                currentCell = playerCell - new Vector3Int(i - 12, j - 12, 0);
                graph[i, j] = new Cell(i, j, currentCell, collisionMap.GetTile(currentCell) == collisionTile);
                toVisit[i, j] = false;
                distance[i, j] = 100;
            }
        }

        // Recursive Check Every Grid Cell
        toVisit[12, 12] = true;
        distance[12, 12] = 0;
        Visit(graph[12, 12], graph, toVisit, distance);

        // Set Darkness
        //Debug.Log("Setting Darkness");
        for (int i = 0; i < 32; i++)
        {
            for (int j = 0; j < 32; j++)
            {
                if (distance[i,j] == 7)
                {
                    fogMap.SetTile(graph[i, j].position, borderTile);
                } else if (distance[i,j] >= 8)
                {
                    fogMap.SetTile(graph[i, j].position, fogTile);
                }
            }
        }
    }

    void Visit(Cell currentCell, Cell[,] graph, bool[,] toVisit, int[,] distance)
    {
        // Get x,y of currentCell
        int x = currentCell.x;
        int y = currentCell.y;

        // Add Check Neighbors
        // Check Left
        if (x - 1 >= 0)
        { // In Bounds
            if (!graph[x-1, y].visited)
            { // Not Finalized
                toVisit[x - 1, y] = true;
                if (graph[x-1,y].hasCollider && !graph[x,y].hasCollider && distance[x-1,y] < 6)
                {
                    distance[x - 1, y] = 6;
                } else
                {
                    distance[x - 1, y] = distance[x, y] + 1;
                }

            }
        }
        // Check Right
        if (x + 1 < 32)
        { // In Bounds
            if (!graph[x + 1, y].visited)
            { // Not Finalized
                toVisit[x + 1, y] = true;
                if (graph[x + 1, y].hasCollider && !graph[x, y].hasCollider && distance[x + 1, y] < 6)
                {
                    distance[x + 1, y] = 6;
                }
                else
                {
                    distance[x + 1, y] = distance[x, y] + 1;
                }

            }
        }
        // Check Up
        if (y - 1 >= 0)
        { // In Bounds
            if (!graph[x, y - 1].visited)
            { // Not Finalized
                toVisit[x, y - 1] = true;
                if (graph[x, y - 1].hasCollider && !graph[x, y].hasCollider && distance[x, y - 1] < 6)
                {
                    distance[x, y - 1] = 6;
                }
                else
                {
                    distance[x, y - 1] = distance[x, y] + 1;
                }

            }
        }
        // Check Down
        if (y + 1 < 32)
        { // In Bounds
            if (!graph[x, y + 1].visited)
            { // Not Finalized
                toVisit[x, y + 1] = true;
                if (graph[x, y + 1].hasCollider && !graph[x, y].hasCollider && distance[x, y + 1] < 6)
                {
                    distance[x, y + 1] = 6;
                }
                else
                {
                    distance[x, y + 1] = distance[x, y] + 1;
                }

            }
        }

        toVisit[x, y] = false;
        currentCell.SetVisited();
        Cell nextCell = GetClosestNeighbor(graph, toVisit, distance);
        if (nextCell != null)
        {
            Visit(nextCell, graph, toVisit, distance);
        }
    }


    Cell GetClosestNeighbor(Cell[,] graph, bool[,] toVisit, int[,] distance)
    {
        int shortDist = 100, shortX = -1, shortY = -1;

        for (int i = 0; i < 32; i++)
        {
            for (int j = 0; j < 32; j++)
            {
                if (toVisit[i,j] && distance[i,j] < shortDist)
                {
                    shortDist = distance[i, j];
                    shortX = i;
                    shortY = j;
                }
            }
        }

        if (shortX == -1)
        {
            return null;
        } else
        {
            return graph[shortX, shortY];
        }
    }
}
