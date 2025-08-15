# Changelog

---

## v1.3.0 – 15/08/2025 – Dynamic Pricing

### Toegevoegd

- **Dynamische prijsberekening** voor resources.
- Prijzen passen zich automatisch aan op basis van voorraad- en vraagveranderingen.
- Integratie met `EconomyService` zodat prijswijzigingen persistent worden opgeslagen in SQLite.
- Realtime prijsupdates zichtbaar in de UI dankzij koppeling met het `OnTick` event-systeem.
- **Testscenario’s** uitgevoerd om prijsfluctuaties te verifiëren in zowel korte als lange simulatieruns.

---

## v1.2.0 – 14/08/2025 – Simulation Core

### Toegevoegd

- **SimulationManager**:
    - Centrale tick-loop met configureerbaar tick-interval.
    - Event-systeem (`OnTick`) voor synchronisatie van game-logica.
- **Economy**:
    - `EconomyRepository` voor CRUD-operaties op `ResourceData` in SQLite.
    - `EconomyService` voor resourcebeheer (laden, opslaan, toevoegen).
    - `EconomyController` ontvangt tick-events en werkt resources bij.
    - Resource-data persistent in `economy.db`.
- **UI**:
    - `ResourceUI` met TextMeshPro voor real-time resourceweergave.
    - Automatische updates op elke tick via event-systeem.
- **Test & verificatie**:
    - Unity scene met `SimulationManager`, `EconomyController` en `ResourceUI`.
    - Tick-loop getest: resources stijgen met `resourcePerTick`.
    - Persistentie getest: waarden blijven behouden na stoppen en herstarten.

---

## v1.1.0 – 14/08/2025 – Database Setup

### Toegevoegd

- **SQLite database-integratie** in Unity.
- **Projectstructuur**:
    - Bootstrap voor initialisatie van database en repositories.
    - Models (`Player`, `Resource`, `Transaction`).
    - Repositories met CRUD-operaties.
    - Services voor databaseconnectie en logica.
- **Proof of Concept** (`DatabaseTestPOC.cs`):
    - Database aangemaakt in `Application.persistentDataPath`.
    - Tabellen gecreëerd.
    - Testdata ge-seed (Alice & Bob).
    - Data gelezen en gelogd in Unity Console.
- **Extra tools**:
    - DB Browser verificatie van databasebestand.
    - Menu-optie in Unity Editor om `Application.persistentDataPath` te openen.

---
