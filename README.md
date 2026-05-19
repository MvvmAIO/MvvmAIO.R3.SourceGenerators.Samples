# MvvmAIO.R3.SourceGenerators — samples

This solution hosts small runnable apps that exercise [**MvvmAIO.R3.SourceGenerators**](https://www.nuget.org/packages/MvvmAIO.R3.SourceGenerators) **0.5.0** together with [**R3**](https://www.nuget.org/packages/R3).

## Projects

| Project | Description |
|--------|---------------|
| `R3.SourceGenerators.Samples` | WPF (.NET 8 Windows) — NuGet **0.5.0**. Demonstrates `FromEvents`, `FromEventHandlers`, interface hierarchy, generic constraints, and `[R3Command]`. |
| `R3.SourceGenerators.Samples.Avalonia` | Avalonia 11 (.NET 8) — NuGet **0.5.0**. Demonstrates **routed** and **attached routed** observable entry points. |

## WPF sample sections

| Nav item | What it shows |
|----------|----------------|
| Overview | Shell intro and section list |
| ObservableEvents · pointer | `FromEvents().MouseMove` on a WPF element |
| ObservableEvents · raised events | `Action` / `EventHandler` CLR events, NRT payloads |
| FromEventHandlers · EventHandler | `FromEventHandlers()` + `DispatcherTimer.Tick` |
| ObservableEvents · inheritance | `IssuerDerived.FromEvents()` — `IIssuerDerivedEvents : IIssuerBaseEvents` |
| ObservableEvents · generic constraints | `where T : ConstraintBase, IContributor, IAuditor` — combined event interface |
| [R3Command] · Commands | Generated commands + `INotifyPropertyChanged.FromEventHandlers()` |

Generated code for `FromEvents` / `FromEventHandlers` uses **event interfaces** (e.g. `IIssuerDerivedEvents`) and internal `*EventsImpl` classes. See the generator repo [design doc](https://github.com/MvvmAIO/MvvmAIO.R3.SourceGenerators/blob/master/docs/design-interface-based-event-generation.md).

## Build

From this directory:

```bash
dotnet build R3.SourceGenerators.Samples/R3.SourceGenerators.Samples.csproj -c Release
dotnet build R3.SourceGenerators.Samples.Avalonia/R3.SourceGenerators.Samples.Avalonia.csproj -c Release
```

If restore does not see **0.5.0** immediately after release:

```bash
dotnet nuget locals http-cache --clear
dotnet restore --force-evaluate
```

Or retry once [nuget.org/packages/MvvmAIO.R3.SourceGenerators/0.5.0](https://www.nuget.org/packages/MvvmAIO.R3.SourceGenerators/0.5.0) lists the version.

## Run

WPF:

```bash
dotnet run --project R3.SourceGenerators.Samples/R3.SourceGenerators.Samples.csproj -c Release
```

Avalonia:

```bash
dotnet run --project R3.SourceGenerators.Samples.Avalonia/R3.SourceGenerators.Samples.Avalonia.csproj -c Release
```

## Local generator development

To dogfood a **local** analyzer build instead of NuGet, replace the `PackageReference` in the `.csproj` with a `ProjectReference` to `MvvmAIO.R3.SourceGenerators.Roslyn4120` (see git history before the 0.5.0 sample update).
