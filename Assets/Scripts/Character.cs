using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Character : MonoBehaviour
{
    
    Dictionary<Need, int> needs;
    Dictionary<Need, Image> pies;
    int needMax;

    Game game;
    GameObject canvas;

    void Awake ()
    {
        game = GameObject.Find("Game").GetComponent<Game>();
        canvas = GameObject.Find("PiesCanvas");

        needs = new Dictionary<Need, int>();
        pies = new Dictionary<Need, Image>();

        needMax = 100;
        int needStart = Mathf.FloorToInt(needMax * 0.5f);

        foreach (Need need in Enum.GetValues(typeof(Need)))
        {
            needs.Add(need, needStart);

            GameObject pie = Instantiate(game.piePrefab);
            pie.transform.Translate(new Vector3(1.2f * (pies.Count - 1.5f), 0f, 0f));
            pie.transform.SetParent(canvas.transform, false);
            Image img = pie.transform.Find("Fill").gameObject.GetComponent<Image>();
            if (img != null)
            {
                pies.Add(need, img);
                img.color = game.GetColor(need);
                img.fillAmount = needs[need] / (float)needMax;
                GameObject parent = img.gameObject.transform.parent.gameObject;
                parent.SetActive(false);
            }
        }
    }

    public void UpdateNeed (Need need, int value)
    {
        if (!needs.ContainsKey(need))
            return;
        
        needs[need] += value;
        if (pies.ContainsKey(need))
            pies[need].fillAmount = needs[need] / (float)needMax;
    }

    public void Click()
    {
        Debug.Log("Character click");
    }

    public void MouseOver ()
    {
        //Debug.Log("Character over");

        foreach (KeyValuePair<Need, Image> kvp in pies)
        {
            GameObject parent = kvp.Value.gameObject.transform.parent.gameObject;
            parent.SetActive(true);
        }
    }

    public void MouseOut ()
    {
        //Debug.Log("Character out");

        foreach (KeyValuePair<Need, Image> kvp in pies)
        {
            GameObject parent = kvp.Value.gameObject.transform.parent.gameObject;
            parent.SetActive(false);
        }
    }

}

public enum Need
{
    BadHygiene,
    Fatigue,
    Boredom,
    Hunger
}
