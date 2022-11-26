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
        public bool IsDark;

        public Node(int x, int y, Vector3Int position, bool hasCollider)
        {
            X = x;
            Y = y;
            Position = position;
            HasCollider = hasCollider;
            Visited = false;
            IsDark = false;
        }

        public void SetVisited(bool isDark)
        {
            Visited = true;
            IsDark = isDark;
        }
    }
    

    Node[,] graph = new Node[25, 25];
    LinkedList<Node> toVisit = new LinkedList<Node>();
    
    public Tilemap fogMap;
    public Tilemap collisionMap;
    public TileBase collisionTile;
    public TileBase fogTile;
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
        for (int i = 0; i < 25; i++) { 
            for (int j = 0; j < 25; j++) {
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
        Debug.Log("Setting Darkness");
        for (int i = 0; i < 25; i++) {
            for (int j = 0; j < 25; j++) {
                if (graph[i,j].IsDark)
                {
                    fogMap.SetTile(graph[i, j].Position, fogTile);
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
        bool isDark = toVisit.First.Value.IsDark;

        // Check Left
        if (x - 1 >= 0) {
            if (!graph[x - 1, y].Visited) {
                toVisit.AddLast(graph[x - 1, y]);
                if (hasCollision || isDark) {
                    graph[x - 1, y].SetVisited(true);
                } else {
                    graph[x - 1, y].SetVisited(false);
                }
            }
        }
        // Check Up
        if (y - 1 >= 0) {
            if (!graph[x, y - 1].Visited) {
                toVisit.AddLast(graph[x, y - 1]);
                if (hasCollision || isDark) {
                    graph[x, y - 1].SetVisited(true);
                } else {
                    graph[x, y - 1].SetVisited(false);
                }
            }
        }
        // Check Right
        if (x + 1 < 25)
        {
            if (!graph[x + 1, y].Visited)
            {
                toVisit.AddLast(graph[x + 1, y]);
                if (hasCollision || isDark)
                {
                    graph[x + 1, y].SetVisited(true);
                }
                else
                {
                    graph[x + 1, y].SetVisited(false);
                }
            }
        }
        // Check Down
        if (y + 1 < 25)
        {
            if (!graph[x, y + 1].Visited)
            {
                toVisit.AddLast(graph[x, y + 1]);
                if (hasCollision || isDark)
                {
                    graph[x, y + 1].SetVisited(true);
                }
                else
                {
                    graph[x, y + 1].SetVisited(false);
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
