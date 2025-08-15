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

Changelog – dynamic-pricing branch
Toegevoegd

DynamicPricingService

Nieuwe service geïntroduceerd voor prijsberekening op basis van marktvoorraad.

Ondersteunt parameters:

minPrice / maxPrice

targetStock

elasticity

Prijs verandert automatisch bij elke voorraadwijziging.

HandleTick in EconomyController

Tick-event gekoppeld aan SimulationManager.

Iedere tick:

Productie (resourcePerTick) en optionele consumptie (consumptionPerTick).

Automatische prijsherberekening via DynamicPricingService.

Transactie logging in de database.

Balans & voorraadbeheer in Buy/Sell

BuyResource(resourceName):

Marktvoorraad neemt af bij aankoop.

Spelerbalans vermindert.

Prijs herberekend na transactie.

Transactie gelogd (negatieve hoeveelheid voor aankoop).

SellResource(resourceName):

Marktvoorraad neemt toe bij verkoop.

Spelerbalans stijgt.

Prijs herberekend na transactie.

Transactie gelogd (positieve hoeveelheid voor verkoop).

UI-uitbreidingen

Toegevoegd:

Laatste 3 transacties (transactionsText).

Dynamische balansweergave (balanceText).

Resource- en prijslabels geüpdatet bij elke tick en transactie.

Resetfunctie

ResetResourceTo100() in EconomyController om voorraad handmatig op 100 te zetten en prijs te resetten.

Gewijzigd

Database bootstrap

Initieert nu altijd een speler (Player1) met startbalans €1000 indien nog niet aanwezig.

Initieert resource indien niet aanwezig met basePrice.

Buy/Sell logica

Oude logica vervangen zodat voorraad correct reageert op speleracties.

Prijs nu afhankelijk van actuele voorraad i.p.v. vast bedrag.

SimulationManager integratie

Handmatige voorraadmutaties vervangen door tick-gebaseerde productie en consumptie.

In bewerking

Integratie van Polygon.io API voor live aandelenprijzen:

Meerdere tickers tegelijk ophalen.

Limietbescherming (15 sec interval).

UI koppelen aan live prijs in plaats van interne prijsberekening.