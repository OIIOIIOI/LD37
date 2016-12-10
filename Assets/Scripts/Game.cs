using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{

    public GameObject characterPrefab;
    public GameObject roomPrefab;

    void Start ()
    {
        GameObject characterObject = Instantiate(characterPrefab);
        Character character = characterObject.GetComponent<Character>();

        GameObject roomObject = Instantiate(roomPrefab);
        Room room = roomObject.GetComponent<Room>();
        room.Init(RoomType.Kitchen);
        room.AddChar(character);
	}
	
	void Update ()
    {
	
	}

}
