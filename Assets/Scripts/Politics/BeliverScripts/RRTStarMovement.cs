using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Threading;

public class RRTStarMovement : MonoBehaviour
{
    public int width = 100;  // 배열 가로 크기
    public int height = 100; // 배열 세로 크기
    public int[][] grid;     // 2차원 배열

    public Vector2Int start; // 출발점 좌표
    public Vector2Int goal;  // 목적지 좌표

    public float stepSize = 0.12f;    // RRT* 알고리즘의 한 스텝 크기
    public float goalThreshold = 0.1f; // 목적지 도달 반경

    public GameObject npc; // NPC 게임 오브젝트
    private BelieverIdle motion;
    private float moveSpeed;

    private List<Vector2Int> tree; // RRT* 트리

    void Start()
    {
        // npc == gameObject
        npc = gameObject;
        motion = npc.GetComponent<BelieverIdle>();
        moveSpeed = npc.GetComponent<Believer>().GetSpeed();

        init();
    }

    bool isFirst = true;
    private void Update()
    {
        if (isFirst)
        {
            isFirst = false;
            setTarget(new Vector2Int(30, 30));
        }
    }

    private void init()
    {
        // 2차원 배열 초기화
        grid = new int[width][];
        for (int i = 0; i < width; i++)
        {
            grid[i] = new int[height];
        }

        // RRT* 트리 초기화
        tree = new List<Vector2Int>();
    }

    private void setTarget(Vector2Int goal)
    {
        // 자유이동 정지
        Believer believerComp = gameObject.GetComponent<Believer>();
        believerComp.SetStatus(Believer.Status.WALKING);

        StopAllCoroutines();
        init();
        // 출발점과 목적지 설정
        this.goal = goal;
        this.start.x = (int)gameObject.transform.position.x;
        this.start.y = (int)gameObject.transform.position.z;
        tree.Add(start);

        RRT();

        // 자유이동 허용
        believerComp.SetStatus(Believer.Status.IDLE);
    }

    void  RRT()
    {
        // RRT* 알고리즘 실행
        while (!IsGoalReached())
        {
            Vector2Int randomPoint = GenerateRandomPoint();
            Vector2Int nearestPoint = FindNearestPoint(randomPoint);
            Vector2Int newPoint = Steer(nearestPoint, randomPoint);
            if (IsPointValid(newPoint))
            {
                tree.Add(newPoint);
                TryConnectToNearest(newPoint);
            }
        }

        // 경로 확보되면 NPC 이동 시작
        //StartCoroutine(MoveToGoal());
        MoveToGoal();
    }

    void MoveToGoal()
    {
        List<Vector3> waypoints = new List<Vector3>();
        int currentPointIndex = tree.Count - 1;
        while (currentPointIndex > 0)
        {
            Vector2Int currentPoint = tree[currentPointIndex];
            Vector3 targetPosition = new Vector3(currentPoint.x, 0, currentPoint.y);

            waypoints.Add(targetPosition);

            // npc.transform.position = targetPosition;
            // yield return new WaitForSeconds(0.2f); // NPC 이동 간격

            currentPointIndex = GetParentIndex(currentPointIndex);
        }

        waypoints.Reverse();
        foreach (Vector3 waypoint in waypoints)
        {
            motion.Goto(waypoint);
        }
        // yield return null;
    }

    bool IsGoalReached()
    {
        Vector2Int lastPoint = tree[tree.Count - 1];
        float distance = Vector2.Distance(lastPoint, goal);
        return distance <= goalThreshold;
    }

    Vector2Int GenerateRandomPoint()
    {
        int x = Random.Range(0, width);
        int y = Random.Range(0, height);
        return new Vector2Int(x, y);
    }

    Vector2Int FindNearestPoint(Vector2Int point)
    {
        float minDistance = float.MaxValue;
        Vector2Int nearestPoint = Vector2Int.zero;

        foreach (Vector2Int p in tree)
        {
            float distance = Vector2.Distance(p, point);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPoint = p;
            }
        }

        return nearestPoint;
    }

    Vector2Int Steer(Vector2Int from, Vector2Int to)
    {
        Vector2 direction = ((Vector2)(to - from)).normalized;
        Vector2Int newPoint = from + Vector2Int.RoundToInt(direction * stepSize);
        return newPoint;
    }

    bool IsPointValid(Vector2Int point)
    {
        // 유효한 배열 범위인지 확인
        if (point.x < 0 || point.x >= width || point.y < 0 || point.y >= height)
        {
            return false;
        }

        // 장애물인지 확인
        if (grid[point.x][point.y] == 1)
        {
            return false;
        }

        return true;
    }

    void TryConnectToNearest(Vector2Int point)
    {
        Vector2Int nearest = FindNearestPoint(point);
        Vector2 direction = ((Vector2)(point - nearest)).normalized;
        Vector2Int intermediatePoint = nearest + Vector2Int.RoundToInt(direction * stepSize * 0.5f);
        if (IsPointValid(intermediatePoint))
        {
            tree.Add(intermediatePoint);
        }
    }

    int GetParentIndex(int currentIndex)
    {
        Vector2Int currentPoint = tree[currentIndex];
        float minDistance = float.MaxValue;
        int parentIndex = -1;

        for (int i = 0; i < currentIndex; i++)
        {
            Vector2Int point = tree[i];
            float distance = Vector2.Distance(point, currentPoint);
            if (distance < minDistance)
            {
                minDistance = distance;
                parentIndex = i;
            }
        }

        return parentIndex;
    }
}
