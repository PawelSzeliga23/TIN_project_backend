# Aplikacja Backendowa - Biblioteka Szczytów Polskich Gór

Projekt semestralny z przedmiotu **Technologie Internetowe (TIN)**.

## Opis
Aplikacja backendowa stworzona w frameworku ASP.NET Core, będąca częścią projektu semestralnego. Celem projektu jest stworzenie REST API dla **Biblioteki Szczytów Polskich Gór**. Aplikacja umożliwia zarządzanie danymi o szczytach górskich, regionach oraz komentarzach, a także obsługę użytkowników. API zapewnia pełną funkcjonalność CRUD (Create, Read, Update, Delete) dla szczytów górskich, regionów i komentarzy oraz wspiera autoryzację i uwierzytelnianie za pomocą JWT.

## Funkcjonalności
- Obsługa użytkowników (rejestracja, logowanie).
- CRUD dla wybranych zasobów (szczyty górskie, regiony, komentarze).
- Autoryzacja i uwierzytelnianie użytkowników z użyciem JWT.
- Obsługa zapytań API zgodnie z architekturą REST.
- Integracja z bazą danych przy pomocy Entity Framework Core, przechowywanie danych o szczytach, regionach i komentarzach.

## Wymagania
- .NET 6.0 (lub wyższy).
- SQL Server (lub inna baza danych, jeśli preferujesz).
- Visual Studio 2022 / Rider.

## Instalacja
1. Sklonuj repozytorium:
   ```bash
   git clone https://github.com/PawelSzeliga23/TIN_project_backend.git
