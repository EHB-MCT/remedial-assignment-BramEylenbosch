using UnityEngine;
using TMPro;
using System.Linq;
using EconomySim.Database.Bootstrap;
using EconomySim.Database.Models;
using EconomySim.Simulation.Services;

public class EconomyController : MonoBehaviour
{
    [Header("Resource Production")]
    public string resourceName = "Wood";
    public int resourcePerTick = 5;

    [Header("Consumption")]
    public bool enableConsumption = true;
    public int consumptionPerTick = 3;

    [Header("Pricing (Inspector)")]
    public float basePrice   = 10f;
    public float minPrice    = 0.5f;
    public float maxPrice    = 50f;
    public int   targetStock = 100;
    public float elasticity  = 1.2f;

    [Header("UI")]
    public TMP_Text resourceText;
    public TMP_Text priceText;
    public TMP_Text transactionsText; 

    private IPricingService _pricing;

    void Start()
    {
        _pricing = new DynamicPricingService(minPrice, maxPrice, targetStock, elasticity);

        var res = DatabaseBootstrapper.Resources.GetByName(resourceName);
        if (res == null)
        {
            res = new Resource { Name = resourceName, Quantity = 0, Price = basePrice };
            DatabaseBootstrapper.Resources.Insert(res);
        }

        SimulationManager.Instance.OnTick += HandleTick;
        UpdateUI();
    }

    private void HandleTick()
    {
        var res = DatabaseBootstrapper.Resources.GetByName(resourceName);
        if (res == null) return;

        res.Quantity += resourcePerTick;

        if (enableConsumption)
            res.Quantity = Mathf.Max(0, res.Quantity - consumptionPerTick);

        res.Price = _pricing.Compute(basePrice, res.Quantity);

        DatabaseBootstrapper.Resources.Update(res);

        DatabaseBootstrapper.Transactions.Insert(new Transaction
        {
            ResourceName = res.Name,
            Quantity = res.Quantity,
            Price = res.Price,
            Timestamp = System.DateTime.UtcNow
        });

        UpdateUI();
    }

    private void UpdateUI()
    {
        var res = DatabaseBootstrapper.Resources.GetByName(resourceName);
        if (res == null) return;

        if (resourceText != null)
            resourceText.text = $"{res.Name}: {res.Quantity}";

        if (priceText != null)
            priceText.text = $"€{res.Price:0.00}";

        if (transactionsText != null)
        {
            var latest = DatabaseBootstrapper.Transactions
                .GetLatest(3) // laatste 3
                .Select(t => $"{t.ResourceName}: {t.Quantity} at €{t.Price:0.00}");

            transactionsText.text = "Last 3 transactions:\n" + string.Join("\n", latest);
        }
    }


    [ContextMenu("Reset Resource to 100")]
    public void ResetResourceTo100()
    {
        var res = DatabaseBootstrapper.Resources.GetByName(resourceName);
        if (res == null)
        {
            res = new Resource { Name = resourceName, Quantity = 100, Price = basePrice };
            DatabaseBootstrapper.Resources.Insert(res);
        }
        else
        {
            res.Quantity = 100;
            res.Price = basePrice;
            DatabaseBootstrapper.Resources.Update(res);
        }

        UpdateUI();
        Debug.Log($"[Economy] {resourceName} reset to 100.");
    }
}
