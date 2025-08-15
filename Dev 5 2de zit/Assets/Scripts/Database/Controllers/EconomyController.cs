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
    public float basePrice = 10f;
    public float minPrice = 0.5f;
    public float maxPrice = 50f;
    public int targetStock = 100;
    public float elasticity = 1.2f;

    [Header("UI")]
    public TMP_Text resourceText;
    public TMP_Text priceText;
    public TMP_Text transactionsText;
    public TMP_Text balanceText;

    private IPricingService _pricing;

    void Start()
    {

        DatabaseBootstrapper.Initialize();

        var player = DatabaseBootstrapper.Players.GetById(1);
        if (player == null)
        {
            player = new Player { Name = "Player1", Balance = 1000f };
            DatabaseBootstrapper.Players.Insert(player);
        }

        var res = DatabaseBootstrapper.Resources.GetByName(resourceName);
        if (res == null)
        {
            res = new Resource { Name = resourceName, Quantity = 0, Price = basePrice };
            DatabaseBootstrapper.Resources.Insert(res);
        }

        _pricing = new DynamicPricingService(minPrice, maxPrice, targetStock, elasticity);

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

        var lastTransactions = DatabaseBootstrapper.Transactions
            .GetByResourceName(resourceName)
            .OrderByDescending(t => t.Timestamp)
            .Take(3)
            .Select(t => $"{t.ResourceName}: {t.Quantity} at €{t.Price:0.00}")
            .ToList();

        if (transactionsText != null)
            transactionsText.text = "Last 3 transactions:\n" + string.Join("\n", lastTransactions);
            var player = DatabaseBootstrapper.Players.GetById(1);
        if (player != null && balanceText != null)
            balanceText.text = $"Balance: €{player.Balance:0.00}";
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



public void BuyResource(string resourceName)
{
    // Zorg dat DB en entiteiten bestaan (zou al gebeurd moeten zijn in Start)
    var player = DatabaseBootstrapper.Players.GetById(1);
    var res    = DatabaseBootstrapper.Resources.GetByName(resourceName);

    if (player == null || res == null)
    {
        Debug.LogError("Player or resource not found!");
        return;
    }

    // Prijs vooraf vastklikken voor de transactie
    float priceBefore = res.Price;

    // Marktvoorraad moet voldoende zijn
    if (res.Quantity <= 0)
    {
        Debug.Log($"No {resourceName} available to buy.");
        return;
    }

    // Speler moet genoeg balans hebben
    if (player.Balance < priceBefore)
    {
        Debug.Log("Not enough balance!");
        return;
    }

    // Transactie: speler koopt 1 ⇒ marktvoorraad --, balans --
    res.Quantity -= 1;
    player.Balance -= priceBefore;

    // Recompute prijs nà voorraadwijziging
    res.Price = _pricing.Compute(basePrice, res.Quantity);

    // Persist
    DatabaseBootstrapper.Resources.Update(res);
    DatabaseBootstrapper.Players.Update(player);

    // Log transactie (negatief = voorraad uit de markt naar speler)
    DatabaseBootstrapper.Transactions.Insert(new Transaction
    {
        ResourceName = res.Name,
        Quantity     = -1,
        Price        = priceBefore,
        Timestamp    = System.DateTime.UtcNow
    });

    UpdateUI();
    Debug.Log($"Bought 1 {resourceName} for €{priceBefore:0.00}. New balance: €{player.Balance:0.00}");
}

public void SellResource(string resourceName)
{
    var player = DatabaseBootstrapper.Players.GetById(1);
    var res    = DatabaseBootstrapper.Resources.GetByName(resourceName);

    if (player == null || res == null)
    {
        Debug.LogError("Player or resource not found!");
        return;
    }

    // Prijs vooraf vastklikken voor de transactie
    float priceBefore = res.Price;

    // (Als je later speler-voorraad bijhoudt, check hier of de speler >0 heeft)
    // Voor nu laten we altijd verkopen toe aan de markt.

    // Transactie: speler verkoopt 1 ⇒ marktvoorraad ++, balans ++
    res.Quantity += 1;
    player.Balance += priceBefore;

    // Recompute prijs nà voorraadwijziging
    res.Price = _pricing.Compute(basePrice, res.Quantity);

    // Persist
    DatabaseBootstrapper.Resources.Update(res);
    DatabaseBootstrapper.Players.Update(player);

    // Log transactie (positief = voorraad de markt in)
    DatabaseBootstrapper.Transactions.Insert(new Transaction
    {
        ResourceName = res.Name,
        Quantity     = +1,
        Price        = priceBefore,
        Timestamp    = System.DateTime.UtcNow
    });

    UpdateUI();
    Debug.Log($"Sold 1 {resourceName} for €{priceBefore:0.00}. New balance: €{player.Balance:0.00}");
}


}
