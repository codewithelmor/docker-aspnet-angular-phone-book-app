const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/api/label",
      "/api/contact",
    ],
    target: "https://localhost:7017",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
