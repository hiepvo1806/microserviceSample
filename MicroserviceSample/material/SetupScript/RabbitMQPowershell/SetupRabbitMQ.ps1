param([String]$RabbitDllPath = "not specified")

$RabbitDllPath = Resolve-Path $RabbitDllPath 
Write-Host "Rabbit DLL Path: " 
Write-Host $RabbitDllPath -foregroundcolor green

set-ExecutionPolicy Unrestricted

$absoluteRabbitDllPath = Resolve-Path $RabbitDllPath

Write-Host "Absolute Rabbit DLL Path: " 
Write-Host $absoluteRabbitDllPath -foregroundcolor green

[Reflection.Assembly]::LoadFile($absoluteRabbitDllPath)

Write-Host "Setting up RabbitMQ Connection Factory"
$factory = new-object RabbitMQ.Client.ConnectionFactory
$hostNameProp = [RabbitMQ.Client.ConnectionFactory].GetField("HostName")
$hostNameProp.SetValue($factory, "192.168.99.100")

$usernameProp = [RabbitMQ.Client.ConnectionFactory].GetField("UserName")
$usernameProp.SetValue($factory, "guest")

$passwordProp = [RabbitMQ.Client.ConnectionFactory].GetField("Password")
$passwordProp.SetValue($factory, "guest")


$portProp = [RabbitMQ.Client.ConnectionFactory].GetField("Port")
$portProp.SetValue($factory, 5672)

$createConnectionMethod = [RabbitMQ.Client.ConnectionFactory].GetMethod("CreateConnection", [Type]::EmptyTypes)
$connection = $createConnectionMethod.Invoke($factory, "instance,public", $null, $null, $null)

Write-Host "Setting up RabbitMQ Model"
$model = $connection.CreateModel()
Write-Host "Creating Alternative Exchange" -foregroundcolor green
$exchangeType = [RabbitMQ.Client.ExchangeType]::Fanout
$model.ExchangeDeclare("CQRSDemo.Exchange.FailuresExchange", $exchangeType, $true)

Write-Host "Creating Failures Queue" -foregroundcolor green
$model.QueueDeclare("CQRSDemo.Exchange.Failures", $true, $false, $false, $null)
$model.QueueBind("CQRSDemo.Exchange.Failures", "CQRSDemo.Exchange.FailuresExchange", "")

Write-Host "Creating Exchange"
$exchangeType = [RabbitMQ.Client.ExchangeType]::Fanout
$args = @{"alternate-exchange"="Module3.Sample10.FailuresExchange";};
$model.ExchangeDeclare("CQRSDemo.Exchange", $exchangeType, $true,$false, $args)
$model.QueueDeclare("CQRSDemo.Exchange.NormalQueue", $true, $false, $false, $null)
$model.QueueBind("CQRSDemo.Exchange.NormalQueue", "CQRSDemo.Exchange", "")
Write-Host "Setup complete"