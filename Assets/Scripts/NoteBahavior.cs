using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBahavior : MonoBehaviour {
    
    public int noteType;
    public GameObject PerfectLine;

    private float speed = 1.0f;
    private GameManager.judges judge;
    private float x, z, startY = 8.0f, targetY;

	void Start () {
        x = transform.position.x;
        z = transform.position.z;
        transform.position = new Vector3(x, startY, z);
        targetY = PerfectLine.transform.position.y;
        
        StartCoroutine(MoveNote());
	}

    IEnumerator MoveNote()
    {
        // 정확히 1초 뒤에 판정선에 도착할 수 있도록 함.
        float amount = (startY - targetY) * 0.01f;
        while (true)
        {
            transform.Translate(Vector3.down * amount);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void Update () {
        
        if(noteType == 0 && Input.GetKey(KeyCode.D))
        {
            GameManager.instance.processJudge(judge, noteType);
            if (judge != GameManager.judges.NONE) Destroy(gameObject);
        }
        else if (noteType == 1 && Input.GetKey(KeyCode.F))
        {
            GameManager.instance.processJudge(judge, noteType);
            if (judge != GameManager.judges.NONE) Destroy(gameObject);
        }
        else if (noteType == 2 && Input.GetKey(KeyCode.J))
        {
            GameManager.instance.processJudge(judge, noteType);
            if (judge != GameManager.judges.NONE) Destroy(gameObject);
        }
        if (noteType == 3 && Input.GetKey(KeyCode.K))
        {
            GameManager.instance.processJudge(judge, noteType);
            if (judge != GameManager.judges.NONE) Destroy(gameObject);
        }
    }

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
            /* 자동 평가 모드 */
            Destroy(gameObject);
            GameManager.instance.processJudge(judge, noteType);
            if (noteType == 0) GameManager.instance.playerBehavior.PressNoteD();
            else if (noteType == 1) GameManager.instance.playerBehavior.PressNoteF();
            else if (noteType == 2) GameManager.instance.playerBehavior.PressNoteJ();
            else if (noteType == 3) GameManager.instance.playerBehavior.PressNoteK();
        }
        else if (other.gameObject.tag == "Miss")
        {
            Destroy(gameObject);
            judge = GameManager.judges.MISS;
            GameManager.instance.processJudge(judge, noteType);
        }
    }

}
