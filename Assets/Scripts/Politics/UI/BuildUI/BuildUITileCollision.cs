using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUITileCollision : MonoBehaviour
{
    private bool isCollided;

    public bool GetCollision() { return isCollided; }

    private void Awake() {
        isCollided = false;
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Tiles")) {
            isCollided = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        isCollided = false;
    }
}
