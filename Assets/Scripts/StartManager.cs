using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour {

    public Image musicImage;
    public Text musicTitle;
    public Text bpm;

    private int musicIndex;
    private int musicCount = 3;
    
    private void UpdateSong(int musicIndex)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        // 리소스에서 비트(Beat) 텍스트 파일을 불러옵니다.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + musicIndex.ToString());
        StringReader reader = new StringReader(textAsset.text);
        // 첫 번째 줄에 적힌 곡 이름을 읽어 UI를 업데이트합니다.
        musicTitle.text = reader.ReadLine();
        // 두 번째 줄에 적힌 BPM을 읽어 UI를 업데이트합니다.
        bpm.text = "BPM: " + reader.ReadLine().Split(' ')[0];
        // 리소스에서 비트(Beat) 음악 파일을 불러와 재생합니다.
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + musicIndex.ToString());
        audioSource.clip = audioClip;
        audioSource.Play();
        // 리소스에서 비트(Beat) 이미지 파일을 불러옵니다.
        musicImage.sprite = Resources.Load<Sprite>("Beats/" + musicIndex.ToString());
    }

    public void Right()
    {
        musicIndex = musicIndex + 1;
        if (musicIndex > musicCount) musicIndex = 1;
        UpdateSong(musicIndex);
    }

    public void Left()
    {
        musicIndex = musicIndex - 1;
        if (musicIndex < 1) musicIndex = musicCount;
        UpdateSong(musicIndex);
    }
   
	void Start () {
        musicIndex = 1;
        new GameInformation();
        UpdateSong(musicIndex);
    }

    public void GameStart()
    {
        GameInformation.instance.music = musicIndex.ToString();
        Screen.SetResolution(1920, 1200, true);
        SceneManager.LoadScene("GameScene");
    }
    
}
