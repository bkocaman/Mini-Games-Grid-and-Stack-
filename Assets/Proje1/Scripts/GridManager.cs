using UnityEngine;
using System.Collections.Generic;


public class GridManager : MonoBehaviour
{
    public GameObject gridPrefab;
    public int size;

    private float cellSize = 1;
    private GameObject[,] grid;

    private void Start()
    {
        CreateGrid();
    }


    public bool CheckOnTheGrid(Vector2Int positionGrid)
    {
        if (positionGrid.x >= 0 && positionGrid.x < size && positionGrid.y >= 0 && positionGrid.y < size)
            return true;
        else
            return false;
    }

    public void ClickGrid(Vector2Int positionGrid)
    {
        GetGrid(positionGrid).GetComponent<Grid>().FillCell();
        CheckAndDeactivateCells(positionGrid);
    }

    private void CheckAndDeactivateCells(Vector2Int lastClickedGrid)
    {
        List<Vector2Int> pairGrid = new List<Vector2Int>();
        pairGrid.Add(lastClickedGrid);

        while (true)
        {
            var initCount = pairGrid.Count;
            List<Vector2Int> newOnes = new List<Vector2Int>();

            foreach (var pair in pairGrid)
            {
                foreach (var neighbor in getFullNeighbors(pair))
                {
                    if (!pairGrid.Contains(neighbor) && !newOnes.Contains(neighbor))
                    {
                        newOnes.Add(neighbor);
                    }
                }
            }

            pairGrid.AddRange(newOnes);

            if (initCount == pairGrid.Count)
                break;
        }

        if (pairGrid.Count > 2)
        {
         

            foreach (var item in pairGrid)
            {
                GetGrid(item).ResetGrid();
            }
        }
    }

    private List<Vector2Int> getFullNeighbors(Vector2Int positionGrid)
    {
        List<Vector2Int> fullNeighbors = new List<Vector2Int>();


        if (positionGrid.y + 1 < size)
        {
            var cell = GetGrid(positionGrid + new Vector2Int(0, 1));
            if (cell.isFull)
                fullNeighbors.Add(cell.GetPosition());
        }

        if (positionGrid.y - 1 >= 0)
        {
            var cell = GetGrid(positionGrid + new Vector2Int(0, -1));
            if (cell.isFull)
                fullNeighbors.Add(cell.GetPosition());
        }

        if (positionGrid.x + 1 < size)
        {
            var cell = GetGrid(positionGrid + new Vector2Int(1, 0));
            if (cell.isFull)
                fullNeighbors.Add(cell.GetPosition());
        }

        if (positionGrid.x - 1 >= 0)
        {
            var cell = GetGrid(positionGrid + new Vector2Int(-1, 0));
            if (cell.isFull)
                fullNeighbors.Add(cell.GetPosition());
        }

        return fullNeighbors;
    }




    public void RebuildGrid(int _size)
    {
        DeleteGrid();
        size = _size;
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new GameObject[size, size];

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                grid[x, y] = Instantiate(gridPrefab, new Vector3(x * cellSize, 0.01f, y * cellSize), Quaternion.identity);
                grid[x, y].GetComponent<Grid>().SetPosition(x, y);
                grid[x, y].GetComponent<Grid>().isFull = false;
                grid[x, y].gameObject.name = "GridCell (X: " + x + ", Y: " + y + ")";
            }
        }
    }

    private void DeleteGrid()
    {
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Destroy(grid[x, y]);
            }
        }
    }

    public Grid GetGrid(Vector2Int PositionGrid)
    {
        return grid[PositionGrid.x, PositionGrid.y].GetComponent<Grid>();
    }

    public Vector2Int GetGridPosFromWorld(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt(worldPos.x / cellSize);
        int y = Mathf.FloorToInt(worldPos.z / cellSize);

        return new Vector2Int(x, y);
    }
}


