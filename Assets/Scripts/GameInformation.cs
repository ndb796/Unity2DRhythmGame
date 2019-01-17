using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformation {

    // GameInformation을 싱글 톤 처리합니다.
    public static GameInformation instance { get; set; }
    public GameInformation()
    {
        instance = this;
    }

    // 사용자가 선택한 곡 정보입니다.
    public string music;
    public string musicTitle;

    // 게임이 끝난 이후 사용자의 최종 점수입니다.
    public float score;
    public int maxCombo;
    public enum ranks { C = 0, B, A, S};
    public ranks rank;

}
