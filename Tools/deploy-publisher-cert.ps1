Param(
	[string]$cert
)	

$store = New-Object System.Security.Cryptography.X509Certificates.X509Store "TrustedPublisher","LocalMachine"
$store.Open("ReadWrite")
$store.Add($cert)
$store.Close()

ls cert:\LocalMachine\TrustedPublisher