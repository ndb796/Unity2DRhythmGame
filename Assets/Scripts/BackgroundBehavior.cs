using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehavior : MonoBehaviour {

    public GameObject backgrondImage;
    public GameObject[] dividingLines;

    private SpriteRenderer backgrondImageSpriteRenderer;

    void Start () {
        backgrondImageSpriteRenderer = backgrondImage.GetComponent<SpriteRenderer>();
        InitBackground();
        InitDividingLines();
    }

    void InitDividingLines()
    {
        for(int i = 0; i < dividingLines.Length; i++)
        {
            Transform transform = dividingLines[i].GetComponent<Transform>();
            float movingDistance = 10.0f;
            transform.Translate(Vector3.up * movingDistance);
            StartCoroutine(MovingDown(transform, movingDistance));
        }
    }

    void InitBackground()
    {
        StartCoroutine(FadeOut(backgrondImageSpriteRenderer));
    }
	
	void Update () {
        
    }

    IEnumerator MovingDown(Transform transform, float movingDistance)
    {
        while (movingDistance > 0.0f)
        {
            movingDistance -= 0.3f;
            transform.Translate(Vector3.down * 0.3f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator FadeOut(SpriteRenderer spriteRenderer)
    {
        Color color = spriteRenderer.color;
        while (color.a > 0.0f)
        {
            color.a -= 0.005f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.005f);
        }
    }

}