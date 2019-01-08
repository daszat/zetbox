$dnsname = "test.cert.com"
$keypassword = "hallo"
$filename = "test"

$cert = New-SelfSignedCertificate -certstorelocation cert:\CurrentUser\my -dnsname $dnsname
$pwd = ConvertTo-SecureString -String $keypassword -Force -AsPlainText

Export-PfxCertificate -cert "cert:\CurrentUser\my\$($cert.Thumbprint)" -FilePath ".\$filename.pfx" -Password $pwd
Export-Certificate -Cert $cert -FilePath "$filename.cer"