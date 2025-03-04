Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser

$manifestsPath = "./kubernetes"

minikube delete

minikube start - driver=docker

minikube -p minikube docker-env | Invoke-Expression

docker compose build

Get-ChildItem -Path $manifestsPath -Filter *.yaml | ForEach-Object {
    kubectl apply -f $_.FullName
}

Start-Sleep -Seconds 10

kubectl get pods

kubectl get services

minikube tunnel