using UnityEngine;

/// <summary>
/// 交互物体：玩家靠近按 F 进入控制模式，之后按 Q/E 旋转远处的目标物体（每次 90°），ESC 退出
/// </summary>
public class RemoteRotator : MonoBehaviour
{
    [Header("需要旋转的远处物体")]
    public GameObject targetObject;          // 远处那个要被旋转的物体

    [Header("旋转设置")]
    public Vector3 rotationAxis = Vector3.up;   // 旋转轴（局部轴），例如 (0,1,0) 绕 Y 轴
    public float rotationAngle = 90f;           // 每次旋转的角度

    [Header("交互设置")]
    public KeyCode interactKey = KeyCode.F;     // 进入控制模式的按键
    public KeyCode rotateLeftKey = KeyCode.Q;   // 向左旋转的按键
    public KeyCode rotateRightKey = KeyCode.E;  // 向右旋转的按键
    public KeyCode exitKey = KeyCode.Escape;    // 退出控制模式的按键

    private bool isControlling = false;          // 是否处于控制模式
    private bool playerInRange = false;          // 玩家是否在触发范围内

    private void OnTriggerEnter(Collider other)
    {
        // 假设玩家的 Collider 带有 "Player" 标签
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            // 玩家离开范围时，强制退出控制模式（避免卡状态）
            if (isControlling)
                ExitControlMode();
        }
    }

    private void Update()
    {
        // 1. 非控制模式下，检测玩家按 F 键进入控制
        if (!isControlling && playerInRange && Input.GetKeyDown(interactKey))
        {
            EnterControlMode();
            return; // 避免同一帧再检测旋转
        }

        // 2. 控制模式下，处理旋转和退出
        if (isControlling)
        {
            // 按 Q 或 E 旋转（每次按下旋转一次，不是按住持续转）
            if (Input.GetKeyDown(rotateLeftKey))
            {
                RotateTarget(-rotationAngle);
            }
            else if (Input.GetKeyDown(rotateRightKey))
            {
                RotateTarget(rotationAngle);
            }
            else if (Input.GetKeyDown(exitKey))
            {
                ExitControlMode();
            }
        }
    }

    private void EnterControlMode()
    {
        isControlling = true;
        // 不需要屏幕提示，所以这里什么都不用显示
        // 可以加一行调试日志，正式版可删除
        Debug.Log("进入控制模式：按 Q/E 旋转目标物体，ESC 退出");
    }

    private void ExitControlMode()
    {
        isControlling = false;
        Debug.Log("退出控制模式");
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