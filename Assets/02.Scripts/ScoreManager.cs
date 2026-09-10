using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // 관리: 특정 데이터에 대한 무결성과 생성, 읽기, 수정, 삭제 등과 관련된 게임 로직

    private int _bestScore;
    private int _crrentScore;

    // UI 책임 추가 (TMP 참조)
    [SerializeField] private TextMeshProUGUI _bestScoreTextUI;
    [SerializeField] private TextMeshProUGUI _crrentScoreTextUI;

    private void Update()
    {
        _bestScoreTextUI.text = $"BestScore: {_bestScore}";
        _crrentScoreTextUI.text = $"Score: {_crrentScore}";
    }
}