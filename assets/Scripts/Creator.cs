using UnityEngine;
using System.Collections;
using System.IO;

/**
 * Create labyrinth.
 * Set player position.
 * Set end game position.
 */
public class Creator : MonoBehaviour {
    /** Text file name with labyrinth. */
    private const string FILE_NAME = "Labyrinth.txt";
    /** Exit point name. */
    private const string EXIT_POINT = "exit";
    /** '*' character. */
    private const char PICK = '*';
    /** Space character. */
    private const char SPACE = ' ';
    /** 'S' character. */
    private const char START = 'S';
    /** 'E' character. */
    private const char EXIT = 'E';
    /** Line feed character. */
    private const char LINE_FEED = '\n';
    /** File contents. */
    private static string fileContents;
    /** Array of content char. */
    private static char[] contentChar;
    /** X location. */
    private static int locationX = 0;
    /** Y location. */
    private static int locationY = 0;
    /** Z location. */
    private static int locationZ = 0;
    /** Player prefab. */
    public GameObject playerPreFab;

    /**
     * Read data from text file. Construct labyrinth.
     * Set player in 'S' position, set exit point in 'E' position.
     */
    void Start() {
        using (StreamReader sr = new StreamReader(Application.dataPath + "/" + FILE_NAME)) {
            while ((fileContents = sr.ReadLine()) != null) {
                contentChar = fileContents.ToCharArray();
                for (int i = 0; i < contentChar.Length; i++) {
                    switch (contentChar[i]) { 
                        case PICK:
                            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                            cube.transform.position = new Vector3(locationX, locationY, locationZ);
                            locationX++;
                            break;
                        case SPACE:
                            locationX++;
                            break;
                        case START:
                            Instantiate(playerPreFab, transform.position = 
                                new Vector3(locationX, locationY, locationZ), transform.rotation);
                            locationX++;
                            break;
                        case EXIT:
                            GameObject.FindGameObjectWithTag(EXIT_POINT).transform.position =
                                new Vector3(locationX, locationY, locationZ);
                            locationX++;
                            break;
                        default:
                            Debug.Log("Invalid char in text file with labyrinth");
                            break;
                    }
                }
                locationX = 0;
                locationY--;
            }
        }
    }

    // Update is called once per frame
    void Update() {
    }
}
