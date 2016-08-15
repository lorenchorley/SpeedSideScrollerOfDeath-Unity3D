using UnityEngine;
using System.Collections;
using Narrate;

public class LosingLine : MonoBehaviour {

    public Sprite graphic;
    public int height = 1;

    SpriteRenderer[] Sprites;
    NarrationCountElement NarrationCountElement;

    void Start () {

        // Create the graphic for the losing line: a series of sprites in a vertical line
        Sprites = new SpriteRenderer[height];
        for (int i = 0; i < height; i++) {
            GameObject go = new GameObject("Losing line sprite");
            Sprites[i] = go.AddComponent<SpriteRenderer>();
            Sprites[i].sprite = graphic;
            go.transform.SetParent(transform);
            go.transform.localPosition = new Vector2(0, i);
        }

        // Find the narration text object for losing
        GameObject LoseNarration = GameObject.Find("LoseNarration");
        NarrationCountElement = LoseNarration.GetComponent<NarrationCountElement>();

    }
	
    // Show the losing text
    public void TriggerNarration() {
        NarrationCountElement.Increment(1);
    }

}
