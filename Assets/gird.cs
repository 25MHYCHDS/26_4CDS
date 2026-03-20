using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;  // 单例方便其他脚本访问

    private List<GridCell> allCells = new List<GridCell>();

    void Awake()
    {
        Instance = this;
        // 自动收集场景中所有 GridCell
        allCells.AddRange(FindObjectsOfType<GridCell>());
        Instance = this;
        allCells.AddRange(FindObjectsOfType<GridCell>());
        Debug.Log("GridManager 找到了 " + allCells.Count + " 个格子");
    }

    /// <summary>
    /// 获取以 center 为中心、radius 为半径的圆形范围内的所有格子
    /// </summary>
    public List<GridCell> GetCellsInRange(Vector3 center, float radius)
    {
        List<GridCell> result = new List<GridCell>();

        // 将中心点 Y 轴设为 0（假设地面在 y=0）
        Vector3 centerFlat = new Vector3(center.x, 0, center.z);

        foreach (GridCell cell in allCells)
        {
            // 将格子位置 Y 轴也设为 0
            Vector3 cellFlat = new Vector3(cell.transform.position.x, 0, cell.transform.position.z);

            float distance = Vector3.Distance(centerFlat, cellFlat);
            if (distance <= radius)
            {
                result.Add(cell);
            }
        }
        return result;
    }
}