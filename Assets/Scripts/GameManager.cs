using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public enum judges { NONE = 0, BAD, GOOD, PERFECT, MISS };
    public GameObject judgement;
    public GameObject scoreUI;
    public GameObject[] JudgeParticles;
    public GameObject comboUI;
    public PlayerBehavior playerBehavior;
    public GameObject[] judgeBoxes;

    private Animator[] boxAnimators;
    private ParticleSystem[] particles;
    private Text scoreText;
    private Text comboText;
    private Sprite[] judgeSprites;
    private SpriteRenderer judgementSpriteRender;
    private Animator judgementSpriteAnimator;
    private Animator comboAnimator;
    private float score;
    private int combo;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    void Start() {
        GameStart();
        playerBehavior = GetComponent<PlayerBehavior>();
        judgementSpriteRender = judgement.GetComponent<SpriteRenderer>();
        judgementSpriteAnimator = judgement.GetComponent<Animator>();
        comboAnimator = comboUI.GetComponent<Animator>();
        comboText = comboUI.GetComponent<Text>();
        scoreText = scoreUI.GetComponent<Text>();
        judgeSprites = new Sprite[4];
        judgeSprites[0] = Resources.Load<Sprite>("Sprites/Bad");
        judgeSprites[1] = Resources.Load<Sprite>("Sprites/Good");
        judgeSprites[2] = Resources.Load<Sprite>("Sprites/Miss");
        judgeSprites[3] = Resources.Load<Sprite>("Sprites/Perfect");

        particles = new ParticleSystem[4];
        for(int i = 0; i < 4; i++) {
            particles[i] = JudgeParticles[i].GetComponent<ParticleSystem>();
            particles[i].Stop();
            particles[i].Emit(1);
        }
        boxAnimators = new Animator[4];
        for(int i = 0; i < 4; i++)
        {
            boxAnimators[i] = judgeBoxes[i].GetComponent<Animator>();
        }
    }
	
	void Update () {
		
	}

    void ShowParticle(int index)
    {
        particles[index].time = 0;
        particles[index].Emit(30);
        boxAnimators[index].SetTrigger("Show");
    }

    void ShowJudgement()
    {
        judgementSpriteAnimator.SetTrigger("Show");
        
        comboAnimator.SetTrigger("Show");
    }

    public void processJudge(judges judge, int noteType)
    {
        if (judge == judges.NONE) return;
        if(judge == judges.MISS)
        {
            judgementSpriteRender.sprite = judgeSprites[2];
            combo = 0;
            if(score >= 15)
            {
                score -= 15;
            }
        }
        else if(judge == judges.BAD)
        {
            judgementSpriteRender.sprite = judgeSprites[0];
            combo = 0;
            if (score >= 5)
            {
                score -= 5;
            }
        }
        else
        {
            score += (float) combo * 0.1f;
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
        }
        ShowJudgement();
        string scoreFormat = "000000";
        scoreText.text = score.ToString(scoreFormat);
        comboText.text = "COMBO " + combo.ToString();
    }

    void GameStart()
    {
        combo = 0;
        score = 0;
        Invoke("MusicStart", 2);
    }

    void MusicStart()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

}
