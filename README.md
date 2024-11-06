# 📚 Gestion de Ventes - Mini-Projet AL-CA2®️ 📖
*Ce projet est une application web de gestion de ventes de produits informatiques, réalisée dans le cadre du cours Architecture logicielle et conception avancée. Elle est conçue pour mettre en pratique les principes d'architecture orientée micro-services et de conception logicielle avancée.*

> [!TIP]
> L'objectif est de développer une application robuste qui permet de gérer divers aspects de la vente de produits informatiques au sein d'une entreprise. Ce projet met en œuvre une architecture micro-services pour une meilleure modularité et évolutivité.

## 📌Architecture du Système📌
L'application est divisée en plusieurs micro-services indépendants, chacun avec des responsabilités spécifiques :

1. **Client Service** - Gère les informations des clients (DotNet).
2. **Produit Service** - Gère les informations des produits et des catégories (DotNet).
3. **Devises Service** - Gère les devises pour les transactions (à consommer).
4. **Facture Service** - Gère les factures des clients (Java).
5. **Règlement Service** - Gère les paiements des factures (Java).

>[!NOTE]
> *Tous les services développés en Java sont enregistrés auprès d’un service de découverte (**Eureka**) et sont gérés par un proxy (**Spring Cloud Gateway**). Les configurations de ces services sont centralisées via un service de configuration.* 

## Fonctionnalités

### @Étape 1 : Développement des APIs REST

- Gestion des clients, produits, catégories, factures, devises et règlements.
- Calcul et affichage des données analytiques par client :
  - Chiffre d'affaires réalisé (global et par année).
  - Montants des paiements en attente.
  - Factures réglées et non réglées.
  - Produits les plus sollicités.
- Liste des clients les plus fidèles.
- Liste des produits les plus vendus (globalement et par année).
- Gestion des stocks :
  - Produits en rupture de stock.
  - Liste des dettes par client.

### @Étape 2 : Sécurité avec JWT

Implémentation d'un micro-service d'authentification (**Auth-Service**) pour sécuriser l'application via JWT. Deux rôles principaux sont définis :
- **USER** : Accès aux opérations de lecture.
- **ADMIN** : Accès à toutes les opérations.

### @Étape 3 : Application FrontEnd

Développement d'une application cliente **React** pour consommer les APIs REST.

### @Étape 4 : Tests

Tester toutes fonctionnalités de l'application


## Installation et Configuration ⚙️
Pour installer et configurer l'application, suivez les étapes suivantes :

1. **Cloner le dépôt**

```bash
git clone https://github.com/Dechateau2035/Application-Web-Microservice.git
```

2. **Configurer les services**

    - Les services Java se configurent automatiquement via le micro-service de configuration.
    - Assurez-vous que les ports et configurations du proxy et des services sont correctement alignés.
    - Configurez le micro-service d'authentification (**Auth-Service**) pour générer les jetons de connexion

3. **Lancer l'application** : Utiliser Docker pour démarrer tous les services via un fichier ``` docker-compose.yml ```

4. **FrontEnd**
    - Installer les dépendances via npm
    ```
    npm install
    ```

## 🛠️ Technologies utilisées

- **BackEnd** : Java (Spring Boot), DotNet
- **FontEnd** : React Js
- **Gestion de Sécurité** : JWT
- **Conténerisation** : Docker
- **Proxy et Discovery** : Spring Cloud Gateway et Eureka

## Contributeurs
- **_[M. FOYOU Dechateau](https://github.com/Dechateau2035)_**