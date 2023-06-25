using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToWorldPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject plane;
    [SerializeField]
    private GameObject itemPrefeb;


    MeshCollider meshCollider;
    Vector3 worldPos;
    Ray ray;

    void Start()
    {
        meshCollider = plane.GetComponent<MeshCollider>();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f)) {
                Vector3 hitPos = hit.point;
                hitPos.y = 0.3f;

                GameObject item = Instantiate(itemPrefeb);
                item.transform.position = hitPos;
            }
        }
    }
}
