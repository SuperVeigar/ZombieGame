using UnityEngine;

// 플레이어 캐릭터를 조작하기 위한 사용자 입력을 감지
// 감지된 입력값을 다른 컴포넌트들이 사용할 수 있도록 제공
public class PlayerInput : MonoBehaviour {
    public string moveAxisName = "Vertical"; // 앞뒤 움직임을 위한 입력축 이름
    public string rotateAxisName = "Horizontal"; // 좌우 회전을 위한 입력축 이름
    public string fireButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름
    public string reloadButtonName = "Reload"; // 재장전을 위한 입력 버튼 이름

    // 값 할당은 내부에서만 가능
    public float move { get; private set; } // 감지된 움직임 입력값
    public float rotate { get; private set; } // 감지된 회전 입력값
    public bool fire { get; private set; } // 감지된 발사 입력값
    public bool reload { get; private set; } // 감지된 재장전 입력값

    public Vector3 CharDirection { get; private set; }
    public Vector3 GunPoint { get; private set; }
    public float move2 { get; private set; }

    // 매프레임 사용자 입력을 감지
    private void Update() {
        // 게임오버 상태에서는 사용자 입력을 감지하지 않는다
        if (GameManager.instance != null && GameManager.instance.isGameover)
        {
            move = 0;
            rotate = 0;
            fire = false;
            reload = false;
            return;
        }

        // move에 관한 입력 감지
        move = Input.GetAxis(moveAxisName);
        // rotate에 관한 입력 감지
        rotate = Input.GetAxis(rotateAxisName);
        // fire에 관한 입력 감지
        fire = Input.GetButton(fireButtonName);
        // reload에 관한 입력 감지
        reload = Input.GetButtonDown(reloadButtonName);

        UpdateDirection();
        UpdateMove2Value();
    }

    private void UpdateDirection()
    {
        Cursor.lockState = CursorLockMode.Confined;
       

        CharDirection = (new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") )).normalized;
        CharDirection = Quaternion.AngleAxis(45f, Vector3.up) * CharDirection;


        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Plane plane = new Plane(Vector3.up, Vector3.zero);
        //float rayLength;
        //if(plane.Raycast(ray, out rayLength))
        //{
        //    GunPoint = ray.GetPoint(rayLength);
        //}

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if(Physics.Raycast(ray,out raycastHit))
        {
            GunPoint = new Vector3(raycastHit.point.x, 0, raycastHit.point.z);
        }
    }

    private void UpdateMove2Value()
    {
        Vector3 GunDirection = GunPoint - transform.position;
        float angle = Vector3.Angle(CharDirection, GunDirection);
        move2 = CharDirection.magnitude * (90 - angle);
    }
}