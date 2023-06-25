using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyCameraControl : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float scrollSpeed;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CameraControl");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator CameraControl()
    {
        while (true)
        {
            HandleMouseInput();
            HandleMovementInput();

            yield return null;
        }
    }


    void HandleMouseInput()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput != 0)//마우스 스크롤을 할때 판단
        {
            // transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime*wheelInput);
        }

        if (Input.GetMouseButtonDown(0))
        {

        }

        if (Input.GetMouseButton(0))
        {

        }

        if (Input.GetMouseButtonDown(2))
        {

        }
        if (Input.GetMouseButton(2))
        {

        }
    }

    void HandleMovementInput()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime, Space.World);
            transform.Translate(Vector3.right * -moveSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * moveSpeed*Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.right * -moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime * 0.05f);
        }

        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime * -0.05f);
        }

        if (Input.GetKey(KeyCode.R))
        {

        }

        if (Input.GetKey(KeyCode.F))
        {

        }
    }
}
