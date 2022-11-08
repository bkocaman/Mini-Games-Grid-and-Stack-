using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Camera mainCamera;

    private bool isActive = true;
    private GridManager gridManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        CameraNewSize(gridManager.size);      
    }

    private void Update()
    {
        if (isActive)
        {
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
            {
                var worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var PositionGrid = gridManager.GetGridPosFromWorld(worldPos);
                if (gridManager.CheckOnTheGrid(PositionGrid))
                {
                    gridManager.ClickGrid(PositionGrid);
                }
            }
        }
    }

    private void CameraNewSize(int size)
    {
        mainCamera.orthographicSize = size + ((float)size / 5);
        var pos = mainCamera.transform.position;
        pos.x = (float)size / 2;
        mainCamera.transform.position = pos;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


}
