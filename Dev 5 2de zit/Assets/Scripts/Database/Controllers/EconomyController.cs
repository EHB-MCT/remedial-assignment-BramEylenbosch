using UnityEngine;
using TMPro;
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
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (resourceText == null) return;

        var res = DatabaseBootstrapper.Resources.GetByName(resourceName);
        if (res != null)
            resourceText.text = $"{res.Name}: {res.Quantity}  |  €{res.Price:0.00}";
        else
            resourceText.text = $"{resourceName}: 0  |  €{basePrice:0.00}";
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
