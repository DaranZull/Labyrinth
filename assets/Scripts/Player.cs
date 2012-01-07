using UnityEngine;
using System.Collections;
using System.IO;

public class Player : MonoBehaviour {
    private const float PAUSE = 1.5f;
    private static int playerSpeed;
    private const float DISTANCE_TO_WALL = 0.1f;
    private const float DISTANCE_NEAR_WALL = 0.4f;
    private float distanceToWall = DISTANCE_TO_WALL;

    private static Vector3[] availableDirection = { Vector3.up , Vector3.down, Vector3.left, Vector3.right };
    private static Vector3 direction;
    private static Vector3 sphere2direction;
    private static RaycastHit hit;
    private static RaycastHit hit2;
    private static Vector3 spherePosition;
    private const float SPHERE_RADIUS = 0.3f;
    private const float RIGHT_ROTATION = -90f;
    private const float LEFT_ROTATION = 90f;
    private static Vector3 rotationAxis = Vector3.forward;
    private bool sphereCastOne;
    private bool sphereCastTwo;
    private bool wasNearWall = false;
    private const string EXIT_POINT = "exit";

    // Use this for initialization
    void Start() {
        /** Set pause. */
        StartCoroutine(awake());
        // Random direction
        direction = availableDirection[Random.Range(0, availableDirection.Length)];
        // Spher 2 direction
        sphere2direction = Quaternion.AngleAxis(LEFT_ROTATION, rotationAxis) * direction;
    }

    // Update is called once per frame
    void Update() {
        // Player move in random direction
        transform.Translate(direction * playerSpeed * Time.deltaTime);
        // Sphere position
        spherePosition = transform.position;

        // Sphere cast 1 and sphere cast 2
        sphereCastOne = Physics.SphereCast(spherePosition, SPHERE_RADIUS, direction, out hit, DISTANCE_TO_WALL);
        sphereCastTwo = Physics.SphereCast(spherePosition, SPHERE_RADIUS, sphere2direction, out hit2, distanceToWall);
        
        if (sphereCastTwo) {
            distanceToWall = DISTANCE_NEAR_WALL;
            wasNearWall = true;
        }
        else {
            distanceToWall = DISTANCE_TO_WALL;
        }

        if (sphereCastOne) {
            if (hit.collider.name == EXIT_POINT) {
                playerSpeed = 0;
            }
            else {
            rotation(RIGHT_ROTATION);
            }
        }
        else if (!sphereCastTwo & wasNearWall) {
            wasNearWall = false;
            rotation(LEFT_ROTATION);
        }
    }
    /**
     * Take angle and change direction of SphereCasts.
     * 
     * @param angle Float variable, contain angle rotation.
     * @param rotationAxis Vector axis of rotation.
     * @param direction SphereCastOne direction vector axis.
     * @param sphere2direction SphereCastTwo direction vector axis.
     */
    private static void rotation(float angle) {
        direction = Quaternion.AngleAxis(angle, rotationAxis) * direction;
        sphere2direction = Quaternion.AngleAxis(angle, rotationAxis) * sphere2direction;
    }
    /**
     * Set pause in seconds before player start moving.
     * @param PAUSE Float variable set time of pause.
     */
    IEnumerator awake() {
        yield return new WaitForSeconds(PAUSE);
        playerSpeed = 2;
    }
}
