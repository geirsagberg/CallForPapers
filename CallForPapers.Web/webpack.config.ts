import { Configuration } from 'webpack'
const path = require('path')
const glob = require('glob')

// Build a map of all entry files indexed by page name
const entries = glob.sync('./Features/**/*Page.tsx').reduce((result, item) => {
  const entryName = path.basename(item, '.tsx')
  result[entryName] = item
  return result
}, {})

const config: Configuration = {
  entry: entries,
  resolve: {
    extensions: [ '.ts', '.tsx', '.js', '.jsx' ],
    alias: {
      '~': path.join(__dirname, 'ClientApp')
    }
  },
  output: {
    path: path.join(__dirname, 'wwwroot', 'dist'),
    filename: '[name].js',
    publicPath: 'dist/'
  },
  devtool: 'source-map',
  module: {
    rules: [
      {
        test: /\.tsx?/,
        use: 'awesome-typescript-loader'
      }
    ]
  }
}

module.exports = config
