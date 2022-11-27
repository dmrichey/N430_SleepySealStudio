using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWar : MonoBehaviour
{
    
    public class Node {
        public int X;
        public int Y;
        public Vector3Int Position;
        public bool HasCollider;
        public bool Visited;
        // 0 - not dark, 1 - collider, 2 - border, 3+ - dark
        public int DistFromCollider;

        public Node(int x, int y, Vector3Int position, bool hasCollider)
        {
            X = x;
            Y = y;
            Position = position;
            HasCollider = hasCollider;
            Visited = false;
            DistFromCollider = 0;
        }

        public void SetVisited(int dist)
        {
            Visited = true;
            DistFromCollider = dist;
        }
    }
    

    Node[,] graph = new Node[33, 33];
    LinkedList<Node> toVisit = new LinkedList<Node>();
    
    public Tilemap fogMap;
    public Tilemap collisionMap;
    public TileBase collisionTile;
    public TileBase fogTile;
    public TileBase borderTile;
    public GameObject player;

    GridLayout gridLayout;

    void Start() {
        gridLayout = this.GetComponent<GridLayout>();      
    }

    
    void Update() {
        fogMap.ClearAllTiles();

        // Get Closest Position of Player in Tilemap
        Vector3Int playerCell = gridLayout.LocalToCell(player.transform.localPosition);
        //Debug.Log(playerCell);

        // Build Graph of Cells around Player
        Vector3Int currentCell;
        for (int i = 0; i < 33; i++) { 
            for (int j = 0; j < 33; j++) {
                currentCell = playerCell - new Vector3Int(i - 12, j - 12, 0);
                graph[i, j] = new Node(i, j, currentCell, collisionMap.GetTile(currentCell) == collisionTile);
            }
        }

        // Recursive Check Every Grid Cell
        LinkedListNode<Node> lln = new LinkedListNode<Node>(graph[12, 12]);
        toVisit.AddFirst(lln);
        //Debug.Log(toVisit);
        Visit(toVisit, graph);

        // Set Darkness
        //Debug.Log("Setting Darkness");
        for (int i = 0; i < 33; i++) {
            for (int j = 0; j < 33; j++) {
                if (graph[i,j].DistFromCollider >= 3)
                {
                    fogMap.SetTile(graph[i, j].Position, fogTile);
                } else if (graph[i, j].DistFromCollider == 2)
                {
                    fogMap.SetTile(graph[i, j].Position, borderTile);
                }
            }
        }
    }

    public void Visit(LinkedList<Node> toVisit, Node[,] graph)
    {
        // Get toVisit.First
        //Debug.Log(toVisit + ", " + toVisit.First);
        int x = toVisit.First.Value.X;
        int y = toVisit.First.Value.Y;
        bool hasCollision = toVisit.First.Value.HasCollider;
        int distance = toVisit.First.Value.DistFromCollider;

        // Check Left
        if (x - 1 >= 0) {
            if (!graph[x - 1, y].Visited) {
                toVisit.AddLast(graph[x - 1, y]);
                if (distance == 0 && hasCollision) {
                    graph[x - 1, y].SetVisited(1);
                } else if (distance > 0) {
                    graph[x - 1, y].SetVisited(distance + 1);
                } else {
                    graph[x - 1, y].SetVisited(0);
                }
            }
        }
        // Check Up
        if (y - 1 >= 0) {
            if (!graph[x, y - 1].Visited) {
                toVisit.AddLast(graph[x, y - 1]);
                if (distance == 0 && hasCollision)
                {
                    graph[x, y - 1].SetVisited(1);
                }
                else if (distance > 0)
                {
                    graph[x, y - 1].SetVisited(distance + 1);
                }
                else
                {
                    graph[x, y - 1].SetVisited(0);
                }
            }
        }
        // Check Right
        if (x + 1 < 25)
        {
            if (!graph[x + 1, y].Visited)
            {
                toVisit.AddLast(graph[x + 1, y]);
                if (distance == 0 && hasCollision)
                {
                    graph[x + 1, y].SetVisited(1);
                }
                else if (distance > 0)
                {
                    graph[x + 1, y].SetVisited(distance + 1);
                }
                else
                {
                    graph[x + 1, y].SetVisited(0);
                }
            }
        }
        // Check Down
        if (y + 1 < 25)
        {
            if (!graph[x, y + 1].Visited)
            {
                toVisit.AddLast(graph[x, y + 1]);
                if (distance == 0 && hasCollision)
                {
                    graph[x, y + 1].SetVisited(1);
                }
                else if (distance > 0)
                {
                    graph[x, y + 1].SetVisited(distance + 1);
                }
                else
                {
                    graph[x, y + 1].SetVisited(0);
                }
            }
        }

        // Remove toVisit.First
        toVisit.RemoveFirst();
        if (toVisit.Count > 0) {
            Visit(toVisit, graph);
        }
    }
}
