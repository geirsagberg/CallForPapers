import { Configuration } from 'webpack'
import * as Path from 'path'

const config: Configuration = {
  entry: ['./Features/Submission/submissionPage.tsx'],
  output: {
    path: Path.join(__dirname, 'wwwroot'),
    filename: '[name].js'
  },
  module: {
    rules: [
      {
        test: /\.tsx?/,
        loader: 'awesome-typescript-loader'
      }
    ]
  }
}

export default config
