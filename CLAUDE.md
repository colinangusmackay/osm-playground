# CLAUDE.md

This file gives Claude Code guidance for working in this repository.

## Project overview

<!-- TODO: Describe what this project does and who it is for. -->

The repository contains .NET applications backed by a PostgreSQL database.

## Tech stack

- **Language / runtime:** C# on .NET 10
- **Database:** PostgreSQL 18.6
- **Data access:** EF Core with Npgsql and it's NetTopologySuite (NTS) plugin.
- **Testing:** xUnit for the tests and Shouldly for the assertions.
- **IDEs:** JetBrains Rider (code), DataGrip (database)

## Repository layout


```
/src        Application projects
/src/tests  Test projects
/db         Database scripts outwith that required by EF Core.
```

### .NET Solution layout



## Common commands

```bash
# Restore, build and test
dotnet restore
dotnet build
dotnet test

# Run a single test project / filter tests
dotnet test tests/<Project>.Tests
dotnet test --filter "FullyQualifiedName~<TestName>"

# Run an application
dotnet run --project src/<Project>

# Format code
dotnet format
```

### Database

```bash
# Apply migrations
# TODO: e.g. dotnet ef database update --project src/<DataProject> --startup-project src/<AppProject>

# Add a migration
# TODO: e.g. dotnet ef migrations add <Name> --project src/<DataProject> --startup-project src/<AppProject>

# Connect with psql
# TODO: e.g. psql -h localhost -U <user> -d <database>
```

## Configuration

- Connection strings live in `appsettings.json` / `appsettings.Development.json` under `ConnectionStrings`.
- Never commit real credentials. Use `dotnet user-secrets`, environment variables, or `appsettings.*.local.json` (git-ignored) for local secrets.

## Coding conventions

- Follow standard .NET naming: `PascalCase` for types, methods and properties; `camelCase` for locals and parameters; `_camelCase` for private fields.
- Enable nullable reference types and treat warnings seriously.
- Prefer `async`/`await` end to end for I/O; pass `CancellationToken` through.
- Use dependency injection rather than static state.
- Keep code style consistent with `.editorconfig` (if present).

## Database conventions

- Enable the `postgis` extension in the newly created database.
- PostgreSQL identifiers use `snake_case` (tables, columns, constraints).
- All schema changes go through EF migrations — never edit the database schema by hand.
- Do not modify migrations that have already been applied to shared environments; add a new one instead.
- Use parameterised queries only; never build SQL by string concatenation.
- <!-- TODO: schema names, naming for indexes/constraints, seeding approach -->

## Testing

- Add or update tests alongside code changes.
- <!-- TODO: how integration tests get a database (Testcontainers, a local instance, etc.) -->
- Run `dotnet test` before considering a change complete.

## Things to avoid

- Don't commit build output (`bin/`, `obj/`) or IDE user files — see `.gitignore`.
- Don't run destructive database commands (`DROP`, `TRUNCATE`, `dotnet ef database drop`) without asking first.

## Scripts

- Scripts should be executable and have the first line be `#!/usr/bin/env bash` or `#!/usr/bin/env pwsh` as appropriate.
- Scripts are stored in the /scripts folder.
