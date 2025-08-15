using UnityEngine;
using TMPro;
using EconomySim.Database.Bootstrap;
using EconomySim.Database.Models;

public class EconomyController : MonoBehaviour
{
    [Header("Resource Production")]
    public string resourceName = "Wood";
    public int resourcePerTick = 5;

    [Header("UI")]
    public TMP_Text resourceText;

    void Start()
    {
        // Ensure DB bootstrap has run (put DatabaseBootstrapper in scene)
        SimulationManager.Instance.OnTick += HandleTick;
        UpdateUI();
    }

    private void HandleTick()
    {
        // Get or create the resource
        var res = DatabaseBootstrapper.Resources.GetByName(resourceName);
        if (res == null)
        {
            res = new Resource { Name = resourceName, Quantity = 0, Price = 1f };
            DatabaseBootstrapper.Resources.Insert(res);
        }

        // Simple production
        res.Quantity += resourcePerTick;

        // (Optional) simple pricing logic: cheaper when plenty, more expensive when scarce
        res.Price = Mathf.Max(0.1f, res.Price + (res.Quantity < 50 ? 0.1f : -0.05f));

        DatabaseBootstrapper.Resources.Update(res);
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (resourceText == null) return;

        var res = DatabaseBootstrapper.Resources.GetByName(resourceName);
        if (res != null)
        {
            resourceText.text = $"{res.Name}: {res.Quantity}  |  €{res.Price:0.00}";
        }
        else
        {
            resourceText.text = $"{resourceName}: 0";
        }
    }
}
