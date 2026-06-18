using UnityEngine;
using TMPro;

public class WaveHud : MonoBehaviour
{
    [SerializeField] WaveManager waveManager;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI aliveText;
    [SerializeField] private TextMeshProUGUI stateText;

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        waveText.text = "WAVE " + waveManager.CurrentWave + " / " + waveManager.MaxWave;
        progressText.text = "SPAWN " + waveManager.SpawnedCount + " / " + waveManager.EnemyCountForThisWave;
        aliveText.text = "ALIVE " + waveManager.AliveCount;
        stateText.text = waveManager.CurrentWave.ToString();
    }
}
