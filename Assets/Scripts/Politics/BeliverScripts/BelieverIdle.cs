using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelieverIdle : MonoBehaviour
{
    [SerializeField]
    private new string name;        // �ŵ��� �̸�
    [SerializeField]
    private int speed;              // �ŵ��� �̵� �ӵ�
    [SerializeField]
    private int loyalty;            // �ŵ��� �漺��
    [SerializeField]
    private new string animation;   // ����� �ִϸ��̼�
    [SerializeField]
    private int group;              // �ش� �ŵ��� �ϴ� ���� �׷��ȣ

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
            case 2: {       // �������� �̵�
                    Vector3 vec = new Vector3(-1, 0, -1);
                    transform.Translate(vec * speed * Time.deltaTime, Space.World);
                    break;
            }
            case 4: {       // �������� �̵�
                    Vector3 vec = new Vector3(-1, 0, 1);
                    transform.Translate(vec * speed * Time.deltaTime, Space.World);
                    break;
            }
            case 6: {       // �������� �̵�
                    Vector3 vec = new Vector3(1, 0, -1);
                    transform.Translate(vec * speed * Time.deltaTime, Space.World);
                    break;
            }
            case 8: {       // �������� �̵�
                    Vector3 vec = new Vector3(1, 0, 1);
                    transform.Translate(vec * speed * Time.deltaTime, Space.World);
                    break;
            }
        }
    }

    public void RandomWalk(int direction) {
        this.animation = GetAnimation(group + direction);       // �� �з� �ڵ�(2�ڸ�) + �ൿ �ڵ�(2�ڸ�)
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

        // 5�ʰ� ������ RandomWalk() �Լ��� �����ϴ� Idle() �ڷ�ƾ ����
        if (moveTime >= 5) {
            StartCoroutine(Idle());
        }
    }

    public IEnumerator Idle() {
        int direction = (Random.Range(1, 4) * 2);                    // 2,4,6,8 ������ ���� �����Ͽ� ������ ���� ����
        moveTime = 0;

        // 2�� ���� RandomWalk() �Լ� ����
        for(moveTime = 0; moveTime <= 2; moveTime += Time.deltaTime) { 
            RandomWalk(direction);
            yield return null;
        }

        // 2�� ��� ��, �ڷ�ƾ ����(�ٽ� ��� ���·� ��ȯ)
        moveTime = 0;
        yield break;
    }
}