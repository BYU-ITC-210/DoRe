const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,
  filenameHashing: false,
  publicPath: process.env.NODE_ENV === 'production'
    ? '/c/'
    : '/',
  outputDir: process.env.NODE_ENV === 'production'
    ? './../DoReFunctions/c'
    : 'dist',
  pages: {
    index: "./src/main.js",
    // This generates a login.html page which works on the Azure Functions host and
    // on GitHub pages or any other static server that defaults to a .html extension
    // when no extension is in the URL.
    login: "./src/main.js"
  }
})
