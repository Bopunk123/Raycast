using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    float cameraMovementOffset = 0.15f;

    GameObject player;

    float cameraZOffset = 17.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraPosition();

        if (Input.GetKey(KeyCode.Space)) {
            CenterCamera();
        }
    }

    void UpdateCameraPosition() {
        if (Input.mousePosition.x >= Screen.width) {
            //moveCam to right
            transform.position = new Vector3(transform.position.x + cameraMovementOffset,transform.position.y,transform.position.z);
        } else if (Input.mousePosition.x <= 0.0f) {
            //moveCam to left
            transform.position = new Vector3(transform.position.x - cameraMovementOffset, transform.position.y, transform.position.z);
        }

        if (Input.mousePosition.y >= Screen.height) {

            //moveCam up
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + cameraMovementOffset);

        } else if (Input.mousePosition.y <= 0.0f) {
            //move cam down
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - cameraMovementOffset);
        }
    }

    void CenterCamera() {
        transform.position = new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z - cameraZOffset);
    }
}
