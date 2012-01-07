using UnityEngine;
using System.Collections;
using System.IO;

public class Creator : MonoBehaviour {
    private const string FILE_NAME = "Labyrinth.txt";/** String variable contain text file name with labyrinth.*/
    private const string EXIT_POINT = "exit";/** String variable contain name of exit point.  */
    private const char PICK = '*';/** Char variable contain '*' character. */
    private const char SPACE = ' ';/** Char variable contain space character. */
    private const char START = 'S';/** Char variable contain 'S' character. */
    private const char EXIT = 'E';/** Char variable contain 'E' character. */
    private const char LINE_FEED = '\n';/** Char variable contain line feed character. */
    private static string fileContents;
    private static char[] contentChar;
    private static int locationX = 0;
    private static int locationY = 0;
    private static int locationZ = 0;
    public GameObject playerPreFab;
    public GameObject exitPreFab;

    /**
     * Read data from text file. Construct labyrinth.
     * Set player in 'S' position, set exit point in 'E' position.
     */
    void Start() {
        using (StreamReader sr = new StreamReader(Application.dataPath + "/" + FILE_NAME)) {
            fileContents = sr.ReadToEnd();
            contentChar = fileContents.ToCharArray();
        }
        for (int i = 0; i < contentChar.Length; i++) {
            if (contentChar[i] == PICK) {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(locationX, locationY, locationZ);
                locationX++;
            }
            else if (contentChar[i] == SPACE) {
                locationX++;
            }
            else if (contentChar[i] == LINE_FEED) {
                 locationX = 0;
                 locationY--;
            }
            else if (contentChar[i] == START) {
                GameObject player = (GameObject)Instantiate(playerPreFab, 
                    transform.position = new Vector3(locationX, locationY, locationZ), transform.rotation);
                locationX++;
            }
            else if (contentChar[i] == EXIT) {
                GameObject.FindGameObjectWithTag(EXIT_POINT).transform.position = 
                    new Vector3(locationX, locationY, locationZ);
                locationX++;
            }
        }
    }

    // Update is called once per frame
    void Update() {
    }
}
