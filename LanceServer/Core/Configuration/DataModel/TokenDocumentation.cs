namespace LanceServer.Core.Configuration.DataModel;

/// <summary>
/// A documentation item for a language token
/// </summary>
public record TokenDocumentation(
    string Code,
    LanguageTokenType Type, 
    string Description, 
    ModalMode Modal, 
    SubProcedureAvailability SubProcedure, 
    SyncActionAvailability SyncAction, 
    Manual[] Manual
);