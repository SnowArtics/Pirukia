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
        Vector3 pos = transform.position;

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
        SetSpeed(2);
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
        int direction = (Random.Range(1, 4) * 2);                    // 2,4,6,8 내에서 난수 생성하여 임의의 방향 지정
        moveTime = 0;

        // 2초 동안 RandomWalk() 함수 실행
        for(moveTime = 0; moveTime <= 2; moveTime += Time.deltaTime) { 
            RandomWalk(direction);
            yield return null;
        }

        // 2초 경과 후, 코루틴 종료(다시 대기 상태로 전환)
        moveTime = 0;
        yield break;
    }
}
