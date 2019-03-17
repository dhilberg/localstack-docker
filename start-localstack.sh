#!/bin/bash

image="localstack/localstack:0.9.0"

# Get the latest Localstack image
docker pull $image

# Run the image in a container with only the S3 service, exposing ports 8080 for the web dashboard and 4572 for S3.
docker run -d \
    -p 8080:8080 \
    -p 4572:4572 \
    -e SERVICES=s3 \
    $image