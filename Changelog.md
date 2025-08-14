[Added]

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