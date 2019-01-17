using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehavior : MonoBehaviour {

    public GameObject backgrondImage; // 배경 이미지
    public GameObject[] dividingLines; // 세로 구분 선
    
    private SpriteRenderer backgrondImageSpriteRenderer;

    void Start () {
        backgrondImageSpriteRenderer = backgrondImage.GetComponent<SpriteRenderer>();
        InitBackgroundImage();
        InitDividingLines();
    }

    // 오브젝트의 투명도가 높아지도록 처리하는 함수입니다. 
    IEnumerator FadeOut(SpriteRenderer spriteRenderer, float amount)
    {
        Color color = spriteRenderer.color;
        while (color.a > 0.0f)
        {
            color.a -= amount;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(amount);
        }
    }

    // 배경색이 천천히 사라지는 애니메이션을 정의합니다.
    void InitBackgroundImage()
    {
        StartCoroutine(FadeOut(backgrondImageSpriteRenderer, 0.005f));
    }

    // 오브젝트가 내려오도록 처리하는 함수입니다.
    IEnumerator MovingDown(Transform transform, float movingDistance, float speed)
    {
        while (movingDistance > 0.0f)
        {
            movingDistance -= speed;
            transform.Translate(Vector3.down * speed);
            yield return new WaitForSeconds(0.01f);
        }
    }

    // 구분선이 내려오는 애니메이션을 정의합니다.
    void InitDividingLines()
    {
        // 모든 구분선에 대하여 내려오는 코루틴 함수를 불러옵니다.
        for (int i = 0; i < dividingLines.Length; i++)
        {
            Transform transform = dividingLines[i].GetComponent<Transform>();
            float movingDistance = 10.0f;
            // 특정 거리만큼 위쪽으로 올라갔다가, 천천히 내려옵니다.
            transform.Translate(Vector3.up * movingDistance);
            StartCoroutine(MovingDown(transform, movingDistance, 0.03f));
        }
    }

}