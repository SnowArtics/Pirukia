using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelieverIdle : MonoBehaviour
{
    [SerializeField]
    private int speed;              // 신도의 이동 속도
    [SerializeField]
    private new string animation;   // 출력할 애니메이션
    [SerializeField]
    private int group;              // 해당 신도가 믿는 신의 그룹번호

    // 신도 이동 관리
    private bool goTo = false;
    private Vector3 target;
    private RRTStarMovement planner;

    // 애니메이션 관리
    private Animator animator;

    private AnimationWalkList nowWalk;
    private float moveTime = 0;
    private enum AnimationWalkList  { none,
        Char_Walk_SouthWest, Char_Walk_South, Char_Walk_EastSouth,
        Char_Walk_West, Char_IDLE, Char_Walk_East,
        Char_Walk_WestNorth, Char_Walk_North, Char_Walk_NorthEast
    };
    private enum AnimationPrayList { none,
        Char_Pray_East, Char_Pray_West
    };

    public void SetAnimation(string animation) {
        this.animation = animation;
    }

    string GetAnimation(int animation) {
        string walkMotion = "Anim_" + name + (AnimationWalkList) animation;
        return walkMotion;
    }
    
    // 길찾기
    struct Point{
        public int x, y;
    }

    Point CalcPosition(GameObject target)
    {
        Point pos;
        pos.x = 1;
        pos.y = 1;
        return pos;
    }

    void findWay(GameObject target) {
        int[,] grid = new int[50, 50];
        for (int i = 0; i < 50; i++)
            for (int j = 0; j < 50; j++)
                grid[i, j] = 0;
    }
    // 길찾기 끝

    // 애니메이션 처리를 SetAnimation에서 하긴 해야겠지만 일단은 여기서 시도
    void Walk(int direction) {
        switch (direction) {
            case 2: {       // 남쪽으로 이동
                    Vector3 vec = new Vector3(-1, 0, -1);
                    transform.Translate(vec * speed * Time.deltaTime, Space.World);
                    break;
            }
            case 4: {       // 서쪽으로 이동
                    Vector3 vec = new Vector3(-1, 0, 1);
                    transform.Translate(vec * speed * Time.deltaTime, Space.World);
                    break;
            }
            case 6: {       // 동쪽으로 이동
                    Vector3 vec = new Vector3(1, 0, -1);
                    transform.Translate(vec * speed * Time.deltaTime, Space.World);
                    break;
            }
            case 8: {       // 북쪽으로 이동
                    Vector3 vec = new Vector3(1, 0, 1);
                    transform.Translate(vec * speed * Time.deltaTime, Space.World);
                    break;
            }
        }
        animator.SetBool("isWalk", true);
        animator.SetInteger("toward", direction);
    }

    public void RandomWalk(int direction) {
        this.animation = GetAnimation(group + direction);       // 신 분류 코드(2자리) + 행동 코드(2자리)
        SetAnimation(animation);
        Walk(direction);
    }

    public void SetSpeed(int speed) {
        this.speed = speed;
    }

    public void Awake() {
        SetSpeed(4);
        animator = gameObject.GetComponent<Animator>();
        planner = gameObject.GetComponent<RRTStarMovement>();
    }

    public void Update() {
        if (gameObject.GetComponent<Believer>().GetStatus() == Believer.Status.IDLE)
        {
            moveTime += Time.deltaTime;

            // 5초가 지나면 RandomWalk() 함수를 실행하는 Idle() 코루틴 실행
            if (moveTime >= 5)
            {
                StartCoroutine(Idle());
            }
        }


        if (goTo)
        {
            Vector3 delta = (target - gameObject.transform.position);
            float dist = delta.magnitude;
            float deltaDist = speed * Time.deltaTime;

            // Debug.Log($"dist:{dist} deltaDist:{deltaDist} fps: {1/Time.deltaTime}");
            transform.Translate(delta.normalized * deltaDist, Space.World);

            if (dist <= 0.5f)
            {
                animator.SetBool("isWalk", false);
                goTo = false;
            }
        }
    }

    public IEnumerator Idle() {
        float posX = this.transform.position.x;
        float posZ = this.transform.position.z;
        int direction;

        if (posX <= 5)
        {
            if (posZ <= 5) { direction = 8; }                   // 맵의 가장 아래쪽으로 가면 북 방향으로만 이동
            else if (posZ >= 95) { direction = 6; }            // 맵의 가장 왼쪽으로 가면 동 방향으로만 이동
            else { direction = Random.Range(3, 5) * 2; }        // 맵의 3사분면 끝에 도달하면 동, 북 방향으로 이동
        }
        else if (posX >= 95)
        {
            if (posZ <= 5) { direction = 4; }                   // 맵의 가장 오른쪽으로 가면 서 방향으로 이동
            else if (posZ >= 95) { direction = 2; }            // 맵의 가장 위쪽으로 가면 남 방향으로 이동
            else { direction = Random.Range(1, 3) * 2; }        // 맵의 1사분면 끝에 도달하면 남, 서 방향으로 이동
        }
        else
        {
            if (posZ <= 5) { direction = Random.Range(1, 3) * 4; }          // 맵의 4사분면 끝에 도달하면 북, 서 방향으로 이동
            else if (posZ >= 95) { direction = (Random.Range(1, 3) * 4) - 2; }   // 맵의 2사분면 끝에 도달하면 남, 동 방향으로 이동
            else { direction = (Random.Range(1, 5) * 2); }      // 필드 내부에 있으면 2,4,6,8 중에 난수 생성하여 임의의 방향 지정
        }

        moveTime = 0;

        // 2초 동안 RandomWalk() 함수 실행
        for (moveTime = 0; moveTime <= 2; moveTime += Time.deltaTime)
        {
            RandomWalk(direction);
            yield return null;
        }

        // 좌표의 소수점을 반올림해서 정수로 처리
        Vector3 pos = this.transform.position;
        this.transform.position = new Vector3(Mathf.Round(pos.x), pos.y, Mathf.Round(pos.z));

        // 2초 경과 후, 코루틴 종료(다시 대기 상태로 전환)
        moveTime = 0;
        animator.SetBool("isWalk", false);
        yield break;
    }

    public void Goto(Vector3 target)
    {
        goTo = true;
        this.target = target;

        animator.SetBool("isWalk", true);

        // 애니매이션 이동 방향 설정
        bool isVertical;
        int animDirection;
        Vector3 delta = target - gameObject.transform.position;
        // 바닥이 45도 회전해 있어서 캐릭터의 이동 방향도 틀어줘야 카메라 기준 좌우 이동이 가능
        delta = Quaternion.AngleAxis(45, Vector3.up) * delta;

        isVertical = Mathf.Abs(delta.x) > Mathf.Abs(delta.z) ? true : false;

        if (isVertical)
            animDirection = (delta.x > 0) ? 8 : 2;
        else
            animDirection = (delta.z > 0) ? 4 : 6;
        animator.SetInteger("toward", animDirection);

        // update에서 이동 종료 확인
    }
}
