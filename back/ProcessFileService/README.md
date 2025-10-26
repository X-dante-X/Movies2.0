# ProcessFileService — gRPC File Upload Microservice

**Status:** Experimental  
**Author:** Yevhenii Solomchenko

---

## Overview

This microservice is responsible for receiving large video/image files via **gRPC streaming**, processing them locally, and uploading the results to **MinIO object storage**.

For video files, the service generates a **multi-bitrate HLS** output (adaptive streaming).  
For images, it uploads original files to type-based directories (poster/backdrop/logo/person).

## Responsibilities

- Accept large files (up to ~1GB per stream)
- Streamed upload — no full file kept in memory
- Temporary local buffering to disk
- HLS encoding via FFmpeg (multiple qualities)
- Upload to MinIO (`uploads` bucket)
- Return final storage path (m3u8 or image file)

## Public Endpoints (gRPC)

**Service name:** `FileUpload`

| RPC | Input Type | Mode | Description |
|---|---|---|---|
| `UploadVideo` | stream `VideoUploadRequest` | Client streaming | Uploads raw video chunks → transcodes to HLS → uploads to MinIO |
| `UploadImage` | stream `ImageUploadRequest` | Client streaming | Uploads image chunks → saves as `.jpg` in predefined directory |

### Message contracts

```proto
rpc UploadVideo (stream VideoUploadRequest) returns (Response);
rpc UploadImage (stream ImageUploadRequest) returns (Response);

message VideoUploadRequest {
  string fileName = 1;
  bytes chunk = 2;
}

message ImageUploadRequest {
  string fileName = 1;
  string type = 2; // poster/backdrop/logo/personphoto
  bytes chunk = 3;
}

message Response {
  string message = 1;
  string absoluteFilePath = 2;
}
```

## Runtime & Infrastructure

- Protocol: gRPC over HTTP/2
- Port: 8081
- Storage: MinIO (uploads bucket)
- Encoding: FFmpeg, HLS VOD
- Limits: ~1GB per gRPC stream

> [!NOTE]
> This project is experimental and subject to breaking changes.
> Not intended yet for production workloads.
