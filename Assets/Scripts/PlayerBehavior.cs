using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

    // 사용자가 입력하는 키에 해당하는 라인들
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
        // 사용자가 입력한 키에 해당하는 라인을 빛나게 처리합니다.
		if (Input.GetKey(KeyCode.D)) shineTrail(0);
        if (Input.GetKey(KeyCode.F)) shineTrail(1);
        if (Input.GetKey(KeyCode.J)) shineTrail(2);
        if (Input.GetKey(KeyCode.K)) shineTrail(3);
        // 한 번 빛나게 된 라인은 반복적으로 다시 어둡게 처리됩니다.
        for (int i = 0; i < trailsSpriteRenderers.Length; i++)
        {
            Color color = trailsSpriteRenderers[i].color;
            color.a -= 0.01f;
            trailsSpriteRenderers[i].color = color;
        }
    }

    // 특정한 키를 눌러 해당 라인을 빛나게 처리합니다.
    public void shineTrail(int index)
    {
        SpriteRenderer spriteRenderer = trails[index].GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 0.32f;
        spriteRenderer.color = color;
    }
}
