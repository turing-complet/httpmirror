
Usage:
```
var client = new MirrorClient("my.domain")

// fetches from my.domain
var resp1 = client.GetAsync("/api/cats/info")

// fetch from blob storage
var resp2 = client.GetAsync("/api/cats/info")
```

Environment variables:

* BLOB_MIRROR_CONNECTION (required)
* ENABLE_MIRROR (optional, set to anything except 1 to disable)