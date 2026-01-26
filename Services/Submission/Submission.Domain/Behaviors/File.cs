using FIleStorage.Contracts;

namespace Submission.Domain.ValuesObjects;

public partial class File
{
    private File() { }

    internal static File CreateFile(UploadResponse uploadResponse, Asset asset, AssetTypeDefinition assetTypeDefinition)
    {
        var fileName = System.IO.Path.GetFileName(uploadResponse.FileName);
        var extension = FileExtension.FromFileName(fileName, assetTypeDefinition);
        var file = new File()
        {
            Name = FileName.FromAsset(asset, extension),
            Extension = extension,
            OriginalName = fileName,
            Size = uploadResponse.FileSize,
            FileServeId = uploadResponse.FileId
        };

        return file;
    }
}