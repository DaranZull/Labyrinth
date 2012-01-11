using UnityEngine;
using System.Collections;
using System.IO;

/**
 * Player moving control.
 * 
 */
public class Player : MonoBehaviour {
    /** Pause in seconds */
    private const float PAUSE = 1.5f;
    /** Player speed */
    public static int playerSpeed;
    /** The length of the SphereCast */
    private const float DISTANCE_TO_WALL = 0.1f;
    /** The length of the sphereCastTwo if they hit object */
    private const float DISTANCE_NEAR_WALL = 0.4f;
    /** The ltngth of the sphereCastTwo if they not hit object */
    private float distanceToWall = DISTANCE_TO_WALL;
    /** Player moving directions */
    private static Vector3[] availableDirection = { Vector3.up , Vector3.down, Vector3.left, Vector3.right };
    /** Player moving direction */
    private static Vector3 direction;
    /** The sphereCastTwo direction */
    private static Vector3 sphere2direction;
    /** The sphereCastOne hitinfo */
    public static RaycastHit hit;
    /** The sphereCastTwo hitinfo */
    private static RaycastHit hit2;
    /** The SphereCast sphere position */
    private static Vector3 spherePosition;
    /** The SphereCast sphere radius */
    private const float SPHERE_RADIUS = 0.3f;
    /** Angle of rotation */
    private const float RIGHT_ROTATION = -90f;
    /** Angle of rotation */
    private const float LEFT_ROTATION = 90f;
    /** Axis of rotation */
    private static Vector3 rotationAxis = Vector3.forward;
    /** Is sphereCastOne hit game object */
    public bool sphereCastOne;
    /** Is sphereCastTwo hit game object */
    private bool sphereCastTwo;
    /** Was near wall */
    private bool wasNearWall = false;

    // Use this for initialization
    void Start() {
        /** Set pause before player start moving. */
        StartCoroutine(awake());
        // Set random direction.
        direction = availableDirection[Random.Range(0, availableDirection.Length)];
        // Set sphereCastTwo direction.
        sphere2direction = Quaternion.AngleAxis(LEFT_ROTATION, rotationAxis) * direction;
    }

    // Update is called once per frame
    void Update() {
        /** Player moving. */
        transform.Translate(direction * playerSpeed * Time.deltaTime);
        /** The SphereCast sphere position. */
        spherePosition = transform.position;

        /** The sphereCastOne initialization. */
        sphereCastOne = Physics.SphereCast(spherePosition, SPHERE_RADIUS, direction, out hit, DISTANCE_TO_WALL);
        /** The sphereCastTwo initialization. */
        sphereCastTwo = Physics.SphereCast(spherePosition, SPHERE_RADIUS, sphere2direction, out hit2, distanceToWall);
        /** Change player direction. */
        if (sphereCastTwo) {
            distanceToWall = DISTANCE_NEAR_WALL;
            wasNearWall = true;
        }
        else {
            distanceToWall = DISTANCE_TO_WALL;
        }

        if (sphereCastOne) {
            rotation(RIGHT_ROTATION);
        }
        else if (!sphereCastTwo & wasNearWall) {
            wasNearWall = false;
            rotation(LEFT_ROTATION);
        }
    }
    /**
     * Rotate direction of SphereCasts.
     * 
     * @param angle Float variable, contain angle of rotation.
     * @param rotationAxis vector axis of rotation.
     * @param direction SphereCastOne vector axis direction.
     * @param sphere2direction SphereCastTwo vector axis direction.
     */
    private static void rotation(float angle) {
        direction = Quaternion.AngleAxis(angle, rotationAxis) * direction;
        sphere2direction = Quaternion.AngleAxis(angle, rotationAxis) * sphere2direction;
    }
    /**
     * Set pause before player start moving.
     * @param PAUSE Float variable set time of pause in seconds.
     */
    IEnumerator awake() {
        yield return new WaitForSeconds(PAUSE);
        playerSpeed = 2;
    }
}
