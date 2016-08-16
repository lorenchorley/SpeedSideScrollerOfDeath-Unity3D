using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class CameraController : MonoBehaviour 
{
	public Transform Player;
	public float m_speed = 0.1f;
	Camera mycam;

    public Transform leftmostPoint;
    public Transform rightmostPoint;
    public float leftmostPosition;
    public float advanceRateModifier;
    public AnimationCurve advanceRateCurve;

    public LosingLine LosingLineTemplate;
    public LosingLine LosingLine;
    public FinishLine FinishLine;

    public Button RestartButton;

    public void Start()
	{
		mycam = GetComponent<Camera> ();
        RestartButton.gameObject.SetActive(false);
	}

	public void Update()
	{

		mycam.orthographicSize = (Screen.height / 100f) / 0.7f;

		if (Player) 
		{
		
			transform.position = Vector3.Lerp(transform.position, Player.position, m_speed) + new Vector3(0, 0, -12);
		}

        // Get the position of the player between the start and end point, as a number between 0 and 1
        float normalisedPositionInLevel = (rightmostPoint.position.x - leftmostPoint.position.x) / (Player.position.x - leftmostPoint.position.x);

        // Update the losing line position
        leftmostPosition += advanceRateModifier * advanceRateCurve.Evaluate(normalisedPositionInLevel) * Time.deltaTime;
        LosingLine.transform.position = new Vector2(leftmostPosition, 0);

        // Check if the player has won or lost, if so show the appropriate text and stop the camera to show that the game is over
        if (Player.position.x <= leftmostPosition) {

            LosingLine.TriggerNarration();
            RestartButton.gameObject.SetActive(true);
            Player.GetComponent<PlayerController>().enabled = false;

            enabled = false;
            return;
        } else if (Player.position.x >= rightmostPoint.position.x) {

            FinishLine.TriggerNarration();
            RestartButton.gameObject.SetActive(true);
            Player.GetComponent<PlayerController>().enabled = false;

            enabled = false;
            return;
        }

    }

    // Set up essential objects and references
    public void FindPlayerAndFinishLine() {

        // Search for the player controller
        PlayerController controller = FindObjectOfType<PlayerController>();

        if (controller == null)
            throw new Exception("Could not find player!");

        Player = controller.transform;

        // Set starting position as the beginning of the level generator
        leftmostPosition = leftmostPoint.position.x;

        // Search for the finish line
        FinishLine = FindObjectOfType<FinishLine>();

        if (FinishLine == null)
            throw new Exception("Could not find the finish line!");

        rightmostPoint = FinishLine.transform;

        // Create the losing line
        LosingLine = GameObject.Instantiate<GameObject>(LosingLineTemplate.gameObject).GetComponent<LosingLine>();
        LosingLine.transform.position = new Vector2(leftmostPosition, 0);

    }

}
