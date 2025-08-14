# Economy Simulation - Database Layer

## Overzicht
Deze branch (`feature/db-setup`) bevat de volledige database-infrastructuur voor de economy simulation.  
De database gebruikt **SQLite-net** en wordt lokaal opgeslagen in `Application.persistentDataPath`.

Het systeem is opgezet volgens **SOLID principles** en maakt gebruik van het **Repository pattern** en een **Service layer**.  
Alle databasefunctionaliteit is centraal beschikbaar via de `DatabaseBootstrapper`.

---

## Mappenstructuur
Assets/
Scripts/
Database/
Models/ # Tabellen: Player, Resource, Transaction
Services/ # Database service + interface
Repositories/ # CRUD logica per tabel
Bootstrap/ # Bootstrapper die alles initialiseert

---

## Installatie
1. Voeg de **SQLite-net** package toe aan Unity via de Package Manager:
   - Open Unity → Window → Package Manager → Add package from git URL
   - URL: `https://github.com/praeclarum/sqlite-net.git`
2. Plaats de scripts volgens bovenstaande structuur.
3. Maak in je scene een leeg GameObject `DatabaseBootstrapper` en hang het script `DatabaseBootstrapper.cs` eraan.
4. Start de game één keer om de database en standaarddata aan te maken.

---

## Gebruik
In elk ander script kun je de database direct aanspreken via de Bootstrapper:


using EconomySim.Database.Bootstrap;

// Alle resources ophalen
var allResources = DatabaseBootstrapper.Resources.GetAll();

// Specifieke speler ophalen
var player = DatabaseBootstrapper.Players.GetById(1);

// Nieuwe transactie toevoegen
DatabaseBootstrapper.Transactions.Insert(new Transaction
{
    PlayerId = 1,
    ResourceId = 2,
    Amount = 50,
    PricePerUnit = 10,
    Timestamp = DateTime.UtcNow
});