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
    Dictionary<Need, int> effects;

    bool isSelected = false;

    Game game;
    
	void Awake ()
    {
        game = GameObject.Find("Game").GetComponent<Game>();

        chars = new List<Character>();
        effects = new Dictionary<Need, int>();
        effects.Add(Need.BadHygiene, game.regularModifier);
        effects.Add(Need.Boredom, game.regularModifier);
        effects.Add(Need.Fatigue, game.regularModifier);
        effects.Add(Need.Hunger, game.regularModifier);
    }

    public void Init (RoomType t)
    {
        this.type = t;

        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();

        switch (t)
        {
            case RoomType.Bathroom:
                sr.sprite = bathroomSprite;
                sr.color = game.GetColor(Need.BadHygiene);
                effects[Need.BadHygiene] = game.goodModifier;
                effects[Need.Boredom] = game.badModifier;
                break;

            case RoomType.Bedroom:
                sr.sprite = bedroomSprite;
                sr.color = game.GetColor(Need.Fatigue);
                effects[Need.Fatigue] = game.goodModifier;
                effects[Need.Hunger] = game.badModifier;
                break;

            case RoomType.Livingroom:
                sr.sprite = livingroomSprite;
                sr.color = game.GetColor(Need.Boredom);
                effects[Need.Boredom] = game.goodModifier;
                effects[Need.Fatigue] = game.badModifier;
                break;

            case RoomType.Kitchen:
                sr.sprite = kitchenSprite;
                sr.color = game.GetColor(Need.Hunger);
                effects[Need.Hunger] = game.goodModifier;
                effects[Need.BadHygiene] = game.badModifier;
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

    public void UpdateChars ()
    {
        foreach (Character character in chars)
        {
            foreach (KeyValuePair<Need, int> kvp in effects)
            {
                character.UpdateNeed(kvp.Key, kvp.Value);
            }
        }
    }

    public void SwapChars (Room roomB)
    {
        List<Character> currentChars = new List<Character>(chars);
        chars = new List<Character>(roomB.chars);
        roomB.chars = new List<Character>(currentChars);
    }

    public void Click()
    {
        Debug.Log("Toggle room select");

        isSelected = true;
        this.gameObject.transform.localScale = new Vector3(1.15f, 1.15f, 1f);
    }

    public void Deselect ()
    {
        isSelected = false;
        this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void MouseOver()
    {
        //Debug.Log("Room over");

        if (isSelected)
            return;
        this.gameObject.transform.localScale = new Vector3(1.05f, 1.05f, 1f);
    }

    public void MouseOut()
    {
        //Debug.Log("Room out");

        if (isSelected)
            return;
        this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

}

public enum RoomType
{
    Bathroom,
    Bedroom,
    Livingroom,
    Kitchen
}
