namespace LanceServer.Core.Configuration.DataModel;

/// <summary>
/// The configuration for the scopes of symbols of different files
/// </summary>
public record SymbolTableConfiguration(string[] DefinitionFileExtensions, string[] SubProcedureFileExtensions, string[] MainProcedureFileExtensions, string[] ManufacturerCyclesDirectories);