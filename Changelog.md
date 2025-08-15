[Added to db-setup]

SQLite database integratie in Unity.

Projectstructuur voor database:

Bootstrap (initialisatie van database en repositories)

Models (Player, Resource, Transaction)

Repositories (CRUD-operaties voor database-objecten)

Services (databaseconnectie en logica)

POC testscript (DatabaseTestPOC.cs) om:

Database aan te maken in persistentDataPath

Tabellen te creëren

Testdata te seeden (Alice & Bob)

Data te lezen en te loggen in Unity Console

DB Browser verificatie: databasebestand getest en geopend in DB Browser.

Menu-optie in Unity Editor om Application.persistentDataPath direct te openen.

[Added to simulation-core]

Simulation Core implementatie in Unity.

Toegevoegde functionaliteiten:

SimulationManager:

Centrale tick-loop met configureerbare tick-interval.

Event-systeem (OnTick) voor synchronisatie van game-logica.

Economy:

EconomyRepository voor CRUD-operaties op ResourceData in SQLite.

EconomyService voor resourcebeheer (laden, opslaan, toevoegen).

EconomyController die tick-events ontvangt en resources bijwerkt.

Resource-data persistent in SQLite via economy.db.

UI:

ResourceUI met TextMeshPro integratie voor real-time resourceweergave.

Automatische updates op elke tick via event-systeem.

Test & verificatie:

Unity scene geconfigureerd met SimulationManager, EconomyController, en ResourceUI.

Tick-loop getest: resources stijgen met resourcePerTick.

Data persistent getest: waarden blijven bewaard na stoppen en herstarten van de game.