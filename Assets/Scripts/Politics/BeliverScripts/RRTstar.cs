using System;
using System.Collections.Generic;


// GPT 생성 코드
struct Point
{
    public int x;
    public int y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

class RRTstar
{
    private const int ROWS = 50;
    private const int COLS = 50;
    private const int OBSTACLE = 1;
    private const int START = -1;
    private const int GOAL = 2;

    private int[,] grid;
    private List<Point> waypoints;

    public RRTstar()
    {
        grid = new int[ROWS, COLS];
        waypoints = new List<Point>();
    }

    public int[,] GenerateGrid()
    {
        // TODO: Generate your grid here with obstacles, start, and goal positions.
        // For this example, we'll assume the grid is already generated.

        return grid;
    }

    public bool IsObstacle(int x, int y)
    {
        if (x < 0 || y < 0 || x >= ROWS || y >= COLS)
            return true;

        return grid[x, y] == OBSTACLE;
    }

    public List<Point> RRTStarPathPlanning(Point start, Point goal)
    {
        Random random = new Random();
        waypoints.Add(start);

        while (true)
        {
            Point randomPoint = new Point(random.Next(ROWS), random.Next(COLS));

            if (!IsObstacle(randomPoint.x, randomPoint.y))
            {
                int nearestIndex = GetNearestWaypointIndex(randomPoint);
                Point nearestPoint = waypoints[nearestIndex];
                Point newPoint = Steer(nearestPoint, randomPoint);

                if (!IsObstacle(newPoint.x, newPoint.y) && LineOfSight(nearestPoint, newPoint))
                {
                    List<int> nearIndices = FindNearWaypoints(newPoint);
                    int parentIndex = nearestIndex;
                    double minCost = GetCost(nearestPoint) + CalculateDistance(nearestPoint, newPoint);

                    foreach (int i in nearIndices)
                    {
                        Point nearPoint = waypoints[i];
                        double cost = GetCost(nearPoint) + CalculateDistance(nearPoint, newPoint);

                        if (cost < minCost && LineOfSight(nearPoint, newPoint))
                        {
                            parentIndex = i;
                            minCost = cost;
                        }
                    }

                    waypoints.Add(newPoint);
                    ConnectToParent(newPoint, parentIndex);
                    RewireNearWaypoints(newPoint, nearIndices);
                }
            }

            if (CalculateDistance(waypoints[waypoints.Count - 1], goal) <= 1.5)
            {
                waypoints.Add(goal);
                ConnectToParent(goal, waypoints.Count - 2);
                break;
            }
        }

        return waypoints;
    }

    private int GetNearestWaypointIndex(Point point)
    {
        int minIndex = 0;
        double minDistance = CalculateDistance(waypoints[minIndex], point);

        for (int i = 1; i < waypoints.Count; i++)
        {
            double distance = CalculateDistance(waypoints[i], point);

            if (distance < minDistance)
            {
                minIndex = i;
                minDistance = distance;
            }
        }

        return minIndex;
    }

    private Point Steer(Point from, Point to)
    {
        double distance = CalculateDistance(from, to);

        if (distance <= 1)
            return to;

        double ratio = 1 / distance;
        int x = (int)Math.Round((to.x - from.x) * ratio) + from.x;
        int y = (int)Math.Round((to.y - from.y) * ratio) + from.y;

        return new Point(x, y);
    }

    private bool LineOfSight(Point from, Point to)
    {
        int x0 = from.x;
        int y0 = from.y;
        int x1 = to.x;
        int y1 = to.y;
        int dx = Math.Abs(x1 - x0);
        int dy = Math.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            if (IsObstacle(x0, y0))
                return false;

            if (x0 == x1 && y0 == y1)
                break;

            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y0 += sy;
            }
        }

        return true;
    }

    private List<int> FindNearWaypoints(Point point)
    {
        List<int> nearIndices = new List<int>();

        for (int i = 0; i < waypoints.Count; i++)
        {
            if (CalculateDistance(waypoints[i], point) <= 5)
                nearIndices.Add(i);
        }

        return nearIndices;
    }

    private double GetCost(Point point)
    {
        double cost = 0;

        for (int i = 1; i < waypoints.Count; i++)
        {
            if (waypoints[i].x == point.x && waypoints[i].y == point.y)
            {
                cost += CalculateDistance(waypoints[i], waypoints[i - 1]);
                point = waypoints[i - 1];
            }
        }

        return cost;
    }

    private void ConnectToParent(Point point, int parentIndex)
    {
        point.x = waypoints[parentIndex].x;
        point.y = waypoints[parentIndex].y;
    }

    private void RewireNearWaypoints(Point newPoint, List<int> nearIndices)
    {
        foreach (int i in nearIndices)
        {
            Point nearPoint = waypoints[i];
            double cost = GetCost(newPoint) + CalculateDistance(newPoint, nearPoint);

            if (cost < GetCost(nearPoint))
            {
                ConnectToParent(nearPoint, waypoints.IndexOf(newPoint));
            }
        }
    }

    private double CalculateDistance(Point a, Point b)
    {
        int dx = a.x - b.x;
        int dy = a.y - b.y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}

class Program
{
    static void Main(string[] args)
    {
        RRTstar rrtStar = new RRTstar();
        int[,] grid = rrtStar.GenerateGrid();

        Point start = new Point(/* Start x */25, /* Start y */25);
        Point goal = new Point(/* Goal x */1, /* Goal y */1);

        List<Point> waypoints = rrtStar.RRTStarPathPlanning(start, goal);

        foreach (Point waypoint in waypoints)
        {
            Console.WriteLine($"Waypoint: ({waypoint.x}, {waypoint.y})");
        }

        Console.ReadLine();
    }
}
