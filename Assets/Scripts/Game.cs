using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour
{

    public int regularModifier;
    public int goodModifier;
    public int badModifier;

    public GameObject characterPrefab;
    public GameObject roomPrefab;
    public GameObject piePrefab;

    public Color bathroomColor;
    public Color bedroomColor;
    public Color livingroomColor;
    public Color kitchenColor;

    public Color firstColor;
    public Color secondColor;
    public Color thirdColor;
    public Color fourthColor;

    List<Room> rooms;

    List<GameObject> underMouse;
    Room selectedRoom;

    int updateTick;

    void Start ()
    {
        rooms = new List<Room>();
        underMouse = new List<GameObject>();
        GameObject characterObject;
        Character character;
        GameObject roomObject;
        Room room;

        // First room and character
        characterObject = Instantiate(characterPrefab);
        characterObject.GetComponent<SpriteRenderer>().color = firstColor;
        character = characterObject.GetComponent<Character>();

        roomObject = Instantiate(roomPrefab);
        room = roomObject.GetComponent<Room>();
        room.Init(RoomType.Kitchen);
        room.AddChar(character);
        rooms.Add(room);

        // Second room and character
        characterObject = Instantiate(characterPrefab);
        characterObject.transform.Translate(new Vector3(4.5f, 0f, 0f));
        characterObject.GetComponent<SpriteRenderer>().color = secondColor;
        character = characterObject.GetComponent<Character>();

        roomObject = Instantiate(roomPrefab);
        roomObject.transform.Translate(new Vector3(4.5f, 0f, 0f));
        room = roomObject.GetComponent<Room>();
        room.Init(RoomType.Bedroom);
        room.AddChar(character);
        rooms.Add(room);

        // Third room and character
        characterObject = Instantiate(characterPrefab);
        characterObject.transform.Translate(new Vector3(0f, 4.5f, 0f));
        characterObject.GetComponent<SpriteRenderer>().color = thirdColor;
        character = characterObject.GetComponent<Character>();

        roomObject = Instantiate(roomPrefab);
        roomObject.transform.Translate(new Vector3(0f, 4.5f, 0f));
        room = roomObject.GetComponent<Room>();
        room.Init(RoomType.Bathroom);
        room.AddChar(character);
        rooms.Add(room);

        // Fourth room and character
        characterObject = Instantiate(characterPrefab);
        characterObject.transform.Translate(new Vector3(4.5f, 4.5f, 0f));
        characterObject.GetComponent<SpriteRenderer>().color = fourthColor;
        character = characterObject.GetComponent<Character>();

        roomObject = Instantiate(roomPrefab);
        roomObject.transform.Translate(new Vector3(4.5f, 4.5f, 0f));
        room = roomObject.GetComponent<Room>();
        room.Init(RoomType.Livingroom);
        room.AddChar(character);
        rooms.Add(room);

        updateTick = -1;
	}

    void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, Vector3.zero);

        List<GameObject> toCancel = new List<GameObject>(underMouse);
        GameObject clickTarget = null;

        foreach (RaycastHit2D hit in hits)
        {
            // Get object
            GameObject go = hit.collider.gameObject;

            // Remove object from cancel list
            if (toCancel.Contains(go))
                toCancel.Remove(go);

            // If object is Character
            Character character = go.GetComponent<Character>();
            if (character != null)
            {
                // If not already registered as under the mouse
                if (!underMouse.Contains(go))
                {
                    underMouse.Add(go);
                    character.MouseOver();
                }
                // Register click if needed
                if (Input.GetMouseButtonDown(0) && clickTarget == null)
                {
                    clickTarget = go;
                }
            }

            // If object is Room
            Room room = go.GetComponent<Room>();
            if (room != null)
            {
                // If not already registered as under the mouse
                if (!underMouse.Contains(go))
                {
                    underMouse.Add(go);
                    room.MouseOver();
                }
                // Register click if needed
                if (Input.GetMouseButtonDown(0))
                {
                    clickTarget = go;
                }
            }
        }

        // Execute click
        if (clickTarget != null)
        {
            // If object is Character
            Character character = clickTarget.GetComponent<Character>();
            if (character != null)
                character.Click();

            // If object is Room
            Room room = clickTarget.GetComponent<Room>();
            if (room != null)
            {
                room.Click();
                if (selectedRoom != null)
                    SwapRooms(selectedRoom, room);
                else
                    selectedRoom = room;
            }
        }

        // Cancel objects that are not under the mouse anymore
        foreach (GameObject go in toCancel)
        {
            underMouse.Remove(go);

            // If object is Character
            Character character = go.GetComponent<Character>();
            if (character != null)
                character.MouseOut();

            // If object is Room
            Room room = go.GetComponent<Room>();
            if (room != null)
                room.MouseOut();
        }
    }
	
	void FixedUpdate ()
    {
        updateTick++;

        if (updateTick >= 60)
        {
            UpdateRooms();
            updateTick = 0;
        }
	}

    void UpdateRooms ()
    {
        foreach (Room room in rooms)
        {
            room.UpdateChars();
        }
    }

    void SwapRooms (Room roomA, Room roomB)
    {
        Debug.Log("Swap rooms");

        selectedRoom = null;

        Vector3 posA = roomA.gameObject.transform.position;
        Vector3 posB = roomB.gameObject.transform.position;
        roomA.gameObject.transform.position = posB;
        roomB.gameObject.transform.position = posA;

        roomA.SwapChars(roomB);

        roomA.Deselect();
        roomB.Deselect();
    }

    public Color GetColor (Need need)
    {
        switch (need)
        {
            case Need.BadHygiene:
                return bathroomColor;

            case Need.Boredom:
                return livingroomColor;

            case Need.Fatigue:
                return bedroomColor;

            case Need.Hunger:
                return kitchenColor;

            default:
                return Color.white;
        }
    }

}
