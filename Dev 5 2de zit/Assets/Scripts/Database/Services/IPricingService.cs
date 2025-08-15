namespace EconomySim.Simulation.Services
{
    public interface IPricingService
    {
        float Compute(float basePrice, int quantity);
    }
}
