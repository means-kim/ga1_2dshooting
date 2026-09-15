using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // 업그레이드 관리자 : 업그레이드들에 대한 무결성과 생성, 조회, 수정, 삭제 등과 관련된 게임 로직
    private static UpgradeManager _instance = null;
    public static UpgradeManager Instance => _instance;

    // 업그레이드 도메인 클래스들
    [SerializeField] private Upgrade[] _upgrades;
    public Upgrade[] Upgrades => _upgrades;

    // 업그레이드 UI들
    [SerializeField] private UI_Upgrade[] _uiUpgrades;
    private const string UpgradeSaveDataKey = "UpgradeSaveData";

    private void Start()
    {
        Load();

        RefreshUI();
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    // UI 갱신
    public void LevelUp(int index)
    {
        // Todo: 묻지말고 시켜라! (아래를 시키는 메서드 생성)
        // 점수 매니저에게 점수가 있는지 물어보고 점수가 있다면 차감 후 업그레이드 호출

        Upgrade upgrade = _upgrades[index];

        if (ScoreManager.Instance.Score <= upgrade.Cost) return;

        ScoreManager.Instance.SpendScore(upgrade.Cost);

        _upgrades[index].LevelUp();

        Save();

        RefreshUI();
    }

    private void RefreshUI()
    {
        foreach (UI_Upgrade uiUpgrade in _uiUpgrades)
        {
            uiUpgrade.Refresh();
        }
    }

    private void Save()
    {
        // 데이터 저장은 유의미한 정보만 저장을 한다.
        // 여기서는 레벨만 저장하면 나머지는 역산이 가능하다.

        UpgradeSavaData saveData = new UpgradeSavaData(_upgrades.Length);
        for (int i = 0; i < _upgrades.Length; i++)
        {
            saveData.Name[i] = _upgrades[i].Name;
            saveData.Level[i] = _upgrades[i].Level;
        }
        // 혹시 게임 데이터를 보면 확장자 별로 게임별로 다 다르다.
        // json 포맷으로 문자열 변환
        // 키와 밸류 형태로 저장한 형태

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(UpgradeSaveDataKey, json);

        PlayerPrefs.Save();
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey(UpgradeSaveDataKey)) return;

        string json = PlayerPrefs.GetString(UpgradeSaveDataKey);

        UpgradeSavaData saveData = JsonUtility.FromJson<UpgradeSavaData>(json);

        for (int i = 0; i < _upgrades.Length; i++)
        {
            Debug.Log($"{_upgrades[i].Name} 로드 완료");
            _upgrades[i].SetLevel(saveData.Level[i]);
        }
    }
}