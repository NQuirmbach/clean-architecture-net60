eval $(minikube docker-env)

cd ../src/cleanarchitecture.Api

docker build . -t cleanarchitecture/api:$1
