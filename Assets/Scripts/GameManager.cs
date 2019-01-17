using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // GameManager를 싱글 톤 처리합니다.
    public static GameManager instance { get; set; }
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    // 사용자가 노트를 맞추었을 때 해당 라인 색상 변경 처리
    public GameObject[] judgeBoxes;
    private Animator[] boxAnimators;

    // 사용자가 노트를 맞추었을 때 발생하는 파티클 이펙트
    public GameObject[] JudgeParticles;
    private ParticleSystem[] particles;

    // 점수 및 콤보와 UI
    public float score;
    private int combo;
    public int maxCombo;
    public GameObject scoreUI;
    public GameObject comboUI;
    private Text scoreText;
    private Text comboText;
    private Animator comboAnimator;

    // 사용자 노트 판정 이미지
    public enum judges { NONE = 0, BAD, GOOD, PERFECT, MISS };
    public GameObject judgement;
    private Sprite[] judgeSprites;
    private SpriteRenderer judgementSpriteRender;
    private Animator judgementSpriteAnimator;

    // 자동 퍼펙트 판정 모드를 위한 변수
    public PlayerBehavior playerBehavior;
    public bool autoPerfect;

    // 음악 변수
    public AudioSource audioSource;

    // 게임 시작을 위한 초기화 함수입니다.
    void GameStart()
    {
        Screen.SetResolution(1920, 1200, true);
        combo = 0;
        maxCombo = 0;
        score = 0;
        Invoke("MusicStart", 2);
    }

    // 음악을 실행하는 함수입니다.
    void MusicStart()
    {
        // 리소스에서 비트(Beat) 음악 파일을 불러와 재생합니다.
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + GameInformation.instance.music);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    void Start() {
        GameStart();
        judgementSpriteRender = judgement.GetComponent<SpriteRenderer>();
        judgementSpriteAnimator = judgement.GetComponent<Animator>();
        scoreText = scoreUI.GetComponent<Text>();
        comboText = comboUI.GetComponent<Text>();
        comboAnimator = comboUI.GetComponent<Animator>();
        playerBehavior = GetComponent<PlayerBehavior>();

        // 판정 결과를 보여주는 스프라이트 이미지를 초기화합니다.
        judgeSprites = new Sprite[4];
        judgeSprites[0] = Resources.Load<Sprite>("Sprites/Bad");
        judgeSprites[1] = Resources.Load<Sprite>("Sprites/Good");
        judgeSprites[2] = Resources.Load<Sprite>("Sprites/Miss");
        judgeSprites[3] = Resources.Load<Sprite>("Sprites/Perfect");

        // 파티클 시스템을 초기화합니다.
        particles = new ParticleSystem[4];
        for(int i = 0; i < 4; i++) {
            particles[i] = JudgeParticles[i].GetComponent<ParticleSystem>();
            particles[i].Stop();
            particles[i].Emit(1);
        }

        // 사용자가 입력하는 키의 라인들을 초기화합니다.
        boxAnimators = new Animator[4];
        for(int i = 0; i < 4; i++)
        {
            boxAnimators[i] = judgeBoxes[i].GetComponent<Animator>();
        }
    }

    // 노트 판정 이후에 판정 결과를 화면에 보여줍니다.
    void ShowJudgement()
    {
        // 점수 이미지를 보여줍니다.
        string scoreFormat = "000000";
        scoreText.text = score.ToString(scoreFormat);
        // 판정 이미지를 보여줍니다.
        judgementSpriteAnimator.SetTrigger("Show");
        // 콤보가 2 이상일 때만 콤보 이미지를 보여줍니다.
        if(combo >= 2)
        {
            comboText.text = "COMBO " + combo.ToString();
            comboAnimator.SetTrigger("Show");
            if (maxCombo < combo) maxCombo = combo;
        }
    }

    // 노트를 맞추었을 때 파티클 이펙트 및 박스 이벤트를 실행합니다.
    void ShowParticle(int index)
    {
        particles[index].time = 0;
        particles[index].Emit(30);
        boxAnimators[index].SetTrigger("Show");
    }

    // 노트 판정을 진행합니다.
    public void processJudge(judges judge, int noteType)
    {
        if (judge == judges.NONE) return;
        // MISS 판정을 받은 경우 콤보를 종료하고, 점수를 많이 깎습니다.
        if(judge == judges.MISS)
        {
            judgementSpriteRender.sprite = judgeSprites[2];
            combo = 0;
            if(score >= 15) score -= 15;
        }
        // BAD 판정을 받은 경우 콤보를 종료하고, 점수를 조금 깎습니다.
        else if(judge == judges.BAD)
        {
            judgementSpriteRender.sprite = judgeSprites[0];
            combo = 0;
            if (score >= 5) score -= 5;
        }
        // PERFECT 혹은 GOOD 판정을 받은 경우 콤보 및 점수를 올립니다.
        else
        {
            if (judge == judges.PERFECT)
            {
                judgementSpriteRender.sprite = judgeSprites[3];
                ShowParticle(noteType);
                score += 20;
            }
            else if(judge == judges.GOOD)
            {
                judgementSpriteRender.sprite = judgeSprites[1];
                ShowParticle(noteType);
                score += 15;
            }
            combo += 1;
            score += (float)combo * 0.1f;
        }
        ShowJudgement();
    }

}