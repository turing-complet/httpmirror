
Usage:
```
var client = new MirrorClient("my.domain")

// fetches from my.domain
var resp1 = client.GetAsync("/api/cats/info")

// fetch from blob storage
var resp2 = client.GetAsync("/api/cats/info")
```

TODO:
read blob connection from environment variable
Disable without changing code by setting ENABLE_MIRROR=0
