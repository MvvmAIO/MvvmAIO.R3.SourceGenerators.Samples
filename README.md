# MvvmAIO.R3.SourceGenerators — samples

This solution hosts small runnable apps that exercise [**MvvmAIO.R3.SourceGenerators**](https://www.nuget.org/packages/MvvmAIO.R3.SourceGenerators) together with [**R3**](https://www.nuget.org/packages/R3).

## Projects

| Project | Description |
|--------|---------------|
| `R3.SourceGenerators.Samples` | WPF (.NET 8 Windows) — **ProjectReference** to the Roslyn 4.12 analyzer project in a sibling clone of [MvvmAIO.R3.SourceGenerators](https://github.com/MvvmAIO/MvvmAIO.R3.SourceGenerators) (see `.csproj` path). |
| `R3.SourceGenerators.Samples.Avalonia` | Avalonia 11 (.NET 8) — references **`MvvmAIO.R3.SourceGenerators` 0.3.0** from NuGet and demonstrates **routed** and **attached routed** observable entry points. |

## Build (Avalonia sample)

From this directory:

```bash
dotnet build R3.SourceGenerators.Samples.Avalonia/R3.SourceGenerators.Samples.Avalonia.csproj -c Release
```

### NuGet 0.3.0 availability and cache

The Avalonia sample pins **`MvvmAIO.R3.SourceGenerators` 0.3.0** on NuGet. Right after publishing a new version, restore can fail until the package is indexed, or your machine may still resolve an older copy from cache.

If restore does not see **0.3.0** yet:

```bash
dotnet nuget locals http-cache --clear
dotnet restore R3.SourceGenerators.Samples.Avalonia/R3.SourceGenerators.Samples.Avalonia.csproj --force-evaluate
```

Or retry after a short delay once [nuget.org/packages/MvvmAIO.R3.SourceGenerators/0.3.0](https://www.nuget.org/packages/MvvmAIO.R3.SourceGenerators/0.3.0) lists the version.

## Run

```bash
dotnet run --project R3.SourceGenerators.Samples.Avalonia/R3.SourceGenerators.Samples.Avalonia.csproj -c Release
```
