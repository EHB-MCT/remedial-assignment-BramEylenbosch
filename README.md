# EconomySim
_Een Unity-project dat een dynamische economie simuleert.

## Overzicht

EconomySim biedt:

- **Speler met balans**
- **Resources** (zoals hout, aandelen, grondstoffen)
- **Kopen en verkopen** via knoppen in de UI
- **Dynamische prijsbepaling** met elasticiteit
- **Transacties en historische data** opgeslagen in SQLite

---

## Features

- ✅ **SQLite database** met spelers, resources en transacties
- ✅ **Buy/Sell knoppen** die balans en voorraad updaten
- ✅ **Dynamic Pricing Service**: prijzen op basis van voorraad, min/max en elasticiteit
- ✅ **Simulation Manager**: ticks simuleren productie en consumptie
- ✅ **Logging** van laatste transacties
- ✅ **UI-integratie** met TextMeshPro

---

## Installatie

1. **Clone of download** dit project.
2. **Open in Unity** (2021.3+ aanbevolen).
3. **Installeer TextMeshPro** via de Package Manager.
4. **Voeg UI-componenten toe** in je scene:
    - EconomyController script aan een GameObject
    - Koppel TextMeshPro UI-elementen (resource, price, transactions, balance)
    - Koppel knoppen aan `BuyResource` en `SellResource`

---

## Gebruik

- Start het spel → speler en resource worden automatisch aangemaakt.
- De simulatie produceert en consumeert resources elke tick.
- Klik op **Buy** om resources te kopen (kost balans, voorraad daalt).
- Klik op **Sell** om resources te verkopen (balans stijgt, voorraad stijgt).
- Prijzen worden dynamisch berekend via de DynamicPricingService.

---

## Bronnen

- [Unity Documentation](https://docs.unity3d.com/)
- [SQLite-net ORM](https://github.com/praeclarum/sqlite-net)
- [TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@latest)
- [Chatgpt] (https://chatgpt.com/share/68a0fcee-77ec-8004-91fc-6b37e37c90fc)