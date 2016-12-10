using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{

    public Sprite bathroomSprite;
    public Sprite bedroomSprite;
    public Sprite livingroomSprite;
    public Sprite kitchenSprite;

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

        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        switch (t)
        {
            case RoomType.Bathroom:
                sr.sprite = bathroomSprite;
                break;
            case RoomType.Bedroom:
                sr.sprite = bedroomSprite;
                break;
            case RoomType.Livingroom:
                sr.sprite = livingroomSprite;
                break;
            case RoomType.Kitchen:
                sr.sprite = kitchenSprite;
                break;
        }
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
