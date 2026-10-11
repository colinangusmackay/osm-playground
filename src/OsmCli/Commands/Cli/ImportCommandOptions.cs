using System.CommandLine;

namespace OsmCli.Commands.Cli;

public static class ImportCommandOptions
{
    public static readonly Option<FileInfo> ImportFileOption = new("--file")
    {
        Description = "The path to the file to import. Can be in the OSM XML or OSM PBF file formats.",
        HelpName = "osm_file_path",
        Required = true,
    };

    public static readonly Option<BoundingBox?> ImportBoundingBoxOption = new("--bounding-box")
    {
        Description = "The bounding box to import. If not specified, all objects in the file will be imported.",
        HelpName = "north_lat,west_long,south_lat,east_long",
        CustomParser = result =>
        {
            var value = result.Tokens.Single().Value;
            if (BoundingBox.TryParse(value, out var boundingBox, out var error))
                return boundingBox;

            result.AddError(error!);
            return null;
        },
    };

    public static readonly Option<bool> ImportCompleteObjectsOption = new("--complete-objects")
    {
        Description =
            "Include objects that cross the bounding box. Otherwise they will be incomplete. Has no effect without --bounding-box.",
    };

    public static readonly Option<ExistingDataStrategy> ImportExistingDataOption = new("--existing-data")
    {
        Description = "How to handle existing data: skip it, or overwrite it with the new data.",
        HelpName = "skip|overwrite",
        DefaultValueFactory = _ => ExistingDataStrategy.Overwrite,
    };
}
