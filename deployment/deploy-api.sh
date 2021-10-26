NS=cleanarchitecture
TAG=$(date +%s)
IMG_REPO=:$IMGTAG

sh build-api-image.sh $TAG

kubectl create ns $NS | true

helm dependency update ./api
helm upgrade api ./api --install -f API_VALUES.yml \
    --set image.tag=$TAG \
    --namespace $NS
    --wait

kubectl get pods -n $NS