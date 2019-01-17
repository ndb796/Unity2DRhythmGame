using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteController : MonoBehaviour {

    // 하나의 노트에 대한 정보를 담는 노트(Note) 클래스 정의
    class Note
    {
        public int noteType; // 노트의 종류
        public int order; // 노트가 떨어지는 순서
        public Note(int noteType, int order)
        {
            this.noteType = noteType;
            this.order = order;
        }
    }

    private string musicTitle;
    private int bpm;
    private int divider;
    private float beatCount;
    private float beatInterval;
    private float startingPoint;

    // 노트 프리팹 및 떨어질 노트에 대한 정보 변수
    public GameObject[] Notes;
    private List<Note> notes = new List<Note>();

    void Start()
    {
        ReadBeat(GameInformation.instance.music);
    }

    void ReadBeat(string music)
    {
        // 리소스에서 비트(Beat) 텍스트 파일을 불러옵니다.
        TextAsset textAsset = Resources.Load<TextAsset>("Beats/" + music);
        StringReader reader = new StringReader(textAsset.text);
        // 첫 번째 줄에 적힌 곡 이름을 읽습니다.
        musicTitle = reader.ReadLine();
        GameInformation.instance.musicTitle = musicTitle;
        // 두 번째 줄에 적힌 비트 정보(BPM, Divider, 시작 시간)를 읽습니다.
        string beatInformation = reader.ReadLine();
        bpm = Convert.ToInt32(beatInformation.Split(' ')[0]);
        divider = Convert.ToInt32(beatInformation.Split(' ')[1]);
        startingPoint = (float) Convert.ToDouble(beatInformation.Split(' ')[2]);
        // 1초마다 떨어지는 비트의 개수
        beatCount = (float) bpm / divider;
        // 비트가 떨어지는 간격 시간
        beatInterval = 1 / beatCount;
        // 각 비트들이 떨어지는 위치 및 시간 정보를 읽습니다.
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            Note note = new Note(
                Convert.ToInt32(line.Split(' ')[0]),
                Convert.ToInt32(line.Split(' ')[1])
            );
            notes.Add(note);
        }
        // 모든 노트를 정해진 시간에 출발하도록 설정합니다.
        for (int i = 0; i < notes.Count; i++)
        {
            StartCoroutine(AwaitMakeNote(notes[i]));
        }
        // 마지막 노트를 기준으로 게임 종료 함수를 불러옵니다.
        StartCoroutine(AwaitGameResult(notes[notes.Count - 1].order));
    }

    void MakeNote(int noteType)
    {
        Instantiate(Notes[noteType]);
    }

    IEnumerator AwaitMakeNote(Note note)
    {
        int noteType = note.noteType;
        int order = note.order;
        yield return new WaitForSeconds(startingPoint + order * beatInterval);
        MakeNote(noteType);
    }

    IEnumerator AwaitGameResult(int order)
    {
        yield return new WaitForSeconds(startingPoint + order * beatInterval + 3.0f);
        GameResult();
    }

    void GameResult()
    {
        GameManager.instance.audioSource.Stop();
        GameInformation.instance.maxCombo = GameManager.instance.maxCombo;
        GameInformation.instance.score = GameManager.instance.score;
        GameInformation.instance.rank = GameInformation.ranks.S;
        SceneManager.LoadScene("ResultScene");
    }

}