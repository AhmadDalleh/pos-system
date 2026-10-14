# 1. Use a modular monolith

Date: 2026-10-15
Status: Accepted

## Context

One developer builds this POS system, working about two hours a day. The goal is working software and also learning production-grade backend design: clear boundaries, transactions, concurrency, and testing.

The system has six areas with different responsibilities: Catalog, Inventory, Sales, Cashiering, Identity, and Reporting.

Without a clear structure, these areas start using each other's internals. For example, Sales code changes stock tables directly. Over time, a change in one area breaks others, and the code becomes hard to understand, test, and maintain.

## Decision

We use a modular monolith: one application and one database, separated into modules (Catalog, Inventory, Sales, Cashiering, Identity, Reporting) with clear boundaries and responsibilities. Each module has its own project and uses Clean Architecture layers inside it, and modules communicate with each other only through Contracts projects.

## Alternatives considered

### Microservices

For one developer, microservices add complexity and overhead without helping the goal:

- Many services to manage and deploy.
- Network calls that can be slow or fail.
- Data split across separate databases.

The biggest problem is consistency. If Sales and Inventory are separate services with separate databases, a sale is saved in the Sales database, then Sales calls Inventory to deduct stock. If Inventory is down at that moment, the sale exists but the stock is never deducted. One transaction cannot cover two databases, so the data becomes inconsistent. In a modular monolith, the sale and the stock change are saved in one transaction: both succeed or both fail.

### One project with Clean Architecture layers

The layers (Domain, Application, Infrastructure) would be separated, but the business areas would not. Nothing would stop Sales code from using Inventory's internal classes directly, so the boundaries between areas would exist only as an intention, and the code would become tightly coupled.

### Three projects per module

With six modules, that would be 18 projects for the layers alone: slower builds and more setup for little benefit at this size. Instead, layers inside a module are folders protected by architecture tests, and modules are separated by projects and Contracts. We get the benefits of separation without the overhead.

## Consequences

### What gets easier

- One application to run, debug, and deploy.
- One database to manage, so a sale, its payments, and its stock changes can be saved in one transaction.
- The compiler enforces the boundaries between modules: a module can only reference another module's Contracts project.
- Architecture tests enforce the layers inside each module, for example, Domain must not depend on Infrastructure.

### What gets harder

- Modules can't join each other's tables. If Sales needs product information, it must get it from Catalog through its Contracts, which means more code than a direct database query.
- Reporting needs data from all modules, so it needs a well-planned way to read it.
- Discipline is needed: every new piece of code must go in the right module, and sometimes the right place is not obvious.
- More projects and tests to maintain than a single-project application.

### What it makes possible later

Because the boundaries are already clean, a module could be extracted into its own service later if there were a real reason, without rewriting the system.