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
    private int _crrentScore = 0;
    private int _lastRefreshScore = -1;

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

    public void AddScore(int score)
    {
        if (score <= 0) return; // 이런 것을 방어 코드라고 한다.

        _crrentScore += score;
        if (_crrentScore > _bestScore)
        {
            _bestScore = _crrentScore;
        }
    }

    private void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        if (_lastRefreshScore == _crrentScore) return;

        _bestScoreTextUI.text = $"BestScore: {_bestScore}";
        _crrentScoreTextUI.text = $"Score: {_crrentScore}";

        _lastRefreshScore = _crrentScore;
    }
}