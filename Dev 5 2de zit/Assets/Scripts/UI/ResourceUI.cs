using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    public TMP_Text resourceText;

    private EconomyService _economyService;

    void Start()
    {
        _economyService = FindObjectOfType<EconomyService>();
        if (_economyService == null)
        {
            Debug.LogError("[UI] EconomyService not found in scene!");
            return;
        }

        SimulationManager.Instance.OnTick += UpdateUI;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (resourceText != null)
        {
            resourceText.text = $"Resources: {_economyService.resourceAmount}";
        }
    }
}
