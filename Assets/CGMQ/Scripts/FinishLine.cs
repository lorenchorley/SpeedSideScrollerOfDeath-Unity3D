using UnityEngine;
using System.Collections;
using Narrate;

public class FinishLine : MonoBehaviour {

    NarrationCountElement NarrationCountElement;

    void Start () {

        // Find the narration text object for winning
        GameObject WinNarration = GameObject.Find("WinNarration");
        NarrationCountElement = WinNarration.GetComponent<NarrationCountElement>();

    }

    // Show the winning text
    public void TriggerNarration() {
        NarrationCountElement.Increment(1);
    }

}
