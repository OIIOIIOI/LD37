using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{

    [HideInInspector]
    public RoomType type;

    List<Character> chars;

    Game game;
    
	void Awake ()
    {
        game = GameObject.Find("Game").GetComponent<Game>();

        chars = new List<Character>();
    }

    public void Init (RoomType t)
    {
        this.type = t;
    }

    public void AddChar(Character c)
    {
        if (chars.Contains(c))
            return;
        chars.Add(c);
    }

    public void RemoveChar(Character c)
    {
        if (!chars.Contains(c))
            return;
        chars.Remove(c);
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
