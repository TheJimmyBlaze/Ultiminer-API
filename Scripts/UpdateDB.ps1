
$DB = $PSScriptRoot + "\..\Database\";
$API = $PSScriptRoot + "\..\API\";

dotnet ef database update --project $DB --startup-project $API