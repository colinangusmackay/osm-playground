# OSM CLI

## Command line

### help

Command: `OsmCli --help`

Displays the command help.

### verbosity

This option is available on all commands.

`--verbosity <level>` is optional. This indicates the verbosity level. If not specified, the default verbosity level is `normal`. The verbosity levels are: `detailed`, `normal`, `warning`, `error`.

* `detailed` writes out diagnostic information about the state of the process along with the progress bar. At the end of the task a summary is displayed.
* `normal` displays the progress bar and any warnings or errors. At the end of the task a summary is displayed.
* `warning` only displays warnings and errors. If the task finishes successfully, there will be no output.
* `error` only displays errors. If the task finishes successfully, there will be no output.

### log & log-level

This option is available to all commands

`--log <file-path> --log-level <level>`

`--log <file-path>` indicates where the log file is to be placed. If the path is a folder, then the file name is generated from the time of the start of the task, e.g. `OsmCli-2026-10-10T23-09-12.log`. If a log file exists already then it will be appended to.

`--log-level <level>` indicates the level of the logs. Valid values are `information`, `warning` and `error`.

### import

`OsmCli import --file <osm_file_path> --bounding-box <north_lat,west_long,south_lat,east_long>`

`--file <osm_file_path>` is mandatory. It is the path to the file to import. Can be in the OSM XML or OSM PBF file formats.

`--bounding-box <north_lat,west_long,south_lat,east_long>` is optional. It is the bounding box to import. If not specified, all objects in the file will be imported.

`--complete-objects` is optional. It is a flag that indicates that objects that cross the bounding box should be included. If not specified, objects that cross the bounding box will be incomplete. If no bounding box is specified, this flag has no effect.

`--existing-data <strategy>` is optional. This indicates how to handle existing data. If not specified, existing data will be overwritten. The strategies are: `skip` existing data will be skipped and not imported; and `overwrite` existing data will be overwritten with the new data.

## General

If `OsmCli` detects that it is running in a terminal it will display a progress bar for long-running operations. The progress text will include a start time, estimated time remaining and estimated finish time.

e.g. For commands with a known number of operations
```
[██████████..........] 50%
Start: yyyy-MM-dd HH:mm:ss Est finish: yyyy-MM-dd HH:mm:ss Est remaining: HH:mm:ss
<custom stats>
```

e.g. For commands with an unknown number of operations
```
[<spinner-animation>]
Start yyyy-MM-dd HH:mm:ss Duration HH:mm:ss
<custom stats>
```

Any warning or error messages will be displayed in the terminal above the progress bar. When the command completes the progress bar will be replaced with a summary of the command execution.
