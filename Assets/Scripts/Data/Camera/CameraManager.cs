using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    private Vector2 currenTagetPanTlit;

    private float rotationPassedTime = 0f;
    private float rotationVelocityX;
    private float rotationVelocityY;
    private float Timer;
    private Vector2 EnemyScreenP;

    private GameObject PlayerCamera;
    public GameObject EnemyLookPoint;
    public bool CameraAimCoolDown = false;
    public bool Focus = false;
    public bool IsAiming = false;


    [SerializeField] public float AimWaitTime = 0.5f;
    [SerializeField] public float MaxDistance = 10f;
    [SerializeField] public float OffsetTime = 0.2f;
    [SerializeField] float CameraAimTime = 0.2f;
    [SerializeField] float MaxMouseDelta = 20f;
    [SerializeField] Vector2 CameraAimArea = new Vector2 (320f,180f);
    [SerializeField] Vector2 CameraDeadZone = new Vector2(1920f, 1080f);
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        EnemyLookPoint = GameObject.FindWithTag("EnemyLookP");
        PlayerCamera = GameObject.FindWithTag("PlayerCamera");

        Timer = AimWaitTime;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(PlayerCamera != null)
        {
            if (EnemyLookPoint != null)
            {
                IsAiming = Vector3.Distance(EnemyLookPoint.transform.localToWorldMatrix.GetPosition(), Player.instance.transform.position) > MaxDistance;
                EnemyScreenP = new Vector2(Camera.main.WorldToScreenPoint(EnemyLookPoint.transform.localToWorldMatrix.GetPosition()).x,
                Camera.main.WorldToScreenPoint(EnemyLookPoint.transform.localToWorldMatrix.GetPosition()).y);

                if (JudgeEnemyOutScreenP() || IsAiming)
                {
                    CameraAimCoolDown = true;

                    SetLookPointOffset(0);

                    return;
                }

                if (CameraAimCoolDown == true)
                {
                    Timer -= Time.deltaTime;

                    if (Timer < 0)
                    {
                        CameraAimCoolDown = false;
                        Timer = AimWaitTime;
                    }
                }

                //锁定下的相机云台
                if ((Input.mousePositionDelta.Abs().x + Input.mousePositionDelta.Abs().y) < MaxMouseDelta
                    && CameraAimCoolDown == false)
                {
                    SetLookPointOffset(0.5f);

                    //if (JudgeEnemyScreenPC() && Focus == false)
                    //{
                    //    return;
                    //}

                    UpdateTargetRotateData(GetTargetDirectionAngle(GetTargetDirection(EnemyLookPoint.transform.localToWorldMatrix.GetPosition(), Player.instance.transform.position)));

                    RotateToTagetDri();
                }
                else
                {
                    CameraAimCoolDown = true;
                    Focus = false;
                }
            }
        } 
    }

    //获取目标方向
    public Vector3 GetTargetDirection(Vector3 TargetLookPointP,Vector3 PlayerPosition)
    {
        return new Vector3(TargetLookPointP.x - PlayerPosition.x, TargetLookPointP.y - PlayerPosition.y,
            TargetLookPointP.z - PlayerPosition.z);
    }

    //获取目标角度
    public Vector2 GetTargetDirectionAngle(Vector3 TargetDirection)
    {
        float PanAngle = Mathf.Atan2(TargetDirection.x, TargetDirection.z) * Mathf.Rad2Deg;

        float TlitAngle = -Mathf.Atan2(TargetDirection.y,(Mathf.Sqrt(Mathf.Pow(TargetDirection.x, 2) + Mathf.Pow(TargetDirection.z, 2)))
            ) * Mathf.Rad2Deg;

        PanAngle = AddRotateOffset(PanAngle);

        if (PanAngle < 0)
        {
            PanAngle += 360;
        }
        if (TlitAngle > 90) 
        {
            TlitAngle = 180 - TlitAngle;
        }
        if(TlitAngle < -90)
        {
            TlitAngle = -180 - TlitAngle;
        }

        return new Vector2(PanAngle, TlitAngle);
    }

    //添加旋转左右移动Offset
    private float AddRotateOffset(float angle)
    {
        if (Player.instance.moveStateMachine.ReuseableData.MovementInput == Vector2.right)
        {
            angle -= 75;
        }
        else
        {
            if (Player.instance.moveStateMachine.ReuseableData.MovementInput == Vector2.left)
            {
                angle += 75;
            }
        }

        if (angle > 360f)
        {
            angle -= 360f;
        }
        return angle;
    }

    //储存云台数据
    public void UpdateTargetRotateData(Vector2 TargetPanTlit)
    {
        currenTagetPanTlit = TargetPanTlit;
        rotationPassedTime = 0f;
    }

    //平滑旋转到Data中的目标角度
    public void RotateToTagetDri()
    {
        CinemachinePanTilt PlayerCameraPanTilt = PlayerCamera.GetComponent<CinemachinePanTilt>();

        Vector2 currentPanTlit = new Vector2(PlayerCameraPanTilt.PanAxis.Value, PlayerCameraPanTilt.TiltAxis.Value);

        if (currentPanTlit == currenTagetPanTlit)
        {
            return;
        }

        float SmoothingAngleP = Mathf.SmoothDampAngle(currentPanTlit.x,currenTagetPanTlit.x,
            ref rotationVelocityX,CameraAimTime - rotationPassedTime);

        float SmoothingAngleT = Mathf.SmoothDampAngle(currentPanTlit.y, currenTagetPanTlit.y,
            ref rotationVelocityY, CameraAimTime - rotationPassedTime);

        rotationPassedTime += Time.deltaTime;

        PlayerCameraPanTilt.PanAxis.Value = SmoothingAngleP;
        PlayerCameraPanTilt.TiltAxis.Value = SmoothingAngleT;
    }
    
    //设置LookPointOffset
    public void SetLookPointOffset(float OffsetY)
    {
        CinemachinePositionComposer PositionComposer = PlayerCamera.GetComponent<CinemachinePositionComposer>();

        float CurrentV = 0;

        PositionComposer.TargetOffset.y = Mathf.SmoothDampAngle(PositionComposer.TargetOffset.y,OffsetY, ref CurrentV, OffsetTime);
    }

    private bool JudgeEnemyOutScreenP()
    {
        return (EnemyScreenP.x > CameraDeadZone.x || EnemyScreenP.x < 0) || (EnemyScreenP.y > CameraDeadZone.y || EnemyScreenP.y < 0);
    }
    private bool JudgeEnemyScreenPC()
    {
        return (CameraDeadZone.x / 2 - CameraAimArea.x < EnemyScreenP.x && EnemyScreenP.x < CameraDeadZone.x / 2 + CameraAimArea.x) &&
               (CameraDeadZone.y / 2 - CameraAimArea.y < EnemyScreenP.y && EnemyScreenP.y < CameraDeadZone.y / 2 + CameraAimArea.y);
    }
    }
