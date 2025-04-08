#!/bin/sh

echo "Starting MinIO server..."
minio server /data --console-address ":9001" &

until curl -s http://localhost:9000; do
  echo "Waiting for MinIO to start..."
  sleep 5
done

mc alias set myminio http://localhost:9000 minioadmin minioadmin

mc mb myminio/uploads

mc anonymous set download myminio/uploads
echo "Bucket policy set to allow anonymous access to uploads bucket."

wait 
