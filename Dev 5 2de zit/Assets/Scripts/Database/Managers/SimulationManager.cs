using UnityEngine;
using System;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance { get; private set; }

    [Header("Simulation Settings")]
    public float tickInterval = 1f;

    private float _tickTimer;

    public event Action OnTick; 

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Debug.Log("[Simulation] SimulationManager gestart.");
        _tickTimer = tickInterval;
    }

    void Update()
    {
        _tickTimer -= Time.deltaTime;
        if (_tickTimer <= 0f)
        {
            RunTick();
            _tickTimer = tickInterval;
        }
    }

    private void RunTick()
    {
        Debug.Log($"[Simulation] Tick uitgevoerd op {DateTime.UtcNow:O}");
        OnTick?.Invoke();
    }
}
