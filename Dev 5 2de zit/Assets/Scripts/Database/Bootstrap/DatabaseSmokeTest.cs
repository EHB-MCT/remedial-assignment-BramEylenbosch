using UnityEngine;
using EconomySim.Database.Bootstrap;
using EconomySim.Database.Models;

public class DatabaseSmokeTest : MonoBehaviour
{
    void Start()
    {
        // Print resources
        foreach (var r in DatabaseBootstrapper.Resources.GetAll())
            Debug.Log($"[DB] Resource: {r.Id} {r.Name} qty={r.Quantity} price={r.Price}");

        // Example: update price of Wood
        var wood = DatabaseBootstrapper.Resources.GetByName("Wood");
        if (wood != null)
        {
            wood.Price += 1.0f;
            DatabaseBootstrapper.Resources.Update(wood);
            DatabaseBootstrapper.Transactions.Insert(new Transaction
            {
                ResourceId = wood.Id,
                PlayerId = null,
                Amount = 0,
                UnitPrice = wood.Price,
                Type = "system"
            });
            Debug.Log("[DB] Updated Wood price and logged system transaction.");
        }
    }
}
