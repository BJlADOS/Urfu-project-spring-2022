const path = require('path')

const webpack = require('webpack')
const HtmlWebpackPlugin = require('html-webpack-plugin')
const MiniCssExtractPlugin = require('mini-css-extract-plugin')
const CopyWebpackPlugin = require('copy-webpack-plugin')
const CleanWebpackPlugin = require('clean-webpack-plugin')

const package = require('./package.json')

const isProduction = process.env.NODE_ENV === 'production'

const sourcePath = path.join(__dirname, './src')
const outPath = path.join(__dirname, './build')

module.exports = {
  context: sourcePath,
  entry: { app: './main.tsx' },
  devtool: !isProduction && 'inline-source-map',
  output: {
    path: outPath,
    publicPath: '/',
    filename: isProduction ? '[contenthash].js' : '[hash].js',
    chunkFilename: isProduction
      ? '[name].[contenthash].js'
      : '[name].[hash].js',
  },
  target: 'web',
  resolve: {
    extensions: ['.js', '.ts', '.tsx'],
    mainFields: ['module', 'browser', 'main'],
    alias: {
      app: path.resolve(__dirname, 'src/app/'),
      assets: path.resolve(__dirname, 'src/assets/'),
      'react-dom': '@hot-loader/react-dom',
    },
  },
  devServer: {
    historyApiFallback: true,
    contentBase: sourcePath,
    hot: true,
    inline: true,
    stats: 'minimal',
    clientLogLevel: 'warning',
    proxy: {
      '/api/**': {
        target: 'https://localhost:44337',
        secure: false,
        changeOrigin: true,
      },
    },
  },
  module: {
    rules: [
      // .ts, .tsx
      {
        test: /\.tsx?$/,
        use: [
          !isProduction && {
            loader: 'babel-loader',
            options: { plugins: ['react-hot-loader/babel'] },
          },
          'ts-loader',
        ].filter(Boolean),
      },
      // global css
      {
        test: /\.(css)$/,
        use: [
          isProduction ? MiniCssExtractPlugin.loader : 'style-loader',
          {
            loader: 'css-loader',
            query: {
              localIdentName: isProduction
                ? '[hash:base64:5]'
                : '[local]__[hash:base64:5]',
            },
          },
          {
            loader: 'postcss-loader',
            options: {
              ident: 'postcss',
              plugins: [
                require('postcss-import')({ addDependencyTo: webpack }),
                require('postcss-url')(),
                require('postcss-preset-env')({ stage: 2 }),
                require('postcss-reporter')(),
                require('postcss-browser-reporter')({ disabled: isProduction }),
              ],
            },
          },
        ],
      },
      // saas
      {
        test: /\.(scss|saas)$/,
        use: [
          isProduction ? MiniCssExtractPlugin.loader : 'style-loader',
          {
            loader: 'css-loader',
            query: {
              modules: true,
              sourceMap: !isProduction,
              importLoaders: 1,
              localIdentName: isProduction
                ? '[hash:base64:5]'
                : '[local]__[hash:base64:5]',
            },
          },
          {
            loader: 'postcss-loader',
            options: {
              ident: 'postcss',
              plugins: [
                require('postcss-import')({ addDependencyTo: webpack }),
                require('postcss-nested')(),
                require('postcss-mixins')(),
                require('postcss-simple-vars')(),
                require('autoprefixer')(),
                require('postcss-easings')(),
                require('postcss-url')(),
                require('postcss-preset-env')({ stage: 2 }),
                require('postcss-reporter')(),
                require('postcss-browser-reporter')({ disabled: isProduction }),
              ],
            },
          },
          { loader: 'sass-loader' },
        ],
      },
      // static assets
      { test: /\.html$/, use: 'html-loader' },
      { test: /\.(a?png|svg)$/, use: 'url-loader?limit=10000' },
      {
        test: /\.(jpe?g|gif|bmp|mp3|mp4|ogg|wav|eot|ttf|woff|woff2)$/,
        use: 'file-loader',
      },
    ],
  },
  optimization: {
    splitChunks: {
      name: true,
      cacheGroups: {
        commons: {
          chunks: 'initial',
          minChunks: 2,
        },
        vendors: {
          test: /[\\/]node_modules[\\/]/,
          chunks: 'all',
          filename: isProduction
            ? 'vendor.[contenthash].js'
            : 'vendor.[hash].js',
          priority: -10,
        },
      },
    },
    concatenateModules: true,
    runtimeChunk: true,
  },
  plugins: [
    new webpack.EnvironmentPlugin(['NODE_ENV', 'APP_API', 'ENABLE_FAKES', 'LOGIN_URFU']),
    new CleanWebpackPlugin(),
    new MiniCssExtractPlugin({
      filename: '[hash].css',
      disable: !isProduction,
    }),
    new CopyWebpackPlugin({
      patterns: [{ from: 'assets', to: 'assets' }],
    }),
    new HtmlWebpackPlugin({
      template: 'index.html',
      minify: {
        minifyJS: true,
        minifyCSS: true,
        removeComments: true,
        useShortDoctype: true,
        collapseWhitespace: true,
        collapseInlineTagWhitespace: true,
      },
      append: { head: `<script src="//cdn.polyfill.io/v3/polyfill.min.js"></script>` },
      meta: {
        title: package.name,
        description: package.description,
        keywords: Array.isArray(package.keywords)
          ? package.keywords.join(',')
          : undefined,
      },
    }),
  ],
  node: {
    fs: 'empty',
    net: 'empty',
  },
}
