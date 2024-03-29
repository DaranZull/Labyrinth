using UnityEngine;
using System.Collections;

/** Game state control. */
public class GameManager : MonoBehaviour {
    /** Exit point name. */
    private const string EXIT_POINT = "exit";
	
	/** Game end. */
	void Update () {
        if (Player.hit.collider != null && Player.hit.collider.tag == EXIT_POINT) {
            Player.playerSpeed = 0;
        }
	}
}
