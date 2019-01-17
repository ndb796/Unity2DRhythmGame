using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {

    public Text musicTitle;
    public Text score;
    public Text maxCombo;
    public Image musicImage;
    public Image Rank;

	void Start () {
        // 리소스에서 비트(Beat) 텍스트 파일을 불러옵니다.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + GameInformation.instance.music);
        StringReader reader = new StringReader(textAsset.text);
        // 첫 번째 줄에 적힌 곡 이름을 읽습니다.
        musicTitle.text = reader.ReadLine();
        score.text = "점수: " + Convert.ToInt32(GameInformation.instance.score).ToString();
        maxCombo.text = "최대 콤보: " + GameInformation.instance.maxCombo.ToString();
        // 리소스에서 비트(Beat) 이미지 파일을 불러옵니다.
        musicImage.sprite = Resources.Load<Sprite>("Beats/" + GameInformation.instance.music);
        // 성적에 맞는 랭크 이미지를 불러옵니다.
        if(GameInformation.instance.rank == GameInformation.ranks.A)
        {
            Rank.sprite = Resources.Load<Sprite>("Sprites/A");
        }
        else if(GameInformation.instance.rank == GameInformation.ranks.B)
        {
            Rank.sprite = Resources.Load<Sprite>("Sprites/B");
        }
        else if (GameInformation.instance.rank == GameInformation.ranks.C)
        {
            Rank.sprite = Resources.Load<Sprite>("Sprites/C");
        }
        else if (GameInformation.instance.rank == GameInformation.ranks.S)
        {
            Rank.sprite = Resources.Load<Sprite>("Sprites/S");
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("StartScene");
    }
	
	void Update () {
		
	}
}
