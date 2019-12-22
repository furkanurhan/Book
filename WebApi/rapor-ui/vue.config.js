module.exports = {
  // baseUrl: './',

  'devServer': {
    // 'proxy': {
    //   '/api': {
    //     'target': 'http://192.168.2.6:9007',
    //     'pathRewrite': {
    //       '/api': 'api'
    //     }
    //   }
    // }
    // 'proxy': {
    //   '/api': {
    //     'target': 'http://10.0.13.5:9314',
    //     'pathRewrite': { '/api': 'api' }
    //   }
    // }
    'proxy': {
      '/api': {
        'target': 'http://localhost:5002',
        'pathRewrite': { '/api': 'api' }
      }
    }
  },
  'transpileDependencies': [
    'vuetify'
  ]
}
