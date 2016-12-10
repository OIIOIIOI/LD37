using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour
{

    Dictionary<Need, int> needs;
    int needMax;

    Game game;

    void Awake ()
    {
        game = GameObject.Find("Game").GetComponent<Game>();

        needMax = 40;
        int needStart = Mathf.FloorToInt(needMax * 0.5f);
        needs = new Dictionary<Need, int>();
        needs.Add(Need.BadHygiene, needStart);
        needs.Add(Need.Boredom, needStart);
        needs.Add(Need.Fatigue, needStart);
        needs.Add(Need.Hunger, needStart);
    }

    void Update ()
    {
	
	}
}

public enum Need
{
    BadHygiene,
    Fatigue,
    Boredom,
    Hunger
}
