using LanceServer.Core.Configuration.DataModel;
using LanceServer.Core.Workspace;

namespace LanceServer.Core.Document;

/// <inheritdoc />
public class DocumentInformation : IDocumentInformation
{
    /// <inheritdoc />
    public Uri Uri { get; }
    
    /// <inheritdoc />
    public string FileExtension { get; }
    
    /// <inheritdoc />
    public DocumentType DocumentType { get; }
    
    /// <inheritdoc />
    public string Encoding { get; }

    /// <summary>
    /// Instantiates a new <see cref="DocumentInformation"/>
    /// </summary>
    public DocumentInformation(Uri uri, DocumentType documentType, string encoding)
    {
        Uri = uri;
        FileExtension = FileUtil.FileExtensionFromUri(uri);
        DocumentType = documentType;
        Encoding = encoding;
    }
    
    /// <summary>
    /// Instantiates a new <see cref="DocumentInformation"/>
    /// </summary>
    public DocumentInformation(Uri uri, SymbolTableConfiguration symbolTableConfiguration, string encoding = "utf8")
    {
        Uri = uri;
        FileExtension = FileUtil.FileExtensionFromUri(uri);
        Encoding = encoding;
        
        //Todo revert from file name to file extension after file extension in CAMeleon project is changed.
        var fileName = Path.GetFileName(uri.LocalPath).ToLower();
        if (symbolTableConfiguration.DefinitionFileExtensions.Any(fileExtension => FileExtension.EndsWith(fileExtension)))
        {
            DocumentType = DocumentType.Definition;
        }
        else if (symbolTableConfiguration.MainProcedureFileExtensions.Any(fileExtension => fileName.EndsWith(fileExtension)))
        {
            DocumentType = DocumentType.MainProcedure;
        } 
        else if (symbolTableConfiguration.SubProcedureFileExtensions.Any(fileExtension => FileExtension.EndsWith(fileExtension)))
        {
            var directories = Path.GetDirectoryName(uri.LocalPath)!.ToLower().Split(Path.DirectorySeparatorChar);
            var isInManufacturerCyclesDirectory = symbolTableConfiguration.ManufacturerCyclesDirectories.Intersect(directories).Any();
            DocumentType = isInManufacturerCyclesDirectory ? DocumentType.ManufacturerSubProcedure : DocumentType.SubProcedure;
        }
        else
        {
            throw new ArgumentException($"The lowercase file extension {FileExtension} has to match with one of the configured file extensions.");
        }
    }
}