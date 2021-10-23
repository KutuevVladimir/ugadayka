# API for music guessing

---
## How to build

```dotnet build```

## But first

One needs to configure the databese (currently MySql is used), for this `dotnet-eq` cli tool needs to be installed with 
```bash
dotnet tool install --global dotnet-ef
```

then

```bash

cp ConnectionStrings.json.template ConntectionStrings.json

```

, which is ignored and should not be commited, and speicify the desired connection string.

Then store this string into `user-secrets` with

```
dotnet user-secrets init
type .\ConnectionStrings.json | dotnet user-secrets set
```

Then you can run

```bash

dotnet-eq database update

```

to apply the last migration. And finally 

```bash

dotnet run

```

