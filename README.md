# Kouta Calculator - Atelier DevMobile 4

Calculatrice mobile réalisée avec .NET MAUI. Elle gère l'addition, la soustraction, la multiplication, la division, les décimales, le changement de signe, le pourcentage, l'effacement, la remise à zéro et la division par zéro sans arrêt de l'application.

## Lancer le projet

1. Installer Visual Studio 2022 avec la charge de travail **.NET Multi-platform App UI development**.
2. Ouvrir `KoutaCalculator.csproj`.
3. Choisir un émulateur Android ou la cible Windows, puis lancer l'application.

## Layouts employés

- `Grid` structure la page et le pavé numérique en lignes/colonnes adaptatives.
- `VerticalStackLayout` empile le titre et les éléments de l'afficheur.
- `HorizontalStackLayout` aligne les quatre commandes utilitaires sur une seule ligne.
- `ScrollView` permet à une expression longue de rester lisible sans déformer l'écran.
- `Border` crée une zone d'affichage visuellement séparée et arrondie.
