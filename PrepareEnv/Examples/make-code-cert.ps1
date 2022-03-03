# execute that line by line
# running als a ps1 will yield a parser error

$domain='demo.zetbox.at'
$certname='demo.zetbox.pfx'

$cert = New-SelfSignedCertificate -DnsName $domain -Type CodeSigning -CertStoreLocation 'Cert:\CurrentUser\My'
$CertPassword = ConvertTo-SecureString -String "hallo" -Force -AsPlainText
Export-PfxCertificate -Cert "cert:\CurrentUser\My\$($cert.Thumbprint)" -FilePath $certname -Password $CertPassword
