using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField] private Transform origin;
    private Grid<GridObject> grid;

    private void Awake()
    {
        grid = new Grid<GridObject>(10, 10, 5f, origin.position,
            (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z), true);
    }

    [ContextMenu("Build random")]
    public bool Build(BuildingType building, out PlacedObject placedObject)
    {
        try
        {
            int firstNumber = 0;
            int secondNumber = 0;
            BuildingType.Dir direction;

            GetRandomPosition(building, out firstNumber, out secondNumber, out direction);
            if (!VerifyBuild(new Vector2Int(firstNumber, secondNumber), building, direction))
            {
                placedObject = null;
                return false;
            }

            placedObject = BuildAtPosition(new Vector2Int(firstNumber, secondNumber), building, direction);
            return true;
        }
        catch 
        {
        }
        placedObject = null;
        return false;
    }

    private void GetRandomPosition(BuildingType building, out int width, out int height, out BuildingType.Dir dir)
    {
        bool isWidth = Random.Range(0, 2) == 0;

        int firstNumber = isWidth ? Random.Range(0, 10 - building.width) : Random.Range(0, 2) * (10 - building.height);
        int secondNumber = isWidth ? Random.Range(0, 2) * (10 - building.width) : Random.Range(0, 10 - building.height);
        BuildingType.Dir direction;
        if (isWidth)
        {
            if (secondNumber == 0)
            {
                direction = BuildingType.Dir.Down;
            }
            else
            {
                direction = BuildingType.Dir.Up;
            }
        }
        else
        {
            if (firstNumber == 0)
            {
                direction = BuildingType.Dir.Left;
            }
            else
            {
                direction = BuildingType.Dir.Right;
            }
        }

        width = firstNumber;
        height = secondNumber;
        dir = direction;
    }

    public PlacedObject BuildAtPosition(Vector2Int position, BuildingType building, BuildingType.Dir dir = BuildingType.Dir.Down)
    {
        
        bool canBuild = VerifyBuild(position, building, dir, out List<Vector2Int> gridPosList);

        if (canBuild)
        {
            Vector2Int offset = building.GetRotationOffset(dir);
            Vector3 placedPosition = grid.GetWorldPosition(position.x, position.y) +
                new Vector3(offset.x, 0, offset.y) * grid.GetCellSize();



            PlacedObject placedObject = PlacedObject.Create(
                placedPosition,
                new Vector2Int(position.x, position.y),
                dir,
                building,
                transform);

            foreach (Vector2Int item in gridPosList)
            {
                grid.GetValue(item.x, item.y).SetPlacedObject(placedObject);
            }

            return placedObject;
        }

        return null;
    }

    private bool VerifyBuild(Vector2Int position, BuildingType building, BuildingType.Dir dir)
    {
        List<Vector2Int> gridPosList = building.GetGridPositionList(new Vector2Int(position.x, position.y), dir);
        bool canBuild = true;

        foreach (Vector2Int pos in gridPosList)
        {
            if (!grid.GetValue(pos.x, pos.y).CanBuild())
            {
                canBuild = false;
                break;
            }
        }

        return canBuild;
    }

    private bool VerifyBuild(Vector2Int position, BuildingType building, BuildingType.Dir dir, out List<Vector2Int> gridPosList)
    {
        gridPosList = building.GetGridPositionList(new Vector2Int(position.x, position.y), dir);
        bool canBuild = true;

        foreach (Vector2Int pos in gridPosList)
        {
            if (!grid.GetValue(pos.x, pos.y).CanBuild())
            {
                canBuild = false;
                break;
            }
        }

        return canBuild;
    }
}
