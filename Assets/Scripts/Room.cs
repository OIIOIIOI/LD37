using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
{

    Game game;

    public RoomType type;
    
	void Start ()
    {
        game = GameObject.Find("Game").GetComponent<Game>();
	}
	
	void Update ()
    {
	
	}

}

public enum RoomType
{
    Bathroom,
    Bedroom,
    Livingroom,
    Kitchen
}
