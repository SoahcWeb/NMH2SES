# 🎬 NHM - Application Blazor Films

##  Description du projet

NHM est une application web développée avec **Blazor** permettant d’afficher des films issus d’une API externe (The Movie Database - TMDB).

L’application permet à un utilisateur de :
- consulter une liste de films
- voir les détails d’un film
- se connecter via une authentification simple
- ajouter des films en favoris
- gérer ses favoris (ajout / suppression)
- ajouter un commentaire et une note à ses favoris

---

##  Fonctionnalités principales

###  Films (API)
- Récupération de films via API
- Affichage des posters et détails
- Navigation entre les pages

###  Authentification
- Login / Logout simple
- Session utilisateur simulée
- Protection de la page favoris

###  Favoris
- Ajout de films en favoris
- Suppression de favoris
- Favoris liés à l’utilisateur connecté
- Sauvegarde locale en JSON

###  Personnalisation des favoris
- Ajout d’un commentaire
- Ajout d’une note (1 à 10)
- Sauvegarde des modifications en localStorage

---

## Architecture

Le projet est structuré en plusieurs couches :

###  Services
- `AuthService` → gestion de l’authentification
- `FavoriteService` → gestion des favoris

###  Models
- `Movie`
- `FavoriteMovieEdit`

###  Pages
- Home
- Movies
- Movie Details
- Favorites
- Login

---

##  Persistance des données

- Favoris stockés en **fichier JSON local**
- Données utilisateur (commentaires / notes) stockées dans **localStorage**

---

##  Tests

Un projet de tests est inclus :

###  AuthServiceTests
- validation login
- gestion logout
- vérification utilisateur courant
- cas d’erreurs

Framework utilisé :
- xUnit

---

##  Technologies utilisées

- Blazor Server / WebAssembly
- .NET
- C#
- HTML / CSS
- JavaScript (interop localStorage)
- xUnit (tests)
- API TMDB

---

##  Fonctionnement de l’authentification

L’authentification est simplifiée :
- un mot de passe unique est utilisé (`password`)
- l’utilisateur est stocké en mémoire
- aucune base de données réelle n’est utilisée

---

##  Fonctionnement des favoris

Les favoris sont :
- liés à l’utilisateur connecté
- stockés localement (JSON)
- modifiables (commentaire + note)
- persistants entre sessions

---

##  Améliorations possibles

- authentification réelle (JWT / Identity)
- base de données (SQL Server / SQLite)
- pagination des films
- recherche avancée
- déploiement cloud

---

##  Auteur

Projet réalisé par Jonathan Pauwels dans le cadre d’un examen de développement web avec Blazor.
