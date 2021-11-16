# API for music guessing

---
## How to build

```dotnet build```

## But first

One needs to configure the databese (currently MySql is used), for this `dotnet-ef` cli tool needs to be installed with 
```bash
dotnet tool install --global dotnet-ef
```

then

```bash

cp ConnectionStrings.json.template ConnectionStrings.json

```

, which is ignored and should not be commited, and speicify the desired connection string.

Then store this string into `user-secrets` with

[windows]

```
dotnet user-secrets init
type .\ConnectionStrings.json | dotnet user-secrets set
```
[linux]

```
dotnet user-secrets init
cat ./ConnectionStrings.json | dotnet user-secrets set
```

Then you can run

```bash

dotnet-ef database update

```

to apply the last migration. And finally 

```bash

dotnet run

```

## Troubleshoot

If `dotnet-ef database update` exit with an error, try to create a migration script `dotnet-ef migrations script -o migration_script.sql` and apply it manually.