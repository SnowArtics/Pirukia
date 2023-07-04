using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWalk : MonoBehaviour
{
    [SerializeField] 
    private GameObject target;

    // 피사체로부터 거리를 두기 위한 함수
    [SerializeField]
    private float distance = 20f;
    [SerializeField]
    private float height = 15f;
    private Vector3 deltaV;

    // 전환 애니메이션을 위한 변수
    private bool pauseUpdate = false;

    public void SetTarget(GameObject t)
    {
        // 전한 애니메이션은 여기서
        this.target = t;
    }

    public void SetDistance(float d)
    {
        this.distance = d;
        this.SetDistance();
    }

    public void SetDistance()
    {
        // 대상 물체와 차이를 나타내는 것이므로 높이는 음수
        deltaV.y = -height;
        deltaV.x = System.MathF.Sqrt(distance * distance - deltaV.y * deltaV.y);
        deltaV.z = deltaV.x;
    }

    private void Following()
    {
        gameObject.transform.position = target.transform.position - deltaV;
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        deltaV = new Vector3();
        SetDistance();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pauseUpdate)
            Following();
    }
}
