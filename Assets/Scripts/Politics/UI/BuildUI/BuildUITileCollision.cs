using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUITileCollision : MonoBehaviour
{
    private bool isCollided;

    public bool getCollision() { return isCollided; }

    private void Awake() {
        isCollided = false;
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.name.StartsWith("buildModeTile")) {
            isCollided = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        isCollided = false;
    }
}
