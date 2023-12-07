const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/api/v1/label",
      "/api/v1/contact",
    ],
    target: "https://localhost:7017",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
