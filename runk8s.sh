manifests_path="./kubernetes"

minikube delete

minikube start — driver=docker

eval $(minikube -p minikube docker-env)

docker compose build

for file in "$manifests_path"/*.yaml; do
  kubectl apply -f "$file"
done

sleep 10

kubectl get pods

kubectl get services

minikube tunnel