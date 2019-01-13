using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    public GameObject[] trails;

    private SpriteRenderer[] trailsSpriteRenderers;

    void Start () {
        trailsSpriteRenderers = new SpriteRenderer[trails.Length];
        for (int i = 0; i < trails.Length; i++)
        {
            trailsSpriteRenderers[i] = trails[i].GetComponent<SpriteRenderer>();
        }
    }
	
	void Update () {
		if(Input.GetKey(KeyCode.D))
        {
            PressNoteD();
        }
        if (Input.GetKey(KeyCode.F))
        {
            PressNoteF();
        }
        if (Input.GetKey(KeyCode.J))
        {
            PressNoteJ();
        }
        if (Input.GetKey(KeyCode.K))
        {
            PressNoteK();
        }
        for (int i = 0; i < trailsSpriteRenderers.Length; i++)
        {
            Color color = trailsSpriteRenderers[i].color;
            color.a -= 0.01f;
            trailsSpriteRenderers[i].color = color;
        }
    }

    public void PressTrail(GameObject trail)
    {
        SpriteRenderer spriteRenderer = trail.GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0.32f;
        spriteRenderer.color = color;
    }

    public void PressNoteD()
    {
        PressTrail(trails[0]);
    }

    public void PressNoteF()
    {
        PressTrail(trails[1]);
    }

    public void PressNoteJ()
    {
        PressTrail(trails[2]);
    }

    public void PressNoteK()
    {
        PressTrail(trails[3]);
    }
}
