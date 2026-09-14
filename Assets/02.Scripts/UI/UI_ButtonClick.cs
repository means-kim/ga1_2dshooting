using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonClick : MonoBehaviour
{
    private Button _button;
    private AudioSource _audioSource;

    [Header("클릭시 애니메이션")]
    [SerializeField] private AnimationCurve _bumpCurve;

    private float _scale = 1.0f;
    private bool _isBumping = false;
    private float _elapsedTime = 0; // 경과 시간
    private const float BumpDuration = 0.3f;
    private const float BumpScale = 1.1f;

    // todo: 버튼 클릭할 때 애니메이션 주기 + 사운드 주기
    // 애니메이션, 사운드 컴포넌트: UI_ButtonClick
    // 애니메이션: 코드로 구현 (약간 커졌다가 작아지기)

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        // 동적으로 버튼 클릭시 실행할 함수 추가
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayAnimation);
        _button.onClick.AddListener(PlaySound);
    }

    private void Update()
    {
        if (!_isBumping) return;

        // 1. 경과 시간 누적
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > BumpDuration) // 시간이 다 지났다면..
        {
            transform.localScale = Vector3.one;
            _isBumping = false;
            return;
        }

        // 2. 누적 시간과 애니메이션 커브에 따른 스케일 변경
        float time = _elapsedTime / BumpDuration; // 얼마나 지났는지 퍼센트 (0 ~ 1)
        float curveValue = _bumpCurve.Evaluate(time); // 퍼센트에 따라 커브 애니메이션 값 추출
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * BumpScale, curveValue);
    }

    public void PlayAnimation()
    {
        _isBumping = true;
        _elapsedTime = 0;
    }

    // 사운드 : 일레븐랩스에서 버튼 클릭 공용 사운드 만들어서 적용
    public void PlaySound()
    {
        _audioSource.Play();
    }
}