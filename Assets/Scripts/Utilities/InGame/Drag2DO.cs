using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;

public class Drag2DO : MonoBehaviour
{

    public GameObject TargetOP;

    [SerializeField] private bool IsSelected;


    private void Start()
    {
        Player.instance.moveStateMachine.Player.Input.gamePlayActions.Disable();
    }

    private void Update()
    {
        if (IsSelected)
        {
            Vector3 MPos = Input.mousePosition;
            MPos.z = 10;
            Vector3 CouserPos = Camera.main.ScreenToWorldPoint(MPos);
            transform.position = new Vector3(CouserPos.x, transform.position.y, CouserPos.z);
        }
        else
        {
            if (math.distance(transform.position, TargetOP.transform.position) < 2)
            {
                transform.position = new Vector3(TargetOP.transform.position.x, transform.position.y, TargetOP.transform.position.z);
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsSelected = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            IsSelected = false;
        }
    }
}
