# TutoMVVM

- [TutoMVVM](#tutomvvm)
  - [1. Introduction](#1-introduction)
  - [2. Versions](#2-versions)
  - [3. RoadMap](#3-roadmap)

#
## 1. Introduction

A trading WPF application with purpose of learning the Model View ViewModel development method.
#
## 2. Versions

Version 0.7:
- Add view for the Portfolio
- Add view and viewmodel for the registration
- Improve logic and re-usability for assets list

Version 0.66:
- Add Json settings file for APIs and DB connection
- Implement host builder for DB connection
- Implement status and error message in view
- Implement async Command
- Improve the exception management

Version 0.65:

- Add a wallet view and viemModel
- Add a dynamic asset transaction logic
- Implement a delegated function to build the ViewModel
- Refactoring statement in view
- Refactoring and improve security for authentication and navigation
- Improve security with a better separation between ViewModel and logic

Version 0.6:

- Implement an authentication service for register and log
- Refactoring the navigation control to avoid circle dependency
- Implement a redirection service
- Immplement a new project for Units test
- Add Unit test for authentication service
- Add view and viewmodel for login
- Refactoring the ressource dictionnary for textbox and passwordbox

Version 0.5:

- Add view and viewmodel for Major Indexes
- Add dynamic View and viewmodel for buying stocks
- Add financial API services, retrieving major indexes and stock value by symbol
- Add a App.config to setup API key
- Implement dependency injection for rooting and access to services
- Add new ressource dictionnaries for button and textbox

Version 0.2:

- Add NavigationBar and Navigator Command
- Add Home view and viewmodel
- Add Portfolio view and viewmodel

Version 0.1:

- Add Entity models
- Add database and migrations
- Add database's services
- Add README

#
## 3. RoadMap

To come up:

- Authentification system - done
- User control for accounts
- Selling interfarce for stocks
- UI improvements
- performance optimization