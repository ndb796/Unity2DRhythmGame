using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBahavior : MonoBehaviour {

    // 노트의 번호 및 판정 상태 변수 설정
    public int noteType;
    private KeyCode keyCode;
    private GameManager.judges judge;
    
    // 노트의 이동을 위한 변수 설정
    private float x, z, startY = 8.0f, targetY;
    public GameObject perfectLine;

    void Start () {
        if (noteType == 0) keyCode = KeyCode.D;
        else if (noteType == 1) keyCode = KeyCode.F;
        else if (noteType == 2) keyCode = KeyCode.J;
        else if (noteType == 3) keyCode = KeyCode.K;
        // 설정된 시작 라인으로 노트를 이동시킵니다.
        x = transform.position.x;
        z = transform.position.z;
        transform.position = new Vector3(x, startY, z);
        // 특정한 속도로 정확히 PERFECT 라인까지 내려오도록 합니다.
        targetY = perfectLine.transform.position.y;
        StartCoroutine(MoveNote(0.015f));
	}

    // 노트가 아래로 내려오도록 처리하는 함수입니다.
    IEnumerator MoveNote(float speed)
    {
        float amount = (startY - targetY) * speed;
        // MISS 처리를 받으면 자동으로 제거되므로 무한정 내려가도록 설정
        while(true)
        {
            transform.Translate(Vector3.down * amount);
            yield return new WaitForSeconds(speed);
        }
    }

    void Update () {
        // 사용자가 노트 키를 입력한 경우
        if(Input.GetKey(keyCode))
        {
            // 해당 노트에 대한 판정을 진행합니다.
            GameManager.instance.processJudge(judge, noteType);
            // 노트가 판정 선에 닿기 시작한 이후로는 해당 노트를 제거합니다.
            if (judge != GameManager.judges.NONE) Destroy(gameObject);
        }
    }

    // 각 노트의 현재 위치에 대하여 판정을 수행합니다.
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Bad")
        {
            judge = GameManager.judges.BAD;
        }
        else if(other.gameObject.tag == "Good")
        {
            judge = GameManager.judges.GOOD;
        }
        else if (other.gameObject.tag == "Perfect")
        {
            judge = GameManager.judges.PERFECT;
            /* 자동 평가 모드가 활성화 된 경우 */
            if(GameManager.instance.autoPerfect)
            {
                GameManager.instance.playerBehavior.shineTrail(noteType);
                GameManager.instance.processJudge(judge, noteType);
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.tag == "Miss")
        {
            judge = GameManager.judges.MISS;
            GameManager.instance.processJudge(judge, noteType);
            Destroy(gameObject);
        }
    }

}
