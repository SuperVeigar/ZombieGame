using UnityEngine;

// 플레이어 캐릭터를 사용자 입력에 따라 움직이는 스크립트
public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 5f; // 앞뒤 움직임의 속도
    public float rotateSpeed = 180f; // 좌우 회전 속도


    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody playerRigidbody; // 플레이어 캐릭터의 리지드바디
    private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터

    private PlayerShooter playerShooter;

    private void Start() {
        // 사용할 컴포넌트들의 참조를 가져오기

        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerShooter = GetComponent<PlayerShooter>();
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate() {
        // 물리 갱신 주기마다 움직임, 회전, 애니메이션 처리 실행

        //Rotate();
        //Move();

        //playerAnimator.SetFloat("Move", playerInput.move);


        Move2();
        Rotate2();

        playerAnimator.SetFloat("Move", playerInput.move2);
    }

    // 입력값에 따라 캐릭터를 앞뒤로 움직임
    private void Move() {

        Vector3 moveDistance = playerInput.move * transform.forward * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    // 입력값에 따라 캐릭터를 좌우로 회전
    private void Rotate() {

        float turn = playerInput.rotate * rotateSpeed * Time.deltaTime;

        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0, turn, 0);

    }

    private void Move2()
    {
        Vector3 moveDistance = playerInput.CharDirection * moveSpeed * Time.deltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
    }

    private void Rotate2()
    {
        Vector3 pointToLook = new Vector3(playerInput.GunPoint.x - transform.position.x, 0, playerInput.GunPoint.z - transform.position.z);
        pointToLook = transform.position + pointToLook.normalized * 3;

        transform.LookAt(pointToLook);

        //Vector3 GunDirection = playerInput.GunPoint - gameObject.transform.position;
        //Quaternion rot = Quaternion.LookRotation(GunDirection.normalized, Vector3.up);
        //transform.rotation = rot;
    }
}