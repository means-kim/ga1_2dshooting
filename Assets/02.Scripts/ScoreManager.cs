using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // static(정적)
    // 싱글톤 패턴
    // 1. 전역적으로 접근 가능하다.
    // 2. 인스턴스(생성된 객체)가 하나임을 보장한다.
    private static ScoreManager _instance = null;
    public static ScoreManager Instance => _instance;

    // 관리: 특정 데이터에 대한 무결성과 생성, 읽기, 수정, 삭제 등과 관련된 게임 로직

    private int _bestScore;
    private int _crrentScore;

    // 저장키
    private const string SaveKey = "BestScore";

    // UI 책임 추가 (TMP 참조)
    [SerializeField] private TextMeshProUGUI _bestScoreTextUI;
    [SerializeField] private TextMeshProUGUI _crrentScoreTextUI;

    private void Awake()
    {
        // 늦게 생성된 매니저는 삭제 (1개만 존재하게끔)
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        // 입력 : Input
        // 저장/불러오기: PlayerPrefs
        if (PlayerPrefs.HasKey(SaveKey))
        {
            _bestScore = PlayerPrefs.GetInt(SaveKey);
        }
        // _bestScore = PlayerPrefs.GetInt(SaveKey, 0);

        Refresh();
    }

    public void AddScore(int score)
    {
        if (score <= 0) return; // 이런 것을 방어 코드라고 한다.

        _crrentScore += score;
        if (_crrentScore > _bestScore)
        {
            _bestScore = _crrentScore;

            // 저장: PlayerPrefs.Set~ 시리즈를 이용해서 int/float/string을 저장 가능하다.
            // 내 컴퓨터 어딘가에 저장이 된다..
            PlayerPrefs.SetInt(SaveKey, _bestScore);
            PlayerPrefs.Save(); // 명시적으로 저장을 해주자 (의도치 않게 종료되면 저장이 안되기 때문)
        }

        Refresh();
    }

    private void Refresh()
    {
        _bestScoreTextUI.text = $"BestScore: {_bestScore}";
        _crrentScoreTextUI.text = $"Score: {_crrentScore}";
    }
}