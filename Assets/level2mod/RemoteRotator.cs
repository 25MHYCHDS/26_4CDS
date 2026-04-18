using UnityEngine;

/// <summary>
/// 交互物体：玩家靠近按 F 进入控制模式，之后按 Q/E 旋转远处的目标物体（每次 90°），ESC 退出
/// </summary>
public class RemoteRotator : MonoBehaviour
{
    [Header("需要旋转的远处物体")]
    public GameObject targetObject;          // 远处那个要被旋转的物体
    public GameObject InteractUI;
    public GameObject InteractUI2;
    public GameObject InteractUI3;

    [Header("旋转设置")]
    public Vector3 rotationAxis = Vector3.up;   // 旋转轴（局部轴），例如 (0,1,0) 绕 Y 轴
    public float rotationAngle = 90f;           // 每次旋转的角度

    [Header("交互设置")]
    public KeyCode rotateLeftKey = KeyCode.Q;   // 向左旋转的按键
    public KeyCode rotateRightKey = KeyCode.E;  // 向右旋转的按键

    private bool playerInRange = false;          // 玩家是否在触发范围内

    private void OnTriggerEnter(Collider other)
    {
        // 假设玩家的 Collider 带有 "Player" 标签
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            InteractUI .SetActive(true);
            InteractUI2.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            InteractUI.SetActive(false);
            InteractUI2 .SetActive(false);
        }
    }

    private void Update()
    {
        if (pnpc2.instance.IsGetBook2 && playerInRange)
        {
            if (Input.GetKeyDown(rotateLeftKey))
            {
                RotateTarget(-rotationAngle);
                InteractUI3.SetActive(true);
            }
            else if (Input.GetKeyDown(rotateRightKey))
            {
                RotateTarget(rotationAngle);
            }
        }
    }


    private void RotateTarget(float angle)
    {
        if (targetObject == null)
        {
            Debug.LogError("RemoteRotator: 未指定要旋转的目标物体！");
            return;
        }

        // 绕自身的局部轴旋转（围绕自身旋转轴）
        targetObject.transform.Rotate(rotationAxis, angle, Space.Self);
    }
}