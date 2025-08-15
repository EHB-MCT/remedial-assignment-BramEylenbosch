using UnityEngine;

namespace EconomySim.Simulation.Services
{

    public class DynamicPricingService : IPricingService
    {
        private readonly float _minPrice;
        private readonly float _maxPrice;
        private readonly int _targetStock;
        private readonly float _elasticity;

        public DynamicPricingService(float minPrice, float maxPrice, int targetStock, float elasticity = 1.0f)
        {
            _minPrice   = Mathf.Max(0.01f, minPrice);
            _maxPrice   = Mathf.Max(_minPrice, maxPrice);
            _targetStock = Mathf.Max(1, targetStock);
            _elasticity = Mathf.Max(0.1f, elasticity);
        }

        public float Compute(float basePrice, int quantity)
        {
            basePrice = Mathf.Max(0.01f, basePrice);
            quantity  = Mathf.Max(0, quantity);

            float ratio = (float)quantity / _targetStock;

            float factor = Mathf.Pow(1f / Mathf.Max(0.25f, ratio), _elasticity);

            float raw = basePrice * factor;
            return Mathf.Clamp(raw, _minPrice, _maxPrice);
        }
    }
}
