using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelieverIdle : MonoBehaviour
{
    [SerializeField]
    private new string name;        // 신도의 이름
    [SerializeField]
    private int speed;              // 신도의 이동 속도
    [SerializeField]
    private int loyalty;            // 신도의 충성도
    [SerializeField]
    private new string animation;   // 출력할 애니메이션
    [SerializeField]
    private int group;              // 해당 신도가 믿는 신의 그룹번호

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
    }

    public void RandomWalk(int direction) {
        this.animation = GetAnimation(group + direction);       // 신 분류 코드(2자리) + 행동 코드(2자리)
        SetAnimation(animation);
        Walk(direction);
    }

    public void SetSpeed(int speed) {
        this.speed = speed;
    }

    public void SetLoyalty(int loyalty) {
        this.loyalty = loyalty;
    }

    public void Awake() {
        SetSpeed(4);
        SetLoyalty(10);
    }

    public void Update() {
        moveTime += Time.deltaTime;

        // 5초가 지나면 RandomWalk() 함수를 실행하는 Idle() 코루틴 실행
        if (moveTime >= 5) {
            StartCoroutine(Idle());
        }
    }

    public IEnumerator Idle() {
        float posX = this.transform.position.x;
        float posZ = this.transform.position.z;
        int direction;

        if (posX <= 2) {
            if (posZ <= 2) { direction = 8; }                   // 맵의 가장 아래쪽으로 가면 북 방향으로만 이동
            else if (posZ >= 98) { direction = 6; }            // 맵의 가장 왼쪽으로 가면 동 방향으로만 이동
            else { direction = Random.Range(3, 5) * 2; }        // 맵의 3사분면 끝에 도달하면 동, 북 방향으로 이동
        }
        else if(posX >= 98) {
            if (posZ <= 2) { direction = 4; }                   // 맵의 가장 오른쪽으로 가면 서 방향으로 이동
            else if (posZ >= 98) { direction = 2; }            // 맵의 가장 위쪽으로 가면 남 방향으로 이동
            else { direction = Random.Range(1, 3) * 2; }        // 맵의 1사분면 끝에 도달하면 남, 서 방향으로 이동
        }
        else {
            if (posZ <= 2) { direction = Random.Range(1, 3) * 4; }          // 맵의 4사분면 끝에 도달하면 북, 서 방향으로 이동
            else if (posZ >= 98) { direction = (Random.Range(1, 3) * 4) - 2; }   // 맵의 2사분면 끝에 도달하면 남, 동 방향으로 이동
            else { direction = (Random.Range(1, 5) * 2); }      // 필드 내부에 있으면 2,4,6,8 중에 난수 생성하여 임의의 방향 지정
        }

        moveTime = 0;

        // 2초 동안 RandomWalk() 함수 실행
        for(moveTime = 0; moveTime <= 2; moveTime += Time.deltaTime) { 
            RandomWalk(direction);
            yield return null;
        }

        // 좌표의 소수점을 반올림해서 정수로 처리
        Vector3 pos = this.transform.position;
        this.transform.position = new Vector3(Mathf.Round(pos.x), pos.y, Mathf.Round(pos.z));

        // 2초 경과 후, 코루틴 종료(다시 대기 상태로 전환)
        moveTime = 0;
        yield break;
    }
}
